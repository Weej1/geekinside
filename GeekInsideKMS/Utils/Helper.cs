using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using msWord = Microsoft.Office.Interop.Word;
using msExcel = Microsoft.Office.Interop.Excel;
using msPPT = Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
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

        public static void ConvertDocumentToSwf(string documentPath)
        {
            FileInfo originalFile = new FileInfo(documentPath);
            string outputPdfFolder = Path.Combine(REPO_ROOT, @"pdf");

            object outputPdfFile = Path.Combine(outputPdfFolder, originalFile.Name.Replace(originalFile.Extension, ".pdf"));

            // C# doesn't have optional arguments so we'll need a dummy value
            object oMissing = System.Reflection.Missing.Value;

            if (originalFile.Extension == ".doc" || originalFile.Extension == ".docx")
            {
                // 将word转成pdf，存放在pdf目录下
                // Create a new Microsoft Word application object
                msWord.Application word = new msWord.Application();

                word.Visible = false;
                word.ScreenUpdating = false;

                Object filename = (Object)originalFile.FullName;

                // Use the dummy value as a placeholder for optional arguments
                msWord.Document doc = word.Documents.Open(ref filename, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                doc.Activate();

                object fileFormat = msWord.WdSaveFormat.wdFormatPDF;

                // Save document into PDF Format
                doc.SaveAs(ref outputPdfFile,
                    ref fileFormat, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                // Close the Word document, but leave the Word application open.
                // doc has to be cast to type _Document so that it will find the
                // correct Close method.                
                object saveChanges = msWord.WdSaveOptions.wdDoNotSaveChanges;
                ((msWord._Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);
                doc = null;

                // word has to be cast to type _Application so that it will find
                // the correct Quit method.
                ((msWord._Application)word).Quit(ref oMissing, ref oMissing, ref oMissing);
                word = null;
            }
            else if (originalFile.Extension == ".xls" || originalFile.Extension == ".xlsx")
            {
                msExcel.Application excel = new msExcel.Application();
                excel.Visible = false;
                excel.ScreenUpdating = false;
                excel.DisplayAlerts = false;

                string filename = originalFile.FullName;

                msExcel.Workbook wbk = excel.Workbooks.Open(filename, oMissing,
                    oMissing, oMissing, oMissing, oMissing, oMissing,
                    oMissing, oMissing, oMissing, oMissing, oMissing,
                    oMissing, oMissing, oMissing);
                wbk.Activate();

                msExcel.XlFixedFormatType fileFormat = msExcel.XlFixedFormatType.xlTypePDF;

                // Save document into PDF Format
                wbk.ExportAsFixedFormat(fileFormat, outputPdfFile,
                    oMissing, oMissing, oMissing,
                    oMissing, oMissing, oMissing,
                    oMissing);

                object saveChanges = msExcel.XlSaveAction.xlDoNotSaveChanges;
                ((msExcel._Workbook)wbk).Close(saveChanges, oMissing, oMissing);
                wbk = null;

                ((msExcel._Application)excel).Quit();
                excel = null;
            }
            else if (originalFile.Extension == ".ppt" || originalFile.Extension == ".pptx")
            {
                msPPT.Application app = new msPPT.Application();
                string sourcePptx = originalFile.FullName;
                msPPT.Presentation pptx = app.Presentations.Open(
                    sourcePptx, MsoTriState.msoFalse, MsoTriState.msoTrue, MsoTriState.msoFalse);
                pptx.SaveAs((string)outputPdfFile, msPPT.PpSaveAsFileType.ppSaveAsPDF, MsoTriState.msoTrue);
                app.Quit();
            }
            

            // 第二步，将pdf转成swf，存放在swf目录下
            ConvertPdfToSwf((string)outputPdfFile);
        }

        // 将pdf转成swf，存放在swf目录下
        public static void ConvertPdfToSwf(string pdfFile)
        {
            string swfFolder = Path.Combine(REPO_ROOT, "swf");
            string swfFile = Path.Combine(swfFolder, Path.GetFileNameWithoutExtension(pdfFile) + ".swf");

            // 执行转换命令
            // swf目录下必须有pdf2swf.exe
            // pdf2swf.exe Paper.pdf -o Paper.swf -f -T 9 -t -s storeallcharacters
            // @see http://flexpaper.devaldi.com/docs_converting.jsp
            string command = String.Format(
                "pdf2swf.exe \"{0}\" -o \"{1}\" -f -T 9 -t -s storeallcharacters",
                pdfFile,
                swfFile);
            ExecuteCommand(command, swfFolder);
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
