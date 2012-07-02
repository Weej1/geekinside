using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Models
{
    public class SearchFilterModel
    {
        //关键字
        public string sw;
        //不包含
        public string sw_notincluded;
        //文件类型
        //  1doc/docx
        //  2xls/xlsx
        //  3ppt/pptx
        //  4pdf
        //  5wmv
        public string[] sw_doctype;
        
    }
}
