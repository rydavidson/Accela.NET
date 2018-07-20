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
        private static readonly object O = new object();

        public string LogFile { get; set; }
        public Logger Log { get; }

        CommonConfig()
        {
        }

        public static CommonConfig Instance
        {
            get
            {
                lock (O)
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
            Log.LogFile = LogFile;
        }

    }
}
