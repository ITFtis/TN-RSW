using Antlr.Runtime.Misc;
using Dou.Misc.Attr;
using RSW.Controllers.Comm;
using RSW.Models.Data;
using RSW.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
            var data = DataService.GetData<WaterLevelPrediction>(DataService.DB_DOU, "SELECT * FROM [WaterLevelPrediction]");
            return Json(data.Select(x => new { x.stt_name, x.dev_id, x.rain_st_name, x.predict_mx, x.predict_dy }).ToList());
        }

        public ActionResult StationData(string id)
        {
            var now = DateTime.Now;
            var dt_hour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            var prediction = DataService.GetData<WaterLevelPrediction>(DataService.DB_DOU,
                "SELECT * FROM [WaterLevelPrediction] WHERE dev_id=@dev_id",
                new SqlParameter[] {
                    new SqlParameter("@dev_id", id)
                }).FirstOrDefault();
            if (prediction == null) return HttpNotFound();
            //            var rain_rt = new Api.DataController().TainanApi("Rain", "Infos","Accumulation","Realtime");
            var cwa_rain_prediction = DataService.GetData<CWARainStPredict>(DataService.DB_DOU,
                "SELECT TOP 1 * FROM [CWA_RainStPredict] WHERE rain_st_id=@rain_st_id ORDER BY predict_datetime DESC",
                new SqlParameter[] {
                    new SqlParameter("@rain_st_id", prediction.rain_st)
                }).FirstOrDefault();
            var rain_prediction = new List<(string, decimal)>();
            if (cwa_rain_prediction != null)
            {
                var type = cwa_rain_prediction.GetType();
                var cwa_dt = cwa_rain_prediction.predict_datetime;
                for (int h = 0; h <= 12; h++)
                {
                    var a = cwa_dt.AddHours(h).ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
                    var b = type.GetProperty("h" + h).GetValue(cwa_rain_prediction) as decimal?;
                    rain_prediction.Add((a, b ?? 0));
                }
            }
            return Json(new
            {
                prediction,
                rain_prediction
            });
        }
    }
}