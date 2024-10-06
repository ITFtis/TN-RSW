using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RSW.Services
{
    public class DataService
    {
        public const string DB_DOU = "DouModelContextExt";
        public const string DB_TN = "TNModelContext";

        public static IQueryable<T> GetData<T>(string db, string sql)
        {
            return GetData<T>(db, sql, new SqlParameter[0]);
        }

        public static IQueryable<T> GetData<T>(string db, string sql, SqlParameter[] parameters)
        {
            DbContext dbContext = new DbContext(db);
            return dbContext.Database.SqlQuery<T>(sql, parameters).AsQueryable();
        }
    }
}
