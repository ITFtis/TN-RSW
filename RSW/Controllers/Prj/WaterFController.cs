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
    [MenuDef(Id = "WaterF", Name = "水情預報", MenuPath = "淹水預警系統", Index = 5)]
    public class WaterFController : NoModelController
    {
        // GET: WaterF
        public ActionResult Index()
        {
            return View();
        }
    }
}