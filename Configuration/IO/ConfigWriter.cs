using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rydavidson.Accela.Configuration.IO
{
    class ConfigWriter
    {
        private string PathToConfigFile;

        public ConfigWriter(string _path)
        {
            PathToConfigFile = _path;
        }

        public void WriteValueToConfig(string key, string val)
        {
            if ((key == "av.db.host" || key == "av.jetspeed.db.host") && val.Contains(":"))
                val = StripPortFromHost(key, val);

            string content = File.ReadAllText(PathToConfigFile, Encoding.Default); // read in the config file
            int indexOfKey = content.IndexOf(key); // get the start of the config item
            if (indexOfKey == -1)
                return; // exit if the config item isn't found

            int indexEndOfLine = content.IndexOf(Environment.NewLine, indexOfKey); // get the end of the config item
            int indexOfEquals = content.IndexOf("=", indexOfKey); // get the index of the equals sign after the config item
            string oldValue = content.Substring(indexOfEquals + 1, (indexEndOfLine - indexOfEquals)).Trim(); // get the old value
            string oldConfigLine = content.Substring(indexOfKey, (indexEndOfLine - indexOfKey)); // get the entire line
            string newConfigLine = "";
            if (oldValue == "" || oldValue == null) // handle empty values
            {
                if (oldConfigLine.Contains("="))
                    newConfigLine = oldConfigLine + val; 
                if (!oldConfigLine.Contains("="))
                    newConfigLine = oldConfigLine + "=" + val;
            }
            else
            {
                newConfigLine = oldConfigLine.Replace(oldValue, val); // replace the old value in the line with the new value
            }

            string newFile = content.Replace(oldConfigLine, newConfigLine);

            File.WriteAllText(PathToConfigFile, newFile, Encoding.Default);

        }

        private string StripPortFromHost(string key, string hostWithPort)
        {
            string port = hostWithPort.Substring(hostWithPort.IndexOf(":") + 1, hostWithPort.Length - hostWithPort.IndexOf(":") - 1);
            switch (key)
            {
                case "av.db.host":
                    new ConfigWriter(PathToConfigFile).WriteValueToConfig("av.db.port", port);
                    break;
                case "av.jetspeed.db.host":
                    new ConfigWriter(PathToConfigFile).WriteValueToConfig("av.jetspeed.db.port", port);
                    break;
                default:
                    break;
            }
            return hostWithPort.Remove(hostWithPort.IndexOf(":"));
        }
    }
}
