// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.FFRKViewDebugging
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Proxy;
using Fiddler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  public class FFRKViewDebugging : UserControl
  {
    private IContainer components = (IContainer) null;
    private List<ListViewItem> mCache;
    private TableLayoutPanel tableLayoutPanel1;
    private ListView listViewHistory;
    private ColumnHeader columnHeaderTime;
    private ColumnHeader columnHeaderPath;
    private TextBox textBoxJson;
    private TreeView treeViewJson;
    private Button buttonClear;

    public FFRKViewDebugging()
    {
      this.InitializeComponent();
      this.mCache = new List<ListViewItem>();
    }

    private void FFRKViewDebugging_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.listViewHistory.VirtualListSize = FFRKProxy.Instance.ResponseHistory.Size;
      FFRKProxy.Instance.OnFFRKResponse += new FFRKProxy.FFRKResponseDelegate(this.FFRKProxy_OnFFRKResponse);
    }

    private void FFRKProxy_OnFFRKResponse(string Path) => ++this.listViewHistory.VirtualListSize;

    private ListViewItem CreateListViewItem(ResponseHistory.HistoryItem data) => new ListViewItem(new string[2]
    {
      data.Timestamp.ToString(),
      ((ClientChatter) data.Session.oRequest).headers.RequestPath
    })
    {
      Tag = (object) data
    };

    private void listViewHistory_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
    {
      this.CacheVirtualItems(e.ItemIndex, e.ItemIndex);
      Debug.Assert(this.mCache[e.ItemIndex] != null);
      e.Item = this.mCache[e.ItemIndex];
    }

    private void listViewHistory_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e) => this.CacheVirtualItems(e.StartIndex, e.EndIndex);

    private void CacheVirtualItems(int StartIndex, int EndIndex)
    {
      if (EndIndex >= this.mCache.Count)
      {
        int num = EndIndex + 1;
        int count = num - this.mCache.Count;
        this.mCache.Capacity = num;
        this.mCache.AddRange(Enumerable.Repeat<ListViewItem>((ListViewItem) null, count));
      }
      for (int index = StartIndex; index <= EndIndex; ++index)
        this.mCache[index] = this.CreateListViewItem(FFRKProxy.Instance.ResponseHistory[index]);
    }

    private void listViewHistory_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.treeViewJson.Nodes.Clear();
      if (this.listViewHistory.SelectedIndices.Count == 0)
        return;
      ResponseHistory.HistoryItem historyItem = FFRKProxy.Instance.ResponseHistory[this.listViewHistory.SelectedIndices[0]];
      if (historyItem.JsonObject == null)
      {
        if (historyItem.Handler == null)
          return;
        historyItem.JsonObject = historyItem.Handler.CreateJsonObject(historyItem.Session);
      }
      this.textBoxJson.Text = JsonConvert.SerializeObject((object) historyItem.JsonObject, Formatting.Indented);
      TreeNode treeNode = new TreeNode("ROOT");
      this.treeViewJson.Nodes.Add(treeNode);
      this.Json2Tree(treeNode, historyItem.JsonObject);
      treeNode.Expand();
      foreach (TreeNode node in treeNode.Nodes)
        node.Expand();
      treeNode.EnsureVisible();
    }

    private void Json2Tree(TreeNode parent, JObject obj)
    {
      foreach (KeyValuePair<string, JToken> keyValuePair in obj)
      {
        TreeNode treeNode1 = new TreeNode();
        if (keyValuePair.Value.Type == JTokenType.Object)
        {
          JObject jobject = (JObject) keyValuePair.Value;
          treeNode1.Text = keyValuePair.Key.ToString();
          this.Json2Tree(treeNode1, jobject);
        }
        else if (keyValuePair.Value.Type == JTokenType.Array)
        {
          treeNode1.Text = keyValuePair.Key.ToString();
          int num = 0;
          foreach (JToken jtoken1 in (IEnumerable<JToken>) keyValuePair.Value)
          {
            if (jtoken1.Type == JTokenType.Object)
            {
              TreeNode treeNode2 = new TreeNode(keyValuePair.Key.ToString() + "[" + (object) num + "]");
              treeNode1.Nodes.Add(treeNode2);
              JObject jobject = (JObject) jtoken1;
              this.Json2Tree(treeNode2, jobject);
              ++num;
            }
            else if (jtoken1.Type == JTokenType.Array)
            {
              ++num;
              TreeNode node = new TreeNode();
              foreach (JToken jtoken2 in (IEnumerable<JToken>) jtoken1)
              {
                node.Text = keyValuePair.Key.ToString() + "[" + (object) num + "]";
                node.Nodes.Add(jtoken2.ToString());
              }
              treeNode1.Nodes.Add(node);
            }
            else
              treeNode1.Nodes.Add(jtoken1.ToString());
          }
        }
        else
          treeNode1.Text = string.Format("'{0}' = '{1}'", (object) keyValuePair.Key.ToString(), (object) keyValuePair.Value.ToString());
        parent.Nodes.Add(treeNode1);
      }
    }

    private void buttonClear_Click(object sender, EventArgs e)
    {
      this.mCache.Clear();
      FFRKProxy.Instance.ResponseHistory.Clear();
      this.listViewHistory.VirtualListSize = 0;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.listViewHistory = new ListView();
      this.columnHeaderTime = new ColumnHeader();
      this.columnHeaderPath = new ColumnHeader();
      this.textBoxJson = new TextBox();
      this.treeViewJson = new TreeView();
      this.buttonClear = new Button();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.tableLayoutPanel1.ColumnCount = 4;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.3306f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.3206f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 31.9318f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.417f));
      this.tableLayoutPanel1.Controls.Add((Control) this.listViewHistory, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.textBoxJson, 2, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.treeViewJson, 3, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.buttonClear, 1, 1);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 22f));
      this.tableLayoutPanel1.Size = new Size(998, 523);
      this.tableLayoutPanel1.TabIndex = 0;
      this.listViewHistory.Columns.AddRange(new ColumnHeader[2]
      {
        this.columnHeaderTime,
        this.columnHeaderPath
      });
      this.tableLayoutPanel1.SetColumnSpan((Control) this.listViewHistory, 2);
      this.listViewHistory.Dock = DockStyle.Fill;
      this.listViewHistory.FullRowSelect = true;
      this.listViewHistory.HideSelection = false;
      this.listViewHistory.Location = new Point(3, 3);
      this.listViewHistory.MultiSelect = false;
      this.listViewHistory.Name = "listViewHistory";
      this.listViewHistory.Size = new Size(338, 455);
      this.listViewHistory.TabIndex = 0;
      this.listViewHistory.UseCompatibleStateImageBehavior = false;
      this.listViewHistory.View = View.Details;
      this.listViewHistory.VirtualMode = true;
      this.listViewHistory.CacheVirtualItems += new CacheVirtualItemsEventHandler(this.listViewHistory_CacheVirtualItems);
      this.listViewHistory.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(this.listViewHistory_RetrieveVirtualItem);
      this.listViewHistory.SelectedIndexChanged += new EventHandler(this.listViewHistory_SelectedIndexChanged);
      this.columnHeaderTime.Text = "Time";
      this.columnHeaderTime.Width = 150;
      this.columnHeaderPath.Text = "Request Path";
      this.columnHeaderPath.Width = 250;
      this.textBoxJson.Dock = DockStyle.Fill;
      this.textBoxJson.Location = new Point(347, 3);
      this.textBoxJson.MaxLength = 131072;
      this.textBoxJson.Multiline = true;
      this.textBoxJson.Name = "textBoxJson";
      this.textBoxJson.ReadOnly = true;
      this.tableLayoutPanel1.SetRowSpan((Control) this.textBoxJson, 2);
      this.textBoxJson.ScrollBars = ScrollBars.Both;
      this.textBoxJson.Size = new Size(312, 495);
      this.textBoxJson.TabIndex = 1;
      this.textBoxJson.WordWrap = false;
      this.treeViewJson.Dock = DockStyle.Fill;
      this.treeViewJson.Location = new Point(665, 3);
      this.treeViewJson.Name = "treeViewJson";
      this.tableLayoutPanel1.SetRowSpan((Control) this.treeViewJson, 2);
      this.treeViewJson.Size = new Size(330, 495);
      this.treeViewJson.TabIndex = 2;
      this.buttonClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.buttonClear.Location = new Point(230, 464);
      this.buttonClear.Name = "buttonClear";
      this.buttonClear.Size = new Size(111, 34);
      this.buttonClear.TabIndex = 3;
      this.buttonClear.Text = "Clear";
      this.buttonClear.UseVisualStyleBackColor = true;
      this.buttonClear.Click += new EventHandler(this.buttonClear_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.Name = nameof (FFRKViewDebugging);
      this.Size = new Size(998, 523);
      this.Load += new EventHandler(this.FFRKViewDebugging_Load);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
