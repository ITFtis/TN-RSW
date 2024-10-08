using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace RSW.Services
{
    public class LineMessageService
    {
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
            LineMessageSettings s = JsonConvert.DeserializeObject<LineMessageSettings>(
                File.ReadAllText("./LineApi.json")
                );
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

                return "OK-" + line_request_id;
            }
            catch (WebException e)
            {
                string responseText;

                using (var reader = new StreamReader(e.Response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                }
                return "ERROR(" + e.Status + ")-" + responseText;
            }
        }
    }
}