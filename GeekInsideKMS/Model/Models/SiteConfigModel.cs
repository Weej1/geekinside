using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Models
{
    public class SiteConfigModel
    {
        public SiteConfigModel()
        {

        }

        public SiteConfigModel(string PropertyName, string PropertyValue, string PropertyDescription)
        {
            this.PropertyName = PropertyName;
            this.PropertyValue = PropertyValue;
            this.PropertyDescription = PropertyDescription;
        }

        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string PropertyDescription { get; set; }
    }
}
