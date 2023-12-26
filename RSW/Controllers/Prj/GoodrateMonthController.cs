using Dou.Controllers;
using RSW.Controllers.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace RSW.Controllers.Prj
{
    //[RSW2HtmlIFrameMenuDef(Id = "SewerGoodrateMonth", Name = "感測器妥善率月報表", MenuPath = "即時監控資訊", Index = 103, Url = "ReliableMonth")]
    public class GoodrateMonthController : HtmlIFrameController
    {
        // GET: GoodrateMonth
        public ActionResult Index()
        {
            return View();
        }
    }
}