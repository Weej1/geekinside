using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using DAL;
using IDAL;

namespace BLL
{
    public class BLLSiteConfig
    {
        public Boolean saveSiteConfig(SiteConfigModel siteConfigModel)
        {
            DALSiteConfig dalSiteConfig = new DALSiteConfig();
            SiteConfigModel siteConfigModelFromDb = dalSiteConfig.getConfigByPropertyName(siteConfigModel.PropertyName);
            if (siteConfigModelFromDb.PropertyValue == "")
            {
                return dalSiteConfig.addConfig(siteConfigModel);
            } 
            else if ( siteConfigModelFromDb.PropertyValue != siteConfigModel.PropertyValue)
            {
                return dalSiteConfig.updateConfig(siteConfigModel);
            }
            else
            {
                return true;
            }
        }

        public SiteConfigModel getSiteConfigByPropertyName(string propertyname)
        {
            DALSiteConfig dalSiteConfig = new DALSiteConfig();
            return dalSiteConfig.getConfigByPropertyName(propertyname);
        }

    }
}
