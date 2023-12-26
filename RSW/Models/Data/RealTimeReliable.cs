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
    /// §´µ½²v
    /// </summary>
    [Table("RealTimeReliable")]
    public partial class RealTimeReliable
    {
        [Key]
        [Column(Order = 0)]
        public string dev_id { get; set; }
        [Key]
        [Column(Order = 1)]
        public int Year { get; set; }
        [Key]
        [Column(Order = 2)]
        public int Month { get; set; }
        [Key]
        [Column(Order = 3)]
        public int Day { get; set; }
        public double? ReliableRate { get; set; }
        public double? UpdateRate { get; set; }
        public double? RealTimeRate { get; set; }
    }
}
