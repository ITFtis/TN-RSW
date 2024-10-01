using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;

namespace RSW.Controllers.Api
{
    public class DataController : ApiController
    {
        #region 取高雄khfloodinfo資料
        /// <summary>
        /// 取高雄水利局資料
        /// </summary>
        /// <param name="para0">取資料路徑</param>
        /// <param name="para1">取資料路徑</param>
        /// <param name="para2">取資料路徑</param>
        /// <param name="para3">取資料路徑</param>
        /// <returns>JToken</returns>
        [Route("api/TainanApi/{para0}")]
        [Route("api/TainanApi/{para0}/{para1}")]
        [Route("api/TainanApi/{para0}/{para1}/{para2}")]
        [Route("api/TainanApi/{para0}/{para1}/{para2}/{para3}")]
        [Route("api/TainanApi/{para0}/{para1}/{para2}/{para3}/{para4}")]
        [Route("api/TainanApi/{para0}/{para1}/{para2}/{para3}/{para4}/{para5}")]
        [Route("api/TainanApi/{para0}/{para1}/{para2}/{para3}/{para4}/{para5}/{para6}")]
        [HttpGet]
        public JToken TainanApi(string para0, string para1 = null, string para2 = null, string para3 = null, string para4 = null, string para5 = null, string para6 = null)
        {
            string url = RSW.Startup.AppSet.TainanApiUrl+ $"{para0}";///{info}?jwt={Token()}";
            ResetGetWaterStation();
            ResetGetRainStation();
            if (!string.IsNullOrEmpty(para1))
                url += $"/{para1}";
            if (!string.IsNullOrEmpty(para2))
                url += $"/{para2}";
            if (!string.IsNullOrEmpty(para3))
                url += $"/{para3}";
            if (!string.IsNullOrEmpty(para4))
                url += $"/{para4}";
            if (!string.IsNullOrEmpty(para5))
                url += $"/{para5}";
            if (!string.IsNullOrEmpty(para6))
                url += $"/{para6}";
            return GetJToken(url);          
        }
       
        #endregion

        [Route("api/getjson")]
        public JToken GetJToken(string url, int cachetimer = 10 * 1000)
        {
            var result = DouHelper.Misc.GetCache<JToken>(cachetimer, url);
            if (result == null)
            {
                var o = DouHelper.HClient.Get<JToken>(url, "application/json", new KeyValuePair<string, string>[] {new KeyValuePair<string, string>("Apikey", "F2AC69CA-CBF6-4851-9B26-3961D2EA3D1A") });//.Result.Message;
                result = o.Result.Result;
                Debug.WriteLine($"{url} count {(result == null ? 0 : result.Count())}");
            }

            return result;
        }

   
        #region 雨量資料
        static object GetRainStationLocker = new object();
        [Route("api/rain/base")]
        public IEnumerable<JToken> GetRainStation()
        {
            string key = "GetRainStation";
            lock (GetRainStationLocker)
            {
                var result = DouHelper.Misc.GetCache<IEnumerable<JToken>>(30 * 60 * 1000, key);
                if (result == null)
                {
                    result = (TainanApi("Rain", "Stations") as JArray).Where(s=>s.Value<bool>("IsEnabled"));
                    DouHelper.Misc.AddCache(result, key);
                }
                return result;
            }
        }
        public static void ResetGetRainStation()
        {
            string key = "GetRainStation";
            DouHelper.Misc.ClearCache(key);
        }
        #endregion

        #region 水位資料
        static object GetWaterStationLocker = new object();
        [Route("api/water/base")]
        public IEnumerable<JToken> GetWaterStation()
        {
            string key = "GetRainStation";
            lock (GetWaterStationLocker)
            {
                var result = DouHelper.Misc.GetCache<IEnumerable<JToken>>(30 * 60 * 1000, key);
                if (result == null)
                {
                    result = (TainanApi("Water", "Stations") as JArray).ToArray();//.Where(s => s.Value<bool>("IsEnabled"));
                    DouHelper.Misc.AddCache(result, key);
                }
                return result;
            }
        }
        public static void ResetGetWaterStation()
        {
            string key = "GetRainStation";
            DouHelper.Misc.ClearCache(key);
        }
        #endregion

