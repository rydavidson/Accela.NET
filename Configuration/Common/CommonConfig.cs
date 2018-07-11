using rydavidson.Accela.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rydavidson.Accela.Configuration.Common
{
    public sealed class CommonConfig
    {
        private static CommonConfig instance = null;
        private static readonly object o = new object();

        public string LogFile { get; set; }
        public Logger Log { get; }

        CommonConfig()
        {
        }

        public static CommonConfig Instance
        {
            get
            {
                lock (o)
                {
                    if (instance == null)
                    {
                        instance = new CommonConfig();
                    }
                    return instance;
                }
            }
        }

        public void UpdateLogger()
        {
            Log.logFile = LogFile;
        }

    }
}
