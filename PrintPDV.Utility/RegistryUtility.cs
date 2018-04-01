using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace PrintPDV.Utility
{
    public class RegistryUtility
    {
        private static readonly string SubKey = "SOFTWARE\\" + Application.ProductName.ToUpper();
        private static readonly RegistryKey BaseRegistryKey = Registry.LocalMachine;
        
        public static string Read(string keyName)
        {
            RegistryKey rk = BaseRegistryKey;
            RegistryKey sk1 = rk.OpenSubKey(SubKey);
            
            if (sk1 == null)
                return null;

            return (string)sk1.GetValue(keyName.ToUpper());
        }

        public static bool Write(string keyName, object value)
        {
            try
            {
                RegistryKey rk = BaseRegistryKey;
                RegistryKey sk1 = rk.CreateSubKey(SubKey);

                if (sk1 == null) return false;

                sk1.SetValue(keyName.ToUpper(), value);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeleteKey(string keyName)
        {
            try
            {
                RegistryKey rk = BaseRegistryKey;
                RegistryKey sk1 = rk.CreateSubKey(SubKey);
        
                if (sk1 == null)
                    return true;
                
                sk1.DeleteValue(keyName);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DeleteSubKeyTree()
        {
            try
            {
                RegistryKey rk = BaseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(SubKey);
        
                if (sk1 != null)
                    rk.DeleteSubKeyTree(SubKey);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static int SubKeyCount()
        {
            try
            {
                RegistryKey rk = BaseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(SubKey);
        
                return sk1 != null ? sk1.SubKeyCount : 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int ValueCount()
        {
            try
            {
                RegistryKey rk = BaseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(SubKey);
        
                return sk1 != null ? sk1.ValueCount : 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
