using RSW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSW.Services
{
    public class DataService
    {
        private static DouModelContextExt dbContext = new DouModelContextExt();
        public static IQueryable<T> GetData<T>(string sql, object[] parameters)
        {
            return dbContext.Database.SqlQuery<T>(sql, parameters).AsQueryable();
        }
    }
}