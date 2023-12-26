using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSW
{
    public class Code
    {
        public enum TempUploadFile
        {
            none = 0,
            上傳檔案 = 1  //瀏覽檔案送出(前)
        }

        public enum UploadFile
        {
            none = 0,
            上傳檔案 = 1  //送出(後)

        }
    }
}