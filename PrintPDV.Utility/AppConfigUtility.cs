using System;
using System.Collections.Generic;
using System.Configuration;

namespace PrintPDV.Utility
{
    public class AppConfigUtility
    {
        public AppConfigUtility(Dictionary<string,string> appConfig)
        {
            if (appConfig == null) throw new ArgumentNullException();

            DefaultLanguage = appConfig["defaultLanguage"];
            DefaultPrintDocumentName = appConfig["printDocumentName"];
            WebsiteUrl = appConfig["websiteUrl"];
            ValidateLicenseUrl = appConfig["validateLicenseUrl"];
            SyncStatisticUrl = appConfig["syncStatisticUrl"];
            FingerPrint = GeneralUtility.GetMachineFingerPrint();
        }

        public static readonly string AppDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public static bool LogEnabled {
            get 
            {
                try
                {
                    return Convert.ToBoolean(ConfigurationManager.AppSettings["logEnabled"]);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static readonly string DatabaseName = "PrintPDV.sqlite";

        public static readonly string DatabasePassword = "e67wyLqU2M9WKCT5";

        public static readonly string DatabaseConnString = string.Format("data source={0}{1}; Version=3; Password={2};", AppDirectory, DatabaseName, DatabasePassword);

        public static readonly string LogFolderPath = AppDirectory + @"\logs";

        public static readonly string LogFilename = LogFolderPath + @"\log.xml";

        public static readonly string CampaignPath = AppDirectory + @"\campaigns";

        public static string DefaultLanguage { get; private set; }
        
        public static string DefaultPrintDocumentName { get; private set; }

        public static string FingerPrint { get; private set; }
        
        public static string WebsiteUrl { get; private set; }

        public static string ValidateLicenseUrl { get; private set; }

        public static string SyncStatisticUrl { get; private set; }
    }
}
