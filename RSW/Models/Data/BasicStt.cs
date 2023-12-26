namespace RSW.Models.Data
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    //using static MongoDB.Driver.WriteConcern;

    [Table("BasicStt")]
    public partial class BasicStt
    {
        //[ColumnDef(Display = "設備代號", EditType = EditType.Select, SelectGearingWith = "stt_no,dev_id"
        //    , SelectItemsClassNamespace = DevSelectItemsClassImp.AssemblyQualifiedName
        //    , Filter = true, FilterAssign = FilterAssignType.Equal, Sortable = true)]
        [ColumnDef(Display = "設備代號", Sortable = true)]
        public string dev_id
        {
            get
            {
                using (var cxt = TNModelContext.CreateTNModelContext())
                {
                    var me = new Dou.Models.DB.ModelEntity<BasicSttDev>(cxt).GetAll();
                    var stt = me.Where(m => m.stt_no == this.stt_no).FirstOrDefault();
                    return stt?.dev_id;
                }
            }
        }

        [ColumnDef(Display = "行政區", EditType = EditType.Select, SelectGearingWith = "stt_no,county_code", SelectItemsClassNamespace = CountySelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Contains, Visible = false, VisibleEdit = false, Sortable = true)]
        public string county_code { get; set; }

        [Key]
        [ColumnDef(Display = "測站代號", EditType = EditType.Select, SelectItemsClassNamespace = NameSelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Contains, Visible = false, VisibleEdit = false)]
        [StringLength(255)]
        public string stt_no { get; set; }

        [ColumnDef(Display = "測站名稱", Sortable = true)]
        [StringLength(255)]
        public string stt_name { get; set; }

        //[ColumnDef(Display = "", Visible = false, VisibleEdit = false)]
        //[StringLength(255)]
        //public string county_code { get; set; }

        [ColumnDef(Display = "", Visible = false, VisibleEdit = false)]
        [StringLength(255)]
        public string urban_plan { get; set; }

        [ColumnDef(Display = "", Visible = false, VisibleEdit = false)]
        [StringLength(255)]
        public string pipe_num { get; set; }
        
        [ColumnDef(Display = "", Visible = false, VisibleEdit = false)]
        [StringLength(255)]
        public string manhole_num { get; set; }

        [ColumnDef(Display = "", Visible = false, VisibleEdit = false)]
        public double? manhole_depth { get; set; }

        [ColumnDef(Display = "", Visible = false, VisibleEdit = false)]
        public double? manhole_lev { get; set; }

        [ColumnDef(Display = "經度")]
        public double? lon { get; set; }

        [ColumnDef(Display = "緯度")]
        public double? lat { get; set; }

        [ColumnDef(Display = "", Visible = false, VisibleEdit = false)]
        [StringLength(255)]
        public string depoist_date { get; set; }

        [ColumnDef(Display = "", Visible = false, VisibleEdit = false)]
        [StringLength(255)]
        public string manager { get; set; }

        [ColumnDef(Display = "", Visible = false, VisibleEdit = false)]
        [StringLength(255)]
        public string addr { get; set; }

        [ColumnDef(Display = "氣壓值參考站")]
        [StringLength(10)]
        public string ref_dev_id { get; set; }

        [ColumnDef(Display = "", Visible = false, VisibleEdit = false)]
        [StringLength(255)]
        public string stt_purpose { get; set; }

        [Required]
        [ColumnDef(Display = "是否發布警戒", EditType = EditType.Radio, SelectItems = "{\"true\":\"是\",\"false\":\"否\"}", DefaultValue = "false"
            , Visible = false, Sortable = true)]
        public bool IsAlert { get; set; }

        [ColumnDef(Display = "現場裝設照片", EditType = EditType.Image, ImageMaxWidth = 320, ImageMaxHeight = 180, Visible = false, Sortable = true)]
        [StringLength(255)]
        public string Pic_url { get; set; }

        [ColumnDef(Display = "", Visible = false, VisibleEdit = false)]
        [StringLength(255)]
        public string desc { get; set; }

        [ColumnDef(Display = "訊號強度", Sortable = true, VisibleEdit = false)]
        public double? sttDev_rssi
        {
            get
            {
                var data = TNModelContext.GetRealTimeStt5().Where(s => s.stt_no == this.stt_no).FirstOrDefault();
                return (data != null) ? data.rssi : 0;
            }
        }
        [ColumnDef(Display = "即時電壓", Sortable = true, VisibleEdit = false)]
        public double? sttDev_voltage
        {
            get
            {
                var data = TNModelContext.GetRealTimeStt5().Where(s => s.stt_no == this.stt_no).FirstOrDefault();
                return (data != null) ? data.voltage : 0;
            }
        }
        [ColumnDef(Display = "水位高程", Sortable = true, VisibleEdit = false)]
        public double? sttDev_level
        {
            get
            {
                var data = TNModelContext.GetRealTimeStt5().Where(s => s.stt_no == this.stt_no).FirstOrDefault();
                if (data != null)
                {
                    if (data.level != null)
                        return Math.Round((double)data.level, 3, MidpointRounding.AwayFromZero);
                }
                return 0;
            }
        }
        [ColumnDef(Display = "水深", Sortable = true, VisibleEdit = false)]
        public double? sttDev_wdepth
        {
            get
            {
                var data = TNModelContext.GetRealTimeStt5().Where(s => s.stt_no == this.stt_no).FirstOrDefault();
                if (data != null)
                {
                    if (data.level != null && data.wdepth != null)
                        return Math.Round((double)data.level - (double)data.wdepth, 2, MidpointRounding.AwayFromZero);
                }
                return 0;
            }
        }
        [ColumnDef(Display = "半滿管高程", Sortable = true, VisibleEdit = false)]
        public double? sttDev_alarm3
        {
            get
            {
                var data = TNModelContext.GetRealTimeStt5().Where(s => s.stt_no == this.stt_no).FirstOrDefault();
                return (data != null) ? data.alarm3 : 0;
            }
        }
        [ColumnDef(Display = "滿管高程", Sortable = true, VisibleEdit = false)]
        public double? sttDev_alarm2
        {
            get
            {
                var data = TNModelContext.GetRealTimeStt5().Where(s => s.stt_no == this.stt_no).FirstOrDefault();
                return (data != null) ? data.alarm2 : 0;
            }
        }
        [ColumnDef(Display = "資料時間", Sortable = true, VisibleEdit = false)]
        public DateTime? sttDatetime
        {
            get
            {
                var data = TNModelContext.GetRealTimeStt5().Where(s => s.stt_no == this.stt_no).FirstOrDefault();
                return (data != null) ? data.datatime : null;
            }
        }
    }
}
