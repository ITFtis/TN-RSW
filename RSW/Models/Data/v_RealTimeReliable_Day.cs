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
    /// �駴���v
    /// </summary>
    [Table("v_RealTimeReliable_Day")]
    public partial class v_RealTimeReliable_Day
    {
        [Key]
        [Column(Order = 0)]
        [ColumnDef(Display = "�]�ƥN��", Visible = false)]
        public string dev_id { get; set; }

        //[ColumnDef(Display = "��F��", EditType = EditType.Select, SelectGearingWith = "stt_no,county_code", SelectItems = "{\"6703400\":{ v:\"�_��\"},\"6703500\":{ v:\"�w�n��\"},\"6703600\":{ v:\"�w����\"},\"6703700\":{ v:\"�����\"}}"
        //    , Filter = true, FilterAssign = FilterAssignType.Contains, Visible = false, Sortable = true)]

        [ColumnDef(Display = "��F��", EditType = EditType.Select, SelectGearingWith = "stt_no,county_code", SelectItemsClassNamespace = CountySelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Contains, Visible = false, Sortable = true)]
        public string county_code { get; set; }

        [ColumnDef(Display = "���쯸", EditType = EditType.Select, SelectItemsClassNamespace = NameSelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Contains, Sortable = true)]
        public string stt_no { get; set; }

        [ColumnDef(Display = "�t��", EditType = EditType.Select, SelectItemsClassNamespace = ManufacturerSelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Equal, Visible = false, Sortable = true)]
        public string manufacturer { get; set; }
        [Key]
        [Column(Order = 1)]
        [ColumnDef(Display = "�~��", EditType = EditType.Select, SelectItemsClassNamespace = YearSelectAllItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Equal, Visible = false, Sortable = true)]
        public int Year { get; set; }
        [Key]
        [Column(Order =2)]
        [ColumnDef(Display = "��", Visible = false, EditType = EditType.Select, SelectItems = "{'1':{v:'1'},'2':{v:'2'},'3':{v:'3'},'4':{v:'4'},'5':{v:'5'},'6':{v:'6'},'7':{v:'7'},'8':{v:'8'},'9':{v:'9'},'10':{v:'10'},'11':{v:'11'},'12':{v:'12'}}"
            , Filter = true, FilterAssign = FilterAssignType.Equal, Sortable = true)]
        public int Month { get; set; }
        [Key]
        [Column(Order = 3)]
        [ColumnDef(Display = "��", Visible = false)]
        public int Day { get; set; }

        [ColumnDef(Display = "���", Sortable = true)]
        public string MMDD { get; set; }

        [ColumnDef(Display = "��W�ǲv(%)")]
        public double? UpdateRate_DD { get; set; }

        [ColumnDef(Display = "��Y�ɲv(%)")]
        public double? RealTimeRate_DD { get; set; }

        [ColumnDef(Display = "�駴���v(%)")]
        public double? ReliableRate_DD { get; set; }
    }
}
