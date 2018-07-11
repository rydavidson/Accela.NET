using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Text;
using rydavidson.Accela.Common;
using rydavidson.Accela.Configuration.Handlers;
using System.Threading.Tasks;
using System.Windows;

namespace rydavidson.Accela.Configuration.Common
{
    public class AAUtil
    {
        private Logger logger;
        private ExceptionHandler exceptionHandler;
        private RegistryKey accelaBaseKey;
        private RegistryKey instanceKey;
        private string AAInstallDir;

        #region constructors
        public AAUtil(string _logfile)
        {
            logger = new Logger(_logfile);
            logger.isEnabled = true;
            exceptionHandler = new ConfigurationExceptionHandler();
            exceptionHandler.SetLogger(logger);
        }

        public AAUtil(Logger _logger)
        {
            logger = _logger;
            logger.isEnabled = true;
            exceptionHandler = new ConfigurationExceptionHandler();
            exceptionHandler.SetLogger(logger);
        }

        public AAUtil()
        {
            logger = new Logger();
            exceptionHandler = new ConfigurationExceptionHandler();
            exceptionHandler.SetLogger(logger);
        }
        #endregion


        #region registry

        private RegistryKey GetAccelaBaseKey()
        {
            RegistryKey hklmReg;

            if (accelaBaseKey != null)
                return accelaBaseKey;

            try
            {
                hklmReg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                try
                {
                    accelaBaseKey = hklmReg.OpenSubKey(@"SOFTWARE\WOW6432Node\Accela Inc.", RegistryKeyPermissionCheck.ReadSubTree);
                    return accelaBaseKey;
                }
                catch (Exception e)
                {
                    exceptionHandler.HandleException(e, "Unable to access Accela registry key");
                }
            }
            catch (Exception e)
            {
                exceptionHandler.HandleException(e, "Unable to open HKLM registry key");
            }
            return accelaBaseKey;
        }

        private RegistryKey GetInstanceKey(string _version, string _instance)
        {

            if (instanceKey != null)
                return instanceKey;

            RegistryKey reg = GetAccelaBaseKey();

            try
            {
                instanceKey = reg.OpenSubKey(string.Format(@"AA Base Installer\{0}\{1}", _version, _instance), RegistryKeyPermissionCheck.ReadSubTree);
            }
            catch (Exception e)
            {
                exceptionHandler.HandleException(e, "Unable to get install directory from registry");
            }

            return instanceKey;
        }

        #endregion

        public List<string> GetAAVersions()
        {
            //if(GetAccelaBaseKey() == null)
            //{
            //    StringBuilder err = new StringBuilder();
            //    err.AppendLine("Couldn't open Accela Base Key");
            //    logger.LogError(err.ToString());
            //    return null;
            //}
            return new List<string>(GetAccelaBaseKey().OpenSubKey(@"AA Base Installer").GetSubKeyNames());
        }

        public List<string> GetInstancesForVersion(string versionToCheck)
        {
            List<string> instances = new List<string>();
            try
            {
                RegistryKey accelaBaseKey = GetAccelaBaseKey();
                List<string> versions = GetAAVersions();
                foreach (string version in versions)
                {
                    if (version.Trim() == versionToCheck.Trim())
                    {
                        foreach (string instance in GetAccelaBaseKey().
                            OpenSubKey(@"AA Base Installer\" + version).GetSubKeyNames())
                        {
                            instances.Add(instance);
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException uaex)
            {
                exceptionHandler.HandleException(uaex, "Access Denied getting instances for version " + versionToCheck);
            }
            catch (Exception e)
            {
                exceptionHandler.HandleException(e, "Unable to get instances for version");
            }

            return instances;
        }

        public string GetAAInstallDir(string version, string instance)
        {
            if(AAInstallDir != null)
                return AAInstallDir;
            RegistryKey reg = GetAccelaBaseKey();
            string installDir = "";
            if (reg != null && version != null && instance != null)
            {
                try
                {
                    instanceKey = reg.OpenSubKey(string.Format(@"AA Base Installer\{0}\{1}", version, instance), RegistryKeyPermissionCheck.ReadSubTree);
                    installDir = instanceKey.GetValue("InstallDir").ToString();
                }
                catch (Exception e)
                {
                    exceptionHandler.HandleException(e, "Unable to get install directory from registry");
                }
            }
            //if (installDir != "")
            AAInstallDir = installDir;
            return AAInstallDir;
        }
        public List<string> GetAAInstalledComponents(string _version, string _instance)
        {
            RegistryKey reg = GetAccelaBaseKey();
            string components = GetInstanceKey(_version, _instance).GetValue("InstallComponents").ToString();
            List<string> compList = new List<string>();
            foreach(string comp in components.Split(','))
            {
                compList.Add(comp);
            }
            return compList;
        }
        public Dictionary<string, string> GetAAConfigFilePaths(string _version, string _instance)
        {
            Dictionary<string, string> paths = new Dictionary<string, string>();
            List<string> components = GetAAInstalledComponents(_version, _instance);
            string installDir = GetAAInstallDir(_version, _instance);

            foreach (string comp in components)
            {
                string stemp = string.Format(@"{0}\{1}\conf\av\ServerConfig.properties", installDir, comp);
                if (File.Exists(stemp))
                    paths.Add(comp, stemp);
            }
            return paths;
        }
    }
}
