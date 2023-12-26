using Dou.Misc.Attr;
using RSW.Controllers.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSW.Controllers.Prj
{
    [MenuDef(Id = "DDashboard", Name = "即時災情儀表板", MenuPath = "即時監控資訊", Index = 3, Action = "Index", AllowAnonymous = false, Func = FuncEnum.None)]
    public class DDashboardController : NoModelController
    {
        // GET: DDashboard
        public ActionResult Index()
        {
            ViewBag.HasGis = true;
            return View();
        }
    }
}