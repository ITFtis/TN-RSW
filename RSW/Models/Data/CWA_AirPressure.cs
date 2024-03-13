namespace RSW.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CWA_AirPressure")]
    public partial class CWA_AirPressure
    {
        public double CWA_AP { get; set; }

        [Key]
        public DateTime datatime { get; set; }
    }
}
