// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.Converters.MedalConditionParser
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System.Text.RegularExpressions;

namespace FFRKInspector.GameData.Converters
{
  internal class MedalConditionParser
  {
    private string input;

    public MedalConditionParser(string myString) => this.input = myString;

    public string translate() => this.translate(true);

    public string translate(bool translateName)
    {
      Translator t = new Translator();
      if (this.getMatch("^ボス戦で戦闘不能にならない$").Success || this.getMatch("^戦闘不能にならない$").Success)
        return "Win without being KO'd";
      if (this.getMatch("戦で戦闘不能にならない$").Success)
      {
        string myBoss = this.input.Replace("戦で戦闘不能にならない", "");
        if (translateName)
          myBoss = this.parseBossName(myBoss, t);
        return string.Format("Win the {0} fight without being KO'd", (object) myBoss);
      }
      Match match1 = this.getMatch("の.{1,4}力を下げた$");
      if (match1.Success)
      {
        string myBoss = this.input.Replace(match1.ToString(), "");
        if (translateName)
          myBoss = this.parseBossName(myBoss, t);
        string stat = this.parseStat(Regex.Match(this.input, "の.{1,4}力", RegexOptions.RightToLeft).ToString().Substring(1));
        return string.Format("Lower {0}'s {1}", (object) myBoss, (object) stat);
      }
      Match match2 = this.getMatch("の.{1,4}を下げた$");
      if (match2.Success)
      {
        string myBoss = this.input.Replace(match2.ToString(), "");
        if (translateName)
          myBoss = this.parseBossName(myBoss, t);
        string stat = this.parseStat(Regex.Match(this.input, "の.{1,4}を", RegexOptions.RightToLeft).ToString().Substring(1));
        return string.Format("Lower {0}'s {1}", (object) myBoss, (object) stat);
      }
      Match match3 = this.getMatch("^.{1,4}力を下げた$");
      if (match3.Success)
        return string.Format("Lower {0}", (object) this.parseStat(match3.ToString().Replace("を下げた", "")));
      Match match4 = this.getMatch("^.{1,4}を下げた$");
      if (match4.Success)
        return string.Format("Lower {0}", (object) this.parseStat(match4.ToString().Replace("を下げた", "")));
      Match match5 = this.getMatch("が.属性微?弱時に.属性攻撃$");
      if (match5.Success)
      {
        string myBoss = this.input.Replace(match5.ToString(), "");
        if (translateName)
          myBoss = this.parseBossName(myBoss, t);
        string element1 = this.parseElement(Regex.Match(this.input, "が.", RegexOptions.RightToLeft).ToString().Substring(1));
        string element2 = this.parseElement(Regex.Match(this.input, "に.", RegexOptions.RightToLeft).ToString().Substring(1));
        bool flag = this.input.Contains("微");
        return string.Format("Use {0} attack on {1} when {2}weak to {3}", (object) this.prefixA(element2), (object) myBoss, flag ? (object) "slightly " : (object) "", (object) element1);
      }
      Match match6 = this.getMatch("に弱点の.属性攻撃$");
      if (match6.Success)
      {
        string myBoss = this.input.Replace(match6.ToString(), "");
        if (translateName)
          myBoss = this.parseBossName(myBoss, t);
        string element = this.parseElement(Regex.Match(this.input, "の.", RegexOptions.RightToLeft).ToString().Substring(1));
        return string.Format("Exploit {0}'s weakness to {1}", (object) myBoss, (object) element);
      }
      Match match7 = this.getMatch("に.属性攻撃$");
      if (match7.Success)
      {
        string myBoss = this.input.Replace(match7.ToString(), "");
        if (translateName)
          myBoss = this.parseBossName(myBoss, t);
        return string.Format("Use {0} attack on {1}", (object) this.prefixA(this.parseElement(Regex.Match(this.input, "に.", RegexOptions.RightToLeft).ToString().Substring(1))), (object) myBoss);
      }
      Match match8 = this.getMatch("に.属性攻撃をしない$");
      if (match8.Success)
      {
        string myBoss = this.input.Replace(match8.ToString(), "");
        if (translateName)
          myBoss = this.parseBossName(myBoss, t);
        return string.Format("Don't use any {0} attacks on {1}", (object) this.parseElement(Regex.Match(this.input, "に.", RegexOptions.RightToLeft).ToString().Substring(1)), (object) myBoss);
      }
      if (this.getMatch("戦闘不能でないメンバーが.人以上でクリア$").Success)
      {
        string s = Regex.Match(this.input, "が.", RegexOptions.RightToLeft).ToString().Substring(1);
        int result = 0;
        return string.Format("Win with at least {0} party member{1} alive", (object) s, !int.TryParse(s, out result) || result != 1 ? (object) "s" : (object) "");
      }
      Match match9 = this.getMatch("を[0-9]+回使われる前に撃破$");
      if (match9.Success)
        return string.Format("Win before {0} has been used {1} times", (object) this.input.Replace(match9.ToString(), ""), (object) Regex.Match(this.input, "を[0-9]+回", RegexOptions.RightToLeft).ToString().Substring(1).Replace("回", ""));
      Match match10 = this.getMatch("を[0-9]+回使う前に撃破$");
      if (match10.Success)
        return string.Format("Win before {0} has been used {1} times", (object) this.input.Replace(match10.ToString(), ""), (object) Regex.Match(this.input, "を[0-9]+回", RegexOptions.RightToLeft).ToString().Substring(1).Replace("回", ""));
      Match match11 = this.getMatch("を使用させない$");
      return match11.Success ? string.Format("Don't allow {0} to be used", (object) this.input.Replace(match11.ToString(), "")) : t.Translate(this.input);
    }

