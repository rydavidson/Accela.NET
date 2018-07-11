using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using rydavidson.Accela.Common;
using rydavidson.Accela.Configuration.Common;
using rydavidson.Accela.Configuration.IO;
using rydavidson.Accela.Configuration.Models;

namespace rydavidson.Accela.Configuration.Handlers
{
    public class ConfigHandler
    {
        // ConfigHandler exposes methods to deal with common functions needed for config files, such as retrieving and updating values

        public string PathToConfigFile { get; set; }
        public string CurrentComponent { get; set; }
        public string CurrentInstance { get; set; }
        public string CurrentVersion { get; set; }
        public Logger Log { get; set; }

        // provide external messaging

        public Action<string> MessageHandler; 
        public Action<string> ErrorMessageHandler;

        // private members

        private bool isMessageHandlerRegistered = false;
        private bool isErrorMessageHandlerRegistered = false;
        private readonly CommonConfig Configs;
        private ConfigWriter configWriter;
        private ConfigReader configReader;

        #region constructors

        public ConfigHandler(string _pathToConfigFile, string _component, string _version, string _instance, Logger _log)
        {
            //PathToConfigFile = _pathToConfigFile.Replace("\"","");
            PathToConfigFile = _pathToConfigFile;
            CurrentComponent = _component;
            CurrentVersion = _version;
            CurrentInstance = _instance;

            configWriter = new ConfigWriter(PathToConfigFile);
            configReader = new ConfigReader(PathToConfigFile);

            Configs = CommonConfig.Instance;
            Log = Configs.Log;
            if (_log != null)
                Log = _log;
        }

        #endregion

        #region register delegates

        public void RegisterMessageDelegate(Action<string> _messageHandler)
        {
            MessageHandler = _messageHandler;
            isMessageHandlerRegistered = true;
        }

        public void RegisterErrorMessageDelegate(Action<string> _errorMessageHander)
        {
            ErrorMessageHandler = _errorMessageHander;
            isErrorMessageHandlerRegistered = true;
        }

        #endregion

        #region message senders

        private void SendMessage(string message)
        {
            if (isMessageHandlerRegistered)
                MessageHandler(message);
        }

        private void SendError(string err)
        {
            if (isErrorMessageHandlerRegistered)
                ErrorMessageHandler(err);
        }

        #endregion

        #region readers

        public MSSQLConfig ReadConfigFromFile()
        {


            return null;
        }

        //public OracleConfig ReadConfigFromFile()
        //{

        //}



        #endregion

        #region writers

        public void WriteConfigToFile(MSSQLConfig mssql)
        {
            if (!File.Exists(PathToConfigFile))
            {
                SendError("File not found: " + PathToConfigFile);
                return;
            }

            if (File.Exists(PathToConfigFile + ".backup"))
                File.Delete(PathToConfigFile + ".backup");

            File.Copy(PathToConfigFile, PathToConfigFile + ".backup");

            configWriter.WriteValueToConfig("av.db.host", mssql.avDBHost);
            configWriter.WriteValueToConfig("av.jetspeed.db.host", mssql.avDBHost);

            configWriter.WriteValueToConfig("av.db.sid", mssql.avDBName);
            configWriter.WriteValueToConfig("av.db.username", mssql.avUser);
            configWriter.WriteValueToConfig("av.db.password", mssql.GetAVDatabasePassword());

            configWriter.WriteValueToConfig("av.jetspeed.db.sid", mssql.avJetspeedDBName);
            configWriter.WriteValueToConfig("av.jetspeed.db.username", mssql.avJetspeedUser);
            configWriter.WriteValueToConfig("av.jetspeed.db.password", mssql.GetJetspeedDatabasePassword());

            SendMessage("Updated config successfully");
        }

        #endregion

        #region getters and setters

        #endregion









    }
}
