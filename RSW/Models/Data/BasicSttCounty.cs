namespace RSW.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BasicSttCounty")]
    public partial class BasicSttCounty
    {
        [Key]
        [StringLength(50)]
        public string county_code { get; set; }

        [StringLength(50)]
        public string county_name { get; set; }
    }
}
