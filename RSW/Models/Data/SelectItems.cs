using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RSW.Models.Data
{
    /// <summary>
    /// 下拉選單-廠商
    /// </summary>
    public class ManufacturerSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "RSW.Models.Data.ManufacturerSelectItemsClassImp, RSW";

        protected static IEnumerable<BasicSttDev> _manufs;
        protected static new IEnumerable<BasicSttDev> MANUFS
        {
            get
            {
                _manufs = DouHelper.Misc.GetCache<IEnumerable<BasicSttDev>>(2 * 60 * 1000, AssemblyQualifiedName);
                if (_manufs == null)
                {
                    _manufs = TNModelContext.GetAllBasicSttDev().Where(s=>s.manufacturer != null);
         
                    DouHelper.Misc.AddCache(_manufs, AssemblyQualifiedName);
                }
                return _manufs;
            }
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return MANUFS.Select(s => new KeyValuePair<string, object>(s.manufacturer, s.manufacturer));
        }
    }
    /// <summary>
    /// 下拉選單-測站名稱
    /// </summary>
    public class NameSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "RSW.Models.Data.NameSelectItemsClassImp, RSW";

        protected static IEnumerable<BasicStt> _stts;
        protected static new IEnumerable<BasicStt> STTS
        {
            get
            {
                _stts = DouHelper.Misc.GetCache<IEnumerable<BasicStt>>(2 * 60 * 1000, AssemblyQualifiedName);
                if (_stts == null)
                {
                    _stts = TNModelContext.GetAllBasicStt();// && s.Fno=="F01721");
                    DouHelper.Misc.AddCache(_stts, AssemblyQualifiedName);
                }
                return _stts;
            }
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var result = STTS.Select(s => new KeyValuePair<string, object>(s.stt_no, 
                JsonConvert.SerializeObject(new { v = s.stt_name, dev_id = s.dev_id, county_code = s.county_code })));
            return result;
        }
    }
    /// <summary>
    /// 下拉選單-行政區
    /// </summary>
    public class CountySelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "RSW.Models.Data.CountySelectItemsClassImp, RSW";

        protected static IEnumerable<BasicSttCounty> _countys;
        protected static new IEnumerable<BasicSttCounty> COUNTYS
        {
            get
            {
                _countys = DouHelper.Misc.GetCache<IEnumerable<BasicSttCounty>>(2 * 60 * 1000, AssemblyQualifiedName);
                if (_countys == null)
                {
                    _countys = TNModelContext.GetAllBasicSttCounty();// && s.Fno=="F01721");
                    DouHelper.Misc.AddCache(_countys, AssemblyQualifiedName);
                }
                return _countys;
            }
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return COUNTYS.Select(s => new KeyValuePair<string, object>(s.county_code, s.county_name));
        }
    }
    /// <summary>
    /// 下拉選單-年度
    /// </summary>
    public class YearSelectAllItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "RSW.Models.Data.YearSelectAllItemsClassImp, RSW";

        protected static IEnumerable<RealTimeReliable> _rtrs;
        protected static new IEnumerable<RealTimeReliable> RTRS
        {
            get
            {
                _rtrs = DouHelper.Misc.GetCache<IEnumerable<RealTimeReliable>>(2 * 60 * 1000, AssemblyQualifiedName);
                if (_rtrs == null)
                {
                    _rtrs = TNModelContext.GetAllRealTimeReliable();// && s.Fno=="F01721");
                    DouHelper.Misc.AddCache(_rtrs, AssemblyQualifiedName);
                }
                return _rtrs;
            }
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return RTRS.Select(s => new KeyValuePair<string, object>(s.Year.ToString(),s.Year));
        }
    } 
    /// <summary>
    /// 下拉選單-設備代號
    /// </summary>
    public class DevSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "RSW.Models.Data.DevSelectItemsClassImp, RSW";

        protected static IEnumerable<BasicSttDev> _devs;
        protected static new IEnumerable<BasicSttDev> DEVS
        {
            get
            {
                _devs = DouHelper.Misc.GetCache<IEnumerable<BasicSttDev>>(2 * 60 * 1000, AssemblyQualifiedName);
                if (_devs == null)
                {
                    _devs = TNModelContext.GetAllBasicSttDev();

                    DouHelper.Misc.AddCache(_devs, AssemblyQualifiedName);
                }
                return _devs;
            }
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return DEVS.Select(s => new KeyValuePair<string, object>(s.dev_id, s.dev_id));
        }
    }

    public class Manuf
    {
        public string ManufId { set; get; }
        public string ManufName { set; get; }
    }
}
