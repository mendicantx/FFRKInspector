// Decompiled with JetBrains decompiler
// Type: FFRKInspector.missingItemsDataSet
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
  [XmlRoot("missingItemsDataSet")]
  [DesignerCategory("code")]
  [ToolboxItem(true)]
  [HelpKeyword("vs.data.DataSet")]
  [XmlSchemaProvider("GetTypedDataSetSchema")]
  [Serializable]
  public class missingItemsDataSet : DataSet
  {
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
    private missingItemsDataSet.missing_itemsDataTable tablemissing_items;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public missingItemsDataSet.missing_itemsDataTable missing_items => this.tablemissing_items;

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

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DebuggerNonUserCode]
    public new DataRelationCollection Relations => base.Relations;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public missingItemsDataSet()
    {
      this.BeginInit();
      this.InitClass();
      CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += changeEventHandler;
      base.Relations.CollectionChanged += changeEventHandler;
      this.EndInit();
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected missingItemsDataSet(SerializationInfo info, StreamingContext context)
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
          if (dataSet.Tables[nameof (missing_items)] != null)
            base.Tables.Add((DataTable) new missingItemsDataSet.missing_itemsDataTable(dataSet.Tables[nameof (missing_items)]));
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

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public override DataSet Clone()
    {
      missingItemsDataSet missingItemsDataSet = (missingItemsDataSet) base.Clone();
      missingItemsDataSet.InitVars();
      missingItemsDataSet.SchemaSerializationMode = this.SchemaSerializationMode;
      return (DataSet) missingItemsDataSet;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
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
        if (dataSet.Tables["missing_items"] != null)
          base.Tables.Add((DataTable) new missingItemsDataSet.missing_itemsDataTable(dataSet.Tables["missing_items"]));
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

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal void InitVars() => this.InitVars(true);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal void InitVars(bool initTable)
    {
      this.tablemissing_items = (missingItemsDataSet.missing_itemsDataTable) base.Tables["missing_items"];
      if (!initTable || this.tablemissing_items == null)
        return;
      this.tablemissing_items.InitVars();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void InitClass()
    {
      this.DataSetName = nameof (missingItemsDataSet);
      this.Prefix = "";
      this.Namespace = "http://tempuri.org/ffrktestDataSet.xsd";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tablemissing_items = new missingItemsDataSet.missing_itemsDataTable();
      base.Tables.Add((DataTable) this.tablemissing_items);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private bool ShouldSerializemissing_items() => false;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
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
      missingItemsDataSet missingItemsDataSet = new missingItemsDataSet();
      XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
      {
        Namespace = missingItemsDataSet.Namespace
      });
      schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = missingItemsDataSet.GetSchemaSerializable();
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
    public delegate void missing_itemsRowChangeEventHandler(
      object sender,
      missingItemsDataSet.missing_itemsRowChangeEvent e);

    [XmlSchemaProvider("GetTypedTableSchema")]
    [Serializable]
    public class missing_itemsDataTable : TypedTableBase<missingItemsDataSet.missing_itemsRow>
    {
      private DataColumn columnequipment_id;
      private DataColumn columnname;
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
      private DataColumn columnrarity;
      private DataColumn columnseries;
      private DataColumn columnsubtype;
      private DataColumn columntype;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn equipment_idColumn => this.columnequipment_id;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn nameColumn => this.columnname;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn base_atkColumn => this.columnbase_atk;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn base_magColumn => this.columnbase_mag;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
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

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn base_mndColumn => this.columnbase_mnd;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn max_atkColumn => this.columnmax_atk;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn max_magColumn => this.columnmax_mag;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn max_accColumn => this.columnmax_acc;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn max_defColumn => this.columnmax_def;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn max_resColumn => this.columnmax_res;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn max_evaColumn => this.columnmax_eva;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn max_mndColumn => this.columnmax_mnd;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn rarityColumn => this.columnrarity;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn seriesColumn => this.columnseries;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn subtypeColumn => this.columnsubtype;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataColumn typeColumn => this.columntype;

      [Browsable(false)]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public int Count => this.Rows.Count;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public missingItemsDataSet.missing_itemsRow this[int index] => (missingItemsDataSet.missing_itemsRow) this.Rows[index];

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event missingItemsDataSet.missing_itemsRowChangeEventHandler missing_itemsRowChanging;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event missingItemsDataSet.missing_itemsRowChangeEventHandler missing_itemsRowChanged;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event missingItemsDataSet.missing_itemsRowChangeEventHandler missing_itemsRowDeleting;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event missingItemsDataSet.missing_itemsRowChangeEventHandler missing_itemsRowDeleted;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public missing_itemsDataTable()
      {
        this.TableName = "missing_items";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal missing_itemsDataTable(DataTable table)
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
      protected missing_itemsDataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Addmissing_itemsRow(missingItemsDataSet.missing_itemsRow row) => this.Rows.Add((DataRow) row);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public missingItemsDataSet.missing_itemsRow Addmissing_itemsRow(
        uint equipment_id,
        string name,
        ushort base_atk,
        ushort base_mag,
        ushort base_acc,
        ushort base_def,
        ushort base_res,
        ushort base_eva,
        ushort base_mnd,
        ushort max_atk,
        ushort max_mag,
        ushort max_acc,
        ushort max_def,
        ushort max_res,
        ushort max_eva,
        ushort max_mnd,
        byte rarity,
        uint series,
        byte subtype,
        byte type)
      {
        missingItemsDataSet.missing_itemsRow missingItemsRow = (missingItemsDataSet.missing_itemsRow) this.NewRow();
        object[] objArray = new object[20]
        {
          (object) equipment_id,
          (object) name,
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
          (object) rarity,
          (object) series,
          (object) subtype,
          (object) type
        };
        missingItemsRow.ItemArray = objArray;
        this.Rows.Add((DataRow) missingItemsRow);
        return missingItemsRow;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public missingItemsDataSet.missing_itemsRow FindByequipment_id(
        uint equipment_id)
      {
        return (missingItemsDataSet.missing_itemsRow) this.Rows.Find(new object[1]
        {
          (object) equipment_id
        });
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public override DataTable Clone()
      {
        missingItemsDataSet.missing_itemsDataTable missingItemsDataTable = (missingItemsDataSet.missing_itemsDataTable) base.Clone();
        missingItemsDataTable.InitVars();
        return (DataTable) missingItemsDataTable;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override DataTable CreateInstance() => (DataTable) new missingItemsDataSet.missing_itemsDataTable();

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal void InitVars()
      {
        this.columnequipment_id = this.Columns["equipment_id"];
        this.columnname = this.Columns["name"];
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
        this.columnrarity = this.Columns["rarity"];
        this.columnseries = this.Columns["series"];
        this.columnsubtype = this.Columns["subtype"];
        this.columntype = this.Columns["type"];
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      private void InitClass()
      {
        this.columnequipment_id = new DataColumn("equipment_id", typeof (uint), (string) null, MappingType.Element);
        this.Columns.Add(this.columnequipment_id);
        this.columnname = new DataColumn("name", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnname);
        this.columnbase_atk = new DataColumn("base_atk", typeof (ushort), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbase_atk);
        this.columnbase_mag = new DataColumn("base_mag", typeof (ushort), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbase_mag);
        this.columnbase_acc = new DataColumn("base_acc", typeof (ushort), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbase_acc);
        this.columnbase_def = new DataColumn("base_def", typeof (ushort), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbase_def);
        this.columnbase_res = new DataColumn("base_res", typeof (ushort), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbase_res);
        this.columnbase_eva = new DataColumn("base_eva", typeof (ushort), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbase_eva);
        this.columnbase_mnd = new DataColumn("base_mnd", typeof (ushort), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbase_mnd);
        this.columnmax_atk = new DataColumn("max_atk", typeof (ushort), (string) null, MappingType.Element);
        this.Columns.Add(this.columnmax_atk);
        this.columnmax_mag = new DataColumn("max_mag", typeof (ushort), (string) null, MappingType.Element);
        this.Columns.Add(this.columnmax_mag);
        this.columnmax_acc = new DataColumn("max_acc", typeof (ushort), (string) null, MappingType.Element);
        this.Columns.Add(this.columnmax_acc);
        this.columnmax_def = new DataColumn("max_def", typeof (ushort), (string) null, MappingType.Element);
        this.Columns.Add(this.columnmax_def);
        this.columnmax_res = new DataColumn("max_res", typeof (ushort), (string) null, MappingType.Element);
        this.Columns.Add(this.columnmax_res);
        this.columnmax_eva = new DataColumn("max_eva", typeof (ushort), (string) null, MappingType.Element);
        this.Columns.Add(this.columnmax_eva);
        this.columnmax_mnd = new DataColumn("max_mnd", typeof (ushort), (string) null, MappingType.Element);
        this.Columns.Add(this.columnmax_mnd);
        this.columnrarity = new DataColumn("rarity", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnrarity);
        this.columnseries = new DataColumn("series", typeof (uint), (string) null, MappingType.Element);
        this.Columns.Add(this.columnseries);
        this.columnsubtype = new DataColumn("subtype", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columnsubtype);
        this.columntype = new DataColumn("type", typeof (byte), (string) null, MappingType.Element);
        this.Columns.Add(this.columntype);
        this.Constraints.Add((Constraint) new UniqueConstraint("Constraint1", new DataColumn[1]
        {
          this.columnequipment_id
        }, true));
        this.columnequipment_id.AllowDBNull = false;
        this.columnequipment_id.Unique = true;
        this.columnname.AllowDBNull = false;
        this.columnname.MaxLength = 45;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public missingItemsDataSet.missing_itemsRow Newmissing_itemsRow() => (missingItemsDataSet.missing_itemsRow) this.NewRow();

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => (DataRow) new missingItemsDataSet.missing_itemsRow(builder);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override Type GetRowType() => typeof (missingItemsDataSet.missing_itemsRow);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.missing_itemsRowChanged == null)
          return;
        this.missing_itemsRowChanged((object) this, new missingItemsDataSet.missing_itemsRowChangeEvent((missingItemsDataSet.missing_itemsRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.missing_itemsRowChanging == null)
          return;
        this.missing_itemsRowChanging((object) this, new missingItemsDataSet.missing_itemsRowChangeEvent((missingItemsDataSet.missing_itemsRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.missing_itemsRowDeleted == null)
          return;
        this.missing_itemsRowDeleted((object) this, new missingItemsDataSet.missing_itemsRowChangeEvent((missingItemsDataSet.missing_itemsRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.missing_itemsRowDeleting == null)
          return;
        this.missing_itemsRowDeleting((object) this, new missingItemsDataSet.missing_itemsRowChangeEvent((missingItemsDataSet.missing_itemsRow) e.Row, e.Action));
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Removemissing_itemsRow(missingItemsDataSet.missing_itemsRow row) => this.Rows.Remove((DataRow) row);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType schemaComplexType = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        missingItemsDataSet missingItemsDataSet = new missingItemsDataSet();
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
          FixedValue = missingItemsDataSet.Namespace
        });
        schemaComplexType.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = nameof (missing_itemsDataTable)
        });
        schemaComplexType.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = missingItemsDataSet.GetSchemaSerializable();
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

    public class missing_itemsRow : DataRow
    {
      private missingItemsDataSet.missing_itemsDataTable tablemissing_items;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public uint equipment_id
      {
        get => (uint) this[this.tablemissing_items.equipment_idColumn];
        set => this[this.tablemissing_items.equipment_idColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public string name
      {
        get => (string) this[this.tablemissing_items.nameColumn];
        set => this[this.tablemissing_items.nameColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public ushort base_atk
      {
        get
        {
          try
          {
            return (ushort) this[this.tablemissing_items.base_atkColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'base_atk' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.base_atkColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public ushort base_mag
      {
        get
        {
          try
          {
            return (ushort) this[this.tablemissing_items.base_magColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'base_mag' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.base_magColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public ushort base_acc
      {
        get
        {
          try
          {
            return (ushort) this[this.tablemissing_items.base_accColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'base_acc' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.base_accColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public ushort base_def
      {
        get
        {
          try
          {
            return (ushort) this[this.tablemissing_items.base_defColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'base_def' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.base_defColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public ushort base_res
      {
        get
        {
          try
          {
            return (ushort) this[this.tablemissing_items.base_resColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'base_res' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.base_resColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public ushort base_eva
      {
        get
        {
          try
          {
            return (ushort) this[this.tablemissing_items.base_evaColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'base_eva' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.base_evaColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public ushort base_mnd
      {
        get
        {
          try
          {
            return (ushort) this[this.tablemissing_items.base_mndColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'base_mnd' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.base_mndColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public ushort max_atk
      {
        get
        {
          try
          {
            return (ushort) this[this.tablemissing_items.max_atkColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'max_atk' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.max_atkColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public ushort max_mag
      {
        get
        {
          try
          {
            return (ushort) this[this.tablemissing_items.max_magColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'max_mag' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.max_magColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public ushort max_acc
      {
        get
        {
          try
          {
            return (ushort) this[this.tablemissing_items.max_accColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'max_acc' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.max_accColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public ushort max_def
      {
        get
        {
          try
          {
            return (ushort) this[this.tablemissing_items.max_defColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'max_def' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.max_defColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public ushort max_res
      {
        get
        {
          try
          {
            return (ushort) this[this.tablemissing_items.max_resColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'max_res' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.max_resColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public ushort max_eva
      {
        get
        {
          try
          {
            return (ushort) this[this.tablemissing_items.max_evaColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'max_eva' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.max_evaColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public ushort max_mnd
      {
        get
        {
          try
          {
            return (ushort) this[this.tablemissing_items.max_mndColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'max_mnd' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.max_mndColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public byte rarity
      {
        get
        {
          try
          {
            return (byte) this[this.tablemissing_items.rarityColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'rarity' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.rarityColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public uint series
      {
        get
        {
          try
          {
            return (uint) this[this.tablemissing_items.seriesColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'series' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.seriesColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public byte subtype
      {
        get
        {
          try
          {
            return (byte) this[this.tablemissing_items.subtypeColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'subtype' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.subtypeColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public byte type
      {
        get
        {
          try
          {
            return (byte) this[this.tablemissing_items.typeColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'type' in table 'missing_items' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tablemissing_items.typeColumn] = (object) value;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      internal missing_itemsRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tablemissing_items = (missingItemsDataSet.missing_itemsDataTable) this.Table;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isbase_atkNull() => this.IsNull(this.tablemissing_items.base_atkColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Setbase_atkNull() => this[this.tablemissing_items.base_atkColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool Isbase_magNull() => this.IsNull(this.tablemissing_items.base_magColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setbase_magNull() => this[this.tablemissing_items.base_magColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool Isbase_accNull() => this.IsNull(this.tablemissing_items.base_accColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Setbase_accNull() => this[this.tablemissing_items.base_accColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool Isbase_defNull() => this.IsNull(this.tablemissing_items.base_defColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Setbase_defNull() => this[this.tablemissing_items.base_defColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool Isbase_resNull() => this.IsNull(this.tablemissing_items.base_resColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Setbase_resNull() => this[this.tablemissing_items.base_resColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isbase_evaNull() => this.IsNull(this.tablemissing_items.base_evaColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setbase_evaNull() => this[this.tablemissing_items.base_evaColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isbase_mndNull() => this.IsNull(this.tablemissing_items.base_mndColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setbase_mndNull() => this[this.tablemissing_items.base_mndColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool Ismax_atkNull() => this.IsNull(this.tablemissing_items.max_atkColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setmax_atkNull() => this[this.tablemissing_items.max_atkColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool Ismax_magNull() => this.IsNull(this.tablemissing_items.max_magColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setmax_magNull() => this[this.tablemissing_items.max_magColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Ismax_accNull() => this.IsNull(this.tablemissing_items.max_accColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Setmax_accNull() => this[this.tablemissing_items.max_accColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool Ismax_defNull() => this.IsNull(this.tablemissing_items.max_defColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Setmax_defNull() => this[this.tablemissing_items.max_defColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool Ismax_resNull() => this.IsNull(this.tablemissing_items.max_resColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setmax_resNull() => this[this.tablemissing_items.max_resColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Ismax_evaNull() => this.IsNull(this.tablemissing_items.max_evaColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Setmax_evaNull() => this[this.tablemissing_items.max_evaColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Ismax_mndNull() => this.IsNull(this.tablemissing_items.max_mndColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void Setmax_mndNull() => this[this.tablemissing_items.max_mndColumn] = Convert.DBNull;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsrarityNull() => this.IsNull(this.tablemissing_items.rarityColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void SetrarityNull() => this[this.tablemissing_items.rarityColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IsseriesNull() => this.IsNull(this.tablemissing_items.seriesColumn);

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void SetseriesNull() => this[this.tablemissing_items.seriesColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IssubtypeNull() => this.IsNull(this.tablemissing_items.subtypeColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetsubtypeNull() => this[this.tablemissing_items.subtypeColumn] = Convert.DBNull;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public bool IstypeNull() => this.IsNull(this.tablemissing_items.typeColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SettypeNull() => this[this.tablemissing_items.typeColumn] = Convert.DBNull;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class missing_itemsRowChangeEvent : EventArgs
    {
      private missingItemsDataSet.missing_itemsRow eventRow;
      private DataRowAction eventAction;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public missingItemsDataSet.missing_itemsRow Row => this.eventRow;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public DataRowAction Action => this.eventAction;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public missing_itemsRowChangeEvent(
        missingItemsDataSet.missing_itemsRow row,
        DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }
    }
  }
}
