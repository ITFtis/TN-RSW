using Newtonsoft.Json.Linq;
using RSW.Models.Data;
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
    class CWAInputProcess
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IDictionary<string, string> CWA_DATA_MAPPING = new Dictionary<string, string>
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

        private static string DecimalString(decimal d)
        {
            return d.ToString("0.##########").TrimEnd('0').TrimEnd('.');
        }
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

        private IDictionary<string, decimal> CreateCache(string content)
        {
            IDictionary<string, decimal> data = new Dictionary<string, decimal>();
            foreach (var line in content.Split("\n"[0]))
            {
                try
                {
                    var parts = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    var lon = Convert.ToDecimal(parts[0]);
                    var lat = Convert.ToDecimal(parts[1]);
                    var val = Convert.ToDecimal(parts[2]);
                    if (val > 0)
                    {
                        data.Add($"{DecimalString(lon)},{DecimalString(lat)}", val);
                    }
                    else
                    {
                        data.Add($"{DecimalString(lon)},{DecimalString(lat)}", 0);
                    }
                }
                catch (Exception ignore)
                {
                    // ignore invalid lines
                }
            }
            return data;
        }

        public string Execute()
        {
            IDictionary<int, IDictionary<string, decimal>> cached = new Dictionary<int, IDictionary<string, decimal>>();
            try
            {
                // 檢查前一個時間點的資料, 一小時為單位, 大約 HH:10 左右會更新資料
                var ref_datetime = DateTime.Now.AddMinutes(-15);
                // 對齊至小時
                var datetime_to_download = new DateTime(
                        ref_datetime.Year, ref_datetime.Month, ref_datetime.Day,
                        ref_datetime.Hour, 00, 00);

                string tempRepacked = Path.GetTempFileName();
                File.Delete(tempRepacked);
                using (ZipArchive repacked = ZipFile.Open(tempRepacked, ZipArchiveMode.Create))
                {
                    foreach (var d in CWA_DATA_MAPPING)
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
                                    // rx.manysplendid.com.tw/rfd-grid 下載之雨量檔
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
                                        using (var decompressed = entry.Open())
                                        {
                                            using (var reader = new StreamReader(decompressed, Encoding.Default))
                                            {
                                                cached.Add(Convert.ToInt32(d.Key.Split('.')[1]), CreateCache(reader.ReadToEnd()));
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
                                        cached.Add(Convert.ToInt32(d.Key.Split('.')[1]), CreateCache(string.Join("\n", lines)));
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
                string output = Path.Combine(AppSettings.TO_NCKU_Path, "GRID_RAIN_" + datetime_to_download.ToString("yyyyMMddHH") + "00.zip");
                File.Copy(tempRepacked, output, true);

                // https://wrbweb.tainan.gov.tw/TainanApi/Rain/Stations 

                if (cached.Count == 13)
                {
                    using (WebClient wc = new WebClient())
                    {
                        wc.Encoding = Encoding.UTF8;
                        var json_string = wc.DownloadString(AppSettings.API_BASEURL + "/rain/base");
                        var json = JArray.Parse(json_string);
                        foreach (var o in json)
                        {
                            string station_id = o["StationID"].Value<string>();
                            string station_name = o["StationName"].Value<string>();
                            bool is_enabled = o["IsEnabled"].Value<bool>();
                            double lon = o["Point"]["Longitude"].Value<double>();
                            double lat = o["Point"]["Latitude"].Value<double>();
                            if (is_enabled)
                            {                                
                                decimal rlon = Convert.ToDecimal(Math.Round(lon * 80) / 80);
                                decimal rlat = Convert.ToDecimal(Math.Round(lat * 80) / 80);
                                try
                                {
                                    var h = new decimal[13];
                                    var acc = new decimal[13];
                                    for (int i = 0; i <= 12; i++)
                                    {
                                        h[i] = cached[i][$"{DecimalString(rlon)},{DecimalString(rlat)}"];
                                        if (i <= 1)
                                        {
                                            acc[i] = h[i];
                                        }
                                        else
                                        {
                                            acc[i] = acc[i - 1] + h[i];
                                        }
                                    }
                                    CWARainStPredict record = new CWARainStPredict
                                    {
                                        PredictDateTime = datetime_to_download,
                                        rain_st_id = station_id,
                                        rain_st_name = station_name,
                                        h0 = h[0],
                                        h1 = h[1],
                                        h2 = h[2],
                                        h3 = h[3],
                                        h4 = h[4],
                                        h5 = h[5],
                                        h6 = h[6],
                                        h7 = h[7],
                                        h8 = h[8],
                                        h9 = h[9],
                                        h10 = h[10],
                                        h11 = h[11],
                                        h12 = h[12],
                                        acc0 = acc[0],
                                        acc1 = acc[1],
                                        acc2 = acc[2],
                                        acc3 = acc[3],
                                        acc4 = acc[4],
                                        acc5 = acc[5],
                                        acc6 = acc[6],
                                        acc7 = acc[7],
                                        acc8 = acc[8],
                                        acc9 = acc[9],
                                        acc10 = acc[10],
                                        acc11 = acc[11],
                                        acc12 = acc[12]
                                    };

                                    DataService.ExecuteDataSql(DataService.DB_DOU,
                                        "DELETE FROM [CWA_RainStPredict] WHERE predict_datetime=@PredictDateTime and rain_st_id=@rain_st_id", record);

                                    DataService.ExecuteDataSql(DataService.DB_DOU,
                                    "INSERT INTO [CWA_RainStPredict] "
                                    + "(predict_datetime,rain_st_id,rain_st_name,h0,h1,h2,h3,h4,h5,h6,h7,h8,h9,h10,h11,h12,acc0,acc1,acc2,acc3,acc4,acc5,acc6,acc7,acc8,acc9,acc10,acc11,acc12)"
                                    + " VALUES(@PredictDateTime,@rain_st_id,@rain_st_name,@h0,@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8,@h9,@h10,@h11,@h12,@acc0,@acc1,@acc2,@acc3,@acc4,@acc5,@acc6,@acc7,@acc8,@acc9,@acc10,@acc11,@acc12)"
                                    , record);

                                    logger.Info("雨量站 " + station_name + " 12小時預報累積雨量 " + acc[12]);
                                }
                                catch (Exception e)
                                {
                                    logger.Warn("雨量站 " + station_name + " 無法解析12小時累積雨量");
                                }
                            }
                        }
                    }
                }

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

