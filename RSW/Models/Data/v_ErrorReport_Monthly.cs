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
    /// 異常月報表
    /// </summary>
    [Table("v_ErrorReport_Monthly")]
    public partial class v_ErrorReport_Monthly
    {
        [Key]
        [Column(Order = 0)]
        [ColumnDef(Display = "設備代號", Sortable = true)]
        public string dev_id { get; set; }

        [ColumnDef(Display = "行政區", EditType = EditType.Select, SelectItemsClassNamespace = CountySelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Equal, Visible = false, Sortable = true)]
        public string county_code { get; set; }

        [ColumnDef(Display = "水位站", EditType = EditType.Select, SelectItemsClassNamespace = NameSelectItemsClassImp.AssemblyQualifiedName, Sortable = true)]
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

        [ColumnDef(Display = "日期", Sortable = true)]
        public string YYYYMM { get; set; }

        [ColumnDef(Display = "達一級警戒(次)", Sortable = true)]
        public int SumAlarm1 { get; set; }

        [ColumnDef(Display = "達二級警戒(次)", Sortable = true)]
        public int SumAlarm2 { get; set; }

        [ColumnDef(Display = "達三級警戒(次)", Sortable = true)]
        public int SumAlarm3 { get; set; }
    }
}
