using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Utils
{
    public class Helper
    {
        public static readonly string REPO_ROOT = @"D:\geekinsidekms\repository";

        private Helper() { }

        // 加密text，返回32的密文
        public static string EncryptByMD5(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string strings = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", string.Empty);
            return strings;
        }

        public static string CreateNewFolderPath(string parentPath)
        {
            return parentPath + DateTime.Now.ToString(@"yyyyMMddHHmmss");
        }

        public static void CreateDirectory(string dbPath)
        {
            Directory.CreateDirectory(REPO_ROOT + dbPath);
        }

        public static void ConvertDocumentToSwf(string documentPath, string swfFolderPath)
        { 
            
        }
    }
}
