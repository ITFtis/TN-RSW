using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RSW.Controllers.Api
{
    public class CctvController : ApiController
    {
        /// <summary>
        /// 取指定url資料
        /// </summary>
        /// <param name="url">服務api url</param>
        /// <returns></returns>
        [Route("api/getjson")]
        [HttpGet]
        public JToken GetJToken(string url)
        {
            var result = DouHelper.Misc.GetCache<JToken>(2 * 1000, url);
            if (result == null)
            {
                var o = DouHelper.HClient.Get<JToken>(url);//.Result.Message;
                result = o.Result.Result;
                DouHelper.Misc.AddCache(result, url);
                //Debug.WriteLine($"{url} count {(result == null ? 0 : result.Count())}");
            }

            return result;
        }
        /// <summary>
        /// 取水情影像雲端平台資料
        /// </summary>
        /// <param name="url">https://fmg.wra.gov.tw/swagger/api/XXXXX，XXXX部分相對路徑，或絕對路徑</param>
        /// <returns></returns>
        [Route("api/fmg/get/url")]
        [HttpGet]
        public JToken GetFmgCctv(string url)
        {
            if (!url.StartsWith("http"))
                url = GetFmgUrlBase() + url;
            if (url.IndexOf("token=") < 0)
            {
                url += (url.IndexOf("?") < 0 ? "?" : "&") + "token=eXysP97yhN";// &sourceid='1'";
            }
            return GetJToken(url);
        }
        /// <summary>
        /// fmg資料來源
        /// </summary>
        /// <param name="apiurlbase"></param>
        /// <returns></returns>
        [Route("api/fmg/get/source")]
        [HttpGet]
        public JArray GetFmgSource(string apiurlbase = "")
        {
            var urlbase = GetFmgUrlBase(apiurlbase);

            string key = "GetFmgSource" + urlbase;
            JArray sources = DouHelper.Misc.GetCache<JArray>(10 * 60 * 1000, key);
            if (sources == null)
            {
                sources = GetFmgCctv(urlbase + "source").Value<JArray>("cctvSource");
                DouHelper.Misc.AddCache(sources, key);
            }
            return sources;

        }

        static object GetFmgAllCctvStationLocker = new object();
        /// <summary>
        /// 取fmg所有cctv基本資料
        /// </summary>
        /// <param name="apiurlbase"></param>
        /// <returns></returns>
        [Route("api/fmg/get/allbase")]
        public List<JToken> GetFmgAllCctvStation(string apiurlbase = "")
        {
            var st = DateTime.Now;
            var urlbase = GetFmgUrlBase(apiurlbase);

            string key = "GetFmgAllCctvStation" + urlbase;
            lock (GetFmgAllCctvStationLocker)
            {
                List<JToken> allJtoken = DouHelper.Misc.GetCache<List<JToken>>(60 * 60 * 1000, key);
                if (allJtoken == null)
                {
                    allJtoken = new List<JToken>(8000);
                    var ss = GetFmgSource(apiurlbase);
                    var i = 0;

                    ss.AsParallel().ForAll(s =>
                    {
                        var sid = s.Value<string>("sourceid");
                        System.Diagnostics.Debug.Write(s.Value<string>("name") + "...");
                        var stas = GetFmgCctv(urlbase + $"cctv_station?sourceid={sid}").Value<JArray>("cctvs");
                        foreach (var j in stas)
                        {
                            j["sourceid"] = sid; //增加查詢方便
                        }
                        allJtoken.AddRange(stas);
                        System.Diagnostics.Debug.WriteLine($"{s.Value<string>("name")}完成 {++i}/{ss.Count}");
                    });
                    DouHelper.Misc.AddCache(allJtoken, key);
                }
                return allJtoken;
            }
        }
        /// <summary>
        /// 一條件取cctv站的即時影像
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sourceid"></param>
        /// <param name="apiurlbase"></param>
        /// <returns></returns>
        [Route("api/fmg/get/cctv/{id}/{sourceid}")]
        public JToken GetFmgCctvCameras(string id, string sourceid, string apiurlbase = "")
        {
            var urlbase = GetFmgUrlBase(apiurlbase);
            string key = "GetFmgCctvCameras" + id + urlbase;
            JToken jk = DouHelper.Misc.GetCache<JToken>(5 * 1000, key);
            if (jk == null)
            {
                jk = GetFmgCctv(urlbase + $"cctv/{id}?sourceid={sourceid}").Value<JArray>("cctvs").FirstOrDefault();
                if (jk != null)
                    DouHelper.Misc.AddCache(jk, key);
            }
            return jk;
        }
        string GetFmgUrlBase(string inApibase = null)
        {
            if (string.IsNullOrEmpty(inApibase) || !inApibase.StartsWith("http"))
                return "https://fmg.wra.gov.tw/swagger/api/";
            else return inApibase;
        }

        static List<Cctv> TNWrbCctvs = null;
        [Route("api/cctv/tnwrb/base")]
        [HttpGet]
        public List<Cctv> TNWrbCctv()
        {
            if (TNWrbCctvs == null)
            {
                List<Cctv> result = new List<Cctv>();
                //List<KeyValuePair<string, string>> header = new List<KeyValuePair<string, string>>();
                //header.Add(new KeyValuePair<string, string>("apiKey", "F2AC69CA-CBF6-4851-9B26-3961D2EA3D1A"));
                try
                {
                    var bases = new DataController().TainanApi("Camera", "Stations", "GroupStations").ToList();// as JArray;//  DouHelper.HClient.Get<JArray>("https://wrbweb.tainan.gov.tw/TainanApi/Camera/Stations/GroupStations", "application/json", header
                    //).Result.Result.ToList();
                    var rts = new DataController().TainanApi("Camera", "Infos", "Realtime").ToList(); //DouHelper.HClient.Get<JArray>("https://wrbweb.tainan.gov.tw/TainanApi/Camera/Infos/Realtime", "application/json", header
                    //).Result.Result.ToList();
                    DateTime st = DateTime.Now;
                    result = bases.Select(jt => new Cctv
                    {
                        name = jt.Value<string>("StationName"),
                        id = jt.Value<string>("StationID"),
                        X = jt.Value<JToken>("Point").Value<double>("Longitude"),
                        Y = jt.Value<JToken>("Point").Value<double>("Latitude"),
                        urls = new List<CctvInfo>()
                    }).ToList();
                    result.ForEach(b =>
                    {
                        var us = rts.Where(rjt => b.id.Equals(rjt.Value<string>("GroupStationID")));
                        b.urls = us.Select(u => new CctvInfo
                        {
                            id = u.Value<string>("CameraID"),
                            name = u.Value<string>("CameraName"),
                            url = u.Value<string>("ImageUrl")
                        }).ToList();
                    });

                }
                catch (Exception ex)
                {
                    result = new List<Cctv>();
                    result.Add(new Cctv { name = ex.ToString() });
                    return result;
                }
                TNWrbCctvs = result;
            }
            return TNWrbCctvs;
        }
    }
    public class Cctv
    {
        public string id { set; get; }
        public string name { set; get; }
        public double X { set; get; }
        public double Y { set; get; }
        public List<CctvInfo> urls { set; get; }
    }

    public class CctvInfo
    {
        public string id { set; get; }
        public int ch { set; get; }
        public string name { set; get; }
        public string url { set; get; }
    }
}