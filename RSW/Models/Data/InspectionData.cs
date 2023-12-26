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
    /// ���ˬ������@
    /// </summary>
    [Table("InspectionData")]
    public partial class InspectionData
    {
        [Key]
        [Column(Order = 0)]
        [ColumnDef(Display = "��", Sortable = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ColumnDef(Display = "��F��", EditType = EditType.Select, SelectGearingWith = "stt_no,county_code", SelectItemsClassNamespace = CountySelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Contains, Visible = false, VisibleEdit = true, Sortable = true, Required = true)]
        public string county_code { get; set; }

        [ColumnDef(Display = "�����N��", EditType = EditType.Select, SelectItemsClassNamespace = NameSelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Contains, Sortable = true)]
        public string stt_no { get; set; }

        [ColumnDef(Display = "�]�ƥN��", VisibleEdit = false, Sortable = true)]
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

        [ColumnDef(Display = "���@���", EditType = EditType.Date, Sortable = true, Required = true)]
        public DateTime? maintaintime { get; set; }

        [ColumnDef(Display = "���@�H��", Required = true)]
        public string maintainer { get; set; }

        [ColumnDef(Display = "�Ƶ�")]
        public string desc { get; set; }

        [Display(Name = "�W���ɮ�")]
        [Column(TypeName = "nvarchar")]
        [ColumnDef(Visible = false)]
        [StringLength(100)]
        public string FileReport { get; set; }

        [Display(Name = "�W���ɮ�DownLoad")]
        [ColumnDef(Visible = false)]
        public string FileReportUrl
        {
            get
            {
                string url = this.FileReport == null ? "" : this.FileReport;
                string path = "";
                path = FileHelper.GetFileFolder(Code.UploadFile.�W���ɮ�) + "/" + this.FileReport;
                url = RSW.Cm.PhysicalToUrl(path);

                var ary = url.Split('_');
                if (ary.Length == 2)
                {
                    string a = this.FileReport.Split('_')[0];  //��Ƨ�(guid)
                    string b = this.FileReport.Split('_')[1];  //�ɦW
                    path = FileHelper.GetFileFolder(Code.UploadFile.�W���ɮ�) + a + "/" + b;
                    path = FileHelper.GetFileFolder(Code.UploadFile.�W���ɮ�)+ "/" + b;
                    url = RSW.Cm.PhysicalToUrl(path);
                }
              
                return url;
            }
        }
    }
}
