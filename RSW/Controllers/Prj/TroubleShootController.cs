using Dou.Controllers;
using Dou.Misc;
using Dou.Misc.Attr;
using Dou.Models.DB;
using RSW.Models;
using RSW.Models.Data;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace RSW.Controllers.Prj
{
    [MenuDef(Id = "TroubleShoot", Name = "叫修案件維護", MenuPath = "即時監控資訊", Action = "Index", Index = 106, Func = FuncEnum.ALL, AllowAnonymous = false)]
    public class TroubleShootController : Dou.Controllers.APaginationModelController<TroubleShootingData>
    {
        // GET: Country
        public ActionResult Index()
        {
            return View();
        }

        protected override IQueryable<TroubleShootingData> BeforeIQueryToPagedList(IQueryable<TroubleShootingData> iquery, params KeyValueParams[] paras)
        { 
            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var options = base.GetDataManagerOptions();
            options.GetFiled("RepairMan").editable = false;
            options.GetFiled("RepairUnit").editable = false;
            options.GetFiled("dev_id").editable = false;
            return options;
        }

        protected override IModelEntity<TroubleShootingData> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<TroubleShootingData>(new TNModelContext());
        }
    }
}