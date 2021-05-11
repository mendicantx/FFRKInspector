// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.FFRKMySqlInstance
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Properties;
using FFRKInspector.Proxy;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Threading;

namespace FFRKInspector.Database
{
  internal class FFRKMySqlInstance
  {
    private BackgroundWorker mDatabaseThread = (BackgroundWorker) null;
    private CancellationTokenSource mCancellationTokenSource = (CancellationTokenSource) null;
    private BlockingCollection<IDbRequest> mDatabaseQueue = (BlockingCollection<IDbRequest>) null;
    private string mConnStr = (string) null;
    private MySqlConnection mConnection = (MySqlConnection) null;
    private FFRKMySqlInstance.ConnectionState mConnectionState;
    private bool mInsertUnknownDungeons;
    private bool mInsertUnknownWorlds;
    private bool mInsertUnknownItems;
    private bool mInsertUnknownBattles;
    private bool mDatabaseDisabled;
    private string mDatabaseHost;

    public string DatabaseHost => this.mDatabaseHost;

    public string ConnectionString => this.mConnStr;

    public bool InsertUnknownDungeons
    {
      get => this.mInsertUnknownDungeons;
      set => this.mInsertUnknownDungeons = value;
    }

    public bool InsertUnknownWorlds
    {
      get => this.mInsertUnknownWorlds;
      set => this.mInsertUnknownWorlds = value;
    }

    public bool InsertUnknownItems
    {
      get => this.mInsertUnknownItems;
      set => this.mInsertUnknownItems = value;
    }

    public bool InsertUnknownBattles
    {
      get => this.mInsertUnknownBattles;
      set => this.mInsertUnknownBattles = value;
    }

    public bool IsDatabaseDisabled => this.mDatabaseDisabled;

    public MySqlConnection Connection => this.mConnection;

    public event FFRKMySqlInstance.ConnectionStateChangedDelegate OnConnectionStateChanged;

    public event FFRKMySqlInstance.ConnectionInitializedDelegate OnConnectionInitialized;

    public event FFRKMySqlInstance.ConnectionInitializedDelegate OnSchemaError;

    public FFRKMySqlInstance()
    {
      this.mDatabaseThread = new BackgroundWorker();
      this.mDatabaseThread.DoWork += new DoWorkEventHandler(this.mDatabaseThread_DoWork);
      this.mDatabaseThread.RunWorkerAsync();
      this.mDatabaseQueue = new BlockingCollection<IDbRequest>();
      this.mCancellationTokenSource = new CancellationTokenSource();
      this.mConnectionState = FFRKMySqlInstance.ConnectionState.Disconnected;
    }

    public void Shutdown() => this.mCancellationTokenSource.Cancel();

    private string BuildConnectString(string Host, string User, string Password, string Schema)
    {
      if (string.IsNullOrEmpty(Host) || string.IsNullOrEmpty(Schema) || string.IsNullOrEmpty(User))
        throw new InvalidProgramException("Database host, user, and schema cannot be empty");
      return string.Format("server={0};user id={1};password={2};database={3};check parameters=false;persistsecurityinfo=True", (object) Host, (object) User, (object) Password, (object) Schema);
    }

    public FFRKMySqlInstance.ConnectResult TestConnect(
      string Host,
      string User,
      string Password,
      string Schema,
      uint MinimumRequiredSchema)
    {
      string connectionString = this.BuildConnectString(Host, User, Password, Schema);
      MySqlConnection connection = (MySqlConnection) null;
      try
      {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        DbOpVerifySchema dbOpVerifySchema = new DbOpVerifySchema(MinimumRequiredSchema);
        dbOpVerifySchema.Execute(connection, (MySqlTransaction) null);
        return this.TranslateSchemaVerificationResult(dbOpVerifySchema.Result);
      }
      finally
      {
        connection?.Close();
      }
    }

