using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Models;
using IDAL;

namespace DAL
{
    public class DALSiteConfig : IDALSiteConfig
    {
        public Boolean addConfig(SiteConfigModel siteConfigModel)
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();
            
            DAL.SiteConfig siteConfig = new DAL.SiteConfig();
            siteConfig.PropertyName = siteConfigModel.PropertyName;
            siteConfig.PropertyValue = siteConfigModel.PropertyValue;
            siteConfig.PropertyDescription = siteConfigModel.PropertyDescription;
            context.AddToSiteConfigs(siteConfig);
            context.SaveChanges();
            return true;
        }
        public Boolean updateConfig(SiteConfigModel siteConfigModel)
        {
            geekinsidekmsEntities context = new geekinsidekmsEntities();

            var sConfig = from sc in context.SiteConfigs
                             where sc.PropertyName.Equals(siteConfigModel.PropertyName)
                             select sc;
            DAL.SiteConfig siteConfig = sConfig.First();
            siteConfig.PropertyName = siteConfigModel.PropertyName;
            siteConfig.PropertyValue = siteConfigModel.PropertyValue;
            siteConfig.PropertyDescription = siteConfigModel.PropertyDescription;
            context.SaveChanges();
            return true;
        }
        public SiteConfigModel getConfigByPropertyName(string PropertyName)
        {
            using (var gikms = new geekinsidekmsEntities())
            {
                var siteConfig = from sc in gikms.SiteConfigs
                                where sc.PropertyName.Equals(PropertyName)
                                select sc;
                SiteConfigModel result;
                try
                {
                    result = new SiteConfigModel(siteConfig.First().PropertyName, siteConfig.First().PropertyValue, siteConfig.First().PropertyDescription);
                }
                catch (System.Exception ex)
                {
                    result = new SiteConfigModel("", "", ""); 
                }
                return result;
            }
        }
    }
}
