using Antlr.Runtime.Misc;
using Dou.Misc.Attr;
using RSW.Controllers.Comm;
using RSW.Models.Data;
using RSW.Services;
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
            List<WaterLevelPrediction> data = DataService.GetData<WaterLevelPrediction>("SELECT * FROM WaterLevelPrediction WHERE predict_mx is not null", new object[0]).ToList();
            ViewBag.stations = data;

            return View();
        }

        public ActionResult List()
        {
            return View();
        }
    }
}