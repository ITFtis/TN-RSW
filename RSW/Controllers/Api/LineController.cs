using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RSW.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Web.Http;

namespace RSW.Controllers.Api
{
    public class LineController : ApiController
    {
        const string KEY_PREFIX = "LineWebhook-";

        //以 Post 接收 webhook 事件
        [HttpPost]
        public void Webhook(JToken tk)
        {
            //Newtonsoft.Json 套件，將 JSON 物件轉成字串
            var str = JsonConvert.SerializeObject(tk);
            //System.Runtime.Caching 組件，記憶體內部快取
            var cache = MemoryCache.Default;
            //將 JSON 字串暫存起來
            cache.Set(KEY_PREFIX + DateTime.Now.ToString("line_yyyyMMddHHmmss"), str, DateTime.Now.AddDays(1));
        }

        [HttpGet]
        [Route("api/line/show")]

        public string Show()
        {
            var cache = MemoryCache.Default;
            List<string> messages = new List<string>();
            foreach (var c in cache.Where(x=>x.Key.StartsWith(KEY_PREFIX) && x.Value != null))
            {
                messages.Add(c.Value.ToString());
            }
            return string.Join("\n", messages);
        }

        [HttpGet]
        [Route("api/line/test")]
        public void Test()
        {
            new LineMessageService().Push("TEST LINE MESSAGE PUSH: " + DateTime.Now );
        }

    }
}