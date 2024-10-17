using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace CWB_NCKU_Data
{
    static class Program
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //     <add key="OnlyStep" value="氣象署轉成大,成大水位結果下載,雨量預報預警發佈" />

        static void Main(string[] args)
        {
            //1.氣象署轉成大
            if (AppSettings.OnlyStep.IndexOf("氣象署轉成大") > -1)
            {
                var start_time = DateTime.Now;
                logger.Info("氣象署轉成大" + ": starting...");
                var run = new CWB_NCKU_Data.Services.CWAInputProcess();
                var output = run.Execute();
                logger.Info("氣象署轉成大" + ": 完成，資料檔案路徑: " + output);

            }

            //2.成大水位結果下載
            if (AppSettings.OnlyStep.IndexOf("成大水位結果下載") > -1)
            {
                var start_time = DateTime.Now;
                logger.Info("成大水位結果下載" + ": starting...");
                var run = new CWB_NCKU_Data.Services.NCKUResultProcess();
                var output = run.Execute();
                logger.Info("成大水位結果下載" + ": 完成，資料檔案路徑: " + output);

            }

            //3.成大水位結果下載
            if (AppSettings.OnlyStep.IndexOf("雨量預報預警發佈") > -1)
            {
                var start_time = DateTime.Now;
                logger.Info("雨量預報預警發佈" + ": starting...");
                var run = new CWB_NCKU_Data.Services.RainWaterLevelAlarm();
                var output = run.Execute();
                logger.Info("雨量預報預警發佈" + ": 完成，" + output);

            }

        }
    }
}
