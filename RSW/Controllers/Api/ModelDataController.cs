using Newtonsoft.Json.Linq;
using RSW.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.Caching;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;

namespace RSW.Controllers.Api
{
    public class ModelDataController : ApiController
    {
        private static TimeSpan CACHE_INTERVAL = TimeSpan.FromMinutes(60);
        private const string OUTPUT_PATH = "~/Data/NCKU_OUTPUT";
        private const string INPUT_DATA_PATH = "~/Data/TO_NCKU";
        private const string GEOJSON_TEMPLATE_FILENAME = "model_grid.geojson";

        private static ObjectCache cache = MemoryCache.Default;

        private static string ReadAllText(string file)
        {
            using (
                var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var textReader = new StreamReader(fileStream))
                {
                    return textReader.ReadToEnd();
                }
            }
        }

        private IHttpActionResult StringContentWithMimeType(string output, string mimetype)
        {
            return base.ResponseMessage(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    output,
                    Encoding.UTF8,
                    mimetype
                )
            });
        }

        private static JObject GetGeojsonTemplate()
        {
            string path = OUTPUT_PATH + "/" + GEOJSON_TEMPLATE_FILENAME;
            lock (cache)
            {
                JObject fromCache = cache.Get(path) as JObject;
                if (fromCache != null)
                {
                    return fromCache.DeepClone() as JObject;
                }
                string json = ReadAllText(System.Web.HttpContext.Current.Server.MapPath(path));
                JObject obj = JObject.Parse(json);
                cache.Add(path, obj, DateTime.Now.Add(CACHE_INTERVAL));
                return obj.DeepClone() as JObject;
            }
        }

        private static int GetHourFromFilename(string name)
        {
            var u = name.ToUpper();
            if (u.Contains("TNPI."))
            {
                string ext = u.Replace(".TXT", "").Split('.').Last();
                if (ext == "RT") return 0;
                if (ext.EndsWith("H")) return Convert.ToInt32(ext.Replace("H", ""));
            }
            else if (u.Contains("GRID_RAIN_"))
            {
                return Convert.ToInt32(u.Split('.').Last());
            }
            return -1;
        }

        private static JObject GenerateGeojson(IDictionary<int, decimal> data)
        {
            var jobj = GetGeojsonTemplate();
            var features = jobj["features"] as JArray;
            foreach (var i in features)
            {
                var f = i as JObject;
                var fid = f.SelectToken("properties.ID");
                if (fid != null)
                {
                    if (data.TryGetValue(fid.Value<int>(), out var val))
                    {
                        (f["properties"] as JObject).Add("Field1", val);
                    }
                }
            }
            return jobj;
        }

        private class WaterLevel
        {
            public DateTime datatime { get; set; }
            public string dev_id { get; set; }
            public double? val61 { get; set; }
        }

        [HttpGet]
        [Route("api/model/input/waterlevel/list")]
        public IEnumerable<string> GetAvailableWaterLevels()
        {
            var rows = DataService.GetData<DateTime>(
                DataService.DB_TN,
                "SELECT distinct datatime FROM [RealTimeStt] ORDER BY datatime desc"
                ).Take(1008); // 7 * 24 * 6 = 1008, 最新的一週的時間
            return rows.Select(dt => dt.ToString("yyyyMMddHHmm"));
        }

        [HttpGet]
        [Route("api/model/input/waterlevel/{time}")]
        public IHttpActionResult GetNCKUWaterLevel(string time)
        {
            long dt = Convert.ToInt64(time); // yyyyMMDDhhmm
            int y = (int)(dt / 100000000);
            int m = (int)(dt / 1000000) % 100;
            int d = (int)(dt / 10000) % 100;
            int hh = (int)(dt / 100) % 100;
            int mm = (int)(dt % 100);

            var datatime = new DateTime(y, m, d, hh, mm, 0);

            var rows = DataService.GetData<WaterLevel>(
                DataService.DB_TN,
                "SELECT dev_id, datatime, val61 FROM [RealTimeStt] WHERE datatime = @datatime",
                new SqlParameter[] { new SqlParameter("@datatime", datatime) });

            List<string> output = new List<string>();
            foreach (WaterLevel r in rows)
            {
                var devid = r.dev_id;
                var val61 = r.val61;
                if (val61 == null) val61 = -999;
                output.Add(devid + "," + val61);
            }

            return StringContentWithMimeType(string.Join("\n",output), "text/csv");
        }

        [HttpGet]
        [Route("api/model/input/grid_rain/list")]
        public IEnumerable<string> GetAvailableGridRain()
        {
            List<string> zips = new List<string>();
            var files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath(INPUT_DATA_PATH));
            foreach (var f in files)
            {
                var s = f.ToUpper();
                if (s.Contains("GRID_RAIN") && s.EndsWith(".ZIP"))
                {
                    string[] parts = s.Split('_', '.');
                    zips.Add($"{parts[parts.Length - 2]},{Url.Content(INPUT_DATA_PATH)}/GRID_RAIN_{parts[parts.Length - 2]}.ZIP");
                }
            }
            return zips;
        }

        [HttpGet]
        [Route("api/model/input/grid_rain/{t0}/{h}")]
        public IHttpActionResult GetGridRain(string t0, int h)
        {
            string mimetype = "text/csv";
            var path = INPUT_DATA_PATH + "/GRID_RAIN_" + t0 + ".zip";
            string key = path + ":" + h;
            string fromCache = cache.Get(key) as string;
            if (fromCache != null)
            {
                return StringContentWithMimeType(fromCache, mimetype);
            }

            lock (cache)
            {
                fromCache = cache.Get(key) as string;
                if (fromCache != null)
                {
                    return StringContentWithMimeType(fromCache, mimetype);
                }

                // 如果不在 cache，從zip資料產生 geojson
                using (ZipArchive zip = ZipFile.Open(System.Web.HttpContext.Current.Server.MapPath(path), ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in zip.Entries)
                    {
                        int hh = GetHourFromFilename(entry.Name);
                        if (hh != h) continue;
                        List<string> output = new List<string>();
                        using (var stream = entry.Open())
                        using (var reader = new StreamReader(stream))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                try
                                {
                                    var parts = line.Split(null);
                                    var lon = Convert.ToDecimal(parts[0]);
                                    var lat = Convert.ToDecimal(parts[1]);
                                    var val = Convert.ToDecimal(parts[2]);
                                    output.Add($"{lon},{lat},{val}");
                                }
                                catch (Exception ignore)
                                {
                                    // ignore invalid lines
                                }
                            }
                        }
                        var data = string.Join("\n", output);
                        cache.Add(path + ":" + hh, data, DateTime.Now.Add(CACHE_INTERVAL));
                        return StringContentWithMimeType(data, mimetype);
                    }
                }
                return null;
            }
        }  

        [HttpGet]
        [Route("api/model/result/list")]
        public IEnumerable<string> GetAvailableModelOutput()
        {
            List<string> zips = new List<string>();
            var files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath(OUTPUT_PATH));
            foreach(var f in files)
            {
                var s = f.ToUpper();
                if (s.Contains("TNPI.") && s.EndsWith(".DEPTH.ZIP")) {
                    string[] parts = s.Split('.');
                    zips.Add($"{parts[parts.Length - 4]}.{parts[parts.Length - 3]}");
                }
            }
            return zips;
        }


        [HttpGet]
        [Route("api/model/result/{t0}/{h}")]
        public IHttpActionResult GetModelResult(string t0, int h)
        {
            string mimetype = "application/json";
            var path = OUTPUT_PATH + "/TNPI." + t0 + ".DEPTH.zip";
            string key = path + ":" + h;
            string fromCache = cache.Get(key) as string;
            if (fromCache != null)
            {                
                return StringContentWithMimeType(fromCache, mimetype);
            }

            lock (cache)
            {
                fromCache = cache.Get(key) as string;
                if (fromCache != null)
                {
                    return StringContentWithMimeType(fromCache, mimetype);
                }

                // 如果不在 cache，從zip資料產生 geojson
                using (ZipArchive zip = ZipFile.Open(System.Web.HttpContext.Current.Server.MapPath(path), ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in zip.Entries)
                    {
                        int hh = GetHourFromFilename(entry.Name);
                        if (hh != h) continue;
                        JObject jobj;
                        using (var stream = entry.Open())
                        using (var reader = new StreamReader(stream))
                        {
                            string line;
                            var data = new Dictionary<int, decimal>();
                            while ((line = reader.ReadLine()) != null)
                            {
                                try
                                {
                                    var parts = line.Split(null);
                                    var id = Convert.ToInt32(parts[0]);
                                    var val = Convert.ToDecimal(parts[1]);
                                    data.Add(id, val);
                                }
                                catch (Exception ignore)
                                {
                                    // ignore invalid lines
                                }
                            }
                            jobj = GenerateGeojson(data);

                        }
                        cache.Add(path + ":" + hh, jobj.ToString(), DateTime.Now.Add(CACHE_INTERVAL));
                        return StringContentWithMimeType(jobj.ToString(), mimetype);
                    }
                }
                return null;
            }
        }
    }
}