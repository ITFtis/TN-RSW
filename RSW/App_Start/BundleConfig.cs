using System.Web;
using System.Web.Optimization;

namespace RSW
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.bundle.min.js"
                    ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new ScriptBundle("~/dou/js").Include(
               "~/Scripts/gis/bootstraptable/bootstrap-table.js",
               "~/Scripts/gis/bootstraptable/extensions/mobile/bootstrap-table-mobile.js",
               "~/Scripts/gis/select/bselect/bootstrap-select-b5.min.js",
               "~/Scripts/Dou/datetimepicker/js/moment.js",
               "~/Scripts/Dou/datetimepicker/js/tempusdominus-bootstrap-4.min.js", //bootstrpa4、5
               "~/Scripts/gis/helper.js",
               "~/Scripts/gis/Main.js",
               "~/Scripts/Dou/Dou.js"
           ));

            bundles.Add(new StyleBundle("~/dou/css").Include(
                "~/Scripts/gis/bootstraptable/bootstrap-table.css",
                "~/Scripts/gis/select/bselect/bootstrap-select-b5.min.css",
                //"~/Scripts/gis/b3/css/bootstrap.css",
                "~/Scripts/gis/Main.css",
                "~/Scripts/Dou/Dou.css",
                "~/Scripts/Dou/datetimepicker/css/bootstrap-datetimepicker.css",
                "~/Content/prj/prj.css"));

            //gis css
            bundles.Add(new StyleBundle("~/Scripts/gis/csskit").Include(
                        //"~/Scripts/gis/jquery/jquery-ui-1.10.4.css",
                        //  "~/Scripts/gis/jspanel/jspanel.css",
                        //  "~/Scripts/gis/animation.css",
                        //   "~/Scripts/gis/Main.css",
                        //  "~/Scripts/gis/leafletExt.css",
                        //   "~/Scripts/prj/map.css",
                        "~/Scripts/gis/jquery/jquery-ui-1.10.4.css",
                      "~/Scripts/gis/jspanel/jspanel.css",
                      "~/Scripts/gis/leafletExt.css",
                      "~/Scripts/gis/animation.css"
                      ));
            //gis javascript
            bundles.Add(new ScriptBundle("~/Scripts/gis/jskit").Include(
                    "~/Scripts/gis/jquery/jquery-ui-1.10.4.min.js",
                    "~/Scripts/gis/jquery/jquery.ui.touch-punch.min.js",
                      "~/Scripts/gis/ext/meter/BasePinCtrl.js",
                      "~/Scripts/gis/ext/meter/LBasePinCtrl.js",
                      "~/Scripts/gis/ext/meter/LKmlCtrl.js",
                      "~/Scripts/gis/ext/geocode/LAddressGeocode.js",
                      //"~/Scripts/gis/ext/leaflet/Dou.Popop.js",
                      "~/Scripts/gis/charthelper.js"
                      ));

            bundles.Add(new ScriptBundle("~/Scripts/prj/rthydro").Include(
                "~/Scripts/prj/data.js",
                "~/Scripts/prj/meterImpl.js",
                "~/Scripts/prj/sub/rthydro/rain.js",
                "~/Scripts/prj/sub/rthydro/water.js",
                "~/Scripts/prj/sub/rthydro/fsensor.js",
                "~/Scripts/prj/sub/rthydro/sewer.js",
                "~/Scripts/prj/sub/rthydro/fsta.js",
                "~/Scripts/prj/sub/rthydro/rdisaster.js",
                "~/Scripts/prj/rthydro.js",
                "~/Scripts/prj/otherpoint.js",
                "~/Scripts/prj/position.js",
                "~/Scripts/prj/village_position.js",
                "~/Scripts/prj/cadastre_position.js",
                "~/Scripts/prj/sub/unitCode.js",
                "~/Scripts/prj/sub/floodQuery.js",
                "~/Scripts/gis/leaflet/dou-MaskRectGrid.js",
                "~/Scripts/gis/other/dom-to-image.min.js",
                "~/Scripts/gis/other/FileSaver.min.js"
                     ));


            bundles.Add(new ScriptBundle("~/Scripts/prj/sewerdashboard").Include(
               "~/Scripts/prj/data.js",
               "~/Scripts/prj/meterImpl.js",
               "~/Scripts/prj/createMapHelper.js",
               "~/Scripts/prj/sub/rthydro/rain.js",
               "~/Scripts/prj/sub/rthydro/water.js",
               "~/Scripts/prj/sub/rthydro/fsensor.js",
               "~/Scripts/prj/sub/rthydro/sewer.js",
               "~/Scripts/prj/sub/unitCode.js",
               "~/Scripts/prj/sub/floodQuery.js",
               "~/Scripts/gis/charthelper.js",
               "~/Scripts/prj/sewerdashboard.js"
                   ));

            bundles.Add(new ScriptBundle("~/Scripts/prj/sewerhistory").Include(
               "~/Scripts/prj/data.js",
               "~/Scripts/prj/meterImpl.js",
               "~/Scripts/prj/createMapHelper.js",
               "~/Scripts/prj/sub/rthydro/rain.js",
               "~/Scripts/prj/sub/rthydro/water.js",
               "~/Scripts/prj/sub/rthydro/fsensor.js",
               "~/Scripts/prj/sub/rthydro/sewer.js",
               "~/Scripts/prj/sub/unitCode.js",
               "~/Scripts/prj/sub/floodQuery.js",
               "~/Scripts/gis/charthelper.js",
               "~/Scripts/prj/sewerhistory.js"
                   ));
        }
    }
}