    public void InitializeConnection(uint MinimumRequiredSchema)
    {
      string databaseHost = Settings.Default.DatabaseHost;
      string databaseUser = Settings.Default.DatabaseUser;
      string databasePassword = Settings.Default.DatabasePassword;
      string databaseSchema = Settings.Default.DatabaseSchema;
      string connectionString = this.BuildConnectString(databaseHost, databaseUser, databasePassword, databaseSchema);
      try
      {
        MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
        mySqlConnection.StateChange += new StateChangeEventHandler(this.MySqlConnection_StateChange);
        mySqlConnection.Open();
        this.mDatabaseHost = databaseHost;
        this.mConnection = mySqlConnection;
        this.mConnStr = connectionString;
        DbOpVerifySchema dbOpVerifySchema = new DbOpVerifySchema(MinimumRequiredSchema);
        dbOpVerifySchema.OnVerificationCompleted += new DbOpVerifySchema.VerifySchemaResultDelegate(this.DbOpGetSchemaInfo_OnVerificationCompleted);
        this.BeginExecuteRequest((IDbRequest) dbOpVerifySchema);
      }
      catch (MySqlException ex)
      {
        if (this.OnConnectionInitialized == null)
          return;
        this.OnConnectionInitialized(FFRKMySqlInstance.ConnectResult.InvalidConnection);
      }
    }

    private FFRKMySqlInstance.ConnectResult TranslateSchemaVerificationResult(
      DbOpVerifySchema.VerificationResult Result)
    {
      switch (Result)
      {
        case DbOpVerifySchema.VerificationResult.DatabaseTooOld:
          return FFRKMySqlInstance.ConnectResult.SchemaTooOld;
        case DbOpVerifySchema.VerificationResult.DatabaseTooNew:
          return FFRKMySqlInstance.ConnectResult.SchemaTooNew;
        default:
          return FFRKMySqlInstance.ConnectResult.Success;
      }
    }

    private void DbOpGetSchemaInfo_OnVerificationCompleted(
      DbOpVerifySchema.VerificationResult VerificationResult)
    {
      FFRKMySqlInstance.ConnectResult ConnectResult = this.TranslateSchemaVerificationResult(VerificationResult);
      if ((uint) ConnectResult > 0U)
      {
        this.mDatabaseDisabled = true;
        this.mConnectionState = FFRKMySqlInstance.ConnectionState.Disabled;
        if (this.OnConnectionStateChanged != null)
          this.OnConnectionStateChanged(this.mConnectionState);
      }
      if (this.OnConnectionInitialized == null)
        return;
      this.OnConnectionInitialized(ConnectResult);
    }

    private void MySqlConnection_StateChange(object sender, StateChangeEventArgs e)
    {
      FFRKMySqlInstance.ConnectionState NewState;
      if (this.mDatabaseDisabled)
      {
        NewState = FFRKMySqlInstance.ConnectionState.Disabled;
      }
      else
      {
        switch (e.CurrentState)
        {
          case System.Data.ConnectionState.Closed:
          case System.Data.ConnectionState.Broken:
            NewState = FFRKMySqlInstance.ConnectionState.Disconnected;
            break;
          case System.Data.ConnectionState.Connecting:
            NewState = FFRKMySqlInstance.ConnectionState.Connecting;
            break;
          default:
            NewState = FFRKMySqlInstance.ConnectionState.Connected;
            break;
        }
      }
      if (NewState == this.mConnectionState)
        return;
      FFRKInspector.Utility.Log.LogFormat("Database connection state changed.  Old = {0}, new = {1}", (object) this.mConnectionState, (object) NewState);
      this.mConnectionState = NewState;
      if (this.OnConnectionStateChanged == null)
        return;
      this.OnConnectionStateChanged(NewState);
    }

