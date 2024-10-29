using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RSW.Models.Data;
using RSW.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CWB_NCKU_Data.Services
{
    class RainWaterLevelAlarm
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Dictionary<string, string> COUNTY = 
            DataService.GetData<BasicSttCounty>(DataService.DB_TN, "SELECT * FROM BasicSttCounty")
            .ToDictionary(x => x.county_code, x => x.county_name);
        private static Dictionary<string, string> RAIN_STATIONS =
            DataService.GetData<CWARainStPredict>(DataService.DB_DOU, "SELECT DISTINCT rain_st_id,rain_st_name FROM CWA_RainStPredict")
            .ToDictionary(x => x.rain_st_id, x => x.rain_st_name);
        private static LineMessageService line_service = new LineMessageService();

        public string Execute()
        {
            try
            {
                // 檢查前一個時間點的資料, 一小時為單位
                var now = DateTime.Now;
                var datetime_to_check = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);

                //var url = string.Format(AppSettings.NCKU_Download_Url2, datetime_to_download);
                //byte[] data = DataService.DownloadData(url);
                //logger.Info("完成自 " + url + " 下載資料，檔案大小：" + data.Length);

                // 假設是 
                // {
                //    "data": [
                //        { "st": "rain_st1", "precipitation_12hr": 10 },
                //        { "st": "rain_st2", "precipitation_12hr": 20 }
                //    ]
                // }
                //
                // 必須要 ToList, 不然 foreach 時會有 另一個 SqlParameterCollection 已經包含 SqlParameter。 的錯誤
                var list = DataService.GetData<CWARainStPredict>(DataService.DB_DOU,
                    "SELECT * FROM CWA_RainStPredict WHERE predict_datetime=@PredictDateTime", new SqlParameter[] {
                        new SqlParameter("@PredictDateTime", datetime_to_check)}).ToList();
                if (list.Count() == 0)
                {
                    datetime_to_check = datetime_to_check.AddHours(-1);
                    // 必須要 ToList, 不然 foreach 時會有 另一個 SqlParameterCollection 已經包含 SqlParameter。 的錯誤
                    list = DataService.GetData<CWARainStPredict>(DataService.DB_DOU,
                                       "SELECT * FROM CWA_RainStPredict WHERE predict_datetime=@PredictDateTime", new SqlParameter[] {
                    new SqlParameter("@PredictDateTime", datetime_to_check)}).ToList();
                }
                if (AppSettings.NOTIFY_DATA == "2") // read using json
                {
                    var url = string.Format(AppSettings.NCKU_Download_Url2);
                    byte[] data = DataService.DownloadData(url);
                    logger.Info("使用模擬資料，完成自 " + url + " 下載資料，檔案大小：" + data.Length);
                    var list0 = new List<CWARainStPredict>();
                    var result = JObject.Parse(System.Text.Encoding.UTF8.GetString(data));
                    var list1 = result["data"].ToList();
                    foreach (var y in list1)
                    {
                        var id = y["rain_st"].Value<string>();
                        RAIN_STATIONS.TryGetValue(id, out var lookup_name);
                        list0.Add(new CWARainStPredict()
                        {
                            rain_st_id = y["rain_st"].Value<string>(),
                            rain_st_name = lookup_name,
                            acc12 = Convert.ToDecimal(y["precipitation_12hr"].Value<float>())
                        });;
                    }
                    list = list0.ToList();
                }
                foreach (var x in list)
                {
                    var rain_st = x.rain_st_name;
                    var input_x = x.acc12;
                    logger.Info("雨量站 " + rain_st + " 12小時預估累積雨量為: " + input_x);

                    if (input_x < 0.001m) continue;

                    // 找出該雨量站對應的水位計公式
                    // 用 ToList() 全部載入記憶體
                    var preidction_data = DataService.GetData<WaterLevelPrediction>(
                        DataService.DB_DOU,
                        "SELECT * FROM WaterLevelPrediction WHERE rain_st_name=@rain_st AND predict_mx IS NOT NULL AND predict_dy IS NOT NULL",
                        new SqlParameter[] { new SqlParameter("@rain_st", rain_st) }).ToList();

                    foreach (var w_dev in preidction_data)
                    {
                        var wl = Convert.ToDecimal(input_x) * w_dev.predict_mx.Value + w_dev.predict_dy.Value;
                        var dwl = Convert.ToDouble(wl);
                        logger.Info($"水位計 {w_dev.stt_name} ({w_dev.dev_id}/{w_dev.stt_no}) 推估水位： {wl}");
                        var basic_stt_dev = DataService.GetData<BasicSttDev>(
                            DataService.DB_TN,
                            "SELECT * FROM BasicSttDev WHERE dev_id=@dev_id AND (alarm1 IS NOT NULL OR alarm2 IS NOT NULL OR alarm3 IS NOT NULL)",
                            new SqlParameter[] { new SqlParameter("@dev_id", w_dev.dev_id) }).ToList();
                        if (basic_stt_dev.Count() == 0)
                        {
                            logger.Error($"水位計 {w_dev.stt_name} ({w_dev.dev_id}/{w_dev.stt_no}) 無警戒值資料");
                        }
                        else
                        {
                            foreach (var a in basic_stt_dev)
                            {
                                string alarm_type = null;
                                double alarm_level = 0;
                                if (a.alarm1.HasValue && a.alarm1.Value < dwl)
                                {
                                    alarm_type = "一級警戒";
                                    alarm_level = a.alarm1.Value;
                                }
                                else if (a.alarm2.HasValue && a.alarm2.Value < dwl)
                                {
                                    alarm_type = "二級警戒";
                                    alarm_level = a.alarm2.Value;
                                }
                                else if (a.alarm3.HasValue && a.alarm3.Value < dwl)
                                {
                                    alarm_type = "三級警戒";
                                    alarm_level = a.alarm3.Value;
                                }
                                if (alarm_type != null)
                                {
                                    logger.Info($"{rain_st} 12小時累積預報雨量為 {input_x}，水位計 {w_dev.stt_name} ({w_dev.dev_id}/{w_dev.stt_no}) 觸發 {alarm_type}, 推估水位： {wl}, 警戒值 {alarm_level}");

                                    string message = $"[{DateTime.Now.ToShortTimeString()}] {rain_st} 12小時累積預報雨量為 {input_x}，{COUNTY[w_dev.county_code]} ({w_dev.stt_name}) 已達 {alarm_type} 標準，預估水位為 {wl.ToString("0.##")} mm，請注意防範";
                                    string push_result = line_service.Push(message);
                                    WaterLevelPredictionAlarm alarm = new WaterLevelPredictionAlarm
                                    {
                                        AlarmDateTime = DateTime.Now,
                                        PredictDateTime = datetime_to_check,
                                        dev_id = w_dev.dev_id,
                                        stt_no = w_dev.stt_no,
                                        stt_name = w_dev.stt_name,
                                        rain_st = w_dev.rain_st,
                                        rain_st_name = w_dev.rain_st_name,
                                        predict_mx = w_dev.predict_mx.Value,
                                        predict_dy = w_dev.predict_dy.Value,
                                        input_x = Convert.ToDecimal(input_x),
                                        calc_y = dwl,
                                        alarm_type = alarm_type,
                                        alarm_threshold = alarm_level,
                                        county_code = w_dev.county_code,
                                        county_name = COUNTY[w_dev.county_code],
                                        lat = Convert.ToDecimal(a.lat),
                                        lon = Convert.ToDecimal(a.lon),
                                        line_messaging_result = push_result,
                                        alarm_message = message
                                    };

                                    DataService.ExecuteDataSql(DataService.DB_DOU,
                                        "INSERT INTO WaterLevelPredictionAlarm "
                                        + "(AlarmDateTime,PredictDateTime,dev_id,stt_no,stt_name,rain_st,rain_st_name,predict_mx,predict_dy,input_x,calc_y,alarm_type,alarm_threshold,county_code,county_name,lat,lon,line_messaging_result,alarm_message)"
                                        + " VALUES(@AlarmDateTime,@PredictDateTime,@dev_id,@stt_no,@stt_name,@rain_st,@rain_st_name,@predict_mx,@predict_dy,@input_x,@calc_y,@alarm_type,@alarm_threshold,@county_code,@county_name,@lat,@lon,@line_messaging_result,@alarm_message)",
                                        alarm);

                                }
                            }
                        }
                    }


                }
                return "";
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            return null;
        }
    }
}
