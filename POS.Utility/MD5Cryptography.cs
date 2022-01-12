using System;
using System.Security.Cryptography;
using System.Text;

namespace POS.Utility
{
    public class MD5Hash
    {
        public static string Hash(string text)
        {
            using(var md5 = MD5.Create())
            {
                var sourceBytes = Encoding.UTF8.GetBytes(text);
                var hasBytes = md5.ComputeHash(sourceBytes);
                var hash = BitConverter.ToString(hasBytes).Replace("-", string.Empty);
                return hash;
            }
        }
    }
}
