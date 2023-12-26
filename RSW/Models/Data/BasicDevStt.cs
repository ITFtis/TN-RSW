namespace RSW.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// �]�ư򥻸��
    /// </summary>
    public partial class BasicDevStt
    {
        /// <summary>
        /// �]�ƥN��
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// �����N��
        /// </summary>
        [StringLength(255)]
        public string stt_no { get; set; }
        /// <summary>
        /// �����W��
        /// </summary>
        [StringLength(255)]
        public string stt_name { get; set; }
        /// <summary>
        /// �m����
        /// </summary>
        [StringLength(255)]
        public string county_code { get; set; }
        /// <summary>
        /// �H�սs��
        /// </summary>
        [StringLength(255)]
        public string manhole_num { get; set; }
        /// <summary>
        /// �g��
        /// </summary>
        public double? lon { get; set; }
        /// <summary>
        /// �n��
        /// </summary>
        public double? lat { get; set; }
        /// <summary>
        /// ĵ�٤���1
        /// </summary>
        public double? alarm1 { get; set; }
        /// <summary>
        /// ĵ�٤���2
        /// </summary>
        public double? alarm2 { get; set; }
        /// <summary>
        /// ĵ�٤���3
        /// </summary>
        public double? alarm3 { get; set; }
        /// <summary>
        /// �a�}
        /// </summary>
        [StringLength(255)]
        public string addr { get; set; }
        /// <summary>
        /// �����ȰѦү��I
        /// </summary>
        [StringLength(255)]
        public string ref_dev_id { get; set; }
        /// <summary>
        /// �Ƶ�
        /// </summary>
        [StringLength(255)]
        public string desc { get; set; }
    }

    /// <summary>
    /// FL�]�ư򥻸��
    /// </summary>
    public partial class BasicDevStt_FL
    {
        /// <summary>
        /// �]�ƥN��
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// �����N��
        /// </summary>
        [StringLength(255)]
        public string stt_no { get; set; }
        /// <summary>
        /// �����W��
        /// </summary>
        [StringLength(255)]
        public string stt_name { get; set; }
        /// <summary>
        /// �m����
        /// </summary>
        [StringLength(255)]
        public string county_code { get; set; }
        /// <summary>
        /// �H�սs��
        /// </summary>
        [StringLength(255)]
        public string manhole_num { get; set; }
        /// <summary>
        /// �g��
        /// </summary>
        public double? lon { get; set; }
        /// <summary>
        /// �n��
        /// </summary>
        public double? lat { get; set; }
        /// <summary>
        /// �a�}
        /// </summary>
        [StringLength(255)]
        public string addr { get; set; }
        /// <summary>
        /// �Ƶ�
        /// </summary>
        [StringLength(255)]
        public string desc { get; set; }
    }

    /// <summary>
    /// �]�ư򥻸��
    /// </summary>
    public partial class BasicDevSttNotify
    {
        /// <summary>
        /// �]�ƥN��
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// �����N��
        /// </summary>
        [StringLength(255)]
        public string stt_no { get; set; }
        /// <summary>
        /// �����W��
        /// </summary>
        [StringLength(255)]
        public string stt_name { get; set; }
        /// <summary>
        /// �m����
        /// </summary>
        [StringLength(255)]
        public string county_code { get; set; }
        /// <summary>
        /// �H�սs��
        /// </summary>
        [StringLength(255)]
        public string manhole_num { get; set; }
        /// <summary>
        /// �g��
        /// </summary>
        public double? lon { get; set; }
        /// <summary>
        /// �n��
        /// </summary>
        public double? lat { get; set; }
        /// <summary>
        /// ĵ�٤���1
        /// </summary>
        public double? alarm1 { get; set; }
        /// <summary>
        /// ĵ�٤���2
        /// </summary>
        public double? alarm2 { get; set; }
        /// <summary>
        /// ĵ�٤���3
        /// </summary>
        public double? alarm3 { get; set; }
        /// <summary>
        /// �a�}
        /// </summary>
        [StringLength(255)]
        public string addr { get; set; }
        /// <summary>
        /// �����ȰѦү��I
        /// </summary>
        [StringLength(255)]
        public string ref_dev_id { get; set; }
        /// <summary>
        /// �Ƶ�
        /// </summary>
        [StringLength(255)]
        public string desc { get; set; }
        /// <summary>
        /// �O�_�o��ĵ��
        /// </summary>
        public bool IsAlert { get; set; }
    }
}
