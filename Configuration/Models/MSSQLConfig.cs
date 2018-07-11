using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using rydavidson.Accela.Common;

namespace rydavidson.Accela.Configuration.Models
{
    public sealed class MSSQLConfig
    {
        #region properties

        public string avDBHost { get; set; }
        public string avDBName { get; set; }
        public string avJetspeedDBName { get; set; }
        public string avUser { get; set; }
        public string avJetspeedUser { get; set; }
        public string avComponent { get; set; }
        public int port { get; set; }
        private SecureString avDBPassword = new SecureString();
        private SecureString avJetspeedDBPassword = new SecureString();

        #endregion

        #region constructors

        public MSSQLConfig() { }

        public MSSQLConfig(string _serverHostName, string _avDatabaseName, string _jetspeedDatabaseName)
        {
            avDBHost = _serverHostName;
            avDBName = _avDatabaseName;
            avJetspeedDBName = _jetspeedDatabaseName;
        }

        public MSSQLConfig(string _serverHostName, string _avDatabaseName, string _jetspeedDatabaseName, string _avDatabaseUser, string _jetspeedDatabaseUser, 
            SecureString _avDatabasePassword, SecureString _jetspeedDatabasePassword)
        {
            avDBHost = _serverHostName;
            avDBName = _avDatabaseName;
            avJetspeedDBName = _jetspeedDatabaseName;
            avUser = _avDatabaseUser;
            avJetspeedUser = _jetspeedDatabaseUser;
            SetAVDatabasePassword(_avDatabasePassword);
            SetJetspeedDatabasePassword(_jetspeedDatabasePassword);
        }

        #endregion

        #region getters and setters

        public void SetAVDatabasePassword(SecureString _password)
        {
            
            if (avDBPassword.IsReadOnly())
            {
                avDBPassword.Dispose();
                avDBPassword = new SecureString();
                avDBPassword = _password;
                avDBPassword.MakeReadOnly();
            }
            else
            {
                avDBPassword.Clear();
                avDBPassword = _password;
                avDBPassword.MakeReadOnly();
            }
        }

        public void SetJetspeedDatabasePassword(SecureString _password)
        {
            
            if (avJetspeedDBPassword.IsReadOnly())
            {
                avJetspeedDBPassword.Dispose();
                avJetspeedDBPassword = new SecureString();
                avJetspeedDBPassword = _password;
                avJetspeedDBPassword.MakeReadOnly();
            }
            else
            {
                avJetspeedDBPassword.Clear();
                avJetspeedDBPassword = _password;
                avJetspeedDBPassword.MakeReadOnly();
            }

        }

        public void SetAVDatabasePassword(string _password)
        {
            
            if (avDBPassword.IsReadOnly())
            {
                avDBPassword.Dispose();
                avDBPassword = new SecureString();
                foreach (char c in _password)
                {
                    avDBPassword.AppendChar(c);
                }
                avDBPassword.MakeReadOnly();
            }
            else
            {
                avDBPassword.Clear();
                foreach (char c in _password)
                {
                    avDBPassword.AppendChar(c);
                }
                avDBPassword.MakeReadOnly();
            }

        }

        public void SetJetspeedDatabasePassword(string _password)
        {
           
            if (avJetspeedDBPassword.IsReadOnly())
            {
                avJetspeedDBPassword.Dispose();
                avJetspeedDBPassword = new SecureString();
                foreach (char c in _password)
                {
                    avJetspeedDBPassword.AppendChar(c);
                }
                avJetspeedDBPassword.MakeReadOnly();
            }
            else
            {
                avJetspeedDBPassword.Clear();
                foreach (char c in _password)
                {
                    avJetspeedDBPassword.AppendChar(c);
                }
                avJetspeedDBPassword.MakeReadOnly();
            }

        }

        public string GetAVDatabasePassword()
        {
            return SecureUtils.SecureStringToString(avDBPassword);
        }

        public string GetJetspeedDatabasePassword()
        {
            return SecureUtils.SecureStringToString(avJetspeedDBPassword);
        }

        public SecureString GetAVDatabasePasswordSecure()
        {
            return avDBPassword;
        }

        public SecureString GetJetspeedDatabasePasswordSecure()
        {
            return avJetspeedDBPassword;
        }



        #endregion

    }
}
