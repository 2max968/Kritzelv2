using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main
{
    public class Language
    {
        public static Dictionary<string, Language> Languages { get; private set; }
        public static Language CurrentLanguage { get; private set; } = null;

        public static void Init()
        {
            Languages = new Dictionary<string, Language>();
            List<string> files = ResManager.ListFiles("lang", "*.ini");
            string currLang = System.Globalization.CultureInfo.InstalledUICulture.TwoLetterISOLanguageName.ToLower();
            Program.MainLog.Add(MessageType.MSG, "System Language: {0}", currLang);
            foreach(string str in files)
            {
                Language lang = new Language(str);
                if (lang.Key != "" && !Languages.ContainsKey(lang.Key))
                {
                    Languages.Add(lang.Key, lang);
                }
            }
            if (CurrentLanguage == null && Languages.ContainsKey("en-US"))
                CurrentLanguage = Languages["en-US"];
            SelectLanguage();
        }

        Dictionary<string, string> texts = new Dictionary<string, string>();
        public string Name { get; private set; } = "";
        public string Key { get; private set; } = "";
        public FileInfo Path { get; private set; } = null;

        public Language(string path)
        {
            string category = "";
            using (Stream stream = ResManager.GetStream(path))
            {
                Path = ResManager.LastSource;
                if (stream == null) return;
                StreamReader reader = new StreamReader(stream);
                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if(line.StartsWith("["))
                    {
                        category = line.Substring(1, line.Length - 2);
                    }
                    else if(!line.StartsWith("#"))
                    {
                        int sep = line.IndexOf('=');
                        if (sep > 0)
                        {
                            string key = category + "." + line.Substring(0, sep);
                            string value = line.Substring(sep + 1);
                            if (!texts.ContainsKey(key))
                                texts.Add(key, value);
                        }
                    }
                }
                reader.Close();
                reader.Dispose();
                stream.Close();
            }

            if (texts.ContainsKey("Info.key"))
                Key = texts["Info.key"];
            if (texts.ContainsKey("Info.name"))
                Name = texts["Info.name"];
            else
                Name = Key;
        }

        public static void SelectLanguage()
        {
            string currLang = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            Program.MainLog.Add(MessageType.MSG, "System Language: {0}", currLang);
            foreach (KeyValuePair<string, Language> kvp in Languages)
            {
                var lang = kvp.Value;
                if (Configuration.Language == "" && lang.Key.Substring(0, 2) == currLang.Substring(0, 2))
                    CurrentLanguage = lang;
                if (Configuration.Language != "" && lang.Key == Configuration.Language)
                    CurrentLanguage = lang;
            }
            if (CurrentLanguage == null && Languages.ContainsKey("en-US"))
                CurrentLanguage = Languages["en-US"];
        }

        public static string GetText(string key)
        {
            if (key == null) return null;
            if(CurrentLanguage != null && CurrentLanguage.texts.ContainsKey(key))
            {
                return CurrentLanguage.texts[key];
            }
            else
            {
                return "$" + key;
            }
        }

        public string Get(string key)
        {
            if (texts.ContainsKey(key))
                return texts[key];
            else
                return key;
        }

        public override string ToString()
        {
            if (texts.ContainsKey("Info.name"))
                return texts["Info.name"];
            return Name;
        }
    }
}
