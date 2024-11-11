using Dou.Controllers;
using Dou.Misc.Attr;
using RSW.Models;
using RSW.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSW.Controllers.Prj
{
    [MenuDef(Id = "Disaster", Name = "災害事件", MenuPath = "代碼資料", Index = 1, Action = "Index", Func = FuncEnum.ALL)]
    public class DisasterController : AGenericModelController<Disaster>
    {
        // GET: Disaster
        public ActionResult Index()
        {
            return View();
        }
        protected override Dou.Models.DB.IModelEntity<Disaster> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<Disaster>(new DouModelContextExt());
        }
    }
}