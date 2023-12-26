using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Configuration;
using System.Reflection;
using System.Collections.Generic;
using RSW.Models.Data;
using RSW.Models.Manager;

namespace RSW.Models
{
    public class TNModelContext : DbContext
    {
        //public new static FtisT8ModelContext CreateT8(bool printLog = false)
        //{
        //    var cxt = new FtisT8ModelContext();
        //    if (printLog) cxt.Database.Log = (log) => Debug.WriteLine(log);
        //    return cxt;
        //}
        public static TNModelContext CreateTNModelContext(bool printLog = false)
        {           
            var cxt = new TNModelContext();
            if (printLog) cxt.Database.Log = (log) => Debug.WriteLine(log);
            return cxt;
        }

        // 您的內容已設定為使用應用程式組態檔 (App.config 或 Web.config)
        // 中的 'FtisModelContext' 連接字串。根據預設，這個連接字串的目標是
        // 您的 LocalDb 執行個體上的 'FtisHelperAsset.DB.FtisModelContext' 資料庫。
        // 
        // 如果您的目標是其他資料庫和 (或) 提供者，請修改
        // 應用程式組態檔中的 'FtisModelContext' 連接字串。
        public TNModelContext()
            : base("name=TNModelContext")
        {
            Database.SetInitializer<TNModelContext>(null);
        }

        // 針對您要包含在模型中的每種實體類型新增 DbSet。如需有關設定和使用
        // Code First 模型的詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=390109。
        public virtual DbSet<BasicStt> BasicStt { get; set; }
        public virtual DbSet<BasicSttDev> BasicSttDev { get; set; }
        public virtual DbSet<RealTimeStt> RealTimeStt { get; set; }
        public virtual DbSet<RealTimeReliable> RealTimeReliable { get; set; }
        public virtual DbSet<v_RealTimeReliable_Month> v_RealTimeReliable_Month { get; set; }
        public virtual DbSet<v_RealTimeReliable_Day> v_RealTimeReliable_Day { get; set; }
        public virtual DbSet<BasicSttCounty> BasicSttCounty { get; set; }
        public virtual DbSet<RemoteCtrlAttribute> RemoteCtrlAttribute { get; set; }
        public virtual DbSet<RealTimeStt5> RealTimeStt5 { get; set; }
        public virtual DbSet<InspectionData> InspectionData { get; set; }
        public virtual DbSet<TroubleShootingData> TroubleShootingData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        static object lockGetDevs = new object();
        static object lockGetAllDevs = new object();
        static object lockGetRealTimeSttDev = new object();
        static object lockGetAllStts = new object();
        static object lockGetAllCountys = new object();
        static object lockGetAllReliables = new object();
        static object lockGetAllRCAs = new object();
        static object lockGetRealTimeStt = new object();
        static object lockGetAllvReliableDD = new object();
        static object lockGetAllvReliableMM = new object();

        /// <summary>
        /// 取得所有基本設備
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Data.BasicSttDev> GetAllBasicSttDev()
        {
            int cachetimer = 5 * 60 * 1000;
            string key = "RSW.Models.Data.BasicSttDev";
            var allDevs = DouHelper.Misc.GetCache<IEnumerable<BasicSttDev>>(cachetimer, key);
            lock (lockGetAllDevs)
            {
                if (allDevs == null)
                {
                    using (var cxt = CreateTNModelContext())
                    {
                        allDevs = cxt.BasicSttDev.ToArray();
                        DouHelper.Misc.AddCache(allDevs, key);
                    }
                }
            }
            return allDevs;
        }
        public static void ResetGetAllSttDev()
        {
            string key = "RSW.Models.Data.BasicSttDev";
            DouHelper.Misc.ClearCache(key);
        }

