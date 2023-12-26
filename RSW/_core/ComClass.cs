using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSW._core
{
    public class ComClass
    {
    }

    #region 匯入縣市鄉鎮路(citytown.json)

    //匯入縣市鄉鎮路(citytown.json)
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class CityTown
    {
        public string CityName { get; set; }
        public string CityEngName { get; set; }
        public List<AreaList> AreaList { get; set; }
    }

    //鄉鎮(citytown.json)
    public class AreaList
    {
        public string ZipCode { get; set; }
        public string AreaName { get; set; }
        public string AreaEngName { get; set; }
        public List<RoadList> RoadList { get; set; }
    }

    //路(citytown.json)
    public class RoadList
    {
        public string RoadName { get; set; }
        public string RoadEngName { get; set; }
    }

    #endregion
}