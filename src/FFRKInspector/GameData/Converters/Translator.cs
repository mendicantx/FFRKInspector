// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.Converters.Translator
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace FFRKInspector.GameData.Converters
{
  public class Translator
  {
    private static Dictionary<string, string> _languageModeMap;

    public static IEnumerable<string> Languages
    {
      get
      {
        Translator.EnsureInitialized();
        return (IEnumerable<string>) Translator._languageModeMap.Keys.OrderBy<string, string>((Func<string, string>) (p => p));
      }
    }

    public TimeSpan TranslationTime { get; private set; }

    public string TranslationSpeechUrl { get; private set; }

    public Exception Error { get; private set; }

    public string Translate(string sourceText)
    {
      string language1 = "Japanese";
      string language2 = "English";
      this.Error = (Exception) null;
      this.TranslationSpeechUrl = (string) null;
      this.TranslationTime = TimeSpan.Zero;
      DateTime now = DateTime.Now;
      string str1 = string.Empty;
      try
      {
        string address = string.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}", (object) Translator.LanguageEnumToIdentifier(language1), (object) Translator.LanguageEnumToIdentifier(language2), (object) HttpUtility.UrlEncode(sourceText));
        string tempFileName = Path.GetTempFileName();
        using (WebClient webClient = new WebClient())
        {
          webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
          webClient.DownloadFile(address, tempFileName);
        }
        if (System.IO.File.Exists(tempFileName))
        {
          string str2 = System.IO.File.ReadAllText(tempFileName);
          int length = str2.IndexOf(string.Format(",,\"{0}\"", (object) Translator.LanguageEnumToIdentifier(language1)));
          if (length == -1)
          {
            int num1 = str2.IndexOf('"');
            if (num1 != -1)
            {
              int num2 = str2.IndexOf('"', num1 + 1);
              if (num2 != -1)
                str1 = str2.Substring(num1 + 1, num2 - num1 - 1);
            }
          }
          else
          {
            string[] strArray = str2.Substring(0, length).Replace("],[", ",").Replace("]", string.Empty).Replace("[", string.Empty).Replace("\",\"", "\"").Split(new char[1]
            {
              '"'
            }, StringSplitOptions.RemoveEmptyEntries);
            for (int index = 0; index < ((IEnumerable<string>) strArray).Count<string>(); index += 2)
            {
              string str3 = strArray[index];
              if (str3.StartsWith(",,"))
                --index;
              else
                str1 = str1 + str3 + "  ";
            }
          }
          str1 = str1.Trim();
          str1 = str1.Replace(" ?", "?");
          str1 = str1.Replace(" !", "!");
          str1 = str1.Replace(" ,", ",");
          str1 = str1.Replace(" .", ".");
          str1 = str1.Replace(" ;", ";");
          this.TranslationSpeechUrl = string.Format("https://translate.googleapis.com/translate_tts?ie=UTF-8&q={0}&tl={1}&total=1&idx=0&textlen={2}&client=gtx", (object) HttpUtility.UrlEncode(str1), (object) Translator.LanguageEnumToIdentifier(language2), (object) str1.Length);
        }
      }
      catch (Exception ex)
      {
        this.Error = ex;
      }
      this.TranslationTime = DateTime.Now - now;
      return str1;
    }

    private static string LanguageEnumToIdentifier(string language)
    {
      string empty = string.Empty;
      Translator.EnsureInitialized();
      Translator._languageModeMap.TryGetValue(language, out empty);
      return empty;
    }

    private static void EnsureInitialized()
    {
      if (Translator._languageModeMap != null)
        return;
      Translator._languageModeMap = new Dictionary<string, string>();
      Translator._languageModeMap.Add("Afrikaans", "af");
      Translator._languageModeMap.Add("Albanian", "sq");
      Translator._languageModeMap.Add("Arabic", "ar");
      Translator._languageModeMap.Add("Armenian", "hy");
      Translator._languageModeMap.Add("Azerbaijani", "az");
      Translator._languageModeMap.Add("Basque", "eu");
      Translator._languageModeMap.Add("Belarusian", "be");
      Translator._languageModeMap.Add("Bengali", "bn");
      Translator._languageModeMap.Add("Bulgarian", "bg");
      Translator._languageModeMap.Add("Catalan", "ca");
      Translator._languageModeMap.Add("Chinese", "zh-CN");
      Translator._languageModeMap.Add("Croatian", "hr");
      Translator._languageModeMap.Add("Czech", "cs");
      Translator._languageModeMap.Add("Danish", "da");
      Translator._languageModeMap.Add("Dutch", "nl");
      Translator._languageModeMap.Add("English", "en");
      Translator._languageModeMap.Add("Esperanto", "eo");
      Translator._languageModeMap.Add("Estonian", "et");
      Translator._languageModeMap.Add("Filipino", "tl");
      Translator._languageModeMap.Add("Finnish", "fi");
      Translator._languageModeMap.Add("French", "fr");
      Translator._languageModeMap.Add("Galician", "gl");
      Translator._languageModeMap.Add("German", "de");
      Translator._languageModeMap.Add("Georgian", "ka");
      Translator._languageModeMap.Add("Greek", "el");
      Translator._languageModeMap.Add("Haitian Creole", "ht");
      Translator._languageModeMap.Add("Hebrew", "iw");
      Translator._languageModeMap.Add("Hindi", "hi");
      Translator._languageModeMap.Add("Hungarian", "hu");
      Translator._languageModeMap.Add("Icelandic", "is");
      Translator._languageModeMap.Add("Indonesian", "id");
      Translator._languageModeMap.Add("Irish", "ga");
      Translator._languageModeMap.Add("Italian", "it");
      Translator._languageModeMap.Add("Japanese", "ja");
      Translator._languageModeMap.Add("Korean", "ko");
      Translator._languageModeMap.Add("Lao", "lo");
      Translator._languageModeMap.Add("Latin", "la");
      Translator._languageModeMap.Add("Latvian", "lv");
      Translator._languageModeMap.Add("Lithuanian", "lt");
      Translator._languageModeMap.Add("Macedonian", "mk");
      Translator._languageModeMap.Add("Malay", "ms");
      Translator._languageModeMap.Add("Maltese", "mt");
      Translator._languageModeMap.Add("Norwegian", "no");
      Translator._languageModeMap.Add("Persian", "fa");
      Translator._languageModeMap.Add("Polish", "pl");
      Translator._languageModeMap.Add("Portuguese", "pt");
      Translator._languageModeMap.Add("Romanian", "ro");
      Translator._languageModeMap.Add("Russian", "ru");
      Translator._languageModeMap.Add("Serbian", "sr");
      Translator._languageModeMap.Add("Slovak", "sk");
      Translator._languageModeMap.Add("Slovenian", "sl");
      Translator._languageModeMap.Add("Spanish", "es");
      Translator._languageModeMap.Add("Swahili", "sw");
      Translator._languageModeMap.Add("Swedish", "sv");
      Translator._languageModeMap.Add("Tamil", "ta");
      Translator._languageModeMap.Add("Telugu", "te");
      Translator._languageModeMap.Add("Thai", "th");
      Translator._languageModeMap.Add("Turkish", "tr");
      Translator._languageModeMap.Add("Ukrainian", "uk");
      Translator._languageModeMap.Add("Urdu", "ur");
      Translator._languageModeMap.Add("Vietnamese", "vi");
      Translator._languageModeMap.Add("Welsh", "cy");
      Translator._languageModeMap.Add("Yiddish", "yi");
    }
  }
}
