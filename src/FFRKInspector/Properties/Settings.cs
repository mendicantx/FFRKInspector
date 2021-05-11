// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Properties.Settings
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace FFRKInspector.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        Settings settings = defaultInstance;
        return settings;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("")]
    [DebuggerNonUserCode]
    public string DatabaseHost
    {
      get => (string) this[nameof (DatabaseHost)];
      set => this[nameof (DatabaseHost)] = (object) value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("")]
    [DebuggerNonUserCode]
    public string DatabaseUser
    {
      get => (string) this[nameof (DatabaseUser)];
      set => this[nameof (DatabaseUser)] = (object) value;
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    [UserScopedSetting]
    public string DatabasePassword
    {
      get => (string) this[nameof (DatabasePassword)];
      set => this[nameof (DatabasePassword)] = (object) value;
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    [UserScopedSetting]
    public string DatabaseSchema
    {
      get => (string) this[nameof (DatabaseSchema)];
      set => this[nameof (DatabaseSchema)] = (object) value;
    }

    [ApplicationScopedSetting]
    [DefaultSettingValue("server=localhost;user id=ffrkserver;persistsecurityinfo=True;database=ffrktest")]
    [DebuggerNonUserCode]
    [SpecialSetting(SpecialSetting.ConnectionString)]
    public string ffrktestConnectionString => (string) this[nameof (ffrktestConnectionString)];

    private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
    {
    }

    private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
    {
    }
  }
}
