using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CWB_NCKU_Data.Services
{
    class CWBInputProcess
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IDictionary<string, string> CWB_DATA_MAPPING = new Dictionary<string, string>
        {
            { "grid_rain_0000.000.zip", "10min_QPESUMS_obs/{0:yyyyMMddHH}00/{1}" },
            { "grid_rain_0000.001.zip", "10min_QPESUMS_fcst/{0:yyyyMMddHH}00/{1}" },
            { "grid_rain_0000.002.zip", "10min_QPESUMS_fcst/{0:yyyyMMddHH}00/{1}" },
            { "grid_rain_0000.003.zip", "10min_QPESUMS_fcst/{0:yyyyMMddHH}00/{1}" },
            { "grid_rain_0000.004.zip", "QPESUMS_WRF/{0:yyyyMMddHH}/{1}" },
            { "grid_rain_0000.005.zip", "QPESUMS_WRF/{0:yyyyMMddHH}/{1}" },
            { "grid_rain_0000.006.zip", "QPESUMS_WRF/{0:yyyyMMddHH}/{1}" },
            { "grid_rain_0000.007.zip", "QPESUMS_WRF/{0:yyyyMMddHH}/{1}" },
            { "grid_rain_0000.008.zip", "QPESUMS_WRF/{0:yyyyMMddHH}/{1}" },
            { "grid_rain_0000.009.zip", "QPESUMS_WRF/{0:yyyyMMddHH}/{1}" },
            { "grid_rain_0000.010.zip", "QPESUMS_WRF/{0:yyyyMMddHH}/{1}" },
            { "grid_rain_0000.011.zip", "QPESUMS_WRF/{0:yyyyMMddHH}/{1}" },
            { "grid_rain_0000.012.zip", "QPESUMS_WRF/{0:yyyyMMddHH}/{1}" },
        };

        private List<string> ConvertFromBin(Stream ms)
        {
            List<string> lines = new List<string>();
            decimal lon = 118.0M;
            for (int x = 0; x < 561; x++)
            {
                decimal lat = 27.0M;
                for (int y = 0; y < 441; y++)
                {
                    int b0 = ms.ReadByte();
                    int b1 = ms.ReadByte();
                    if (b0 > 0 || b1 > 0)
                    {
                        lines.Add($"{lon} {lat} {b1 * 256 + b0}");
                    }
                    lat -= 0.0125M;
                }
                lon += 0.0125M;
            }
            return lines;
        }

        public string Execute()
        {
            try
            {
                // 檢查前一個時間點的資料, 一小時為單位, 大約 HH:10 左右會更新資料
                var now = DateTime.Now;
                var datetime_to_download = now
                    .AddSeconds(-now.Second)
                    .AddMinutes(-(now.Minute))
                    .AddMinutes(-15);

                string tempRepacked = Path.GetTempFileName();
                File.Delete(tempRepacked);
                using (ZipArchive repacked = ZipFile.Open(tempRepacked, ZipArchiveMode.Create))
                {
                    foreach (var d in CWB_DATA_MAPPING)
                    {
                        try
                        {
                            var url = string.Format(AppSettings.CWB_Download_Url + d.Value, datetime_to_download, d.Key);
                            byte[] data = DataService.DownloadData(url);
                            logger.Info("完成自 " + url + " 下載資料，檔案大小：" + data.Length);

                            string tempExtracted = Path.GetTempFileName();
                            File.WriteAllBytes(tempExtracted, data);

                            // 資料解析及格式轉換
                            // 既有的 binary 格式
                            using (ZipArchive zip = ZipFile.Open(tempExtracted, ZipArchiveMode.Read))
                            {
                                foreach (ZipArchiveEntry entry in zip.Entries)
                                {
                                    string output_filename = null;
                                    if (entry.Name.StartsWith("grid_rain_0000."))
                                    {

                                        using (var decompressed = entry.Open())
                                        {

                                            var new_entry = repacked.CreateEntry(entry.Name);
                                            using (var os = new_entry.Open())
                                            {
                                                decompressed.CopyTo(os);
                                            }
                                        }
                                    }
                                    else if (entry.Name.Contains("qpfqpe_"))
                                    {
                                        output_filename = "grid_rain_0000.000";
                                    }
                                    else if (entry.Name.Contains("_1H_"))
                                    {
                                        output_filename = "grid_rain_0000.001";
                                    }
                                    else if (entry.Name.Contains("_3H_"))
                                    {
                                        output_filename = "grid_rain_0000.003";
                                    }
                                    else if (entry.Name.Contains("_6H_"))
                                    {
                                        output_filename = "grid_rain_0000.006";
                                    }
                                    else if (entry.Name.Contains("_12H_"))
                                    {
                                        output_filename = "grid_rain_0000.012";
                                    }

                                    // 原來的 bin 轉換的 code
                                    if (output_filename != null)
                                    {
                                        List<string> lines = new List<string>();
                                        lines.Add($"氣象局：{datetime_to_download.ToString("yyyyMMddHHmm")} 解析度(經緯度)：  0.0125");
                                        lines.Add("3");
                                        lines.Add("Longitude");
                                        lines.Add("Latitude");
                                        lines.Add("intensity(mm/hr)");
                                        using (GZipStream gz_decompress = new GZipStream(entry.Open(), CompressionMode.Decompress))
                                        {
                                            using (MemoryStream decompressed = new MemoryStream())
                                            {
                                                gz_decompress.CopyTo(decompressed);
                                                // 跳過 170個 byte;
                                                decompressed.Seek(170, SeekOrigin.Begin);
                                                lines.AddRange(ConvertFromBin(decompressed));
                                            }

                                        }
                                        string temp_output = Path.GetTempFileName();
                                        File.WriteAllLines(temp_output, lines);
                                        repacked.CreateEntryFromFile(temp_output, output_filename);
                                    }

                                }
                            }
                        }


                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                        }
                    }
                }




                // 轉存至網站 Data/TO_NCKU 路徑
                Directory.CreateDirectory(AppSettings.TO_NCKU_Path);
                string output = Path.Combine(AppSettings.TO_NCKU_Path, "GRID_RAIN_" + datetime_to_download.ToString("yyyyMMddHH") + ".zip");
                File.Copy(tempRepacked, output, true);

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

