using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PS4_Cheater
{
    public class LangHelper
    {
        private static LangHelper Instance = new LangHelper();
        private List<String> Locales = new List<string>();
        private JObject Lang;

        private LangHelper()
        {
            this.List();
        }

        public void List()
        {
            this.Locales.Clear();
            DirectoryInfo dir = new DirectoryInfo("Lang");
            FileInfo[] files = dir.GetFiles("*.json", SearchOption.TopDirectoryOnly);
            foreach(FileInfo f in files)
            {
                string fname = f.Name;
                string loc = fname.Replace("Lang-", "").Replace(".json", "");
                Locales.Add(loc);
                if(this.Lang==null)
                {
                    this.Load(loc);
                }
            }
        }

        public void Load(string locale)  {
            if ("EN".Equals(locale))
            {
                this.Lang = new JObject();
                return;
            }
            StreamReader fileReader = File.OpenText(String.Format("Lang/Lang-{0}.json", locale));
            string json = fileReader.ReadToEnd();
            fileReader.Close();
            JObject lang = (JObject)JsonConvert.DeserializeObject(json);
            this.Lang = lang;
        }


        public static List<String> GetLocales()
        {
            return Instance.Locales;
        }

        public static void SetLocale(string locale)
        {
            Instance.Load(locale);
        }

        public static string GetLang(string key)
        {
            string val = Instance.Lang.Value<string>(key);
            return val == null ? key : val;
        }

        public static string[] GetLangs(string[] keys)
        {
            string[] vals = new string[keys.Length];
            int idx = 0;
            foreach(string key in keys)
            {
                vals[idx] = GetLang(key);
                idx++;
            }
            return vals;
        }
    }
}
