using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace RSW
{
    public class ValidateHelper
    {
        /// <summary>
        /// 驗身分證
        /// </summary>
        /// <param name="id">身分證字號</param>
        /// <returns>身分字號是否符合格式</returns>
        public static bool CheckPid(string id)
        {
            var format = new Regex(@"^[A-Z]\d{9}$");
            if (!format.IsMatch(id)) return false;

            id = id.ToUpper();

            var a = new[]
            {
        10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35, 23, 24, 25, 26, 27, 28, 29, 32, 30, 31, 33
    };

            var b = new int[11];

            b[1] = a[(id[0]) - 65] % 10;

            var c = b[0] = a[(id[0]) - 65] / 10;

            for (var i = 1; i <= 9; i++)
            {
                b[i + 1] = id[i] - 48;
                c += b[i] * (10 - i);
            }

            return ((c % 10) + b[10]) % 10 == 0;
        }
    }
}