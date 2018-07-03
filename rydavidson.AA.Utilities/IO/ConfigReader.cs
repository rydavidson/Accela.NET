using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rydavidson.Accela.Utilities.IO
{
    class ConfigReader
    {
        public ConfigReader(string PathToConfigFile)
        {

        }
        public string FindValue(string key) // get a config value from a given key
        {
                string content = File.ReadAllText(PathToConfigFile, Encoding.Default);
                int indexOfKey = content.IndexOf(key);
                int indexEndOfValue = content.IndexOf(Environment.NewLine, indexOfKey);
                int indexOfEquals = content.IndexOf("=", indexOfKey);
                string value = content.Substring(indexOfEquals + 1, (indexEndOfValue - indexOfEquals) - 1);

                switch (key)
                {
                    case "av.db.host":
                        value += ":" + new ConfigReader(PathToConfigFile, CurrentComponent, CurrentVersion, CurrentInstance).FindValue("av.db.port");
                        break;
                    case "av.jetspeed.db.host":
                        value += ":" + new ConfigReader(PathToConfigFile, CurrentComponent, CurrentVersion, CurrentInstance).FindValue("av.jetspeed.db.port");
                        break;
                    default:
                        break;
                }

                return value;
        }

        public Dictionary<string, string> FindValues(List<string> _keys)
        {
            Dictionary<string, string> configPairs = new Dictionary<string, string>();
            foreach (string key in _keys)
            {
                configPairs.Add(key, FindValue(key));
            }
            return configPairs;
        }
    }
}
