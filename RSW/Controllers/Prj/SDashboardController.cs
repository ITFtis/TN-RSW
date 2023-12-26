using Antlr.Runtime.Misc;
using Dou.Misc.Attr;
using RSW.Controllers.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace RSW.Controllers.Prj
{
    [MenuDef(Id = "SDashboard", Name = "雨水下水道水位站多元展示", MenuPath = "即時監控資訊", Index = 9, Action = "Index", AllowAnonymous = false, Func = FuncEnum.None)]
    public class SDashboardController : NoModelController
    {
        // GET: SDashboard
        public ActionResult Index()
        {
            ViewBag.HasGis = true;
            return View();
        }
    }
}