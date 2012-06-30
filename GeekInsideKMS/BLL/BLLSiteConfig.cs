using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;

namespace BLL
{
    public class BLLSiteConfig
    {
        IDALSiteConfig siteConfigDAL = DALFactory.DataAccess.CreateSiteConfiguraionDAL();

        public Boolean saveSiteConfig(SiteConfigModel siteConfigModel)
        {
            SiteConfigModel siteConfigModelFromDb = siteConfigDAL.getConfigByPropertyName(siteConfigModel.PropertyName);
            if (siteConfigModelFromDb.PropertyValue == "")
            {
                return siteConfigDAL.addConfig(siteConfigModel);
            } 
            else if ( siteConfigModelFromDb.PropertyValue != siteConfigModel.PropertyValue)
            {
                return siteConfigDAL.updateConfig(siteConfigModel);
            }
            else
            {
                return true;
            }
        }

        public SiteConfigModel getSiteConfigByPropertyName(string propertyname)
        {
            return siteConfigDAL.getConfigByPropertyName(propertyname);
        }

    }
}
