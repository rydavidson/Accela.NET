using rydavidson.Accela.Configuration.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rydavidson.Accela.Configuration.IO
{
    class ConfigReader
    {
        private string pathToConfigFile;

        public ConfigReader(string _pathToConfigFile)
        {
            pathToConfigFile = _pathToConfigFile;
                
        }
        public string FindValue(string key) // get a config value from a given key
        {
                string content = File.ReadAllText(pathToConfigFile, Encoding.Default);
                int indexOfKey = content.IndexOf(key);
                int indexEndOfValue = content.IndexOf(Environment.NewLine, indexOfKey);
                int indexOfEquals = content.IndexOf("=", indexOfKey);
                string value = content.Substring(indexOfEquals + 1, (indexEndOfValue - indexOfEquals) - 1);
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

        public MSSQLConfig ReadFromConfigFile()
        {
            MSSQLConfig sql = new MSSQLConfig();

            sql.avDBHost = FindValue("av.db.host");

            return sql;
        }
    }
}
