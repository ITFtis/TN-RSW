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
    public partial class BasicDevStt
    {
        /// <summary>
        /// 設備代號
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// 測站代號
        /// </summary>
        [StringLength(255)]
        public string stt_no { get; set; }
        /// <summary>
        /// 測站名稱
        /// </summary>
        [StringLength(255)]
        public string stt_name { get; set; }
        /// <summary>
        /// 鄉鎮市區
        /// </summary>
        [StringLength(255)]
        public string county_code { get; set; }
        /// <summary>
        /// 人孔編號
        /// </summary>
        [StringLength(255)]
        public string manhole_num { get; set; }
        /// <summary>
        /// 經度
        /// </summary>
        public double? lon { get; set; }
        /// <summary>
        /// 緯度
        /// </summary>
        public double? lat { get; set; }
        /// <summary>
        /// 警戒水位1
        /// </summary>
        public double? alarm1 { get; set; }
        /// <summary>
        /// 警戒水位2
        /// </summary>
        public double? alarm2 { get; set; }
        /// <summary>
        /// 警戒水位3
        /// </summary>
        public double? alarm3 { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(255)]
        public string addr { get; set; }
        /// <summary>
        /// 氣壓值參考站點
        /// </summary>
        [StringLength(255)]
        public string ref_dev_id { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [StringLength(255)]
        public string desc { get; set; }
    }

    /// <summary>
    /// FL設備基本資料
    /// </summary>
    public partial class BasicDevStt_FL
    {
        /// <summary>
        /// 設備代號
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// 測站代號
        /// </summary>
        [StringLength(255)]
        public string stt_no { get; set; }
        /// <summary>
        /// 測站名稱
        /// </summary>
        [StringLength(255)]
        public string stt_name { get; set; }
        /// <summary>
        /// 鄉鎮市區
        /// </summary>
        [StringLength(255)]
        public string county_code { get; set; }
        /// <summary>
        /// 人孔編號
        /// </summary>
        [StringLength(255)]
        public string manhole_num { get; set; }
        /// <summary>
        /// 經度
        /// </summary>
        public double? lon { get; set; }
        /// <summary>
        /// 緯度
        /// </summary>
        public double? lat { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(255)]
        public string addr { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [StringLength(255)]
        public string desc { get; set; }
    }

    /// <summary>
    /// 設備基本資料
    /// </summary>
    public partial class BasicDevSttNotify
    {
        /// <summary>
        /// 設備代號
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// 測站代號
        /// </summary>
        [StringLength(255)]
        public string stt_no { get; set; }
        /// <summary>
        /// 測站名稱
        /// </summary>
        [StringLength(255)]
        public string stt_name { get; set; }
        /// <summary>
        /// 鄉鎮市區
        /// </summary>
        [StringLength(255)]
        public string county_code { get; set; }
        /// <summary>
        /// 人孔編號
        /// </summary>
        [StringLength(255)]
        public string manhole_num { get; set; }
        /// <summary>
        /// 經度
        /// </summary>
        public double? lon { get; set; }
        /// <summary>
        /// 緯度
        /// </summary>
        public double? lat { get; set; }
        /// <summary>
        /// 警戒水位1
        /// </summary>
        public double? alarm1 { get; set; }
        /// <summary>
        /// 警戒水位2
        /// </summary>
        public double? alarm2 { get; set; }
        /// <summary>
        /// 警戒水位3
        /// </summary>
        public double? alarm3 { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(255)]
        public string addr { get; set; }
        /// <summary>
        /// 氣壓值參考站點
        /// </summary>
        [StringLength(255)]
        public string ref_dev_id { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [StringLength(255)]
        public string desc { get; set; }
        /// <summary>
        /// 是否發布警戒
        /// </summary>
        public bool IsAlert { get; set; }
    }
}
