using Dou.Controllers;
using Dou.Help;
using Dou.Misc;
using Dou.Misc.Attr;
using Dou.Models.DB;
using RSW.Models;
using RSW.Models.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Web;
//using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RSW.Controllers.Manager
{
    // GET: User
    [Dou.Misc.Attr.MenuDef(Id ="User", Name = "使用者管理", MenuPath = "系統管理", Action = "Index", Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class UserController : Dou.Controllers.UserBaseControll<User, Role>
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable<User> GetDataDBObject(IModelEntity<User> dbEntity, params KeyValueParams[] paras)
        {
            string AccountCreateTime_Start_Between = HelperUtilities.GetFilterParaValue(paras, "AccountCreateTime-Start-Between_");
            string AccountCreateTime_End_Between = HelperUtilities.GetFilterParaValue(paras, "AccountCreateTime-End-Between_");
            if (!string.IsNullOrEmpty(AccountCreateTime_Start_Between) && !string.IsNullOrEmpty(AccountCreateTime_End_Between))
            {
                //所有的使用者
                var allUser = dbEntity.GetAll().ToArray();
                //篩選創建時間
                var UserByCraeteTime = allUser.Where(x=>x.AccountCreateTime >= DateTime.Parse(AccountCreateTime_Start_Between)
                                        && x.AccountCreateTime <= DateTime.Parse(AccountCreateTime_End_Between).AddDays(1)).ToList();
                return UserByCraeteTime;
            }
            var obj = base.GetDataDBObject(dbEntity, paras);
            return obj;
        }

        protected override Dou.Models.DB.IModelEntity<User> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<User>(RoleController._dbContext);
        }


        protected override void AddDBObject(IModelEntity<User> dbEntity, IEnumerable<User> objs)
        {
            try
            {
                //更新user的帳號創建時間
                foreach (var item in objs)
                {
                    item.AccountCreateTime = DateTime.Now;
                }
                base.AddDBObject(dbEntity, objs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void UpdateDBObject(IModelEntity<User> dbEntity, IEnumerable<User> objs)
        {
            try
            {
                base.UpdateDBObject(dbEntity, objs);
            }catch (Exception ex)
            {
                throw ex;
            }
        }
        internal static string TNWRB_SSO_MESSAGE = "~TNWRB_SSO_MESSAGE~";
        public static string TNWRB_SSO_PORTAL = "https://wrbweb.tainan.gov.tw/wrbportal/";
        internal static string TNWRB_SSO_GETUSERDATA = "https://wrbweb.tainan.gov.tw/wrbportal/api/UserApi/GetUserData";
        internal static string TNWRB_SSO_KEY = "cf1f4d5318c6b0a76eb889ba14443cbeede151b5aeaa940df803100c7b804f19";
        internal static string TNWRB_SSO_SAFETYCODE = "25681";

        internal static string LOGIN_FROM_SSO_KEY = "~LOGIN_FROM_SSO_KEY~";
        public override ActionResult DouLogin(User user, string returnUrl, bool redirectLogin = false)
        {
            #region SSO 訊息格式
            //{{
            //"isSuccess": true,
            //"Message": {
            //  "A_Account": "NCKU",
            //  "A_UserName": "成大防災-監測組",
            //  "A_Department": "成大防災-監測組",
            //  "A_Email": "NCKU@yahoo.com.tw",
            //  "A_Tel": null,
            //  "A_MobilePhone": null,
            //  "A_Address": null,
            //  "StartDateTime": "2023-07-13T10:18:54.72",
            //  "EndDateTime": "2023-07-13T10:48:49.887"
            //}
            //      }
            //  }
            #endregion
            dynamic ssomessage = Dou.Help.DouUnobtrusiveSession.Session[TNWRB_SSO_MESSAGE];
            if (ssomessage != null)
            {
                Dou.Help.DouUnobtrusiveSession.Session.Add(LOGIN_FROM_SSO_KEY, true);
               //ssomessage.isSuccess 必是true, false已於Start.OnAModelControllerActionExecuting排除

                dynamic ssouser = ssomessage.Message;
                string ssouid = ssouser.A_Account;
                string ssouname = ssouser.A_UserName;//姓名
                User u = FindUser(ssouid);//已驗證，故直接取系統使用者
                redirectLogin = false;
                if (u != null)
                {
                    user = u;
                }
                else//系統尚無此使用者
                {
                    user = new User() { Id = ssouid, Name = ssouname, Password = Dou.Context.Config.PasswordEncode(Dou.Context.Config.DefaultPassword), Enabled = false, VerifyErrorCount=0, LastRenewPassword = DateTime.Now };
                    this.AddDBObject(GetModelEntity(), new User[] { user });
                }
            }

            ActionResult v = null;
            if (ViewBag.ErrorMessage == null)
                v = base.DouLogin(user, returnUrl, redirectLogin);
            if (ViewBag.ErrorMessage != null)
            {
                if (ssomessage != null)
                    return PartialView("SsoMessage", user);//, new { u=user, msg= ViewBag.ErrorMessage });
                else
                    return v;
            }
            else
            {
                Dou.Help.DouUnobtrusiveSession.Session.Remove(TNWRB_SSO_MESSAGE);//移除SSO資訊，ErrorMessage != null不清除避免refresh進入DouLogin畫面
                return v is RedirectResult || v is RedirectToRouteResult ? v : PartialView(user);
            }
        }

        public override ActionResult DouLogoff()
        {
            var r=base.DouLogoff();
            bool? f = (bool?)Dou.Help.DouUnobtrusiveSession.Session[LOGIN_FROM_SSO_KEY];
            if (f != null && f.Value) //從單一遷入Login
                r = new RedirectResult(TNWRB_SSO_PORTAL);
            Dou.Help.DouUnobtrusiveSession.Session.Remove(LOGIN_FROM_SSO_KEY);
            return r;
        }
        [AllowAnonymous]
        public ActionResult SsoMessagePageRelogin()
        {
            Dou.Help.DouUnobtrusiveSession.Session.Remove(TNWRB_SSO_MESSAGE);
            DouLogoff();
            return new RedirectResult(TNWRB_SSO_PORTAL);
        }
        [AllowAnonymous]
        public ActionResult SsoMessage()
        {
            ViewBag.Message = Dou.Help.DouUnobtrusiveSession.Session["SsoMessage"] ;
            Dou.Help.DouUnobtrusiveSession.Session.Remove("SsoMessage");
            return View();
        }
        //[AllowAnonymous]
        //public ActionResult SkipSso()
        //{
        //    //DouUnobtrusiveSession.Session.Add(SkipSsoKey, true);
        //    return base.DouLogin();
        //}
    }

}