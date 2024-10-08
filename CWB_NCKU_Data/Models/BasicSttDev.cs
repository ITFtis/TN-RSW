namespace RSW.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 設備基本資料
    /// </summary>
    [Table("BasicSttDev")]
    public partial class BasicSttDev
    {
        [Key]
        [StringLength(255)]
        public string dev_id { get; set; }

        [StringLength(255)]
        public string stt_no { get; set; }

        public double? lon { get; set; }

        public double? lat { get; set; }

        [StringLength(255)]
        public string dev_purpose { get; set; }

        [StringLength(255)]
        public string manufacturer { get; set; }

        [StringLength(255)]
        public string dev_model { get; set; }

        [StringLength(255)]
        public string depoist_date { get; set; }

        [StringLength(255)]
        public string trans_method { get; set; }

        [StringLength(255)]
        public string ip { get; set; }

        [StringLength(255)]
        public string power { get; set; }

        public int? sampling_period { get; set; }

        public int? measure_period { get; set; }

        public int? upload_period { get; set; }

        public double? base_elev { get; set; }

        public double? culvert_depth { get; set; }

        public double? dev_depth1 { get; set; }

        public double? dev_depth2 { get; set; }

        public double? alarm1 { get; set; }

        public double? alarm2 { get; set; }

        public double? alarm3 { get; set; }

        [StringLength(255)]
        public string desc { get; set; }
    }
}
