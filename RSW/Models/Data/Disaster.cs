using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RSW.Models.Data
{
    /// <summary>
    /// 災害事件資料
    /// </summary>
    [Table("Disaster")]
    public class Disaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        [ColumnDef(VisibleEdit = false)]
        public int Id { get; set; }

        [Display(Name = "災害名稱")]
        public string Name { get; set; }

        [Display(Name = "起始日")]
        [ColumnDef(EditType = EditType.Date)]
        [Column(TypeName = "date")]
        public DateTime SDate { get; set; }

        [Display(Name = "結束日")]
        [ColumnDef(EditType = EditType.Date)]
        [Column(TypeName = "date")]
        public DateTime EDate { get; set; }

        [Display(Name = "說明")]
        public string Note { get; set; }
    }
}