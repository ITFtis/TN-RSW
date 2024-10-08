using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CWB_NCKU_Data.Services
{
    class CWBInputProcess
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string Execute()
        {
            try
            {
                // 檢查前一個時間點的資料, 10分鐘為單位
                var now = DateTime.Now;
                var datetime_to_download = now
                    .AddSeconds(-now.Second)
                    .AddMinutes(-(now.Minute % 10));

                var url = string.Format(AppSettings.CWB_Download_Url, datetime_to_download);
                byte[] data = DataService.DownloadData(url);
                logger.Info("完成自 " + url + " 下載資料，檔案大小：" + data.Length);

                // 資料解析及格式轉換
                // 2024-10-08 等正式資料介接後再實作

                // 轉存至網站 Data/TO_NCKU 路徑
                Directory.CreateDirectory(AppSettings.TO_NCKU_Path);
                string output = Path.Combine(AppSettings.TO_NCKU_Path, "GRID_RAIN_" + datetime_to_download.ToString("yyyyMMddHHmm") + ".zip");
                File.WriteAllBytes(output, data);

                //var list = DataService.GetData<dynamic>(DataService.DB_TN, 
                //    "SELECT [dev_id],[datatime],[val02],[val61] FROM [RealTimeStt]");
                //Console.Out.WriteLine(list.Count());
                return output;
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
