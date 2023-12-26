namespace RSW.Models.Data
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    /// <summary>
    /// 站台基本資料
    /// </summary>
    [Table("RealTimeStt")]
    public partial class RealTimeStt
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(255)]
        public string dev_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime? datatime { get; set; }

        public double? val02 { get; set; }

        public double? val61 { get; set; }

        public double? voltage { get; set; }

        public double? baro { get; set; }

        public double? rssi { get; set; }

        public double? alarm1 { get; set; }

        public double? alarm2 { get; set; }

        public double? alarm3 { get; set; }

        public bool? uptype { get; set; }

        public DateTime? inserttime { get; set; }

        [StringLength(2000)]
        public string desc { get; set; }
    }

    public partial class RealTimeStt2
    {
        /// <summary>
        /// 設備代碼
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// 資料時間
        /// </summary>
        public DateTime? datatime { get; set; }
        /// <summary>
        /// 監測數據(壓力式水位)
        /// </summary>
        public double? val02 { get; set; }
        /// <summary>
        /// 監測數據(壓力/複合式水位)
        /// </summary>
        public double? level { get; set; }
        /// <summary>
        /// 監測數據(複合式水位)
        /// </summary>
        public double? val61 { get; set; }
    }

    //所有水位站即時資料用到的欄位
    public partial class RealTimeStt3
    {
        /// <summary>
        /// 設備代碼
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// 資料時間
        /// </summary>
        public DateTime? datatime { get; set; }
        /// <summary>
        /// 監測數據(壓力式水位)
        /// </summary>
        public double? val02 { get; set; }
        /// <summary>
        /// 監測數據(複合式水位)
        /// </summary>
        public double? val61 { get; set; }
        /// <summary>
        /// 即時電壓
        /// </summary>
        public double? voltage { get; set; }
        /// <summary>
        /// 氣壓值
        /// </summary>
        public double? baro { get; set; }
        /// <summary>
        /// 訊號強度
        /// </summary>
        public double? rssi { get; set; }
        /// <summary>
        /// 上傳模式
        /// </summary>
        public bool? uptype { get; set; }
        /// <summary>
        /// 資料傳入時間
        /// </summary>
        public DateTime? inserttime { get; set; }
        /// <summary>
        /// 監測數據(壓力/複合式水位)
        /// </summary>
        public double? level { get; set; }
    }

    //所有流速站即時資料用到的欄位
    public partial class RealTimeStt_FL
    {
        /// <summary>
        /// 設備代碼
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// 資料時間
        /// </summary>
        public DateTime? datatime { get; set; }
        /// <summary>
        /// 監測數據(複合式水位)
        /// </summary>
        public double? FL { get; set; }
        /// <summary>
        /// 即時電壓
        /// </summary>
        public double? voltage { get; set; }
        /// <summary>
        /// 氣壓值
        /// </summary>
        public double? baro { get; set; }
        /// <summary>
        /// 訊號強度
        /// </summary>
        public double? rssi { get; set; }
        /// <summary>
        /// 上傳模式
        /// </summary>
        public bool? uptype { get; set; }
        /// <summary>
        /// 資料傳入時間
        /// </summary>
        public DateTime? inserttime { get; set; }
    }

    //所有流速站即時資料用到的欄位
    public partial class RealTimeStt_FL_His
    {
        /// <summary>
        /// 設備代碼
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// 資料時間
        /// </summary>
        public DateTime? datatime { get; set; }
        /// <summary>
        /// 監測數據(複合式水位)
        /// </summary>
        public double? FL { get; set; }
    }

    //UI查詢所有資料用
    public partial class RealTimeStt4
    {
        /// <summary>
        /// 原始資料
        /// </summary>
        [StringLength(2000)]
        public string desc { get; set; }
        /// <summary>
        /// 資料時間
        /// </summary>
        public DateTime? datatime { get; set; }
        /// <summary>
        /// 資料傳入時間
        /// </summary>
        public DateTime? inserttime { get; set; }
        /// <summary>
        /// 延遲秒數
        /// </summary>
        public int? delaySec { get; set; }
    }

    /// <summary>
    /// UI即時資料用到的欄位
    /// </summary>
    public partial class RealTimeStt5
    {
        [Key]
        public string dev_id { get; set; }
        /// <summary>
        /// 設備代碼
        /// </summary>
        public string stt_no { get; set; }
        /// 監測數據(壓力式水位)
        /// </summary>
        public double? val02 { get; set; }
        /// <summary>
        /// 監測數據(複合式水位)
        /// </summary>
        public double? val61 { get; set; }
        /// <summary>
        /// 即時電壓
        /// </summary>
        public double? voltage { get; set; }
        /// <summary>
        /// 訊號強度
        /// </summary>
        public double? rssi { get; set; }
        /// <summary>
        /// 水位
        /// </summary>
        public double? wdepth { get; set; }
        /// <summary>
        /// 滿管高程
        /// </summary>
        public double? alarm2 { get; set; }
        /// <summary>
        /// 半滿管高程
        /// </summary>
        public double? alarm3 { get; set; }
        /// <summary>
        /// 監測數據(壓力/複合式水位)
        /// </summary>
        public double? level { get; set; }
        /// <summary>
        /// 資料時間
        /// </summary> 
        public DateTime? datatime { get; set; }
    }

    /// <summary>
    /// cpami history即時資料用到的欄位
    /// </summary>
    public partial class RealTimeStt6
    {
        [Key]
        /// </summary>
        /// <summary>
        /// 設備代碼
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// 測站代碼
        /// </summary>
        public string stt_no { get; set; }
        /// <summary>
        /// 資料時間
        /// </summary> 
        public DateTime? datatime { get; set; }
        /// <summary>
        /// 監測數據(壓力式水位)
        /// </summary>
        public double? val02 { get; set; }
        /// <summary>
        /// 監測數據(壓力/複合式水位)
        /// </summary>
        public double? level { get; set; }
        /// <summary>
        /// 監測數據(複合式水位)
        /// </summary>
        public double? val61 { get; set; }
    }


    /// <summary>
    /// Cpamiupload即時資料用到的欄位
    /// </summary>
    public partial class RealTimeStt7
    {
        [Key]
        /// <summary>
        /// 設備代碼
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// 測站代碼
        /// </summary>
        public string stt_no { get; set; }
        /// <summary>
        /// 量測時間
        /// </summary> 
        public DateTime? measure_time { get; set; }
        /// <summary>
        /// 上傳時間
        /// </summary> 
        public DateTime? upload_time { get; set; }
        /// <summary>
        /// 監測數據(壓力/複合式水位)
        /// </summary>
        public double? val { get; set; }
    }
}
