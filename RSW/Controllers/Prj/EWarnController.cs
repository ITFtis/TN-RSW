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
    [MenuDef(Id = "EWarn", Name = "預警管理", MenuPath = "淹水預警系統", Index = 1, Action = "Index", AllowAnonymous = false, Func = FuncEnum.None)]
    public class EWarnController : NoModelController
    {
        // GET: EWarn
        public ActionResult Index()
        {
            ViewBag.qpesums = new List<string>
            {
                "2024/09/25 18:40",
                "2024/09/25 18:30",
                "2024/09/25 18:20",
            };
            return View();
        }

        public ActionResult List()
        {
            return View();
        }
    }
}