using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace rydavidson.Accela.Common
{
    public class Logger
    {
        public string logFile { get; set; }
        public Boolean isVerbose { get; set; }
        public Boolean isDebug { get; set; }
        public Type callingClass { get; set; }

        public Boolean isEnabled { get; set; }
        
        StringBuilder log = new StringBuilder();

        #region constructors

        public Logger()
        {
            isEnabled = false;
        }

        //public Logger(Boolean _isEnabled) 
        //{
        //    isEnabled = _isEnabled;
        //}

        public Logger(Type _callingClass)
        {
            logFile = _callingClass.Name + ".log";
        }

        public Logger(string _logFile)
        {
            logFile = _logFile;
        }

        public Logger(string _logFile, Boolean _isDebug, Boolean _isVerbose)
        {
            logFile = _logFile;
            isDebug = _isDebug;
            isVerbose = _isVerbose;
        }

        #endregion


        public void Log(string s)
        {
            ProcessWrite(s + Environment.NewLine);
        }

        public void Info(string s)
        {
            log.AppendLine(s);
            ProcessWrite(" - INFO: " + log.ToString());
            log.Clear();
        }
        public void Warn(string s)
        {
            log.AppendLine(s);
            ProcessWrite(" - WARN: " + log.ToString());
            log.Clear();
        }
        public void Error(string s)
        {
            log.AppendLine(s);
            ProcessWrite(" - ERROR: " + log.ToString());
            log.Clear();
        }
        public void Debug(string s)
        {
            if (isDebug)
            {
                log.AppendLine(s);
                ProcessWrite(" - DEBUG: " + log.ToString());
                log.Clear();
            }
        }
        public void Trace(string s)
        {
            
            if (isVerbose)
            {
                log.AppendLine(s);
                ProcessWrite(" - TRACE: " + log.ToString());
                log.Clear();
            }
        }

        private void ProcessWrite(string text)
        {
            if (!isEnabled)
                return;

            if (!File.Exists(logFile))
                return;

            if (callingClass != null)
                text = callingClass.ToString() + " " + text;

            text = DateTime.Now.ToString() + text;
            File.AppendAllText(logFile, text);
        }

        // TODO Figure out why the async file writing sometimes causes log entries to not be written

        //Task ProcessWrite(string text)
        //{
        //    return WriteTextAsync(logFile, text);
        //}

        //async Task WriteTextAsync(string filePath, string text)
        //{
        //    text = DateTime.Now.Tostring() + text;
        //    byte[] encodedText = Encoding.Unicode.GetBytes(text); // ran into issues with Encoding.Default so I'm using Unicode to force i18n compat
        //    using (FileStream sourceStream = new FileStream(filePath,
        //        FileMode.Append, FileAccess.Write, FileShare.None,
        //        bufferSize: 4096, useAsync: true))
        //    {
        //        await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
        //    };
        //}
    }
}
