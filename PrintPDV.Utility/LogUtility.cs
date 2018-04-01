using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using PrintPDV.LanguagePack;

namespace PrintPDV.Utility
{
    public class LogUtility
    {
        private static readonly object Locker = new object();

        static LogUtility()
        {
            try
            {
                if (!Directory.Exists(AppConfigUtility.LogFolderPath))
                    Directory.CreateDirectory(AppConfigUtility.LogFolderPath);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format(AppStrings.Application_ErrorMessage_FolderNotCreated, AppConfigUtility.LogFolderPath), AppStrings.Generic_MessageBox_ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Log(LogType type, string message)
        {
            Log(type, message, string.Empty);
        }

        public static void Log(LogType type, string message, string exception)
        {
            if (!AppConfigUtility.LogEnabled) return;

            lock (Locker)
            {
                var log = !File.Exists(AppConfigUtility.LogFilename)
                    ? new Log { Logs = new List<LogItem>() }
                    : File.ReadAllText(AppConfigUtility.LogFilename).DeserializeFromXml<Log>();

                log.Logs.Add(new LogItem { DateTime = DateTime.Now, Type = type.ToString(), Exception = exception, Message = message });

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(log.SerializeToXml());
                xmlDoc.Save(AppConfigUtility.LogFilename);
            }
        }

        public enum LogType
        {
            Error,
            Warning,
            Information,
            SystemError
        }
    }

    [Serializable]
    [XmlRoot("Log")]
    public class Log
    {
        [XmlArray("Logs"), XmlArrayItem(typeof(LogItem), ElementName = "LogItem")]
        public List<LogItem> Logs { get; set; }
    }

    [Serializable]
    public class LogItem
    {
        public string Type { get; set; }

        public DateTime DateTime { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }
    }
}
