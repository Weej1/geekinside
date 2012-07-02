using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Collections.Specialized;
using Utils;

namespace Index
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            IServiceProvider provider = (IServiceProvider)context;
            HttpWorkerRequest wr = (HttpWorkerRequest)provider.GetService(typeof(HttpWorkerRequest));

            int count = 8192;
            byte[] buffer;

            if (!wr.IsEntireEntityBodyIsPreloaded())
            {
                string chunk = context.Request["chunk"];
                string chunks = context.Request["chunks"];
                string fileName = context.Request["name"];
                string id = context.Request["id"];

                string extension = fileName.Substring(fileName.LastIndexOf('.'));
                buffer = new Byte[count];
                string filePath = Helper.REPO_ROOT + "\\temp\\" + id + extension;
                using (var fs = new FileStream(filePath, chunk.Equals("0") ? FileMode.Create : FileMode.Append))
                {
                    if (context.Request.Files.Count > 0)
                    {
                        HttpPostedFile httpFile = context.Request.Files[0];
                        while (true)
                        {
                            count = httpFile.InputStream.Read(buffer, 0, count);
                            if (count == 0)
                            {
                                break;
                            }
                            saveFile(fs, buffer, count);
                        }
                    }
                    fs.Close();
                    fs.Dispose();
                }

                if (Convert.ToInt32(chunk) == (Convert.ToInt32(chunks) - 1))
                {
                    context.Response.Write("success");
                }
            }

        }

        public void saveFile(FileStream fs, Byte[] buffer, int count)
        {
            fs.Write(buffer, 0, count);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}