using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace RSW
{
    public class BkTask
    {
        public BkTask()
        {
            // 從組態檔載入相關參數，例如 SmtpHost、SmtpPort、SenderEmail 等等.
        }
        private DateTime startdt = DateTime.Now;
        private int runCount = 0;
        private bool _stopping = false;


        public void Run()
        {
            Logger.Log.For(this).Info("啟動BkTask背景");
            System.IO.File.AppendAllText(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(("~/log")), "startlog.txt"), $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}啟動BkTask背景" + Environment.NewLine);
            var aThread = new Thread(TaskLoop);
            aThread.IsBackground = true;
            aThread.Priority = ThreadPriority.BelowNormal;  // 避免此背景工作拖慢 ASP.NET 處理 HTTP 請求.
            aThread.Start();
        }

        public void Stop()
        {
            _stopping = true;
        }



        private void TaskLoop()
        {
            // 設定每一輪工作執行完畢之後要間隔幾分鐘再執行下一輪工作.
            const int LoopIntervalInMinutes = 1000 * 60 * 3;

            Logger.Log.For(this).Info("背景TaskLoop on thread ID: " + Thread.CurrentThread.ManagedThreadId.ToString());
            while (!_stopping)
            {
                try
                {
                    Todo();
                }
                catch (Exception ex)
                {
                    // 發生意外時只記在 log 裡，不拋出 exception，以確保迴圈持續執行.
                    Logger.Log.For(this).Error("BkTask.TaskLoop錯誤:"+ex.ToString());
                }
                finally
                {
                    // 每一輪工作完成後的延遲.
                    System.Threading.Thread.Sleep(LoopIntervalInMinutes);
                }
            }
        }

        private void Todo()
        {
            Logger.Log.For(this).Info($"To do ......啟動時間:{startdt.ToString("yyyy/MM/dd HH:mm:ss")};次數:{(++runCount)}");
        }
    }
}