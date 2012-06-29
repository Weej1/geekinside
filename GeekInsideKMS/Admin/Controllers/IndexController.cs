using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Model.Models;
using BLL;

namespace Admin.Controllers
{
    public class IndexController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewData["sitename"] = new BLLSiteConfig().getSiteConfigByPropertyName("sitename").PropertyValue;
            ViewData["smtpaddress"] = new BLLSiteConfig().getSiteConfigByPropertyName("smtpaddress").PropertyValue;
            ViewData["smtpusername"] = new BLLSiteConfig().getSiteConfigByPropertyName("smtpusername").PropertyValue;
            ViewData["smtppassword"] = new BLLSiteConfig().getSiteConfigByPropertyName("smtppassword").PropertyValue;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult doIndex()
        {
            SiteConfigModel[] siteConfigModel = new SiteConfigModel[4];
            siteConfigModel[0] = new SiteConfigModel("sitename", Request.Form["sitename"], "sitename");
            siteConfigModel[1] = new SiteConfigModel("smtpaddress", Request.Form["smtpaddress"], "smtpaddress");
            siteConfigModel[2] = new SiteConfigModel("smtpusername", Request.Form["smtpusername"], "smtpusername");
            siteConfigModel[3] = new SiteConfigModel("smtppassword", Request.Form["smtppassword"], "smtppassword");
            
            //站点名称不能为空，其它皆可为空
            if (siteConfigModel[0].PropertyValue == "")
            {
                TempData["errorMsg"] = "站点名称不能为空！";
                return RedirectToAction("Index", "Index");
            }
            BLLSiteConfig bllSiteConfig = new BLLSiteConfig();
            Boolean result = true;
            for (int i = 0; i < 4; i++ )
            {
                if (!bllSiteConfig.saveSiteConfig(siteConfigModel[i]))
                {
                    result = false;
                }
            }

            if (result == true)
            {
                TempData["successMsg"] = "保存成功！";
                return RedirectToAction("Index", "Index");
            }
            else
            {
                TempData["errorMsg"] = "保存失败，请重试。";
                return RedirectToAction("Index", "Index");
            }
        }

    }
}
