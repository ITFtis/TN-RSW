using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CWB_NCKU_Data.Services
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

        public static int ExecuteDataSql<T>(string db, string sql, T data)
        {
            List<SqlParameter> p = new List<SqlParameter>();
            var t = data.GetType();
            foreach(var f in t.GetProperties())
            {
                p.Add(new SqlParameter(f.Name, f.GetValue(data)));
            }

            DbContext dbContext = new DbContext(db);
            return dbContext.Database.ExecuteSqlCommand(sql, p.ToArray());
        }

        public static byte[] DownloadData(string url)
        {
            var client = new WebClient();
            Uri uri = url.Contains(":") ? new Uri(url) : new Uri(Path.GetFullPath(url));
            byte[] data = client.DownloadData(uri);
            return data;
        }
    }
}