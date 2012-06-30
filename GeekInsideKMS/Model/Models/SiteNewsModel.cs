using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Models
{
    public class SiteNewsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string NewsContent { get; set; }
        public DateTime PubTime { get; set; }
        public Boolean IsOnTop { get; set; }
    }
}
