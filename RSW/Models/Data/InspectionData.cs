namespace RSW.Models.Data
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using RSW;
    using RSW.Models.Manager;

    //using ZXing.QrCode.Internal;

    /// <summary>
    /// 巡檢紀錄維護
    /// </summary>
    [Table("InspectionData")]
    public partial class InspectionData
    {
        [Key]
        [Column(Order = 0)]
        [ColumnDef(Display = "序", Sortable = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ColumnDef(Display = "行政區", EditType = EditType.Select, SelectGearingWith = "stt_no,county_code", SelectItemsClassNamespace = CountySelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Contains, Visible = false, VisibleEdit = true, Sortable = true, Required = true)]
        public string county_code { get; set; }

        [ColumnDef(Display = "測站代號", EditType = EditType.Select, SelectItemsClassNamespace = NameSelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Contains, Sortable = true)]
        public string stt_no { get; set; }

        [ColumnDef(Display = "設備代號", VisibleEdit = false, Sortable = true)]
        public string dev_id
        {
            get
            {
                using (var cxt = TNModelContext.CreateTNModelContext())
                {
                    var me = new Dou.Models.DB.ModelEntity<BasicSttDev>(cxt).GetAll();
                    var stt = me.Where(m => m.stt_no == this.stt_no).FirstOrDefault();
                    return stt?.dev_id;
                }
            }
            set { }
        }

        [ColumnDef(Display = "維護日期", EditType = EditType.Date, Sortable = true, Required = true)]
        public DateTime? maintaintime { get; set; }

        [ColumnDef(Display = "維護人員", Required = true)]
        public string maintainer { get; set; }

        [ColumnDef(Display = "備註")]
        public string desc { get; set; }

        [Display(Name = "上傳檔案")]
        [Column(TypeName = "nvarchar")]
        [ColumnDef(Visible = false)]
        [StringLength(100)]
        public string FileReport { get; set; }

        [Display(Name = "上傳檔案DownLoad")]
        [ColumnDef(Visible = false)]
        public string FileReportUrl
        {
            get
            {
                string url = this.FileReport == null ? "" : this.FileReport;
                string path = "";
                path = FileHelper.GetFileFolder(Code.UploadFile.上傳檔案) + "/" + this.FileReport;
                url = RSW.Cm.PhysicalToUrl(path);

                var ary = url.Split('_');
                if (ary.Length == 2)
                {
                    string a = this.FileReport.Split('_')[0];  //資料夾(guid)
                    string b = this.FileReport.Split('_')[1];  //檔名
                    path = FileHelper.GetFileFolder(Code.UploadFile.上傳檔案) + a + "/" + b;
                    path = FileHelper.GetFileFolder(Code.UploadFile.上傳檔案)+ "/" + b;
                    url = RSW.Cm.PhysicalToUrl(path);
                }
              
                return url;
            }
        }
    }
}
