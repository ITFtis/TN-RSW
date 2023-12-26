using Dou.Controllers;
using Dou.Misc;
using Dou.Models.DB;
using RSW.Models;
using RSW.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;

namespace RSW.Controllers.Prj
{
    public class RealTimeDataController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            //base.OnActionExecuting(ctx);
        }

        // GET: Data
        public ActionResult Index()
        {
            using (var db = new TNModelContext())
            {
                var alldatas = (from a in db.RealTimeStt
                                where a.desc != "" 
                                orderby a.inserttime descending
                                select new RealTimeStt4
                                {
                                    desc = a.desc,
                                    datatime = a.datatime,
                                    inserttime = a.inserttime,
                                    delaySec = SqlFunctions.DateDiff("second", a.datatime, a.inserttime)
                                }).Take(1000).ToList();
                return View(alldatas);
            }
        }
    }
}