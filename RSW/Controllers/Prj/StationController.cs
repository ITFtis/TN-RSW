using Dou.Controllers;
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
using System.Web.UI.WebControls;

namespace RSW.Controllers.Prj
{    
    [MenuDef(Id = "Station", Name = "下水道監測站管理", MenuPath = "即時監控資訊", Action = "Index", Index = 99, Func = FuncEnum.ALL, AllowAnonymous = false)]
    public class StationController : Dou.Controllers.APaginationModelController<BasicStt>
    {
        // GET: Country
        public ActionResult Index()
        {
            return View();
        }

        protected override IQueryable<BasicStt> BeforeIQueryToPagedList(IQueryable<BasicStt> iquery, params KeyValueParams[] paras)
        {
            //分頁模式IQueryable要先轉AsEnumerable，處理完畢再轉回AsQueryable
            //var result = base.BeforeIQueryToPagedList(iquery, paras);
            IEnumerable<BasicStt> result = iquery.AsEnumerable();
            var sorts = paras.FirstOrDefault(s => s.key == "sort");
            var order = paras.FirstOrDefault(s => s.key == "order");
            if (sorts.value != null)
            {
                if (order.value.ToString() == "asc")
                {
                    if (sorts.value.ToString() == "sttDev_rssi")
                        result = result.OrderBy(s => s.sttDev_rssi);
                    else if (sorts.value.ToString() == "sttDev_voltage")
                        result = result.OrderBy(s => s.sttDev_voltage);
                    else if (sorts.value.ToString() == "sttDev_level")
                        result = result.OrderBy(s => s.sttDev_level);
                    else if (sorts.value.ToString() == "sttDev_wdepth")
                        result = result.OrderBy(s => s.sttDev_wdepth);
                    else if (sorts.value.ToString() == "sttDev_alarm3")
                        result = result.OrderBy(s => s.sttDev_alarm3);
                    else if (sorts.value.ToString() == "sttDev_alarm2")
                        result = result.OrderBy(s => s.sttDev_alarm2);
                    else if (sorts.value.ToString() == "sttDatetime")
                        result = result.OrderBy(s => s.sttDatetime);
                }
                else
                {
                    if (sorts.value.ToString() == "sttDev_rssi")
                        result = result.OrderByDescending(s => s.sttDev_rssi);
                    else if (sorts.value.ToString() == "sttDev_voltage")
                        result = result.OrderByDescending(s => s.sttDev_voltage);
                    else if (sorts.value.ToString() == "sttDev_level")
                        result = result.OrderByDescending(s => s.sttDev_level);
                    else if (sorts.value.ToString() == "sttDev_wdepth")
                        result = result.OrderByDescending(s => s.sttDev_wdepth);
                    else if (sorts.value.ToString() == "sttDev_alarm3")
                        result = result.OrderByDescending(s => s.sttDev_alarm3);
                    else if (sorts.value.ToString() == "sttDev_alarm2")
                        result = result.OrderByDescending(s => s.sttDev_alarm2);
                    else if (sorts.value.ToString() == "sttDatetime")
                        result = result.OrderByDescending(s => s.sttDatetime);
                }
            }
            TNModelContext.ResetGetRealTimeSttDev();
            TNModelContext.ResetGetRealTimeStt5();
            return result.AsQueryable();
        }

        /// <summary>
        /// 取得所有設備基本資料，提供下控設定用
        /// </summary>
        /// <param name="td"></param>
        /// <returns></returns>
        public ActionResult GData1(tmpdata td)
        {
            var devid = td.dev_id.ToString();
            using (var cxt = TNModelContext.CreateTNModelContext())
            {
                var me = new Dou.Models.DB.ModelEntity<BasicSttDev>(cxt).GetAll();
                var _dev = me.Where(m => m.dev_id == devid).FirstOrDefault();
                //20230809, add by markhong 暫時寫入BsDev
                var base_elev = _dev.base_elev.ToString();
                var alarm1 = _dev.alarm1.ToString();
                var alarm2 = _dev.alarm2.ToString();
                var alarm3 = _dev.alarm3.ToString();
                return Json(new List<string> { base_elev, alarm1, alarm2, alarm3 });
            }
        }

        /// <summary>
        /// 儲存下控設定
        /// </summary>
        /// <param name="td"></param>
        /// <returns></returns>
        public ActionResult SaveData(RemoteCtrlAttribute td)
        {
            var devid = td.dev_id.ToString();
            //取BasicAttr資料
            using (var cxt = TNModelContext.CreateTNModelContext())
            {
                var me = new Dou.Models.DB.ModelEntity<RemoteCtrlAttribute>(cxt);
                //檢測資料若存在則更新，否則新增
                if (me.FirstOrDefault(s => s.dev_id == td.dev_id) == null)
                    me.Add(td);
                else
                    me.Update(td);
            }
            return Json("success");
        }

        /// <summary>
        /// 儲存下控設定(Dev)
        /// </summary>
        /// <param name="td"></param>
        /// <returns></returns>
        public ActionResult SaveData_Dev(BasicSttDev td)
        {
            //取BasicAttr資料
            using (var cxt = TNModelContext.CreateTNModelContext())
            {
                var me = new Dou.Models.DB.ModelEntity<BasicSttDev>(cxt);
                var me2 = me.FirstOrDefault(s => s.dev_id == td.dev_id);
                var utd = td;
                utd.dev_id = me2.dev_id;
                utd.stt_no = me2.stt_no;
                utd.lon = me2.lon;
                utd.lat = me2.lat;
                utd.dev_purpose = me2.dev_purpose;
                utd.manufacturer = me2.manufacturer;
                utd.dev_model = me2.dev_model;
                utd.depoist_date = me2.depoist_date;
                utd.trans_method = me2.trans_method;
                utd.ip = me2.ip;
                utd.power = me2.power;
                utd.sampling_period = me2.sampling_period;
                utd.measure_period = me2.measure_period;
                utd.upload_period = me2.upload_period;
                utd.base_elev = td.base_elev;
                utd.culvert_depth = me2.culvert_depth;
                utd.dev_depth1 = me2.dev_depth1;
                utd.dev_depth2 = me2.dev_depth2;
                utd.alarm1 = td.alarm1;
                utd.alarm2 = td.alarm2;
                utd.alarm3 = td.alarm3;
                utd.desc = me2.desc;
  
                me.Update(utd);
            }
            return Json("success");
        }
        protected override IModelEntity<BasicStt> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<BasicStt>(new TNModelContext());
        }
    }

    public class tmpdata
    {
        public string dev_id { get; set; }
    }
}