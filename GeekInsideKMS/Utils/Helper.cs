using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;

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

        public static void ConvertDocumentToSwf(string documentPath, string serverPath)
        {
            FileInfo originalFile = new FileInfo(documentPath);
            string outputPdfFolder = Path.Combine(serverPath, @"uploadfiles\temp");
            string outputSwfFolder = Path.Combine(serverPath, @"swf");

            // 第一步，将word转成pdf，存放在uploadfiles\temp目录下
            // Create a new Microsoft Word application object
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();

            // C# doesn't have optional arguments so we'll need a dummy value
            object oMissing = System.Reflection.Missing.Value;

            word.Visible = false;
            word.ScreenUpdating = false;

            Object filename = (Object)originalFile.FullName;

            // Use the dummy value as a placeholder for optional arguments
            Document doc = word.Documents.Open(ref filename, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            doc.Activate();

            object outputPdfFile = Path.Combine(outputPdfFolder, originalFile.Name.Replace(originalFile.Extension, ".pdf"));
            object fileFormat = WdSaveFormat.wdFormatPDF;

            // Save document into PDF Format
            doc.SaveAs(ref outputPdfFile,
                ref fileFormat, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            // Close the Word document, but leave the Word application open.
            // doc has to be cast to type _Document so that it will find the
            // correct Close method.                
            object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
            ((_Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);
            doc = null;

            // word has to be cast to type _Application so that it will find
            // the correct Quit method.
            ((_Application)word).Quit(ref oMissing, ref oMissing, ref oMissing);
            word = null;

            // 第二步，将pdf转成swf，存放在swf目录下
            string pdfFilePath = (string)outputPdfFile;
            string swfFilePath = Path.Combine(outputSwfFolder, Path.GetFileNameWithoutExtension(pdfFilePath) + ".swf");

            // 执行转换命令
            // pdf2swf.exe Paper.pdf -o Paper.swf -f -T 9 -t -s storeallcharacters
            // @see http://flexpaper.devaldi.com/docs_converting.jsp
            string command = String.Format(
                "pdf2swf.exe \"{0}\" -o \"{1}\" -f -T 9 -t -s storeallcharacters",
                pdfFilePath,
                swfFilePath);
            ExecuteCommand(command, outputSwfFolder);
        }

        public static void ExecuteCommand(string command, string workingDir)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + command;
            startInfo.WorkingDirectory = workingDir;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;    // 不显示窗口

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            process.Close();
        }
    }
}
