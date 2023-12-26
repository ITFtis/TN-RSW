using Dou.Misc.Attr;
using RSW.Controllers.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSW.Controllers.Prj
{
    [MenuDef(Id = "RtHydro", Name = "測站即時監控資訊", MenuPath = "即時監控資訊", Index = 1, Action = "Index", AllowAnonymous = false, Func = FuncEnum.None)]
    public class RtHydroController : NoModelController
    {
        // GET: RtHydro
        public ActionResult Index()
        {
            ViewBag.HasGis = true;
            return View();
        }
    }
}