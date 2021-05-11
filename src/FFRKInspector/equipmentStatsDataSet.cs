// Decompiled with JetBrains decompiler
// Type: FFRKInspector.equipmentStatsDataSet
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
  [XmlRoot("equipmentStatsDataSet")]
  [DesignerCategory("code")]
  [ToolboxItem(true)]
  [HelpKeyword("vs.data.DataSet")]
  [Serializable]
  public class equipmentStatsDataSet : DataSet
  {
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    private equipmentStatsDataSet.equipment_statsDataTable tableequipment_stats;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [DebuggerNonUserCode]
    public equipmentStatsDataSet.equipment_statsDataTable equipment_stats => this.tableequipment_stats;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public override SchemaSerializationMode SchemaSerializationMode
    {
      get => this._schemaSerializationMode;
      set => this._schemaSerializationMode = value;
    }

    [DebuggerNonUserCode]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public new DataTableCollection Tables => base.Tables;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataRelationCollection Relations => base.Relations;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public equipmentStatsDataSet()
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
    protected equipmentStatsDataSet(SerializationInfo info, StreamingContext context)
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
          if (dataSet.Tables[nameof (equipment_stats)] != null)
            base.Tables.Add((DataTable) new equipmentStatsDataSet.equipment_statsDataTable(dataSet.Tables[nameof (equipment_stats)]));
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
      equipmentStatsDataSet equipmentStatsDataSet = (equipmentStatsDataSet) base.Clone();
      equipmentStatsDataSet.InitVars();
      equipmentStatsDataSet.SchemaSerializationMode = this.SchemaSerializationMode;
      return (DataSet) equipmentStatsDataSet;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override bool ShouldSerializeTables() => false;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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
        if (dataSet.Tables["equipment_stats"] != null)
          base.Tables.Add((DataTable) new equipmentStatsDataSet.equipment_statsDataTable(dataSet.Tables["equipment_stats"]));
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

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
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
      this.tableequipment_stats = (equipmentStatsDataSet.equipment_statsDataTable) base.Tables["equipment_stats"];
      if (!initTable || this.tableequipment_stats == null)
        return;
      this.tableequipment_stats.InitVars();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void InitClass()
    {
      this.DataSetName = nameof (equipmentStatsDataSet);
      this.Prefix = "";
      this.Namespace = "http://tempuri.org/ffrktestDataSet.xsd";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tableequipment_stats = new equipmentStatsDataSet.equipment_statsDataTable();
      base.Tables.Add((DataTable) this.tableequipment_stats);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private bool ShouldSerializeequipment_stats() => false;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void SchemaChanged(object sender, CollectionChangeEventArgs e)
    {
      if (e.Action != CollectionChangeAction.Remove)
        return;
      this.InitVars();
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
    {
      equipmentStatsDataSet equipmentStatsDataSet = new equipmentStatsDataSet();
      XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
      {
        Namespace = equipmentStatsDataSet.Namespace
      });
      schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = equipmentStatsDataSet.GetSchemaSerializable();
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

    [XmlSchemaProvider("GetTypedTableSchema")]
    [Serializable]
    public class equipment_statsDataTable : TypedTableBase<equipmentStatsDataSet.equipment_statsRow>
    {
      private DataColumn columnequipment_id;
      private DataColumn columnbase_atk;
      private DataColumn columnbase_mag;
      private DataColumn columnbase_acc;
      private DataColumn columnbase_def;
      private DataColumn columnbase_res;
      private DataColumn columnbase_eva;
      private DataColumn columnbase_mnd;
      private DataColumn columnmax_atk;
      private DataColumn columnmax_mag;
      private DataColumn columnmax_acc;
      private DataColumn columnmax_def;
      private DataColumn columnmax_res;
      private DataColumn columnmax_eva;
      private DataColumn columnmax_mnd;
      private DataColumn columnname;
      private DataColumn columnrarity;
      private DataColumn columntype;
      private DataColumn columnsubtype;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn equipment_idColumn => this.columnequipment_id;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn base_atkColumn => this.columnbase_atk;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn base_magColumn => this.columnbase_mag;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn base_accColumn => this.columnbase_acc;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn base_defColumn => this.columnbase_def;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn base_resColumn => this.columnbase_res;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn base_evaColumn => this.columnbase_eva;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn base_mndColumn => this.columnbase_mnd;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn max_atkColumn => this.columnmax_atk;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn max_magColumn => this.columnmax_mag;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn max_accColumn => this.columnmax_acc;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn max_defColumn => this.columnmax_def;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn max_resColumn => this.columnmax_res;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn max_evaColumn => this.columnmax_eva;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn max_mndColumn => this.columnmax_mnd;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn nameColumn => this.columnname;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn rarityColumn => this.columnrarity;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn typeColumn => this.columntype;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn subtypeColumn => this.columnsubtype;

      [Browsable(false)]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public int Count => this.Rows.Count;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public equipmentStatsDataSet.equipment_statsRow this[int index] => (equipmentStatsDataSet.equipment_statsRow) this.Rows[index];

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event equipmentStatsDataSet.equipment_statsRowChangeEventHandler equipment_statsRowChanging;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event equipmentStatsDataSet.equipment_statsRowChangeEventHandler equipment_statsRowChanged;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event equipmentStatsDataSet.equipment_statsRowChangeEventHandler equipment_statsRowDeleting;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event equipmentStatsDataSet.equipment_statsRowChangeEventHandler equipment_statsRowDeleted;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public equipment_statsDataTable()
      {
        this.TableName = "equipment_stats";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal equipment_statsDataTable(DataTable table)
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
      protected equipment_statsDataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Addequipment_statsRow(equipmentStatsDataSet.equipment_statsRow row) => this.Rows.Add((DataRow) row);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public equipmentStatsDataSet.equipment_statsRow Addequipment_statsRow(
        uint equipment_id,
        short base_atk,
        short base_mag,
        short base_acc,
        short base_def,
        short base_res,
        short base_eva,
        short base_mnd,
        short max_atk,
        short max_mag,
        short max_acc,
        short max_def,
        short max_res,
        short max_eva,
        short max_mnd,
        string name,
        byte rarity,
        byte type,
        byte subtype)
      {
        equipmentStatsDataSet.equipment_statsRow equipmentStatsRow = (equipmentStatsDataSet.equipment_statsRow) this.NewRow();
        object[] objArray = new object[19]
        {
          (object) equipment_id,
          (object) base_atk,
          (object) base_mag,
          (object) base_acc,
          (object) base_def,
          (object) base_res,
          (object) base_eva,
          (object) base_mnd,
          (object) max_atk,
          (object) max_mag,
          (object) max_acc,
          (object) max_def,
          (object) max_res,
          (object) max_eva,
          (object) max_mnd,
          (object) name,
          (object) rarity,
          (object) type,
          (object) subtype
        };
        equipmentStatsRow.ItemArray = objArray;
        this.Rows.Add((DataRow) equipmentStatsRow);
        return equipmentStatsRow;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public equipmentStatsDataSet.equipment_statsRow FindByequipment_id(
        uint equipment_id)
      {
        return (equipmentStatsDataSet.equipment_statsRow) this.Rows.Find(new object[1]
        {
          (object) equipment_id
        });
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public override DataTable Clone()
      {
        equipmentStatsDataSet.equipment_statsDataTable equipmentStatsDataTable = (equipmentStatsDataSet.equipment_statsDataTable) base.Clone();
        equipmentStatsDataTable.InitVars();
        return (DataTable) equipmentStatsDataTable;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override DataTable CreateInstance() => (DataTable) new equipmentStatsDataSet.equipment_statsDataTable();

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal void InitVars()
      {
        this.columnequipment_id = this.Columns["equipment_id"];
        this.columnbase_atk = this.Columns["base_atk"];
        this.columnbase_mag = this.Columns["base_mag"];
        this.columnbase_acc = this.Columns["base_acc"];
        this.columnbase_def = this.Columns["base_def"];
        this.columnbase_res = this.Columns["base_res"];
        this.columnbase_eva = this.Columns["base_eva"];
        this.columnbase_mnd = this.Columns["base_mnd"];
        this.columnmax_atk = this.Columns["max_atk"];
        this.columnmax_mag = this.Columns["max_mag"];
        this.columnmax_acc = this.Columns["max_acc"];
        this.columnmax_def = this.Columns["max_def"];
        this.columnmax_res = this.Columns["max_res"];
        this.columnmax_eva = this.Columns["max_eva"];
        this.columnmax_mnd = this.Columns["max_mnd"];
        this.columnname = this.Columns["name"];
        this.columnrarity = this.Columns["rarity"];
        this.columntype = this.Columns["type"];
        this.columnsubtype = this.Columns["subtype"];
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      private void InitClass()
      {
        this.columnequipment_id = new DataColumn("equipment_id", typeof (uint), (string) null, MappingType.Element);
        this.Columns.Add(this.columnequipment_id);
        this.columnbase_atk = new DataColumn("base_atk", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbase_atk);
        this.columnbase_mag = new DataColumn("base_mag", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbase_mag);
        this.columnbase_acc = new DataColumn("base_acc", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbase_acc);
        this.columnbase_def = new DataColumn("base_def", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbase_def);
        this.columnbase_res = new DataColumn("base_res", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbase_res);
        this.columnbase_eva = new DataColumn("base_eva", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbase_eva);
        this.columnbase_mnd = new DataColumn("base_mnd", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbase_mnd);
        this.columnmax_atk = new DataColumn("max_atk", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.columnmax_atk);
        this.columnmax_mag = new DataColumn("max_mag", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.columnmax_mag);
        this.columnmax_acc = new DataColumn("max_acc", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.columnmax_acc);
        this.columnmax_def = new DataColumn("max_def", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.columnmax_def);
        this.columnmax_res = new DataColumn("max_res", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.columnmax_res);
        this.columnmax_eva = new DataColumn("max_eva", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.columnmax_eva);
        this.columnmax_mnd = new DataColumn("max_mnd", typeof (short), (string) null, MappingType.Element);
        this.Columns.Add(this.columnmax_mnd);
        this.columnname = new DataColumn("name", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnname);
        this.columnrarity = new DataColumn("rarity", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnrarity);
        this.columntype = new DataColumn("type", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columntype);
        this.columnsubtype = new DataColumn("subtype", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnsubtype);
        this.Constraints.Add((Constraint) new UniqueConstraint("Constraint1", new DataColumn[1]
        {
          this.columnequipment_id
        }, true));
        this.columnequipment_id.AllowDBNull = false;
        this.columnequipment_id.Unique = true;
        this.columnname.MaxLength = 45;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public equipmentStatsDataSet.equipment_statsRow Newequipment_statsRow() => (equipmentStatsDataSet.equipment_statsRow) this.NewRow();

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => (DataRow) new equipmentStatsDataSet.equipment_statsRow(builder);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override Type GetRowType() => typeof (equipmentStatsDataSet.equipment_statsRow);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.equipment_statsRowChanged == null)
          return;
        this.equipment_statsRowChanged((object) this, new equipmentStatsDataSet.equipment_statsRowChangeEvent((equipmentStatsDataSet.equipment_statsRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.equipment_statsRowChanging == null)
          return;
        this.equipment_statsRowChanging((object) this, new equipmentStatsDataSet.equipment_statsRowChangeEvent((equipmentStatsDataSet.equipment_statsRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.equipment_statsRowDeleted == null)
          return;
        this.equipment_statsRowDeleted((object) this, new equipmentStatsDataSet.equipment_statsRowChangeEvent((equipmentStatsDataSet.equipment_statsRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.equipment_statsRowDeleting == null)
          return;
        this.equipment_statsRowDeleting((object) this, new equipmentStatsDataSet.equipment_statsRowChangeEvent((equipmentStatsDataSet.equipment_statsRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Removeequipment_statsRow(equipmentStatsDataSet.equipment_statsRow row) => this.Rows.Remove((DataRow) row);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        equipmentStatsDataSet equipmentStatsDataSet = new equipmentStatsDataSet();
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
          FixedValue = equipmentStatsDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = nameof (equipment_statsDataTable)
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = equipmentStatsDataSet.GetSchemaSerializable();
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

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public delegate void equipment_statsRowChangeEventHandler(
      object sender,
      equipmentStatsDataSet.equipment_statsRowChangeEvent e);

    public class equipment_statsRow : DataRow
    {
      private equipmentStatsDataSet.equipment_statsDataTable tableequipment_stats;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public uint equipment_id
      {
        get => (uint) this[this.tableequipment_stats.equipment_idColumn];
        set => this[this.tableequipment_stats.equipment_idColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public short base_atk
      {
        get
        {
          try
          {
            return (short) this[this.tableequipment_stats.base_atkColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'base_atk' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.base_atkColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public short base_mag
      {
        get
        {
          try
          {
            return (short) this[this.tableequipment_stats.base_magColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'base_mag' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.base_magColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public short base_acc
      {
        get
        {
          try
          {
            return (short) this[this.tableequipment_stats.base_accColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'base_acc' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.base_accColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public short base_def
      {
        get
        {
          try
          {
            return (short) this[this.tableequipment_stats.base_defColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'base_def' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.base_defColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public short base_res
      {
        get
        {
          try
          {
            return (short) this[this.tableequipment_stats.base_resColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'base_res' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.base_resColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public short base_eva
      {
        get
        {
          try
          {
            return (short) this[this.tableequipment_stats.base_evaColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'base_eva' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.base_evaColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public short base_mnd
      {
        get
        {
          try
          {
            return (short) this[this.tableequipment_stats.base_mndColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'base_mnd' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.base_mndColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public short max_atk
      {
        get
        {
          try
          {
            return (short) this[this.tableequipment_stats.max_atkColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'max_atk' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.max_atkColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public short max_mag
      {
        get
        {
          try
          {
            return (short) this[this.tableequipment_stats.max_magColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'max_mag' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.max_magColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public short max_acc
      {
        get
        {
          try
          {
            return (short) this[this.tableequipment_stats.max_accColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'max_acc' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.max_accColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public short max_def
      {
        get
        {
          try
          {
            return (short) this[this.tableequipment_stats.max_defColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'max_def' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.max_defColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public short max_res
      {
        get
        {
          try
          {
            return (short) this[this.tableequipment_stats.max_resColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'max_res' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.max_resColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public short max_eva
      {
        get
        {
          try
          {
            return (short) this[this.tableequipment_stats.max_evaColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'max_eva' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.max_evaColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public short max_mnd
      {
        get
        {
          try
          {
            return (short) this[this.tableequipment_stats.max_mndColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'max_mnd' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.max_mndColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public string name
      {
        get
        {
          try
          {
            return (string) this[this.tableequipment_stats.nameColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'name' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.nameColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public byte rarity
      {
        get
        {
          try
          {
            return (byte) this[this.tableequipment_stats.rarityColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'rarity' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.rarityColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public byte type
      {
        get
        {
          try
          {
            return (byte) this[this.tableequipment_stats.typeColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'type' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.typeColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public byte subtype
      {
        get
        {
          try
          {
            return (byte) this[this.tableequipment_stats.subtypeColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'subtype' in table 'equipment_stats' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableequipment_stats.subtypeColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal equipment_statsRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableequipment_stats = (equipmentStatsDataSet.equipment_statsDataTable) this.Table;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isbase_atkNull() => this.IsNull(this.tableequipment_stats.base_atkColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setbase_atkNull() => this[this.tableequipment_stats.base_atkColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isbase_magNull() => this.IsNull(this.tableequipment_stats.base_magColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setbase_magNull() => this[this.tableequipment_stats.base_magColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool Isbase_accNull() => this.IsNull(this.tableequipment_stats.base_accColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Setbase_accNull() => this[this.tableequipment_stats.base_accColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isbase_defNull() => this.IsNull(this.tableequipment_stats.base_defColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setbase_defNull() => this[this.tableequipment_stats.base_defColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isbase_resNull() => this.IsNull(this.tableequipment_stats.base_resColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Setbase_resNull() => this[this.tableequipment_stats.base_resColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isbase_evaNull() => this.IsNull(this.tableequipment_stats.base_evaColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Setbase_evaNull() => this[this.tableequipment_stats.base_evaColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isbase_mndNull() => this.IsNull(this.tableequipment_stats.base_mndColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Setbase_mndNull() => this[this.tableequipment_stats.base_mndColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Ismax_atkNull() => this.IsNull(this.tableequipment_stats.max_atkColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setmax_atkNull() => this[this.tableequipment_stats.max_atkColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Ismax_magNull() => this.IsNull(this.tableequipment_stats.max_magColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setmax_magNull() => this[this.tableequipment_stats.max_magColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Ismax_accNull() => this.IsNull(this.tableequipment_stats.max_accColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setmax_accNull() => this[this.tableequipment_stats.max_accColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Ismax_defNull() => this.IsNull(this.tableequipment_stats.max_defColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setmax_defNull() => this[this.tableequipment_stats.max_defColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool Ismax_resNull() => this.IsNull(this.tableequipment_stats.max_resColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setmax_resNull() => this[this.tableequipment_stats.max_resColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool Ismax_evaNull() => this.IsNull(this.tableequipment_stats.max_evaColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Setmax_evaNull() => this[this.tableequipment_stats.max_evaColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Ismax_mndNull() => this.IsNull(this.tableequipment_stats.max_mndColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setmax_mndNull() => this[this.tableequipment_stats.max_mndColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IsnameNull() => this.IsNull(this.tableequipment_stats.nameColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void SetnameNull() => this[this.tableequipment_stats.nameColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsrarityNull() => this.IsNull(this.tableequipment_stats.rarityColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void SetrarityNull() => this[this.tableequipment_stats.rarityColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IstypeNull() => this.IsNull(this.tableequipment_stats.typeColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SettypeNull() => this[this.tableequipment_stats.typeColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IssubtypeNull() => this.IsNull(this.tableequipment_stats.subtypeColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void SetsubtypeNull() => this[this.tableequipment_stats.subtypeColumn] = Convert.DBNull;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class equipment_statsRowChangeEvent : EventArgs
    {
      private equipmentStatsDataSet.equipment_statsRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public equipmentStatsDataSet.equipment_statsRow Row => this.eventRow;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataRowAction Action => this.eventAction;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public equipment_statsRowChangeEvent(
        equipmentStatsDataSet.equipment_statsRow row,
        DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }
  }
}