    private void ProcessDbRequestOnThisThread(IDbRequest Request)
    {
      try
      {
        FFRKInspector.Utility.Log.LogFormat("Database exceuting operation {0}", (object) Request.GetType().Name);
        this.EnsureConnected();
        if (Request.RequiresTransaction)
        {
          using (MySqlTransaction transaction = this.mConnection.BeginTransaction(IsolationLevel.ReadUncommitted))
          {
            try
            {
              Request.Execute(this.mConnection, transaction);
              transaction.Commit();
              Request.Respond();
            }
            catch (Exception ex)
            {
              FFRKInspector.Utility.Log.LogFormat("An error occurred executing the operation in a transaction.  Rolling back.  {0}", (object) ex.Message);
              FFRKInspector.Utility.Log.LogFormat(ex.StackTrace);
              transaction.Rollback();
            }
          }
        }
        else
        {
          Request.Execute(this.mConnection, (MySqlTransaction) null);
          Request.Respond();
        }
      }
      catch (Exception ex)
      {
        FFRKInspector.Utility.Log.LogFormat("An error occurred executing request {0}.  Message = {1}.\n{2}", (object) Request.GetType().Name, (object) ex.Message, (object) ex.StackTrace);
      }
    }

    private void mDatabaseThread_DoWork(object sender, DoWorkEventArgs e)
    {
      while (!this.mCancellationTokenSource.IsCancellationRequested)
      {
        try
        {
          FFRKInspector.Utility.Log.LogString("Database thread waiting for request");
          IDbRequest Request = this.mDatabaseQueue.Take(this.mCancellationTokenSource.Token);
          FFRKInspector.Utility.Log.LogFormat("Database thread dequeued request of type {0}", (object) Request.GetType().Name);
          DbOpVerifySchema dbOpVerifySchema = new DbOpVerifySchema(FFRKProxy.Instance.MinimumRequiredSchema);
          this.ProcessDbRequestOnThisThread((IDbRequest) dbOpVerifySchema);
          if ((uint) dbOpVerifySchema.Result > 0U)
          {
            FFRKInspector.Utility.Log.LogString("Schema verification failed.  Disabling database connectivity.");
            this.mConnectionState = FFRKMySqlInstance.ConnectionState.Disabled;
            if (this.OnSchemaError != null)
              this.OnSchemaError(this.TranslateSchemaVerificationResult(dbOpVerifySchema.Result));
            if (this.OnConnectionStateChanged != null)
              this.OnConnectionStateChanged(this.mConnectionState);
            this.Shutdown();
          }
          else
            this.ProcessDbRequestOnThisThread(Request);
        }
        catch (OperationCanceledException ex)
        {
          FFRKInspector.Utility.Log.LogString("Database worker thread shutting down because cancellation was requested.");
        }
        catch (Exception ex)
        {
          FFRKInspector.Utility.Log.LogFormat("Database worker thread encountered an unknown exception.  {0}\n{1}", (object) ex.Message, (object) ex.StackTrace);
        }
      }
      FFRKInspector.Utility.Log.LogString("Database worker thread exiting.");
    }

    private void EnsureConnected()
    {
      switch (this.mConnection.State)
      {
        case System.Data.ConnectionState.Closed:
          this.mConnection.Open();
          break;
        case System.Data.ConnectionState.Broken:
          FFRKInspector.Utility.Log.LogString("Database connection broken.  Attempting to re-open.");
          this.mConnection.Close();
          goto case System.Data.ConnectionState.Closed;
      }
    }

    public void BeginExecuteRequest(IDbRequest Request)
    {
      if (this.mDatabaseDisabled)
      {
        FFRKInspector.Utility.Log.LogFormat("Ignoring request {0} because database connectivity is disabled.", (object) Request.GetType().Name);
      }
      else
      {
        try
        {
          this.mDatabaseQueue.Add(Request, this.mCancellationTokenSource.Token);
        }
        catch (Exception ex)
        {
          FFRKInspector.Utility.Log.LogFormat("An error occurred initiating request {0}.  Message = {1}.\n{2}", (object) Request.GetType().Name, (object) ex.Message, (object) ex.StackTrace);
          Debugger.Break();
        }
      }
    }

    public enum ConnectionState
    {
      Connecting,
      Connected,
      Disabled,
      Disconnected,
    }

    public enum ConnectResult
    {
      Success,
      SchemaTooOld,
      SchemaTooNew,
      InvalidConnection,
    }

    public delegate void ConnectionStateChangedDelegate(FFRKMySqlInstance.ConnectionState NewState);

    public delegate void ConnectionInitializedDelegate(FFRKMySqlInstance.ConnectResult ConnectResult);
  }
}
