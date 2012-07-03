using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Models
{
    public class PageModel
    {
        public int TotalCount { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
