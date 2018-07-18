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
    public sealed class MssqlConfig
    {
        #region properties

        public string AvDbHost { get; set; }
        public string AvDbName { get; set; }
        public string AvJetspeedDbName { get; set; }
        public string AvUser { get; set; }
        public string AvJetspeedUser { get; set; }
        public string AvComponent { get; set; }
        public string Port { get; set; }
        private SecureString avDbPassword = new SecureString();
        private SecureString avJetspeedDbPassword = new SecureString();

        #endregion

        #region constructors

        public MssqlConfig() { }

        public MssqlConfig(string _serverHostName, string _avDatabaseName, string _jetspeedDatabaseName)
        {
            AvDbHost = _serverHostName;
            AvDbName = _avDatabaseName;
            AvJetspeedDbName = _jetspeedDatabaseName;
        }

        public MssqlConfig(string _serverHostName, string _avDatabaseName, string _jetspeedDatabaseName, string _avDatabaseUser, string _jetspeedDatabaseUser, 
            SecureString _avDatabasePassword, SecureString _jetspeedDatabasePassword)
        {
            AvDbHost = _serverHostName;
            AvDbName = _avDatabaseName;
            AvJetspeedDbName = _jetspeedDatabaseName;
            AvUser = _avDatabaseUser;
            AvJetspeedUser = _jetspeedDatabaseUser;
            SetAvDatabasePassword(_avDatabasePassword);
            SetJetspeedDatabasePassword(_jetspeedDatabasePassword);
        }

        #endregion

        #region getters and setters

        public void SetAvDatabasePassword(SecureString _password)
        {
            
            if (avDbPassword.IsReadOnly())
            {
                avDbPassword.Dispose();
                avDbPassword = new SecureString();
                avDbPassword = _password;
                avDbPassword.MakeReadOnly();
            }
            else
            {
                avDbPassword.Clear();
                avDbPassword = _password;
                avDbPassword.MakeReadOnly();
            }
        }

        public void SetJetspeedDatabasePassword(SecureString _password)
        {
            
            if (avJetspeedDbPassword.IsReadOnly())
            {
                avJetspeedDbPassword.Dispose();
                avJetspeedDbPassword = new SecureString();
                avJetspeedDbPassword = _password;
                avJetspeedDbPassword.MakeReadOnly();
            }
            else
            {
                avJetspeedDbPassword.Clear();
                avJetspeedDbPassword = _password;
                avJetspeedDbPassword.MakeReadOnly();
            }

        }

        public void SetAvDatabasePassword(string _password)
        {
            
            if (avDbPassword.IsReadOnly())
            {
                avDbPassword.Dispose();
                avDbPassword = new SecureString();
                foreach (char c in _password)
                {
                    avDbPassword.AppendChar(c);
                }
                avDbPassword.MakeReadOnly();
            }
            else
            {
                avDbPassword.Clear();
                foreach (char c in _password)
                {
                    avDbPassword.AppendChar(c);
                }
                avDbPassword.MakeReadOnly();
            }

        }

        public void SetJetspeedDatabasePassword(string _password)
        {
           
            if (avJetspeedDbPassword.IsReadOnly())
            {
                avJetspeedDbPassword.Dispose();
                avJetspeedDbPassword = new SecureString();
                foreach (char c in _password)
                {
                    avJetspeedDbPassword.AppendChar(c);
                }
                avJetspeedDbPassword.MakeReadOnly();
            }
            else
            {
                avJetspeedDbPassword.Clear();
                foreach (char c in _password)
                {
                    avJetspeedDbPassword.AppendChar(c);
                }
                avJetspeedDbPassword.MakeReadOnly();
            }

        }

        public string GetAvDatabasePassword()
        {
            return SecureUtils.SecureStringToString(avDbPassword);
        }

        public string GetJetspeedDatabasePassword()
        {
            return SecureUtils.SecureStringToString(avJetspeedDbPassword);
        }

        public SecureString GetAvDatabasePasswordSecure()
        {
            return avDbPassword;
        }

        public SecureString GetJetspeedDatabasePasswordSecure()
        {
            return avJetspeedDbPassword;
        }



        #endregion


        public new string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("av.db.host: " + AvDbHost);
            sb.AppendLine("av.db.sid: " + AvDbHost);
            sb.AppendLine("av.db.username: " + AvDbHost);
            sb.AppendLine("av.db.password: " + AvDbHost);
            sb.AppendLine("av.db.port: " + AvDbHost);
            
            if(!string.IsNullOrWhiteSpace(AvJetspeedDbName))
                sb.AppendLine("av.jetspeed.db.sid: " + AvJetspeedDbName);
            if(!string.IsNullOrWhiteSpace(AvJetspeedUser))
                sb.AppendLine("av.jetspeed.db.username: " + AvJetspeedUser);
            if(!string.IsNullOrWhiteSpace(SecureUtils.SecureStringToString(avJetspeedDbPassword)))
                sb.AppendLine("av.jetspeed.db.password: " + SecureUtils.SecureStringToString(avJetspeedDbPassword));
            if(!string.IsNullOrWhiteSpace(AvJetspeedDbName))
                sb.AppendLine("av.jetspeed.db.port: " + AvDbHost);
            return sb.ToString();
        }
    }
}
