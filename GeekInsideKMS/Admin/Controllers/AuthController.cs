﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Admin.Controllers
{
    public class AuthController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