    private Match getMatch(string pat) => new Regex(pat).Match(this.input);

    private string parseBossName(string myBoss, Translator t)
    {
      string oldValue = new Regex("^【.+】").Match(myBoss).ToString();
      string str = oldValue.Equals("") ? t.Translate(myBoss) : t.Translate(myBoss.Replace(oldValue, ""));
      return oldValue + str;
    }

    private string parseStat(string stat)
    {
      if (stat.Equals("魔力") || stat.Equals("魔") || stat.Equals("魔法力") || stat.Equals("魔法"))
        return "Magic";
      if (stat.Equals("攻撃力") || stat.Equals("攻撃"))
        return "Attack";
      if (stat.Equals("防御力") || stat.Equals("防御") || stat.Equals("防力") || stat.Equals("防"))
        return "Defense";
      if (stat.Equals("魔防力") || stat.Equals("魔防") || (stat.Equals("魔法防御力") || stat.Equals("魔法防御")) || stat.Equals("魔法防力") || stat.Equals("魔法防"))
        return "Resistance";
      return stat.Equals("精神力") || stat.Equals("精神") || stat.Equals("精力") || stat.Equals("精") ? "Mind" : stat;
    }

    private string parseElement(string elem)
    {
      if (elem.Equals("炎"))
        return "Fire";
      if (elem.Equals("氷"))
        return "Ice";
      if (elem.Equals("雷"))
        return "Lightning";
      if (elem.Equals("地"))
        return "Earth";
      if (elem.Equals("風"))
        return "Wind";
      if (elem.Equals("水"))
        return "Water";
      if (elem.Equals("聖"))
        return "Holy";
      if (elem.Equals("闇"))
        return "Dark";
      return elem.Equals("毒") ? "Bio (element)" : elem;
    }

    private string prefixA(string s)
    {
      char ch = s[0];
      int num;
      switch (ch)
      {
        case 'A':
        case 'E':
        case 'I':
        case 'O':
        case 'a':
        case 'e':
        case 'i':
        case 'o':
        case 'u':
          num = 1;
          break;
        default:
          num = ch == 'U' ? 1 : 0;
          break;
      }
      return num != 0 ? "an " + s : "a " + s;
    }
  }
}
