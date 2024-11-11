using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSW.Controllers.Path
{
    [MenuDef(Id = "CodePath", Name = "代碼資料", Index = 98, IsOnlyPath = true)]
    public class CodePathController : Controller
    {
        // GET: CodePath
        public ActionResult Index()
        {
            return View();
        }
    }
}