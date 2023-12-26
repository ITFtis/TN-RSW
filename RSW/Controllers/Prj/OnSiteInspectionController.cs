using Dou.Controllers;
using Dou.Misc;
using Dou.Misc.Attr;
using Dou.Models.DB;
using RSW.Models;
using RSW.Models.Data;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
//using ZXing.QrCode.Internal;

namespace RSW.Controllers.Prj
{
    [MenuDef(Id = "OnSiteInspection", Name = "巡檢紀錄維護", MenuPath = "即時監控資訊", Action = "Index", Index = 105, Func = FuncEnum.ALL, AllowAnonymous = false)]
    public class OnSiteInspectionController : Dou.Controllers.APaginationModelController<InspectionData>
    {
        // GET: Country
        public ActionResult Index()
        {
            return View();
        }

        protected override IQueryable<InspectionData> BeforeIQueryToPagedList(IQueryable<InspectionData> iquery, params KeyValueParams[] paras)
        {
            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        protected override void AddDBObject(IModelEntity<InspectionData> dbEntity, IEnumerable<InspectionData> objs)
        {
            base.AddDBObject(dbEntity, objs);
            //移動檔案
            FileHelper.MoveFileF1(objs.First().FileReport, Code.TempUploadFile.上傳檔案, Code.UploadFile.上傳檔案);
        }

        protected override void UpdateDBObject(IModelEntity<InspectionData> dbEntity, IEnumerable<InspectionData> objs)
        {
            int id = objs.First().ID;
            Dou.Models.DB.IModelEntity<InspectionData> model = GetModelEntity();
            var ori = model.GetAll(a => a.ID == id).First();

            base.UpdateDBObject(dbEntity, objs);

            //Update(檔案挑選變動)        
            if (ori.FileReport != objs.First().FileReport)
            {
            //移動檔案
            //FileHelper.MoveFileF1(objs.First().FileReport, Code.TempUploadFile.上傳檔案, Code.UploadFile.上傳檔案);

                //刪除檔案(舊檔案)
                FileHelper.DeleteFileF1(ori.FileReport, Code.UploadFile.上傳檔案);
            }
            FileHelper.MoveFileF1(objs.First().FileReport, Code.TempUploadFile.上傳檔案, Code.UploadFile.上傳檔案);
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var opts = base.GetDataManagerOptions();
            opts.GetFiled("dev_id").editable = false;
            //共用頁面
            //opts.editformWindowStyle = "showEditformOnly";

            return opts;
        }

        protected override IModelEntity<InspectionData> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<InspectionData>(new TNModelContext());
        }

        //上傳檔案
        public ActionResult UpFile()
        {
            string url = "";
            string fileName = "";

            try
            {
                string folder = FileHelper.GetFileFolder(Code.TempUploadFile.上傳檔案);
                string uploadfolder = FileHelper.GetFileFolder(Code.UploadFile.上傳檔案);

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                HttpPostedFileBase file = Request.Files[0];
                //fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                //string path = folder + fileName;
                int counter = 0;
                string extension = System.IO.Path.GetExtension(file.FileName);
                string newFileName = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                string newFullPath = folder + file.FileName;
                string newuploadFullPath = uploadfolder + file.FileName;
                fileName = file.FileName;
                //檢測上傳檔案是否已存在，若已存在則檔案流水號+1
                while (System.IO.File.Exists(newuploadFullPath))
                {
                    fileName = counter == 0 ? string.Format("{0}{1}", newFileName, extension) : 
                        string.Format("{0}({1}){2}", newFileName, counter, extension);
                    newFullPath = folder + fileName;
                    newuploadFullPath = uploadfolder + fileName;
                    counter++;
                }
            
                string path = newFullPath;

                if (file.ContentLength > 0)
                {
                    file.SaveAs(path);
                }

                url = RSW.Cm.PhysicalToUrl(path);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, errorMessage = ex.Message });
            }

            return Json(new { result = true, url = url, fileName = fileName }, JsonRequestBehavior.AllowGet);
        }
    }
}