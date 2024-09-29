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
            var dt = DateTime.Now;
            dt = dt.AddMinutes(-dt.Minute % 10);
            var list = new List<string>();
            foreach (var i in new int[] { -10, -20, -30, -40, -50, -60, -70, -80, -90, -100, -110, -120 })
            {
                list.Add(dt.AddMinutes(i).ToString("yyyy/MM/dd HH:mm"));
            }
            ViewBag.qpesums = list;
            return View();
        }
    }
}