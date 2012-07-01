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
        public string[] sw_doctype;
        //文件分类
        public string sw_folder;
    }
}
