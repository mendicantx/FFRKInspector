// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Properties.Resources
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FFRKInspector.Properties
{
  [DebuggerNonUserCode]
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (FFRKInspector.Properties.Resources.resourceMan == null)
          FFRKInspector.Properties.Resources.resourceMan = new ResourceManager("FFRKInspector.Properties.Resources", typeof (FFRKInspector.Properties.Resources).Assembly);
        return FFRKInspector.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => FFRKInspector.Properties.Resources.resourceCulture;
      set => FFRKInspector.Properties.Resources.resourceCulture = value;
    }

    internal static Icon opened_folder => (Icon) FFRKInspector.Properties.Resources.ResourceManager.GetObject(nameof (opened_folder), FFRKInspector.Properties.Resources.resourceCulture);

    internal static Bitmap paypal_donate_button => (Bitmap) FFRKInspector.Properties.Resources.ResourceManager.GetObject(nameof (paypal_donate_button), FFRKInspector.Properties.Resources.resourceCulture);

    internal Resources()
    {
    }
  }
}
