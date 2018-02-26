using System.Security.Cryptography;
using System.Text;

namespace MediaShop.Common
{
    public static class StringExtentions
    {
        public static string GetHash(this string data)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(data);
            MD5CryptoServiceProvider csp =
                new MD5CryptoServiceProvider();
            byte[] byteHash = csp.ComputeHash(bytes);

            string hash = string.Empty;

            foreach (byte b in byteHash)
            {
                hash += $"{b:x2}";
            }

            return hash;
        }
    }
}