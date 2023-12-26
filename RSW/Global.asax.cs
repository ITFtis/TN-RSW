using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RSW
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Logger.Log.LoadConfig(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(("~/Config")), "Log4netConfig.xml"));
            Logger.Log.AutoDeleteExpiredData(System.Web.Hosting.HostingEnvironment.MapPath(("~/log")), 20);
            Logger.Log.For(null).Info("RSW Application_Start");

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(2000);
                try
                {
                    new Controllers.Api.CctvController().GetFmgAllCctvStation();
                }
                /*
                 * 偶有以下問題
                 * 會有來源陣列不夠長。請檢查 srcIndex 與長度，以及陣列的下限。
                 * */
                catch
                {
                    System.Threading.Thread.Sleep(2000);
                    try
                    {
                        new Controllers.Api.CctvController().GetFmgAllCctvStation();
                    }
                    catch
                    {

                    }
                }

            }
            ).Start();

            try
            {
                var task = new BkTask();
                task.Run();
            }
            catch (Exception ex)
            {
                Logger.Log.For(this).Error("BkTask 錯誤:"+ex.Message);
            }
        }
    }
}

