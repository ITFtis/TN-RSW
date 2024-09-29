using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RSW.Models.Data
{
    public class WaterLevelPrediction
    {
        [Key]
        [StringLength(255)]
        public string dev_id { get; set; }

        [StringLength(255)]
        public string stt_no { get; set; }

        [StringLength(255)]
        public string stt_name { get; set; }

        [StringLength(255)]
        public string county_code { get; set; }

        [StringLength(255)]
        public string rain_st { get; set; }

        [StringLength(255)]
        public string rain_st_name { get; set; }

        public decimal? predict_mx { get; set; }
        public decimal? predict_dy { get; set; }
        public decimal? r2 { get; set; }

    }
}