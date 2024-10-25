using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace RSW.Services
{
    public class LineMessageService
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private class LineMessageSettings
        {
            public string CHANNEL_ID { get; set; }
            public string CHANNEL_SECRET { get; set; }
            public string CHANNEL_TOKEN { get; set; }
            public string TO_GROUP_ID { get; set; }
        }

        private static LineMessageSettings SETTINGS = GetLineMessageSettings();

        private static LineMessageSettings GetLineMessageSettings()
        {
            string CHANNEL_ID = ConfigurationManager.AppSettings["CHANNEL_ID"].ToString();
            string CHANNEL_SECRET = ConfigurationManager.AppSettings["CHANNEL_SECRET"].ToString();
            string CHANNEL_TOKEN = ConfigurationManager.AppSettings["CHANNEL_TOKEN"].ToString();
            string TO_GROUP_ID = ConfigurationManager.AppSettings["TO_GROUP_ID"].ToString();

            LineMessageSettings s = new LineMessageSettings
            {
                CHANNEL_ID = CHANNEL_ID,
                CHANNEL_SECRET = CHANNEL_SECRET,
                CHANNEL_TOKEN = CHANNEL_TOKEN,
                TO_GROUP_ID = TO_GROUP_ID
            };
            logger.Info("已載入 Line API 設定");
            return s;
        }
        public string Push(string text)
        {
            try
            {
                //JSON訊息
                var data = new
                {
                    // 填入 Webhook 接收到的 userId
                    to = SETTINGS.TO_GROUP_ID,
                    messages = new List<object>(){
                new {
                    type = "text",
                    text,
                }
                    },
                };

                //傳送訊息
                string line_request_id = null;
                using (var wc = new WebClient())
                {
                    //Channel access token
                    var channelAccessToken = SETTINGS.CHANNEL_TOKEN;
                    wc.Headers.Add("Authorization", $"Bearer {channelAccessToken}");
                    wc.Headers.Add("Content-Type", $"application/json");
                    wc.Encoding = Encoding.UTF8;
                    var dataStr = JsonConvert.SerializeObject(data);
                    var dataBytes = Encoding.UTF8.GetBytes(dataStr);
                    byte[] ret = wc.UploadData($"https://api.line.me/v2/bot/message/push", "post", dataBytes);
                    WebHeaderCollection headers = wc.ResponseHeaders;
                    for (int i = 0; i < headers.Count; i++)
                    {
                        var key = headers.GetKey(i);
                        if (key.ToLower() == "x-line-request-id")
                        {
                            line_request_id = headers.Get(i);
                            break;
                        }
                    }
                }
                logger.Info("Line訊息發送成功");

                return "OK-" + line_request_id;
            }
            catch (WebException e)
            {
                logger.Error("Line訊息發送失敗");
                string responseText;

                using (var reader = new StreamReader(e.Response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                }
                string msg = "ERROR(" + e.Status + ")-" + responseText;
                logger.Error("錯誤內容為：" + msg);
                return msg;
            }
        }
    }
}