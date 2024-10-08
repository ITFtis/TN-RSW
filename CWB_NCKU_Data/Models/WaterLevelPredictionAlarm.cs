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
    [Table("WaterLevelPredictionAlarm")]
    public partial class WaterLevelPredictionAlarm
    {
        public int id { get; set; }

        public DateTime AlarmDateTime { get; set; }

        public DateTime PredictDateTime { get; set; }

        public string dev_id { get; set; }

        public string stt_no { get; set; }

        public string stt_name { get; set; }

        public string rain_st { get; set; }

        public string rain_st_name { get; set; }

        public string county_code { get; set; }

        public string county_name { get; set; }
        public decimal? lon { get; set; }
        public decimal? lat { get; set; }
        public decimal input_x { get; set; }
        public decimal predict_mx { get; set; }
        public decimal predict_dy { get; set; }
        public double calc_y { get; set; }
        public string alarm_type { get; set; }
        public double alarm_threshold { get; set; }
        public string alarm_message { get; set; }
        public string line_messaging_result { get; set; }
    }
}
