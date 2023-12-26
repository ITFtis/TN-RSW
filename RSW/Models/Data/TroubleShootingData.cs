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
    /// 叫修案件維護
    /// </summary>
    [Table("TroubleShootingData")]
    public partial class TroubleShootingData
    {
        [Key]
        [Column(Order = 0)]
        [ColumnDef(Display = "序", Sortable = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ColumnDef(Display = "叫修人員", VisibleEdit = false, Sortable = true)]
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
        [ColumnDef(Display = "叫修單位", VisibleEdit = false, DefaultValue = "")]
        public string RepairUnit
        {
            get
            {
                //取出[User]的Id
                var RoleId = Dou.Context.CurrentUser<User>().RoleUsers.ToList().First().RoleId.ToString();
                //再用此Id再去[Role]比對取出Name
                var RoleName = new DouModelContextExt().Role.Where(s=>s.Id==RoleId).FirstOrDefault().Name.ToString();
                return RoleName;
            }
            set
            {
            }
        }

        [ColumnDef(Display = "行政區", EditType = EditType.Select, SelectGearingWith = "stt_no,county_code", SelectItemsClassNamespace = CountySelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Contains, Visible = false, VisibleEdit = true, Sortable = true, Required = true)]
        public string county_code { get; set; }

        [ColumnDef(Display = "測站代號", EditType = EditType.Select, SelectItemsClassNamespace = NameSelectItemsClassImp.AssemblyQualifiedName
            , Filter = true, FilterAssign = FilterAssignType.Contains, Visible = false, Sortable = true)]
        public string stt_no { get; set; }

        [ColumnDef(Display = "設備代碼", VisibleEdit = false, Sortable = true)]
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

        [ColumnDef(Display = "叫修原因", Required = true)]
        public string RepairReason { get; set; }

        [ColumnDef(Display = "叫修時間", EditType = EditType.Date, Sortable = true, Required = true)]
        public DateTime? RepairTime { get; set; }

        [ColumnDef(Display = "完成時間", EditType = EditType.Date, Sortable = true)]
        public DateTime? CompleteTime { get; set; }

        [ColumnDef(Display = "修復情況")]
        public string RepairStatus{ get; set; }

        [ColumnDef(Display = "是否結案", EditType = EditType.Radio, SelectItems = "{\"true\":\"是\",\"false\":\"否\"}", DefaultValue = "false"
            , Required = true, Sortable = true)]
        public bool isClosed { get; set; }
    }
}
