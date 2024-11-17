using Antlr.Runtime.Misc;
using Dou.Misc.Attr;
using RSW.Controllers.Comm;
using RSW.Models.Data;
using RSW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using RSW.Services;

namespace RSW.Controllers.Prj
{
    [MenuDef(Id = "SHistory", Name = "災害事件多元展示", MenuPath = "即時監控資訊", Index = 150, Action = "Index", AllowAnonymous = false, Func = FuncEnum.None)]
    public class SHistoryController : NoModelController
    {
        // GET: SDashboard
        public ActionResult Index()
        {
            ViewBag.HasGis = true;
            ViewBag.Disaster = (new DouModelContextExt()).Disaster.OrderByDescending(x=>x.SDate).ToList();
            return View();
        }

        public ActionResult GetAlarmList(int id)
        {
            var d = (new DouModelContextExt()).Disaster.Where(x => x.Id == id).FirstOrDefault();
            if (d != null)
            {
                var alarm1 = DataService.GetData<RealTimeStt>(DataService.DB_TN,
                    "SELECT * FROM [RealTimeStt] WHERE datatime BETWEEN @D1 AND @D2 AND val61 > alarm1",
                    new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter ("@D1", d.SDate),
                        new System.Data.SqlClient.SqlParameter ("@D2", d.EDate.AddDays(1))
                    });
                var alarm2 = DataService.GetData<RealTimeStt>(DataService.DB_TN,
                    "SELECT * FROM [RealTimeStt] WHERE datatime BETWEEN @D1 AND @D2 AND val61 > alarm2",
                    new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter ("@D1", d.SDate),
                        new System.Data.SqlClient.SqlParameter ("@D2", d.EDate.AddDays(1))
                    });
                var alarm3 = DataService.GetData<RealTimeStt>(DataService.DB_TN,
                    "SELECT * FROM [RealTimeStt] WHERE datatime BETWEEN @D1 AND @D2 AND val61 > alarm2",
                    new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter ("@D1", d.SDate),
                        new System.Data.SqlClient.SqlParameter ("@D2", d.EDate.AddDays(1))
                    });
                return Json(new { alarm1, alarm2, alarm3 }, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }
    }
}