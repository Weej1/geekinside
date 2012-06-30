using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Utils
{
    public class Helper
    {
        private Helper() { }

        // 加密text，返回32的密文
        public static string EncryptByMD5(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string strings = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", string.Empty);
            return strings;
        }
    }
}
