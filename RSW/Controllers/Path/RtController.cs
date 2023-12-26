using Antlr.Runtime.Misc;
using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace RSW.Controllers.Path
{
    [MenuDef(Name = "即時監控資訊", Index = 1,IsOnlyPath =true)]
    public class RtController : Controller
    {
        // GET: Rt
        public ActionResult Index()
        {
            return View();
        }
    }
}