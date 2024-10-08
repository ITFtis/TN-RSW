using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWB_NCKU_Data
{
    class AppSettings
    {
        public static string OnlyStep = ConfigurationManager.AppSettings["OnlyStep"].ToString();
        public static string CWB_Download_Url = ConfigurationManager.AppSettings["CWB_Download_Url"].ToString();
        // 模式結果下載點1: 水位輸出
        public static string NCKU_Download_Url1 = ConfigurationManager.AppSettings["NCKU_Download_Url1"].ToString();
        // 模式結果下載點1: 雨量站12小時累積雨量輸出
        public static string NCKU_Download_Url2 = ConfigurationManager.AppSettings["NCKU_Download_Url2"].ToString();
        public static string TO_NCKU_Path = ConfigurationManager.AppSettings["TO_NCKU_Path"].ToString();
        public static string NCKU_OUTPUT_Path = ConfigurationManager.AppSettings["NCKU_OUTPUT_Path"].ToString();
    }
}
