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
    [Table("[CWA_RainStPredict]")]
    public partial class CWARainStPredict
    {
        public long id { get; set; }

        public DateTime PredictDateTime { get; set; }

        public string rain_st_id { get; set; }
        public string rain_st_name { get; set; }

        public decimal h0 { get; set; }
        public decimal h1 { get; set; }
        public decimal h2 { get; set; }
        public decimal h3 { get; set; }
        public decimal h4 { get; set; }
        public decimal h5 { get; set; }
        public decimal h6 { get; set; }
        public decimal h7 { get; set; }
        public decimal h8 { get; set; }
        public decimal h9 { get; set; }
        public decimal h10 { get; set; }
        public decimal h11 { get; set; }
        public decimal h12 { get; set; }
        public decimal acc0 { get; set; }
        public decimal acc1 { get; set; }
        public decimal acc2 { get; set; }
        public decimal acc3 { get; set; }
        public decimal acc4 { get; set; }
        public decimal acc5 { get; set; }
        public decimal acc6 { get; set; }
        public decimal acc7 { get; set; }
        public decimal acc8 { get; set; }
        public decimal acc9 { get; set; }
        public decimal acc10 { get; set; }
        public decimal acc11 { get; set; }
        public decimal acc12 { get; set; }
    }
}
