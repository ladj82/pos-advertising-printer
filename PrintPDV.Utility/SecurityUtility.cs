using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace PrintPDV.Utility
{
    public class SecurityUtility
    {
        public static string GetMd5Hash(string input)
        {
            var enc = Encoding.Unicode.GetEncoder();

            var unicodeText = new byte[input.Length * 2];
            enc.GetBytes(input.ToCharArray(), 0, input.Length, unicodeText, 0, true);

            MD5 md5 = new MD5CryptoServiceProvider();
            var result = md5.ComputeHash(unicodeText);

            var sb = new StringBuilder();

            foreach (var t in result)
                sb.Append(t.ToString("X2"));

            return sb.ToString();
        }

        public static bool CheckMd5Hash(string input, string hash)
        {
            var hashOfInput = GetMd5Hash(input);
            var comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(hashOfInput, hash);
        }

        public static string EncryptString(SecureString input)
        {
            var encryptedData = ProtectedData.Protect(
                Encoding.Unicode.GetBytes(ToInsecureString(input)),
                entropy,
                DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encryptedData);
        }

        public static SecureString DecryptString(string encryptedData)
        {
            try
            {
                var decryptedData = ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    entropy,
                    DataProtectionScope.CurrentUser);

                return ToSecureString(Encoding.Unicode.GetString(decryptedData));
            }
            catch
            {
                return new SecureString();
            }
        }

        private static SecureString ToSecureString(string input)
        {
            var secure = new SecureString();

            foreach (var c in input)
                secure.AppendChar(c);

            secure.MakeReadOnly();

            return secure;
        }

        private static string ToInsecureString(SecureString input)
        {
            string returnValue;

            var ptr = Marshal.SecureStringToBSTR(input);

            try
            {
                returnValue = Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }

            return returnValue;
        }

        private static readonly byte[] entropy = Encoding.Unicode.GetBytes("}*JF=G5x~?6rg>4)x5b32B7+8LNi?IF4k)M01d051>0J]4-9[g,o%7ivX`!V1kV");
    }
}
