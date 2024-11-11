using RSW.Models.Data;
using RSW.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace RSW.Models
{
    public partial class DouModelContextExt : Dou.Models.ModelContextBase<User, Role>
    {

        //public static DouModelContextExt CreateDouModelContext(bool printLog = false)
        //{
        //    var cxt = new DouModelContextExt();
        //    if (printLog) cxt.Database.Log = (log) => Debug.WriteLine(log);
        //    return cxt;
        //}
        public DouModelContextExt()
            : base("name=DouModelContextExt")
        {
            Database.SetInitializer<DouModelContextExt>(null);
        }

        public virtual DbSet<Disaster> Disaster { get; set; }

        //public new virtual DbSet<Role> Role { get; set; }

        //static object lockGetAllRoles = new object();

        /// <summary>
        /// 取得所有腳色
        /// </summary>
        /// <returns></returns>
        //public static IEnumerable<Manager.Role> GetAllRoles()
        //{
        //    int cachetimer = 5 * 60 * 1000;
        //    string key = "RSW.Models.Manager.Roles";
        //    var allRoles = DouHelper.Misc.GetCache<IEnumerable<Role>>(cachetimer, key);
        //    lock (lockGetAllRoles)
        //    {
        //        if (allRoles == null)
        //        {
        //            using (var cxt = CreateDouModelContext())
        //            {
        //                allRoles = cxt.Role.ToArray();
        //                DouHelper.Misc.AddCache(allRoles, key);
        //            }
        //        }
        //    }
        //    return allRoles;
        //}
        //public static void ResetGetAllRoles()
        //{
        //    string key = "RSW.Models.Manager.Roles";
        //    DouHelper.Misc.ClearCache(key);
        //}
    }
}