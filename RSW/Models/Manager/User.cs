using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RSW.Models.Manager
{
    [Table("User")]
    public class User : Dou.Models.UserBaseExt
    {
        [Key]
        [Display(Name = "帳號", Order = 0)]
        [Column(Order = 0, TypeName = "varchar")]
        [ColumnDef(Filter = true, FilterAssign = FilterAssignType.Contains, Sortable = true, Index = 0)]
        [StringLength(48)]
        public override string Id { get; set; }

        [Display(Name = "使用者名稱")]
        [Column(Order = 1)]   
        [ColumnDef(Filter = true, FilterAssign = FilterAssignType.Contains, Sortable = true, Index = 1)]
        [StringLength(50)]
        public override string Name { get; set; }

        [ColumnDef(EditType = EditType.Select, Sortable = true, Index = 50,
            SelectItems = "{\"true\":\"啟用\",\"false\":\"未啟用\"}", Filter = true)]
        [Display(Name = "狀態")]
        public override bool? Enabled { get; set; }

        [Column(Order = 2147483578)]
        [ColumnDef(Display = "創建時間", EditType = EditType.Date, Filter = true, FilterAssign = FilterAssignType.Between)]
        public DateTime? AccountCreateTime { get; set; }
    }
}