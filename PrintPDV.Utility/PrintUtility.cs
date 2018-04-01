using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Management;

namespace PrintPDV.Utility
{
    public class PrintUtility
    {
        public class PrinterInfo
        {
            public string Name { get; set; }

            public string ComPort { get; set; }

            public string Network { get; set; }

            public List<KeyValuePair<string,string>> Models { get; set; }
        }

        public static string GetCenterAlignmentText(string text, int qntCharactersPerLine)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentException();

            var qntSpaces = qntCharactersPerLine / 2 - text.Length / 2;
            qntSpaces = qntSpaces < 0 ? 0 : qntSpaces;

            var spaces = string.Concat(Enumerable.Repeat(" ", qntSpaces));

            return string.Format("{0}{1}", spaces, text);
        }

        public static string GetRightAlignmentText(string text, int qntCharactersPerLine)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentException();

            var qntSpaces = qntCharactersPerLine - text.Length;
            var spaces = new string(' ', qntSpaces);

            return string.Format("{0}{1}", spaces, text);
        }

        public static List<PrinterInfo> GetSpoolDevicesInfo()
        {
            var printers = new ManagementObjectSearcher("SELECT * from Win32_Printer");
            var spoolDevicesList = new List<PrintUtility.PrinterInfo>();

            foreach (var item in printers.Get())
            {
                var deviceCaption = item["Caption"];

                if (deviceCaption == null) continue;

                var spoolDevice = new PrintUtility.PrinterInfo
                {
                    Name = item["Name"].ToString(),
                    Network = null,
                    ComPort = null
                };

                spoolDevicesList.Add(spoolDevice);
            }

            return spoolDevicesList;
        }

        public static List<PrinterInfo> GetComDevicesInfo()
        {
            var options = ProcessConnection.ProcessConnectionOptions();
            var connectionScope = ProcessConnection.ConnectionScope(Environment.MachineName, options, @"\root\CIMV2");

            var objectQuery = new ObjectQuery("SELECT Name, Caption FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0");
            var comPortSearcher = new ManagementObjectSearcher(connectionScope, objectQuery);
            var comPortDevicesList = new List<PrinterInfo>();

            using (comPortSearcher)
            {
                foreach (var item in comPortSearcher.Get())
                {
                    var deviceCaption = item["Caption"];

                    if (deviceCaption == null) continue;

                    if (!deviceCaption.ToString().Contains("(COM")) continue;

                    var comDevice = new PrintUtility.PrinterInfo
                    {
                        Name = item["Name"].ToString(),
                        ComPort = deviceCaption.ToString().Substring(deviceCaption.ToString().LastIndexOf("(COM", StringComparison.Ordinal)).Replace("(", string.Empty).Replace(")", string.Empty)
                    };

                    comPortDevicesList.Add(comDevice);
                }
            }

            return comPortDevicesList;
        }

        public static List<PrinterInfo> GetNetworkDevicesInfo()
        {
            var remoteDevicesList = new List<PrinterInfo>();

            using (var ds = new DirectorySearcher())
            {
                ds.SearchRoot = new DirectoryEntry("");
                ds.Filter = "(|(&(objectCategory=printQueue)(name=*)))";

                ds.PropertiesToLoad.Add("printername");
                ds.PropertiesToLoad.Add("description");
                ds.PropertiesToLoad.Add("servername");
                ds.PropertiesToLoad.Add("printStatus");
                ds.PropertiesToLoad.Add("portname");
                //ds.PropertiesToLoad.Add("location");
                //ds.PropertiesToLoad.Add("printNetworkAddress");
                //ds.PropertiesToLoad.Add("cn");
                //ds.PropertiesToLoad.Add("name");
                //ds.PropertiesToLoad.Add("printsharename");
                ds.ReferralChasing = ReferralChasingOption.None;
                ds.Sort = new SortOption("printername", SortDirection.Ascending);
                ds.PageSize = 0;
                ds.SizeLimit = 4;

                using (var src = ds.FindAll())
                {
                    foreach (SearchResult sr in src)
                    {
                        var remoteDevice = new PrinterInfo
                        {
                            Name = sr.Properties["printername"].Count > 0 ? sr.Properties["printername"][0].ToString() : null,
                            Network = sr.Properties["servername"].Count > 0 ? sr.Properties["servername"][0].ToString() : null,
                            ComPort = sr.Properties["portname"].Count > 0 ? sr.Properties["portname"][0].ToString() : null,
                        };

                        remoteDevicesList.Add(remoteDevice);
                    }
                }
            }

            return remoteDevicesList;
        }

        internal class ProcessConnection
        {
            public static ConnectionOptions ProcessConnectionOptions()
            {
                var options = new ConnectionOptions
                {
                    Impersonation = ImpersonationLevel.Impersonate,
                    Authentication = AuthenticationLevel.Default,
                    EnablePrivileges = true
                };

                return options;
            }

            public static ManagementScope ConnectionScope(string machineName, ConnectionOptions options, string path)
            {
                var connectScope = new ManagementScope
                {
                    Path = new ManagementPath(@"\\" + machineName + path),
                    Options = options
                };

                connectScope.Connect();

                return connectScope;
            }
        }
    }
}