        /// <summary>
        /// 取得單一基本設備
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<BasicSttDev> GetBasicSttDev(string devid)
        {
            int cachetimer = 5 * 60 * 1000;
            string key = "RSW.Models.Data.BasicSttDev";
            var allDevs = DouHelper.Misc.GetCache<IEnumerable<BasicSttDev>>(cachetimer, key);
            lock (lockGetDevs)
            {
                if (allDevs == null)
                {
                    using (var cxt = CreateTNModelContext())
                    {
                        allDevs = cxt.BasicSttDev;
                        DouHelper.Misc.AddCache(allDevs, key);
                    }
                }
            }
            return allDevs.Where(x => x.dev_id == devid).ToArray();
        }
        public static void ResetGetSttDev()
        {
            string key = "RSW.Models.Data.BasicSttDev";
            DouHelper.Misc.ClearCache(key);
        }
        /// <summary>
        /// 取得所有測站
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<BasicStt> GetAllBasicStt()
        {
            int cachetimer = 5 * 60 * 1000;
            string key = "RSW.Models.Data.BasicStt";
            var allStts = DouHelper.Misc.GetCache<IEnumerable<BasicStt>>(cachetimer, key);
            lock (lockGetAllStts)
            {
                if (allStts == null)
                {
                    using (var cxt = CreateTNModelContext())
                    {
                        allStts = cxt.BasicStt.ToArray();
                        DouHelper.Misc.AddCache(allStts, key);
                    }
                }
            }
            return allStts;
        }
        public static void ResetGetStt()
        {
            string key = "RSW.Models.Data.BasicStt";
            DouHelper.Misc.ClearCache(key);
        }
        /// <summary>
        /// 取得即時設備資料
        /// </summary>
        /// <param name="devid"></param>
        /// <returns></returns>
        public static RealTimeStt GetRealTimeSttDev(string devid)
        {
            int cachetimer = 5 * 60 * 1000;
            if (string.IsNullOrEmpty(devid))
                return null;
            string key = "RSW.Models.Data.RealTimeStt";
            var Dev = DouHelper.Misc.GetCache<IEnumerable<RealTimeStt>>(cachetimer, key);
            lock (lockGetRealTimeSttDev)
            {
                if (Dev == null)
                {
                    using (var cxt = CreateTNModelContext())
                    {
                        Dev = cxt.RealTimeStt.Where(m => m.dev_id == devid).OrderByDescending(x => x.inserttime).ToArray();
                        DouHelper.Misc.AddCache(Dev, key);
                    }
                }
            }
            return Dev.FirstOrDefault();
        }
        public static void ResetGetRealTimeSttDev()
        {
            string key = "RSW.Models.Data.RealTimeStt";
            DouHelper.Misc.ClearCache(key);
        }
        /// <summary>
        /// 取得即時設備資料
        /// </summary>
        /// <param name="sttno"></param>
        /// <returns></returns>
        public static IEnumerable<RealTimeStt5> GetRealTimeStt5()
        {
            int cachetimer = 5 * 60 * 1000;
            string key = "RSW.Models.Data.RealTimeStt5";
            var Dev = DouHelper.Misc.GetCache<IEnumerable<RealTimeStt5>>(cachetimer, key);
            lock (lockGetRealTimeStt)
            {
                if (Dev == null)
                {
                    using (var db = new TNModelContext())
                    {
                        //var rs1 = (from b in db.RealTimeStt.AsEnumerable()
                        //          group b by b.dev_id into g
                        //          select new { dev_id = g.Key, inserttime = g.Max(x => x.inserttime) }).ToArray();
                        var t2 = (from a in db.RealTimeStt
                                  join d in (from b in db.RealTimeStt
                                             group b by b.dev_id into g
                                             select new { dev_id = g.Key, inserttime = g.Max(x => x.inserttime) })
                                  on new { a.dev_id, a.inserttime } equals new { d.dev_id, d.inserttime }
                                  where a.dev_id.Substring(0, 2) == "SW" || a.dev_id.Substring(0, 2) == "FL"
                                  select a);
                        Dev = (from c in t2.AsEnumerable()
                               join d in db.BasicSttDev
                               on c.dev_id equals d.dev_id
                                    orderby c.dev_id
                                    select new RealTimeStt5
                                    {
                                        stt_no = d.stt_no,
                                        dev_id = c.dev_id,
                                        datatime= c.datatime,
                                        val02 = c.val02,
                                        val61 = c.val61,
                                        voltage = c.voltage,
                                        //wdepth = d.culvert_depth,
                                        wdepth = d.base_elev, //20231030, edit by markhong 修改來源
                                        rssi = c.rssi,
                                        level = c.val61 == null ? reCalVal02(c.dev_id, c.val02) : c.val61,
                                        alarm2= d.alarm2, //20230911, edit by markhong 改回從BasicSttDev抓值
                                        alarm3 = d.alarm3, //20230911, edit by markhong 改回從BasicSttDev抓值
                                    }).ToArray();
                        DouHelper.Misc.AddCache(Dev, key);
                    }
                }
            }
            return Dev;
        }
        public static void ResetGetRealTimeStt5()
        {
            string key = "RSW.Models.Data.RealTimeStt5";
            DouHelper.Misc.ClearCache(key);
        }

