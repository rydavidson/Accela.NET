using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security;
using Accela.Utilities.AA.Models;
using Accela.Utilities.Common;

namespace rydavidson.Accela.Utilities.Handlers
{
    public class ConfigHandler
    {
        // ConfigHandler exposes methods to deal with common functions needed for config files, such as retrieving and updating values

        public string PathToConfigFile { get; set; }
        public string CurrentComponent { get; set; }
        public string CurrentInstance { get; set; }
        public string CurrentVersion { get; set; }
        public Logger Log { get; set; }

        public Action<string> MessageHandler; // provide external messaging
        public Action<string> ErrorMessageHandler;

        private bool isMessageHandlerRegistered = false;
        private bool isErrorMessageHandlerRegistered = false;

        #region constructors

        public ConfigHandler(string _pathToConfigFile, string _component, string _version, string _instance)
        {
            //PathToConfigFile = _pathToConfigFile.Replace("\"","");
            PathToConfigFile = _pathToConfigFile;
            CurrentComponent = _component;
            CurrentVersion = _version;
            CurrentInstance = _instance;
        }

        #endregion

        #region registration

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

        public void RegisterLogger(Logger _logger)
        {
            Log = _logger;
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



        #endregion

        #region writers


        public void WriteMSSQLConfigToFile(MSSQLConfig mssql)
        {

            if (File.Exists(PathToConfigFile + ".backup"))
                File.Delete(PathToConfigFile + ".backup");

            File.Copy(PathToConfigFile, PathToConfigFile + ".backup");

            WriteValueToConfig("av.db.host", mssql.serverHostname);
            WriteValueToConfig("av.jetspeed.db.host", mssql.serverHostname);

            WriteValueToConfig("av.db.sid", mssql.avDatabaseName);
            WriteValueToConfig("av.db.username", mssql.avDatabaseUser);
            WriteValueToConfig("av.db.password", mssql.GetAVDatabasePassword());

            WriteValueToConfig("av.jetspeed.db.sid", mssql.jetspeedDatabaseName);
            WriteValueToConfig("av.jetspeed.db.username", mssql.jetspeedDatabaseUser);
            WriteValueToConfig("av.jetspeed.db.password", mssql.GetJetspeedDatabasePassword());

            logger.LogToUI("Updated config successfully");

        }

        #endregion

        #region getters and setters

        #endregion









    }
}
