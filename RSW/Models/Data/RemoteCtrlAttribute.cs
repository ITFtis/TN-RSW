namespace RSW.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RemoteCtrlAttribute")]
    public partial class RemoteCtrlAttribute
    {
        [Key]
        [StringLength(10)]
        public string dev_id { get; set; }

        public double? base_elev { get; set; }

        public double? alarm1 { get; set; }

        public double? alarm2 { get; set; }

        public double? alarm3 { get; set; }

        public int? rRate { get; set; }

        public bool beUpdate { get; set; }

        public bool alUpdate { get; set; }

        public bool rrUpdate { get; set; }
    }
}