        #region 淹水感測資料
        static object GetFloodWaterStationLocker = new object();
        [Route("api/floodwater/base")]
        public IEnumerable<JToken> GetFloodWaterStation()
        {
            string key = "GetFloodWaterStation";
            lock (GetFloodWaterStationLocker)
            {
                var result = DouHelper.Misc.GetCache<IEnumerable<JToken>>(30 * 60 * 1000, key);
                if (result == null)
                {
                    result = (TainanApi("FloodWater", "Stations") as JArray).Where(s => s.Value<bool>("IsEnabled"));
                    DouHelper.Misc.AddCache(result, key);
                }
                return result;
            }
        }
        #endregion

        #region 取累計雨量去背圖資訊
        static object DayRainfallLockObject = new object();
        // GET api/<controller>
        [Route("api/rain/img/dayrainfall")]
        [HttpGet]
        public ImageData DayRainfall()
        {
            string key = "day-rainfall";
            lock (DayRainfallLockObject)
            {
                ImageData img = DouHelper.Misc.GetCache<ImageData>(2 * 60 * 1000, key);
                if (img == null)
                {
                    img = GetDayRainfall();
                    DouHelper.Misc.AddCache(img, key);
                }
                return img;
            }
        }
        ImageData GetDayRainfall()
        {
            ImageData img = new ImageData();
            using (WebClient client = new WebClient())
            {
                var dfile = HttpContext.Current.Server.MapPath($@"~/download/{Guid.NewGuid()}/O-A0040-003.kmz");
                var dir = System.IO.Path.GetDirectoryName(dfile);
                var edir = dir + @"/Extract";
                bool istest = false;
                if (istest)
                    edir = @"D:\CVS_SRC\SourceCode\水利署\水利署重大水災情\SFC\SFC\download\O-A0040-003";
                try
                {
                    if (!istest)
                    {
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);
                        client.DownloadFile(RSW.Startup.AppSet.CWBDayRainfallUrl, dfile);

                        ZipFile.ExtractToDirectory(dfile, edir);
                    }
                    string linkkml = null;
                    using (StreamReader sr = new StreamReader(edir + @"\doc.kml"))
                    {
                        XDocument doc = XDocument.Load(sr);
                        var xe = doc.Root.Descendants(doc.Root.Name.Namespace + "name").FirstOrDefault();
                        var fname = xe.Value;
                        if (!string.IsNullOrEmpty(fname))
                        {
                            try
                            {
                                img.Name = fname.Substring(fname.LastIndexOf("_") + 1);
                                System.Globalization.CultureInfo provider = new System.Globalization.CultureInfo("zh-TW");
                                img.Time = DateTime.ParseExact(fname.Replace(img.Name, ""), "yyyy-MM-dd_HHmm_", provider, System.Globalization.DateTimeStyles.None);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.ToString());
                                img.Name = ex.ToString();
                            }
                        }

                        //xe = doc.Root.Element(doc.Root.Name.Namespace + @"Network/Link/Link/href");
                        xe = doc.Root.Descendants(doc.Root.Name.Namespace + @"href").FirstOrDefault();
                        linkkml = xe.Value;

                    }
                    using (StreamReader sr = new StreamReader(edir + @"\" + linkkml))
                    {
                        XDocument doc = XDocument.Load(sr);
                        var xe = doc.Root.Descendants(doc.Root.Name.Namespace + "LatLonBox").FirstOrDefault();
                        img.MaxX = Convert.ToDouble(xe.Element(xe.Name.Namespace + "east").Value);
                        img.MinX = Convert.ToDouble(xe.Element(xe.Name.Namespace + "west").Value);
                        img.MaxY = Convert.ToDouble(xe.Element(xe.Name.Namespace + "north").Value);
                        img.MinY = Convert.ToDouble(xe.Element(xe.Name.Namespace + "south").Value);
                    }

                    using (System.Drawing.Image image = System.Drawing.Image.FromFile((edir + @"\" + linkkml).Replace(".kml", ".png")))
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();

                            // Convert byte[] to Base64 String
                            string base64String = Convert.ToBase64String(imageBytes);
                            img.Url = "data:image/png;base64," + base64String;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log.For(this).Error("GetDayRainfall Error:" + ex.Message);
                }
                finally
                {
                    if (!istest && Directory.Exists(dir))
                        Directory.Delete(dir, true);
                }
            }
            return img;
        }
        public class ImageData
        {
            public string Name { set; get; }
            public DateTime Time { set; get; }
            public double MinX { set; get; }
            public double MaxX { set; get; }
            public double MinY { set; get; }
            public double MaxY { set; get; }
            public string Url { set; get; }
        }
        #endregion

        #region 雷達回波圖
        [Route("api/rad/rt")]
        public QpesumsData GetRadRt()
        {
            QpesumsData result = null;
            DateTime n = DateTime.Now;//.AddHours(-8);//資料路徑格式是UTC
            n = Convert.ToDateTime(n.ToString("yyyy/MM/dd HH:mm").Substring(0, 15) + "0:00");
            while (true)
            {
                var c = GetRadTime(n);
                if (c != null)
                {
                    result = new QpesumsData
                    {
                        Datetime = n,//.AddHours(8), //還原UTC+8
                        Content = c
                    };
                    break;
                }
                else
                {
                    n = n.AddMinutes(-10);
                    if ((DateTime.Now - n).TotalHours > 1)
                    {
                        break;
                    }
                }
            }
            return result;
        }
        [Route(@"api/rad/time/{dt:datetime}")]
        public string GetRadTime(DateTime dt)
        {
            string result = null;
            try
            {
                using (WebClient wc = new WebClient())
                {
                    var udt = dt.AddHours(-8);
                    udt = Convert.ToDateTime(udt.ToString("yyyy/MM/dd HH:mm").Substring(0, 15) + "0:00");
                    string url = RSW.Startup.AppSet.CWBRadUrl+ $"{udt.Year}/{udt.Month.ToString("00")}/{udt.Day.ToString("00")}/{udt.Hour.ToString("00")}/CREF.{udt.ToString("yyyyMMdd.HHmm00")}.dat";
                    var stream = wc.OpenRead(url);
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        result = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                var ss = ex;
            }
            return result;
        }
        #endregion

        #region qpesums預報1小時資料
        [Route("api/qpesums/qpf060min/rt")]
        public QpesumsData GetQpfqpe060minRt()
        {
            QpesumsData result = null;
            DateTime n = DateTime.Now;//.AddHours(-8);//資料路徑格式是UTC
            n = Convert.ToDateTime(n.ToString("yyyy/MM/dd HH:mm").Substring(0, 15) + "0:00");
            while (true)
            {
                var c = GetQpfqpe060min(n);
                if (c != null)
                {
                    result = new QpesumsData
                    {
                        Datetime = n,//.AddHours(8), //還原UTC+8
                        Content = c
                    };
                    break;
                }
                else
                {
                    n = n.AddMinutes(-10);
                    if ((DateTime.Now - n).TotalHours > 1)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        // using YYYYMMDDHHmm format as a long, to avoid /: in path
        [Route(@"api/qpesums/qpf060min/result/{ts}")]
        public QpesumsData GetQpfqpe060minDt(long ts)
        {
            DateTime dt = new DateTime((int)(ts / 100000000), (int)(ts / 1000000 % 100), (int)(ts / 10000 % 100), (int)(ts / 100 % 100), (int)(ts % 100), 0);
            var c = GetQpfqpe060min(dt);
            if (c != null)
            {
                return new QpesumsData
                {
                    Datetime = dt,//.AddHours(8), //還原UTC+8
                    Content = c
                };
            }
            return null;
        }


        //[Route(@"daterange/{startDate:regex(^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{3}Z$)}/{endDate:regex(^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{3}Z$)}")]
        //[Route(@"api/qpesums/qpf060min/time/{dt:regex(^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{3}Z$)}")]
        [Route(@"api/qpesums/qpf060min/time/{dt:datetime}")]
        public string GetQpfqpe060min(DateTime dt)
        {
            string result = null;
            try
            {
                using (WebClient wc = new WebClient())
                {
                    var udt = dt.AddHours(-8);
                    udt = Convert.ToDateTime(udt.ToString("yyyy/MM/dd HH:mm").Substring(0, 15) + "0:00");
                    string url = RSW.Startup.AppSet.CWBQpf1h+$"{udt.Year}/{udt.Month.ToString("00")}/{udt.Day.ToString("00")}/{udt.Hour.ToString("00")}/qpfqpe_060min.{udt.ToString("yyyyMMdd.HHmm")}.dat";
                    var stream = wc.OpenRead(url);
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        result = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                var ss = ex;
            }
            return result;
        }
        public class QpesumsData
        {
            public DateTime Datetime { set; get; }
            public string Content { set; get; }
        }
        #endregion


    }
}