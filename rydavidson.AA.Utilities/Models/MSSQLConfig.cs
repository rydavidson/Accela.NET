﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Accela.Utilities.AA.Models
{
    public sealed class MSSQLConfig
    {
        #region properties

        public string serverHostname { get; set; }
        public string avDatabaseName { get; set; }
        public string jetspeedDatabaseName { get; set; }
        public string avDatabaseUser { get; set; }
        public string jetspeedDatabaseUser { get; set; }
        public string component { get; set; }
        private SecureString avDatabasePassword = new SecureString();
        private SecureString jetspeedDatabasePassword = new SecureString();

        #endregion

        #region constructors

        public MSSQLConfig() { }

        public MSSQLConfig(string _serverHostName, string _avDatabaseName, string _jetspeedDatabaseName)
        {
            serverHostname = _serverHostName;
            avDatabaseName = _avDatabaseName;
            jetspeedDatabaseName = _jetspeedDatabaseName;
        }

        public MSSQLConfig(string _serverHostName, string _avDatabaseName, string _jetspeedDatabaseName, string _avDatabaseUser, string _jetspeedDatabaseUser, 
            SecureString _avDatabasePassword, SecureString _jetspeedDatabasePassword)
        {
            serverHostname = _serverHostName;
            avDatabaseName = _avDatabaseName;
            jetspeedDatabaseName = _jetspeedDatabaseName;
            avDatabaseUser = _avDatabaseUser;
            jetspeedDatabaseUser = _jetspeedDatabaseUser;
            SetAVDatabasePassword(_avDatabasePassword);
            SetJetspeedDatabasePassword(_jetspeedDatabasePassword);
        }

        #endregion

        #region serialization

        public void DeserializeFileToConfig(ConfigHandler _config)
        {

        }

        #endregion
        //TODO Move secure password handling to CommonUtils so the code isn't repeated
        #region getters and setters

        public void SetAVDatabasePassword(SecureString _password)
        {
            
            if (avDatabasePassword.IsReadOnly())
            {
                avDatabasePassword.Dispose();
                avDatabasePassword = new SecureString();
                avDatabasePassword = _password;
                avDatabasePassword.MakeReadOnly();
            }
            else
            {
                avDatabasePassword.Clear();
                avDatabasePassword = _password;
                avDatabasePassword.MakeReadOnly();
            }
        }

        public void SetJetspeedDatabasePassword(SecureString _password)
        {
            
            if (jetspeedDatabasePassword.IsReadOnly())
            {
                jetspeedDatabasePassword.Dispose();
                jetspeedDatabasePassword = new SecureString();
                jetspeedDatabasePassword = _password;
                jetspeedDatabasePassword.MakeReadOnly();
            }
            else
            {
                jetspeedDatabasePassword.Clear();
                jetspeedDatabasePassword = _password;
                jetspeedDatabasePassword.MakeReadOnly();
            }

        }

        public void SetAVDatabasePassword(string _password)
        {
            
            if (avDatabasePassword.IsReadOnly())
            {
                avDatabasePassword.Dispose();
                avDatabasePassword = new SecureString();
                foreach (char c in _password)
                {
                    avDatabasePassword.AppendChar(c);
                }
                avDatabasePassword.MakeReadOnly();
            }
            else
            {
                avDatabasePassword.Clear();
                foreach (char c in _password)
                {
                    avDatabasePassword.AppendChar(c);
                }
                avDatabasePassword.MakeReadOnly();
            }

        }

        public void SetJetspeedDatabasePassword(string _password)
        {
           
            if (jetspeedDatabasePassword.IsReadOnly())
            {
                jetspeedDatabasePassword.Dispose();
                jetspeedDatabasePassword = new SecureString();
                foreach (char c in _password)
                {
                    jetspeedDatabasePassword.AppendChar(c);
                }
                jetspeedDatabasePassword.MakeReadOnly();
            }
            else
            {
                jetspeedDatabasePassword.Clear();
                foreach (char c in _password)
                {
                    jetspeedDatabasePassword.AppendChar(c);
                }
                jetspeedDatabasePassword.MakeReadOnly();
            }

        }

        public string GetAVDatabasePassword()
        {
            return AAUtil.DecryptSecureString(avDatabasePassword);
        }

        public string GetJetspeedDatabasePassword()
        {
            return AAUtil.DecryptSecureString(jetspeedDatabasePassword);
        }

        public SecureString GetAVDatabasePasswordSecure()
        {
            return avDatabasePassword;
        }

        public SecureString GetJetspeedDatabasePasswordSecure()
        {
            return jetspeedDatabasePassword;
        }

        #endregion

    }
}
