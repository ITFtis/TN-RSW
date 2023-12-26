using Microsoft.Owin;
using Owin;
using RSW.Controllers.Manager;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static System.Net.WebRequestMethods;

[assembly: OwinStartup(typeof(RSW.Startup))]

namespace RSW
{
    public class Startup
    {
        internal static AppSet AppSet { get; set; }
        public void Configuration(IAppBuilder app)
        {
            bool isDebug = true;
            // 如需如何設定應用程式的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkID=316888
            Dou.Context.Init(new Dou.DouConfig
            {
                //SystemManagerDBConnectionName = "DouModelContextExt",
                DefaultPassword = "1234@1qaz#EDC", //"1234@1qaz#EDC",
                SessionTimeOut = 20,
                UserIdMaxLength = 48,
                SqlDebugLog = isDebug,
                AllowAnonymous = false,
                LoginPage = new UrlHelper(System.Web.HttpContext.Current.Request.RequestContext).Action("DouLogin", "User", new { }),
                LoggerListen = (log) =>
                {
                    if (log.WorkItem == Dou.Misc.DouErrorHandler.ERROR_HANDLE_WORK_ITEM)
                    {
                        Debug.WriteLine("DouErrorHandler發出的錯誤!!\n" + log.LogContent);
                        Logger.Log.For(null).Error("DouErrorHandler發出的錯誤!!\n" + log.LogContent);
                    }
                },
                EnforceChangePasswordFirstLogin = false,
                VerifyUserPasswordErrorCount = 3,
                RenewPasswordDeadline = int.MaxValue,
                VerifyUserErrorLockTime = 10,
                OnAModelControllerActionExecuting = (ctx) => {
                    var ac = ctx.RequestContext.HttpContext.Request["AuthCode"];
                    //var ssomessage = Dou.Help.DouUnobtrusiveSession.Session["SsoMessage"];
                    //if (!string.IsNullOrEmpty(ssomessage))
                    //    return new RedirectResult(new UrlHelper(ctx.RequestContext).Action("SsoMessage", "User"));
                    if (string.IsNullOrEmpty(ac))
                        return null;
                    else {
                        System.IO.File.AppendAllText(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(("~/log")), $"sso{DateTime.Now.ToString("yyyyyMM")}.txt"), $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}:{ac}" + Environment.NewLine, System.Text.Encoding.Default);
                        var rsso = GetUserDataFromSSO(ac);
                        if (rsso.isSuccess == true)
                        {
                            Dou.Help.DouUnobtrusiveSession.Session.Add(UserController.TNWRB_SSO_MESSAGE, rsso);
                            //ViewBag.User = Newtonsoft.Json.JsonConvert.SerializeObject(rsso);// JsonArrayAttribute  Result;
                            //ViewBag.Message = rsso.Message.A_UserName + " 您好， 目前系統建構中尚未開放...";
                            //return new RedirectResult(new UrlHelper(ctx.RequestContext).Action("SsoMessage", "User"));
                            return new RedirectResult(Dou.Context.Config.LoginPage);
                        }
                        else
                            return new RedirectResult(UserController.TNWRB_SSO_PORTAL);
                        //return new RedirectResult(new UrlHelper(ctx.RequestContext).Action(a, c))
                    }
                }
            });
            AppSet = DouHelper.Misc.DeSerializeObjectLoadJsonFile<AppSet>(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Config"), "AppSet.json"));
            //AppSet = new AppSet
            //{
            //    CadastreApiGetSignCodeUrl = "https://ldgis.tainan.gov.tw/portal/WebAPI/service/GetSignCode/",
            //    CadastreApiGetDataUrl = "https://ldgis.tainan.gov.tw/portalAPI/WS/A20230717-0001/00005-5d956f39-7189-4de6-be3d-317132a64d7f/",
            //    CadastreApID = "A20230717-0001",
            //    CadastreApPSWD = "1234@1qaz#EDC",
            //    CWBDayRainfallUrl = "https://cwbopendata.s3.ap-northeast-1.amazonaws.com/DIV2/O-A0040-003.kmz",
            //    CWBRadUrl = "https://qpeplus.cwb.gov.tw/static/data/grid_0.01deg/cref/",
            //    CWBQpf1h = "https://qpeplus.cwb.gov.tw/static/data/grid/qpfqpe_060min/",
            //    TainanApiUrl = "https://wrbweb.tainan.gov.tw/TainanApi/"

            //};
            //DouHelper.Misc.SerializeObjectSaveJsonFile(AppSet, Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(("~/Config")), "AppSet.json"));
        }
       
        public dynamic GetUserDataFromSSO(string AuthCod)
        {
            //TODO:序列化
            var serializer = new JavaScriptSerializer();
            var jsonText = serializer.Serialize(new
            {
                Key = UserController.TNWRB_SSO_KEY,
                SafetyCode = UserController.TNWRB_SSO_SAFETYCODE,
                AuthCode = AuthCod
            });

            var jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonText);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UserController.TNWRB_SSO_GETUSERDATA);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/json";
            request.ContentLength = jsonBytes.Length;

            try
            {
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(jsonBytes, 0, jsonBytes.Length);
                    requestStream.Flush();
                }
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        return Newtonsoft.Json.Linq.JObject.Parse(reader.ReadToEnd()); ;
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                dynamic obj = new System.Dynamic.ExpandoObject();
                obj.isSuccess = false;
                obj.Message = ex.Message;
                return obj;
            }
        }
    }
}
