// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpVerifySchema
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace FFRKInspector.Database
{
  internal class DbOpVerifySchema : IDbRequest
  {
    private DbOpVerifySchema.VerificationResult mResult;
    private uint mClientSchemaVersion;

    public bool RequiresTransaction => false;

    public DbOpVerifySchema.VerificationResult Result => this.mResult;

    public event DbOpVerifySchema.VerifySchemaResultDelegate OnVerificationCompleted;

    public DbOpVerifySchema(uint ClientSchemaVersion) => this.mClientSchemaVersion = ClientSchemaVersion;

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      List<DbOpVerifySchema.VersionPoint> versions = new List<DbOpVerifySchema.VersionPoint>();
      using (MySqlCommand mySqlCommand = new MySqlCommand(new SelectBuilder()
      {
        Table = "schema_version"
      }.ToString(), connection, transaction))
      {
        using (MySqlDataReader Record = mySqlCommand.ExecuteReader())
        {
          while (Record.Read())
          {
            uint Version = (uint) Record["version"];
            bool Breaking = false;
            if (Record.ColumnExists("breaking"))
              Breaking = (bool) Record["breaking"];
            versions.Add(new DbOpVerifySchema.VersionPoint(Version, Breaking));
          }
        }
      }
      versions.Sort((Comparison<DbOpVerifySchema.VersionPoint>) ((x, y) => x.Version.CompareTo(y.Version)));
      this.mResult = this.DoVerify(versions);
    }

    private DbOpVerifySchema.VerificationResult DoVerify(
      List<DbOpVerifySchema.VersionPoint> versions)
    {
      bool flag = false;
      foreach (DbOpVerifySchema.VersionPoint version in versions)
      {
        if (version.Version >= this.mClientSchemaVersion)
        {
          flag = true;
          if (version.Version > this.mClientSchemaVersion && version.Breaking)
            return DbOpVerifySchema.VerificationResult.DatabaseTooNew;
        }
      }
      return !flag ? DbOpVerifySchema.VerificationResult.DatabaseTooOld : DbOpVerifySchema.VerificationResult.OK;
    }

    public void Respond()
    {
      if (this.OnVerificationCompleted == null)
        return;
      this.OnVerificationCompleted(this.mResult);
    }

    private class VersionPoint
    {
      private uint mVersion;
      private bool mBreaking;

      public uint Version => this.mVersion;

      public bool Breaking => this.mBreaking;

      public VersionPoint(uint Version, bool Breaking)
      {
        this.mVersion = Version;
        this.mBreaking = Breaking;
      }
    }

    public enum VerificationResult
    {
      OK,
      DatabaseTooOld,
      DatabaseTooNew,
    }

    public delegate void VerifySchemaResultDelegate(DbOpVerifySchema.VerificationResult result);
  }
}
