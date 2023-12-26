using Dou.Controllers;
using Dou.Misc.Attr;
using Dou.Models.DB;
using RSW.Controllers.Comm;
using RSW.Models;
using RSW.Models.Data;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSW.Controllers.Prj
{
    [MenuDef(Id = "ReliableDay", Name = "感測器妥善率日報表", MenuPath = "即時監控資訊", Action = "Index", Index = 103, Func = FuncEnum.ALL, AllowAnonymous = false)]
    public class ReliableDayController : Dou.Controllers.AGenericModelController<v_RealTimeReliable_Day>
    {
        public static bool initialtype = false;
        // GET: Country
        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable<v_RealTimeReliable_Day> GetDataDBObject(IModelEntity<v_RealTimeReliable_Day> dbEntity, params KeyValueParams[] paras)
        {
            var countycode = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "county_code");
            var stt_no = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "stt_no");
            var manuf = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "manufacturer");
            var strYear = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "Year");
            var strMonth = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "Month");

            //20230814, add by markhong 初始沒有篩選條件時不顯示資料
            if (countycode == null && stt_no == null && manuf == null && strYear == null && strMonth == null)
                return base.GetDataDBObject(dbEntity, paras).Take(0);
            return base.GetDataDBObject(dbEntity, paras);
        }

        /// <summary>
        /// 取得所有篩選資料
        /// </summary>
        /// <param name="td"></param>
        /// <returns></returns>
        public ActionResult GDataDD(Para1 td)
        {
            using (var cxt = TNModelContext.CreateTNModelContext())
            {
                //取BasicSttDev資料與篩選
                var alldata = cxt.v_RealTimeReliable_Day.Where
                    (x => (string.IsNullOrEmpty(td.county_code) || x.county_code == td.county_code)
                    && (string.IsNullOrEmpty(td.stt_no) || x.stt_no == td.stt_no)
                    && (string.IsNullOrEmpty(td.manuf) || x.manufacturer == td.manuf)
                    && (string.IsNullOrEmpty(td.Year) || x.Year.ToString() == td.Year)
                    && (string.IsNullOrEmpty(td.Month) || x.Month.ToString() == td.Month)
                    ).ToArray();

                //20230814, add by markhong 沒有資料就不顯示統計資料
                if (alldata.Count() == 0 ||
                    (string.IsNullOrEmpty(td.county_code) && string.IsNullOrEmpty(td.stt_no) &&
                    string.IsNullOrEmpty(td.manuf) && string.IsNullOrEmpty(td.Year) && string.IsNullOrEmpty(td.Month)))
                    return Json(new List<string> { "", "", "" });

                //計算三率
                double? d1 = 0;
                double? d2 = 0;
                double? d3 = 0;
                int tdCount = alldata.Count();
                foreach (var item in alldata)
                {
                    d1 += item.UpdateRate_DD;
                    d2 += item.RealTimeRate_DD;
                    d3 += item.ReliableRate_DD;
                }

                var avgd1 = Math.Round((double)d1 / tdCount, 2, MidpointRounding.AwayFromZero).ToString();
                var avgd2 = Math.Round((double)d2 / tdCount, 2, MidpointRounding.AwayFromZero).ToString();
                var avgd3 = Math.Round((double)d3 / tdCount, 2, MidpointRounding.AwayFromZero).ToString();

                if (avgd1 == "非數值") avgd1 = "";
                if (avgd2 == "非數值") avgd2 = "";
                if (avgd3 == "非數值") avgd3 = "";

                //20230814, add by markhong 沒有資料就不顯示統計資料
                return Json(new List<string> { avgd1, avgd2, avgd3 });
            }
        }

        protected override IModelEntity<v_RealTimeReliable_Day> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<v_RealTimeReliable_Day>(new TNModelContext());
        }
    }
    public class Para1
    {
        public string county_code { get; set; }
        public string stt_no { get; set; }
        public string manuf { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
    }
}