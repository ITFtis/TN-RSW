using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSW
{
    
    public class AppSet
    {
        //地籍圖token url
        public string CadastreApiGetSignCodeUrl { set; get; }
        //地籍圖data url
        public string CadastreApiGetDataUrl { set; get; }
        //地籍圖應用程式ID
        public string CadastreApID { set; get; }
        //地籍圖應用程式密碼
        public string CadastreApPSWD { set; get; }
        //台南水利局Api
        public string TainanApiUrl { set; get; }
        //累計雨量圖
        public string CWBDayRainfallUrl { set; get; }
        //雷達回波圖
        public string CWBRadUrl { set; get; }
        //Qpeums預報1小時
        public string CWBQpf1h { set; get; }
    }
}