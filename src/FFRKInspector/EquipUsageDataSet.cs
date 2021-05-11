// Decompiled with JetBrains decompiler
// Type: FFRKInspector.EquipUsageDataSet
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FFRKInspector
{
  [XmlSchemaProvider("GetTypedDataSetSchema")]
  [HelpKeyword("vs.data.DataSet")]
  [DesignerCategory("code")]
  [ToolboxItem(true)]
  [XmlRoot("EquipUsageDataSet")]
  [Serializable]
  public class EquipUsageDataSet : DataSet
  {
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    private EquipUsageDataSet.equip_usageDataTable tableequip_usage;
    private EquipUsageDataSet.charactersDataTable tablecharacters;
    private DataRelation relationFK_Equip_Character;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public EquipUsageDataSet.equip_usageDataTable equip_usage => this.tableequip_usage;

    [Browsable(false)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public EquipUsageDataSet.charactersDataTable characters => this.tablecharacters;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override SchemaSerializationMode SchemaSerializationMode
    {
      get => this._schemaSerializationMode;
      set => this._schemaSerializationMode = value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataTableCollection Tables => base.Tables;

    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public new DataRelationCollection Relations => base.Relations;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public EquipUsageDataSet()
    {
      this.BeginInit();
      this.InitClass();
      CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += changeEventHandler;
      base.Relations.CollectionChanged += changeEventHandler;
      this.EndInit();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected EquipUsageDataSet(SerializationInfo info, StreamingContext context)
      : base(info, context, false)
    {
      if (this.IsBinarySerialized(info, context))
      {
        this.InitVars(false);
        CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
        this.Tables.CollectionChanged += changeEventHandler;
        this.Relations.CollectionChanged += changeEventHandler;
      }
      else
      {
        string s = (string) info.GetValue("XmlSchema", typeof (string));
        if (this.DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema)
        {
          DataSet dataSet = new DataSet();
          dataSet.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
          if (dataSet.Tables[nameof (equip_usage)] != null)
            base.Tables.Add((DataTable) new EquipUsageDataSet.equip_usageDataTable(dataSet.Tables[nameof (equip_usage)]));
          if (dataSet.Tables[nameof (characters)] != null)
            base.Tables.Add((DataTable) new EquipUsageDataSet.charactersDataTable(dataSet.Tables[nameof (characters)]));
          this.DataSetName = dataSet.DataSetName;
          this.Prefix = dataSet.Prefix;
          this.Namespace = dataSet.Namespace;
          this.Locale = dataSet.Locale;
          this.CaseSensitive = dataSet.CaseSensitive;
          this.EnforceConstraints = dataSet.EnforceConstraints;
          this.Merge(dataSet, false, MissingSchemaAction.Add);
          this.InitVars();
        }
        else
          this.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
        this.GetSerializationData(info, context);
        CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
        base.Tables.CollectionChanged += changeEventHandler;
        this.Relations.CollectionChanged += changeEventHandler;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override void InitializeDerivedDataSet()
    {
      this.BeginInit();
      this.InitClass();
      this.EndInit();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public override DataSet Clone()
    {
      EquipUsageDataSet equipUsageDataSet = (EquipUsageDataSet) base.Clone();
      equipUsageDataSet.InitVars();
      equipUsageDataSet.SchemaSerializationMode = this.SchemaSerializationMode;
      return (DataSet) equipUsageDataSet;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override bool ShouldSerializeTables() => false;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected override bool ShouldSerializeRelations() => false;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override void ReadXmlSerializable(XmlReader reader)
    {
      if (this.DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
      {
        this.Reset();
        DataSet dataSet = new DataSet();
        int num = (int) dataSet.ReadXml(reader);
        if (dataSet.Tables["equip_usage"] != null)
          base.Tables.Add((DataTable) new EquipUsageDataSet.equip_usageDataTable(dataSet.Tables["equip_usage"]));
        if (dataSet.Tables["characters"] != null)
          base.Tables.Add((DataTable) new EquipUsageDataSet.charactersDataTable(dataSet.Tables["characters"]));
        this.DataSetName = dataSet.DataSetName;
        this.Prefix = dataSet.Prefix;
        this.Namespace = dataSet.Namespace;
        this.Locale = dataSet.Locale;
        this.CaseSensitive = dataSet.CaseSensitive;
        this.EnforceConstraints = dataSet.EnforceConstraints;
        this.Merge(dataSet, false, MissingSchemaAction.Add);
        this.InitVars();
      }
      else
      {
        int num = (int) this.ReadXml(reader);
        this.InitVars();
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override XmlSchema GetSchemaSerializable()
    {
      MemoryStream memoryStream = new MemoryStream();
      this.WriteXmlSchema((XmlWriter) new XmlTextWriter((Stream) memoryStream, (Encoding) null));
      memoryStream.Position = 0L;
      return XmlSchema.Read((XmlReader) new XmlTextReader((Stream) memoryStream), (ValidationEventHandler) null);
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    internal void InitVars() => this.InitVars(true);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal void InitVars(bool initTable)
    {
      this.tableequip_usage = (EquipUsageDataSet.equip_usageDataTable) base.Tables["equip_usage"];
      if (initTable && this.tableequip_usage != null)
        this.tableequip_usage.InitVars();
      this.tablecharacters = (EquipUsageDataSet.charactersDataTable) base.Tables["characters"];
      if (initTable && this.tablecharacters != null)
        this.tablecharacters.InitVars();
      this.relationFK_Equip_Character = this.Relations["FK_Equip_Character"];
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private void InitClass()
    {
      this.DataSetName = nameof (EquipUsageDataSet);
      this.Prefix = "";
      this.Namespace = "http://tempuri.org/EquipUsageDataSet.xsd";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tableequip_usage = new EquipUsageDataSet.equip_usageDataTable();
      base.Tables.Add((DataTable) this.tableequip_usage);
      this.tablecharacters = new EquipUsageDataSet.charactersDataTable();
      base.Tables.Add((DataTable) this.tablecharacters);
      this.relationFK_Equip_Character = new DataRelation("FK_Equip_Character", new DataColumn[1]
      {
        this.tablecharacters.idColumn
      }, new DataColumn[1]
      {
        this.tableequip_usage.character_idColumn
      }, false);
      this.Relations.Add(this.relationFK_Equip_Character);
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private bool ShouldSerializeequip_usage() => false;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private bool ShouldSerializecharacters() => false;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void SchemaChanged(object sender, CollectionChangeEventArgs e)
    {
      if (e.Action != CollectionChangeAction.Remove)
        return;
      this.InitVars();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
    {
      EquipUsageDataSet equipUsageDataSet = new EquipUsageDataSet();
      XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
      {
        Namespace = equipUsageDataSet.Namespace
      });
      schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = equipUsageDataSet.GetSchemaSerializable();
      if (xs.Contains(schemaSerializable.TargetNamespace))
      {
        MemoryStream memoryStream1 = new MemoryStream();
        MemoryStream memoryStream2 = new MemoryStream();
        try
        {
          schemaSerializable.Write((Stream) memoryStream1);
          foreach (XmlSchema schema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
          {
            memoryStream2.SetLength(0L);
            schema.Write((Stream) memoryStream2);
            if (memoryStream1.Length == memoryStream2.Length)
            {
              memoryStream1.Position = 0L;
              memoryStream2.Position = 0L;
              do
                ;
              while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
              if (memoryStream1.Position == memoryStream1.Length)
                return schemaComplexType;
            }
          }
        }
        finally
        {
          memoryStream1?.Close();
          memoryStream2?.Close();
        }
      }
      xs.Add(schemaSerializable);
      return schemaComplexType;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public delegate void equip_usageRowChangeEventHandler(
      object sender,
      EquipUsageDataSet.equip_usageRowChangeEvent e);

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public delegate void charactersRowChangeEventHandler(
      object sender,
      EquipUsageDataSet.charactersRowChangeEvent e);

    [XmlSchemaProvider("GetTypedTableSchema")]
    [Serializable]
    public class equip_usageDataTable : TypedTableBase<EquipUsageDataSet.equip_usageRow>
    {
      private DataColumn columncharacter_id;
      private DataColumn columnequip_category;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn character_idColumn => this.columncharacter_id;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn equip_categoryColumn => this.columnequip_category;

      [Browsable(false)]
      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public int Count => this.Rows.Count;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public EquipUsageDataSet.equip_usageRow this[int index] => (EquipUsageDataSet.equip_usageRow) this.Rows[index];

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event EquipUsageDataSet.equip_usageRowChangeEventHandler equip_usageRowChanging;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event EquipUsageDataSet.equip_usageRowChangeEventHandler equip_usageRowChanged;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event EquipUsageDataSet.equip_usageRowChangeEventHandler equip_usageRowDeleting;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event EquipUsageDataSet.equip_usageRowChangeEventHandler equip_usageRowDeleted;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public equip_usageDataTable()
      {
        this.TableName = "equip_usage";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      internal equip_usageDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if (table.CaseSensitive != table.DataSet.CaseSensitive)
          this.CaseSensitive = table.CaseSensitive;
        if (table.Locale.ToString() != table.DataSet.Locale.ToString())
          this.Locale = table.Locale;
        if (table.Namespace != table.DataSet.Namespace)
          this.Namespace = table.Namespace;
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected equip_usageDataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Addequip_usageRow(EquipUsageDataSet.equip_usageRow row) => this.Rows.Add((DataRow) row);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public EquipUsageDataSet.equip_usageRow Addequip_usageRow(
        EquipUsageDataSet.charactersRow parentcharactersRowByFK_Equip_Character,
        byte equip_category)
      {
        EquipUsageDataSet.equip_usageRow equipUsageRow = (EquipUsageDataSet.equip_usageRow) this.NewRow();
        object[] objArray = new object[2]
        {
          null,
          (object) equip_category
        };
        if (parentcharactersRowByFK_Equip_Character != null)
          objArray[0] = parentcharactersRowByFK_Equip_Character[0];
        equipUsageRow.ItemArray = objArray;
        this.Rows.Add((DataRow) equipUsageRow);
        return equipUsageRow;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public EquipUsageDataSet.equip_usageRow FindBycharacter_idequip_category(
        uint character_id,
        byte equip_category)
      {
        return (EquipUsageDataSet.equip_usageRow) this.Rows.Find(new object[2]
        {
          (object) character_id,
          (object) equip_category
        });
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public override DataTable Clone()
      {
        EquipUsageDataSet.equip_usageDataTable equipUsageDataTable = (EquipUsageDataSet.equip_usageDataTable) base.Clone();
        equipUsageDataTable.InitVars();
        return (DataTable) equipUsageDataTable;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override DataTable CreateInstance() => (DataTable) new EquipUsageDataSet.equip_usageDataTable();

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal void InitVars()
      {
        this.columncharacter_id = this.Columns["character_id"];
        this.columnequip_category = this.Columns["equip_category"];
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columncharacter_id = new DataColumn("character_id", typeof (uint), (string) null, MappingType.Element);
        this.Columns.Add(this.columncharacter_id);
        this.columnequip_category = new DataColumn("equip_category", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnequip_category);
        this.Constraints.Add((Constraint) new UniqueConstraint("Constraint1", new DataColumn[2]
        {
          this.columncharacter_id,
          this.columnequip_category
        }, true));
        this.columncharacter_id.AllowDBNull = false;
        this.columnequip_category.AllowDBNull = false;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public EquipUsageDataSet.equip_usageRow Newequip_usageRow() => (EquipUsageDataSet.equip_usageRow) this.NewRow();

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => (DataRow) new EquipUsageDataSet.equip_usageRow(builder);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override Type GetRowType() => typeof (EquipUsageDataSet.equip_usageRow);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.equip_usageRowChanged == null)
          return;
        this.equip_usageRowChanged((object) this, new EquipUsageDataSet.equip_usageRowChangeEvent((EquipUsageDataSet.equip_usageRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.equip_usageRowChanging == null)
          return;
        this.equip_usageRowChanging((object) this, new EquipUsageDataSet.equip_usageRowChangeEvent((EquipUsageDataSet.equip_usageRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.equip_usageRowDeleted == null)
          return;
        this.equip_usageRowDeleted((object) this, new EquipUsageDataSet.equip_usageRowChangeEvent((EquipUsageDataSet.equip_usageRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.equip_usageRowDeleting == null)
          return;
        this.equip_usageRowDeleting((object) this, new EquipUsageDataSet.equip_usageRowChangeEvent((EquipUsageDataSet.equip_usageRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Removeequip_usageRow(EquipUsageDataSet.equip_usageRow row) => this.Rows.Remove((DataRow) row);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        EquipUsageDataSet equipUsageDataSet = new EquipUsageDataSet();
        XmlSchemaAny xmlSchemaAny1 = new XmlSchemaAny();
        xmlSchemaAny1.Namespace = "http://www.w3.org/2001/XMLSchema";
        xmlSchemaAny1.MinOccurs = 0M;
        xmlSchemaAny1.MaxOccurs = Decimal.MaxValue;
        xmlSchemaAny1.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny1);
        XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
        xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        xmlSchemaAny2.MinOccurs = 1M;
        xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny2);
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "namespace",
          FixedValue = equipUsageDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = nameof (equip_usageDataTable)
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = equipUsageDataSet.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
          MemoryStream memoryStream1 = new MemoryStream();
          MemoryStream memoryStream2 = new MemoryStream();
          try
          {
            schemaSerializable.Write((Stream) memoryStream1);
            foreach (XmlSchema schema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
            {
              memoryStream2.SetLength(0L);
              schema.Write((Stream) memoryStream2);
              if (memoryStream1.Length == memoryStream2.Length)
              {
                memoryStream1.Position = 0L;
                memoryStream2.Position = 0L;
                do
                  ;
                while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
                if (memoryStream1.Position == memoryStream1.Length)
                  return schemaComplexType;
              }
            }
          }
          finally
          {
            memoryStream1?.Close();
            memoryStream2?.Close();
          }
        }
        xs.Add(schemaSerializable);
        return schemaComplexType;
      }
    }

    [XmlSchemaProvider("GetTypedTableSchema")]
    [Serializable]
    public class charactersDataTable : TypedTableBase<EquipUsageDataSet.charactersRow>
    {
      private DataColumn columnid;
      private DataColumn columnname;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn idColumn => this.columnid;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn nameColumn => this.columnname;

      [Browsable(false)]
      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public int Count => this.Rows.Count;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public EquipUsageDataSet.charactersRow this[int index] => (EquipUsageDataSet.charactersRow) this.Rows[index];

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event EquipUsageDataSet.charactersRowChangeEventHandler charactersRowChanging;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event EquipUsageDataSet.charactersRowChangeEventHandler charactersRowChanged;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event EquipUsageDataSet.charactersRowChangeEventHandler charactersRowDeleting;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event EquipUsageDataSet.charactersRowChangeEventHandler charactersRowDeleted;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public charactersDataTable()
      {
        this.TableName = "characters";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal charactersDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if (table.CaseSensitive != table.DataSet.CaseSensitive)
          this.CaseSensitive = table.CaseSensitive;
        if (table.Locale.ToString() != table.DataSet.Locale.ToString())
          this.Locale = table.Locale;
        if (table.Namespace != table.DataSet.Namespace)
          this.Namespace = table.Namespace;
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected charactersDataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void AddcharactersRow(EquipUsageDataSet.charactersRow row) => this.Rows.Add((DataRow) row);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public EquipUsageDataSet.charactersRow AddcharactersRow(uint id, string name)
      {
        EquipUsageDataSet.charactersRow charactersRow = (EquipUsageDataSet.charactersRow) this.NewRow();
        object[] objArray = new object[2]
        {
          (object) id,
          (object) name
        };
        charactersRow.ItemArray = objArray;
        this.Rows.Add((DataRow) charactersRow);
        return charactersRow;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public EquipUsageDataSet.charactersRow FindByid(uint id) => (EquipUsageDataSet.charactersRow) this.Rows.Find(new object[1]
      {
        (object) id
      });

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public override DataTable Clone()
      {
        EquipUsageDataSet.charactersDataTable charactersDataTable = (EquipUsageDataSet.charactersDataTable) base.Clone();
        charactersDataTable.InitVars();
        return (DataTable) charactersDataTable;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override DataTable CreateInstance() => (DataTable) new EquipUsageDataSet.charactersDataTable();

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal void InitVars()
      {
        this.columnid = this.Columns["id"];
        this.columnname = this.Columns["name"];
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnid = new DataColumn("id", typeof (uint), (string) null, MappingType.Element);
        this.Columns.Add(this.columnid);
        this.columnname = new DataColumn("name", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnname);
        this.Constraints.Add((Constraint) new UniqueConstraint("Constraint1", new DataColumn[1]
        {
          this.columnid
        }, true));
        this.columnid.AllowDBNull = false;
        this.columnid.Unique = true;
        this.columnname.AllowDBNull = false;
        this.columnname.MaxLength = 45;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public EquipUsageDataSet.charactersRow NewcharactersRow() => (EquipUsageDataSet.charactersRow) this.NewRow();

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => (DataRow) new EquipUsageDataSet.charactersRow(builder);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override Type GetRowType() => typeof (EquipUsageDataSet.charactersRow);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.charactersRowChanged == null)
          return;
        this.charactersRowChanged((object) this, new EquipUsageDataSet.charactersRowChangeEvent((EquipUsageDataSet.charactersRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.charactersRowChanging == null)
          return;
        this.charactersRowChanging((object) this, new EquipUsageDataSet.charactersRowChangeEvent((EquipUsageDataSet.charactersRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.charactersRowDeleted == null)
          return;
        this.charactersRowDeleted((object) this, new EquipUsageDataSet.charactersRowChangeEvent((EquipUsageDataSet.charactersRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.charactersRowDeleting == null)
          return;
        this.charactersRowDeleting((object) this, new EquipUsageDataSet.charactersRowChangeEvent((EquipUsageDataSet.charactersRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void RemovecharactersRow(EquipUsageDataSet.charactersRow row) => this.Rows.Remove((DataRow) row);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        EquipUsageDataSet equipUsageDataSet = new EquipUsageDataSet();
        XmlSchemaAny xmlSchemaAny1 = new XmlSchemaAny();
        xmlSchemaAny1.Namespace = "http://www.w3.org/2001/XMLSchema";
        xmlSchemaAny1.MinOccurs = 0M;
        xmlSchemaAny1.MaxOccurs = Decimal.MaxValue;
        xmlSchemaAny1.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny1);
        XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
        xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        xmlSchemaAny2.MinOccurs = 1M;
        xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny2);
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "namespace",
          FixedValue = equipUsageDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = nameof (charactersDataTable)
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = equipUsageDataSet.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
          MemoryStream memoryStream1 = new MemoryStream();
          MemoryStream memoryStream2 = new MemoryStream();
          try
          {
            schemaSerializable.Write((Stream) memoryStream1);
            foreach (XmlSchema schema in (IEnumerable) xs.Schemas(schemaSerializable.TargetNamespace))
            {
              memoryStream2.SetLength(0L);
              schema.Write((Stream) memoryStream2);
              if (memoryStream1.Length == memoryStream2.Length)
              {
                memoryStream1.Position = 0L;
                memoryStream2.Position = 0L;
                do
                  ;
                while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
                if (memoryStream1.Position == memoryStream1.Length)
                  return schemaComplexType;
              }
            }
          }
          finally
          {
            memoryStream1?.Close();
            memoryStream2?.Close();
          }
        }
        xs.Add(schemaSerializable);
        return schemaComplexType;
      }
    }

    public class equip_usageRow : DataRow
    {
      private EquipUsageDataSet.equip_usageDataTable tableequip_usage;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public uint character_id
      {
        get => (uint) this[this.tableequip_usage.character_idColumn];
        set => this[this.tableequip_usage.character_idColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public byte equip_category
      {
        get => (byte) this[this.tableequip_usage.equip_categoryColumn];
        set => this[this.tableequip_usage.equip_categoryColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public EquipUsageDataSet.charactersRow charactersRow
      {
        get => (EquipUsageDataSet.charactersRow) this.GetParentRow(this.Table.ParentRelations["FK_Equip_Character"]);
        set => this.SetParentRow((DataRow) value, this.Table.ParentRelations["FK_Equip_Character"]);
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      internal equip_usageRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableequip_usage = (EquipUsageDataSet.equip_usageDataTable) this.Table;
      }
    }

    public class charactersRow : DataRow
    {
      private EquipUsageDataSet.charactersDataTable tablecharacters;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public uint id
      {
        get => (uint) this[this.tablecharacters.idColumn];
        set => this[this.tablecharacters.idColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public string name
      {
        get => (string) this[this.tablecharacters.nameColumn];
        set => this[this.tablecharacters.nameColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      internal charactersRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tablecharacters = (EquipUsageDataSet.charactersDataTable) this.Table;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public EquipUsageDataSet.equip_usageRow[] Getequip_usageRows() => this.Table.ChildRelations["FK_Equip_Character"] == null ? new EquipUsageDataSet.equip_usageRow[0] : (EquipUsageDataSet.equip_usageRow[]) this.GetChildRows(this.Table.ChildRelations["FK_Equip_Character"]);
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class equip_usageRowChangeEvent : EventArgs
    {
      private EquipUsageDataSet.equip_usageRow eventRow;
      private DataRowAction eventAction;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public EquipUsageDataSet.equip_usageRow Row => this.eventRow;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataRowAction Action => this.eventAction;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public equip_usageRowChangeEvent(EquipUsageDataSet.equip_usageRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class charactersRowChangeEvent : EventArgs
    {
      private EquipUsageDataSet.charactersRow eventRow;
      private DataRowAction eventAction;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public EquipUsageDataSet.charactersRow Row => this.eventRow;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataRowAction Action => this.eventAction;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public charactersRowChangeEvent(EquipUsageDataSet.charactersRow row, DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }
  }
}
