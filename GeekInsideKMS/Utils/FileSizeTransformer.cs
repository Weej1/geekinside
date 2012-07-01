using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public class FileSizeTransformer
    {
        public static string TransformSize(int size)
        {
            string contentLength = null;
            if (size < 1048576 && size >= 1024)
            {
                contentLength = size / 1024 + "KB";
            }
            else if (size >= 1048576 && size < 1073741824)
            {
                contentLength = size / (1024 * 1024) + "MB";
            }
            else if (size >= 1073741824)
            {
                contentLength = size / (1024 * 1024 * 1024) + "GB";
            }
            else
            {
                contentLength = size + "B";
            }
            return contentLength;
        }
    }
}
