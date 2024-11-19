using Dou.Controllers;
using Dou.Misc;
using Dou.Misc.Attr;
using Dou.Models.DB;
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
    [MenuDef(Id = "ErrorMonthlyReport", Name = "異常統計月報表", MenuPath = "即時監控資訊", Action = "Index", Index = 105, Func = FuncEnum.ALL, AllowAnonymous = false)]
    public class ErrorMonthlyReportController : Dou.Controllers.AGenericModelController<v_ErrorReport_Monthly>
    {
        public static bool initialtype = false;
        // GET: Country
        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable<v_ErrorReport_Monthly> GetDataDBObject(IModelEntity<v_ErrorReport_Monthly> dbEntity, params KeyValueParams[] paras)
        {
            //20230814, add by markhong 初始沒有篩選條件時不顯示資料
            var countycode = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "county_code");
            var manuf = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "manufacturer");
            var strYear = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "Year");
            var strMonth = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "Month");

            //20230814, add by markhong 初始沒有篩選條件時不顯示資料
            if (countycode == null && manuf == null && strYear == null && strMonth == null)
                return base.GetDataDBObject(dbEntity, paras).Take(0);
            return base.GetDataDBObject(dbEntity, paras);
        }

        /// <summary>
        /// 取得所有篩選資料
        /// </summary>
        /// <param name="td"></param>
        /// <returns></returns>
        public ActionResult GDataMM(Para1 td)
        {
            using (var cxt = TNModelContext.CreateTNModelContext())
            {
                //取BasicSttDev資料與篩選
                var alldata = cxt.v_ErrorReport_Monthly.Where
                    (x => (string.IsNullOrEmpty(td.county_code) || x.county_code == td.county_code)
                    && (string.IsNullOrEmpty(td.manuf) || x.manufacturer == td.manuf)
                    && (string.IsNullOrEmpty(td.Year) || x.Year.ToString() == td.Year)
                    && (string.IsNullOrEmpty(td.Month) || x.Month.ToString() == td.Month)
                    ).ToArray();

                //20230814, add by markhong 沒有資料就不顯示統計資料
                if (alldata.Count() == 0 ||
                    (string.IsNullOrEmpty(td.county_code) && string.IsNullOrEmpty(td.stt_no) &&
                    string.IsNullOrEmpty(td.manuf) && string.IsNullOrEmpty(td.Year) && string.IsNullOrEmpty(td.Month)))
                    return Json(new List<string> { "", "", "" });

                //計算總計
                int? d1 = 0;
                int? d2 = 0;
                int? d3 = 0;
                int tdCount = alldata.Count();
                foreach (var item in alldata)
                {
                    d1 += item.SumAlarm1;
                    d2 += item.SumAlarm2;
                    d3 += item.SumAlarm3;
                }

                return Json(new List<string> { d1.ToString(), d2.ToString(), d3.ToString() });
            }
        }

        protected override IModelEntity<v_ErrorReport_Monthly> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<v_ErrorReport_Monthly>(new TNModelContext());
        }
    }
}