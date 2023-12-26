namespace RSW.Models.Data
{
    using Dou.Controllers;
    using Dou.Help;
    using Dou.Misc.Attr;
    using Dou.Models.DB;
    using RSW.Models.Manager;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    /// <summary>
    /// �s�׮ץ���@
    /// </summary>
    [Table("TroubleShootingData")]
    public partial class TroubleShootingData
    {
        [Key]
        [Column(Order = 0)]
        [ColumnDef(Display = "��", Sortable = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ColumnDef(Display = "�s�פH��", VisibleEdit = false, Sortable = true)]
        public string RepairMan
        {
            get
            {
                var UserName = Dou.Context.CurrentUser<User>().Name;
                return UserName;
            }
            set
            {
            }
        }
        [ColumnDef(Display = "�s�׳��", VisibleEdit = false, DefaultValue = "")]
        public string RepairUnit
        {
            get
            {
                //���X[User]��Id
                var RoleId = Dou.Context.CurrentUser<User>().RoleUsers.ToList().First().RoleId.ToString();
                //�A�Φ�Id�A�h[Role]�����XName
                var RoleName = new DouModelContextExt().Role.Where(s=>s.Id==RoleId).FirstOrDefault().Name.ToString();
                return RoleName;
            }
            set
            {
            }
        }

        [ColumnDef(Display = "��F��", EditType = EditType.Select, SelectGearingWith = "stt_no,county_code", SelectItemsClassNamespace = CountySelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Contains, Visible = false, VisibleEdit = true, Sortable = true, Required = true)]
        public string county_code { get; set; }

        [ColumnDef(Display = "�����N��", EditType = EditType.Select, SelectItemsClassNamespace = NameSelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Contains, Visible = false, Sortable = true)]
        public string stt_no { get; set; }

        [ColumnDef(Display = "�]�ƥN�X", VisibleEdit = false, Sortable = true)]
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

        [ColumnDef(Display = "�s�׭�]", Required = true)]
        public string RepairReason { get; set; }

        [ColumnDef(Display = "�s�׮ɶ�", EditType = EditType.Date, Sortable = true, Required = true)]
        public DateTime? RepairTime { get; set; }

        [ColumnDef(Display = "�����ɶ�", EditType = EditType.Date, Sortable = true)]
        public DateTime? CompleteTime { get; set; }

        [ColumnDef(Display = "�״_���p")]
        public string RepairStatus{ get; set; }

        [ColumnDef(Display = "�O�_����", EditType = EditType.Radio, SelectItems = "{\"true\":\"�O\",\"false\":\"�_\"}", DefaultValue = "false"
            , Required = true, Sortable = true)]
        public bool isClosed { get; set; }
    }
}
