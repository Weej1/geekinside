using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;

namespace IDAL
{
    public interface IDALSiteConfig
    {
        Boolean addConfig(SiteConfigModel siteConfigModel);
        Boolean updateConfig(SiteConfigModel siteConfigModel);
        SiteConfigModel getConfigByPropertyName(string PropertyName);
    }
}