        /// <summary>
        /// 壓力式水位要特別計算
        /// </summary>
        /// <param name="_devid">設備代號</param>
        /// <param name="_val">水位</param>
        /// <returns></returns>
        private static double? reCalVal02(string _devid, double? _val)
        {
            double? _baro = 0;
            using (var db = new TNModelContext())
            {
                //先用devid去[BasicSttDev]找出[stt_no]
                var vBasicSttDev = (from c in db.BasicSttDev
                                    where c.dev_id == _devid
                                    select c).FirstOrDefault();
                if (vBasicSttDev == null) return null;
                var vsttno = vBasicSttDev.stt_no.ToString();
                //再用sttno去[BasicStt]找出[ref_dev_id]
                if (vsttno != "")
                {
                    var vBasicStt = (from c in db.BasicStt
                                     where c.stt_no == vsttno
                                     select c).FirstOrDefault();
                    var vrefdevid = vBasicStt.ref_dev_id.ToString();
                    //最後用refdevid去[RealTimeStt]找出[baro]，寫入時間排序取最近一筆資料
                    if (vrefdevid != "")
                    {
                        var vRealTimeStt = (from c in db.RealTimeStt
                                            where c.dev_id == vrefdevid
                                            orderby c.inserttime descending
                                            select c).FirstOrDefault();
                        _baro = vRealTimeStt != null ? vRealTimeStt.baro == null ? 0 : vRealTimeStt.baro : _baro;
                    }
                }
            }
            double? newval02 = _val - (_baro * 10);
            return newval02;
        }
        /// <summary>
        /// 取得行政區
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<BasicSttCounty> GetAllBasicSttCounty()
        {
            int cachetimer = 5 * 60 * 1000;
            string key = "RSW.Models.Data.BasicSttCounty";
            var allCountys = DouHelper.Misc.GetCache<IEnumerable<BasicSttCounty>>(cachetimer, key);
            lock (lockGetAllCountys)
            {
                if (allCountys == null)
                {
                    using (var cxt = CreateTNModelContext())
                    {
                        allCountys = cxt.BasicSttCounty.ToArray();
                        DouHelper.Misc.AddCache(allCountys, key);
                    }
                }
            }
            return allCountys;
        }
        public static void ResetGetSttCounty()
        {
            string key = "RSW.Models.Data.BasicSttDev";
            DouHelper.Misc.ClearCache(key);
        }
        /// <summary>
        /// 取得所有妥善率
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<RealTimeReliable> GetAllRealTimeReliable()
        {
            int cachetimer = 5 * 60 * 1000;
            string key = "RSW.Models.Data.RealTimeReliable";
            var allReliables = DouHelper.Misc.GetCache<IEnumerable<RealTimeReliable>>(cachetimer, key);
            lock (lockGetAllReliables)
            {
                if (allReliables == null)
                {
                    using (var cxt = CreateTNModelContext())
                    {
                        allReliables = cxt.RealTimeReliable.ToArray();
                        DouHelper.Misc.AddCache(allReliables, key);
                    }
                }
            }
            return allReliables;
        }
        public static void ResetGetReliable()
        {
            string key = "RSW.Models.Data.RealTimeReliable";
            DouHelper.Misc.ClearCache(key);
        }

        /// <summary>
        /// 取得所有日妥善率檢視表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<v_RealTimeReliable_Day> GetAllvRealTimeReliableDD()
        {
            int cachetimer = 5 * 60 * 1000;
            string key = "RSW.Models.Data.v_RealTimeReliable_Day";
            var allReliables = DouHelper.Misc.GetCache<IEnumerable<v_RealTimeReliable_Day>>(cachetimer, key);
            lock (lockGetAllvReliableDD)
            {
                if (allReliables == null)
                {
                    using (var cxt = CreateTNModelContext())
                    {
                        allReliables = cxt.v_RealTimeReliable_Day.ToArray();
                        DouHelper.Misc.AddCache(allReliables, key);
                    }
                }
            }
            return allReliables;
        }
        public static void ResetGetvReliableDD()
        {
            string key = "RSW.Models.Data.v_RealTimeReliable_Day";
            DouHelper.Misc.ClearCache(key);
        }

        /// <summary>
        /// 取得所有月妥善率檢視表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<v_RealTimeReliable_Month> GetAllvRealTimeReliableMM()
        {
            int cachetimer = 5 * 60 * 1000;
            string key = "RSW.Models.Data.v_RealTimeReliable_Month";
            var allReliables = DouHelper.Misc.GetCache<IEnumerable<v_RealTimeReliable_Month>>(cachetimer, key);
            lock (lockGetAllvReliableMM)
            {
                if (allReliables == null)
                {
                    using (var cxt = CreateTNModelContext())
                    {
                        allReliables = cxt.v_RealTimeReliable_Month.ToArray();
                        DouHelper.Misc.AddCache(allReliables, key);
                    }
                }
            }
            return allReliables;
        }
        public static void ResetGetvReliableMM()
        {
            string key = "RSW.Models.Data.v_RealTimeReliable_Month";
            DouHelper.Misc.ClearCache(key);
        }
        /// <summary>
        /// 取得所有下控資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<RemoteCtrlAttribute> GetAllRemoteCtrlAttr()
        {
            int cachetimer = 5 * 60 * 1000;
            string key = "RSW.Models.Data.RemoteCtrlAttribute";
            var allRCAs = DouHelper.Misc.GetCache<IEnumerable<RemoteCtrlAttribute>>(cachetimer, key);
            lock (lockGetAllRCAs)
            {
                if (allRCAs == null)
                {
                    using (var cxt = CreateTNModelContext())
                    {
                        allRCAs = cxt.RemoteCtrlAttribute.ToArray();
                        DouHelper.Misc.AddCache(allRCAs, key);
                    }
                }
            }
            return allRCAs;
        }
        public static void ResetGetRemoteCtrlAttr()
        {
            string key = "RSW.Models.Data.RemoteCtrlAttribute";
            DouHelper.Misc.ClearCache(key);
        }
    }
}