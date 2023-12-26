using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSW.Controllers.Comm
{
    public class RSW2HtmlIFrameMenuDefAttribute : HtmlIFrameMenuDefAttribute
    {
        string _Url = null;
        public override string Url
        {
            set
            {
                if (_Url == null)
                {
                    var rurl = HttpContext.Current.Request.Url;
                    if (rurl.Host.Equals("127.0.0.1") || rurl.Host.Equals("localhost"))
                        _Url = "https://pj.ftis.org.tw/RSW2/" + value;
                    else
                        _Url = rurl.Scheme + "://" + rurl.Host + "/RSW2/" + value;

                }
            }
            get
            {
                return _Url;
            }
        }
    }
}