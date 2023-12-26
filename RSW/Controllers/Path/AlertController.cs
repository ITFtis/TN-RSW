using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSW.Controllers.Path
{
    [Dou.Misc.Attr.MenuDef(Name = "淹水預警系統", Index = 99, IsOnlyPath = true)]
    public class AlertController : Controller
    {
        // GET: Alert
        public ActionResult Index()
        {
            return View();
        }
    }
}