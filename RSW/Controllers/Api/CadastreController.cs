using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml.Linq;
using static DouHelper.HClient;

namespace RSW.Controllers.Api
{
    public class CadastreController : ApiController
    {
        // GET api/<controller>
        [Route("api/cadastre/token")]
        [HttpGet]
        public string GetToken()
        {
            //請求簽章的服務網址
            string reqUrl = RSW.Startup.AppSet.CadastreApiGetSignCodeUrl;// "https://ldgis.tainan.gov.tw/portal/WebAPI/service/GetSignCode/";
            //您申請的應用系統編號
            string apID = RSW.Startup.AppSet.CadastreApID;// "A20230717-0001";
            //您申請的應用系統編號
            string apPSWD = RSW.Startup.AppSet.CadastreApPSWD;// "1234@1qaz#EDC";
            //取得簽章(驗證碼)內容
            string signCode = GetSignCode(reqUrl, apID, apPSWD);
            return signCode;
        }
        [Route("api/cadastre/{sec}/{no?}")]
        [HttpGet]
        public CWFSClass Getcadastre(string sec , string no=null)
        {
            string key = sec;// +no; //查地段所有資料，在篩選
            CWFSClass result = DouHelper.Misc.GetCache<CWFSClass>(60*60*1000,key);
            if (result == null)
            {
                var token = GetToken().Split(',')[1];
                //查地段所有地號
                string url = RSW.Startup.AppSet.CadastreApiGetDataUrl + $"{token}/?QM=2&QD={{'SEC':'{sec}'}}&EPSG=4326";
                //if (no == null)
                    //url =RSW.Startup.AppSet.CadastreApiGetDataUrl+  $"{token}/?QM=2&QD={{'SEC':'{sec}'}}&EPSG=4326";
                //else
                    //url = $"https://ldgis.tainan.gov.tw/portalAPI/WS/A20230717-0001/00005-5d956f39-7189-4de6-be3d-317132a64d7f/{token}/?QM=1&QD={{'SEC':'{sec}','NO':'{no}'}}&EPSG=4326";
                try
                {
                    result = new CWFSClass();
                    var jtoken = DouHelper.HClient.Get<JToken>(url).Result.Result;
                    result.RESULT = jtoken.Value<string>("RESULT");
                    result.MSG = jtoken.Value<string>("MSG");
                    result.ResultMsg = jtoken.Value<string>("resultMsg"); //token驗證錯誤時

                    result.WFSDATA = jtoken.Value<string>("WFSDATA");
                    //result.WFSDATA = new StreamReader(Server.MapPath("~/Other/XMLFile2SEC.xml")).ReadToEnd();
                    if (result.WFSDATA != null && !result.WFSDATA.Trim().Equals(""))
                    {
                        result.WFSDATA = result.WFSDATA.Replace("\\\"", "\"").Replace("\n", "").Replace("\r\n", "");
                        var doc = System.Xml.Linq.XDocument.Parse(result.WFSDATA);
                        XNamespace gmlns = "http://www.opengis.net/gml";

                        //var posLiwwsts = doc.Root.Descendants(gmlns + "posList");//Poygon
                        var featureMembers = doc.Root.Descendants(gmlns + "featureMember");
                        result.Data = new List<CData>();
                        foreach (var f in featureMembers)
                        {
                            CData c = new CData();
                            result.Data.Add(c);
                            var posLists = f.Descendants(gmlns + "posList");//Poygon
                            if (posLists.Count() > 0)
                            {
                                var coors = posLists.ElementAt(0).Value;
                                var cs = coors.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                c.PosList = new List<XY>();
                                for (var i = 0; i < cs.Length; i++)
                                {
                                    c.PosList.Add(new XY { X = Convert.ToDouble(cs[i]), Y = Convert.ToDouble(cs[++i]) });
                                }
                                var xes = f.Descendants();
                                var pifs = c.GetType().GetProperties().ToList();
                                foreach (var xe in xes)
                                {
                                    if (!xe.Name.LocalName.Equals("posList"))
                                    {
                                        var pif = pifs.Find(p => p.Name == xe.Name.LocalName);
                                        if (pif != null)
                                            pif.SetValue(c, xe.Value);
                                    }
                                    //System.Diagnostics.Debug.WriteLine(xe.Name.LocalName +">>"+xe.Name.Namespace+">>"+xe.Value);
                                }
                            }
                        }

                        DouHelper.Misc.AddCache(result, key);
                    }
                }
                catch (Exception ex)
                {
                    result.ResultMsg = ex.Message;
                }
            }
            if(no != null)
            {
                CWFSClass temp = new CWFSClass { MSG=result.MSG, RESULT=result.RESULT , Data = new List<CData>()};
                if (temp.RESULT == "0000")
                {
                    var d = result.Data.FirstOrDefault(s => s.地號全碼 == no);
                    if(d==null)
                        temp.RESULT = "0009";
                    else
                        temp.Data = new List<CData>(new CData[] { d});
                }
                result = temp;
            }
            return result;
        }
        

        //取得簽章函式
        public string GetSignCode(string reqUrl, string apID, string apPSWD)
        {
            string param = @"apID={0}&apPSWD={1}";
            param = string.Format(param, apID, apPSWD);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(reqUrl);
            byte[] PostData = Encoding.UTF8.GetBytes(param);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = PostData.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(PostData, 0, PostData.Length);
            }
            using (WebResponse wr = req.GetResponse())
            {
                using (Stream myStream = wr.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(myStream))
                    {
                        string result = "";
                        //取回的簽章(驗證碼)內容
                        result = myStreamReader.ReadToEnd();
                       
                        myStreamReader.Close();
                        return result;
                    }
                }
            }
        }

        public class CWFSClass
        {
            public string RESULT { set; get; }
            public string MSG { set; get; }
            //[JsonProperty("resultMsg")]
            public string ResultMsg { set; get; }

            internal string WFSDATA { set; get; }
            //public List<XY> PosList { set; get; }
            public List<CData> Data { set; get; }
        }
        public class XY
        {
            public double X { set; get; }
            public double Y { set; get; }
        }

        public class CData
        {
            public List<XY> PosList { set; get; }
            public string 地所代碼 { set; get; }
            public string 區名稱 { set; get; }
            public string 地段編號 { set; get; }
            public string 地號全碼 { set; get; }
            public string 地號 { set; get; }
            public string 登記面積 { set; get; }
            public string 登記日期 { set; get; }
            public string 地目 { set; get; }
            public string 使用分區 { set; get; }
            public string 使用地類別 { set; get; }
            public string 公告地現值 { set; get; }
            public string 公告地價 { set; get; }
            public string 所有權人 { set; get; }
            public string 管理機關 { set; get; }
            public string 權屬 { set; get; }
            public string 量測方法 { set; get; }
        }
    }
}