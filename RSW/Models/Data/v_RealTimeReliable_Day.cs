namespace RSW.Models.Data
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    /// <summary>
    /// 日妥善率
    /// </summary>
    [Table("v_RealTimeReliable_Day")]
    public partial class v_RealTimeReliable_Day
    {
        [Key]
        [Column(Order = 0)]
        [ColumnDef(Display = "設備代號", Visible = false)]
        public string dev_id { get; set; }

        //[ColumnDef(Display = "行政區", EditType = EditType.Select, SelectGearingWith = "stt_no,county_code", SelectItems = "{\"6703400\":{ v:\"北區\"},\"6703500\":{ v:\"安南區\"},\"6703600\":{ v:\"安平區\"},\"6703700\":{ v:\"中西區\"}}"
        //    , Filter = true, FilterAssign = FilterAssignType.Contains, Visible = false, Sortable = true)]

        [ColumnDef(Display = "行政區", EditType = EditType.Select, SelectGearingWith = "stt_no,county_code", SelectItemsClassNamespace = CountySelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Contains, Visible = false, Sortable = true)]
        public string county_code { get; set; }

        [ColumnDef(Display = "水位站", EditType = EditType.Select, SelectItemsClassNamespace = NameSelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Contains, Sortable = true)]
        public string stt_no { get; set; }

        [ColumnDef(Display = "廠商", EditType = EditType.Select, SelectItemsClassNamespace = ManufacturerSelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Equal, Visible = false, Sortable = true)]
        public string manufacturer { get; set; }
        [Key]
        [Column(Order = 1)]
        [ColumnDef(Display = "年度", EditType = EditType.Select, SelectItemsClassNamespace = YearSelectAllItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Equal, Visible = false, Sortable = true)]
        public int Year { get; set; }
        [Key]
        [Column(Order =2)]
        [ColumnDef(Display = "月", Visible = false, EditType = EditType.Select, SelectItems = "{'1':{v:'1'},'2':{v:'2'},'3':{v:'3'},'4':{v:'4'},'5':{v:'5'},'6':{v:'6'},'7':{v:'7'},'8':{v:'8'},'9':{v:'9'},'10':{v:'10'},'11':{v:'11'},'12':{v:'12'}}"
            , Filter = true, FilterAssign = FilterAssignType.Equal, Sortable = true)]
        public int Month { get; set; }
        [Key]
        [Column(Order = 3)]
        [ColumnDef(Display = "日", Visible = false)]
        public int Day { get; set; }

        [ColumnDef(Display = "日期", Sortable = true)]
        public string MMDD { get; set; }

        [ColumnDef(Display = "日上傳率(%)")]
        public double? UpdateRate_DD { get; set; }

        [ColumnDef(Display = "日即時率(%)")]
        public double? RealTimeRate_DD { get; set; }

        [ColumnDef(Display = "日妥善率(%)")]
        public double? ReliableRate_DD { get; set; }
    }
}
