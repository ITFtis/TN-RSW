﻿using Antlr.Runtime.Misc;
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
    [MenuDef(Id = "RainData", Name = "雨量監測與預報", MenuPath = "淹水預警系統", Index = 3)]
    public class RainDataController : NoModelController
    {
        // GET: RainData
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Stations()
        {
            var data = DataService.GetData<BasicStt>(DataService.DB_TN, "SELECT * FROM [BasicStt]");
            return Json(data.Select(x=>new { x.stt_name, x.addr, x.lon, x.lat }).ToList());
        }
    }
}