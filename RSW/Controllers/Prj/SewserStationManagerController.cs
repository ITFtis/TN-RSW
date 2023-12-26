using Dou.Controllers;
using Dou.Misc.Attr;
using RSW.Controllers.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSW.Controllers.Prj
{
    //[RSW2HtmlIFrameMenuDef(Id = "SewserStationManager", Name = "下水道監測站管理", MenuPath = "即時監控資訊", Index = 99,  Url = "Station")]
    public class SewserStationManagerController : HtmlIFrameController
    {
        // GET: RSW2Statsion
        public ActionResult Index()
        {
            return View();
        }
    }
}