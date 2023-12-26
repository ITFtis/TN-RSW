namespace RSW.Models.Data
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    /// <summary>
    /// ���x�򥻸��
    /// </summary>
    [Table("RealTimeStt")]
    public partial class RealTimeStt
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(255)]
        public string dev_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime? datatime { get; set; }

        public double? val02 { get; set; }

        public double? val61 { get; set; }

        public double? voltage { get; set; }

        public double? baro { get; set; }

        public double? rssi { get; set; }

        public double? alarm1 { get; set; }

        public double? alarm2 { get; set; }

        public double? alarm3 { get; set; }

        public bool? uptype { get; set; }

        public DateTime? inserttime { get; set; }

        [StringLength(2000)]
        public string desc { get; set; }
    }

    public partial class RealTimeStt2
    {
        /// <summary>
        /// �]�ƥN�X
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// ��Ʈɶ�
        /// </summary>
        public DateTime? datatime { get; set; }
        /// <summary>
        /// �ʴ��ƾ�(���O������)
        /// </summary>
        public double? val02 { get; set; }
        /// <summary>
        /// �ʴ��ƾ�(���O/�ƦX������)
        /// </summary>
        public double? level { get; set; }
        /// <summary>
        /// �ʴ��ƾ�(�ƦX������)
        /// </summary>
        public double? val61 { get; set; }
    }

    //�Ҧ����쯸�Y�ɸ�ƥΨ쪺���
    public partial class RealTimeStt3
    {
        /// <summary>
        /// �]�ƥN�X
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// ��Ʈɶ�
        /// </summary>
        public DateTime? datatime { get; set; }
        /// <summary>
        /// �ʴ��ƾ�(���O������)
        /// </summary>
        public double? val02 { get; set; }
        /// <summary>
        /// �ʴ��ƾ�(�ƦX������)
        /// </summary>
        public double? val61 { get; set; }
        /// <summary>
        /// �Y�ɹq��
        /// </summary>
        public double? voltage { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public double? baro { get; set; }
        /// <summary>
        /// �T���j��
        /// </summary>
        public double? rssi { get; set; }
        /// <summary>
        /// �W�ǼҦ�
        /// </summary>
        public bool? uptype { get; set; }
        /// <summary>
        /// ��ƶǤJ�ɶ�
        /// </summary>
        public DateTime? inserttime { get; set; }
        /// <summary>
        /// �ʴ��ƾ�(���O/�ƦX������)
        /// </summary>
        public double? level { get; set; }
    }

    //�Ҧ��y�t���Y�ɸ�ƥΨ쪺���
    public partial class RealTimeStt_FL
    {
        /// <summary>
        /// �]�ƥN�X
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// ��Ʈɶ�
        /// </summary>
        public DateTime? datatime { get; set; }
        /// <summary>
        /// �ʴ��ƾ�(�ƦX������)
        /// </summary>
        public double? FL { get; set; }
        /// <summary>
        /// �Y�ɹq��
        /// </summary>
        public double? voltage { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public double? baro { get; set; }
        /// <summary>
        /// �T���j��
        /// </summary>
        public double? rssi { get; set; }
        /// <summary>
        /// �W�ǼҦ�
        /// </summary>
        public bool? uptype { get; set; }
        /// <summary>
        /// ��ƶǤJ�ɶ�
        /// </summary>
        public DateTime? inserttime { get; set; }
    }

    //�Ҧ��y�t���Y�ɸ�ƥΨ쪺���
    public partial class RealTimeStt_FL_His
    {
        /// <summary>
        /// �]�ƥN�X
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// ��Ʈɶ�
        /// </summary>
        public DateTime? datatime { get; set; }
        /// <summary>
        /// �ʴ��ƾ�(�ƦX������)
        /// </summary>
        public double? FL { get; set; }
    }

    //UI�d�ߩҦ���ƥ�
    public partial class RealTimeStt4
    {
        /// <summary>
        /// ��l���
        /// </summary>
        [StringLength(2000)]
        public string desc { get; set; }
        /// <summary>
        /// ��Ʈɶ�
        /// </summary>
        public DateTime? datatime { get; set; }
        /// <summary>
        /// ��ƶǤJ�ɶ�
        /// </summary>
        public DateTime? inserttime { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public int? delaySec { get; set; }
    }

    /// <summary>
    /// UI�Y�ɸ�ƥΨ쪺���
    /// </summary>
    public partial class RealTimeStt5
    {
        [Key]
        public string dev_id { get; set; }
        /// <summary>
        /// �]�ƥN�X
        /// </summary>
        public string stt_no { get; set; }
        /// �ʴ��ƾ�(���O������)
        /// </summary>
        public double? val02 { get; set; }
        /// <summary>
        /// �ʴ��ƾ�(�ƦX������)
        /// </summary>
        public double? val61 { get; set; }
        /// <summary>
        /// �Y�ɹq��
        /// </summary>
        public double? voltage { get; set; }
        /// <summary>
        /// �T���j��
        /// </summary>
        public double? rssi { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public double? wdepth { get; set; }
        /// <summary>
        /// ���ް��{
        /// </summary>
        public double? alarm2 { get; set; }
        /// <summary>
        /// �b���ް��{
        /// </summary>
        public double? alarm3 { get; set; }
        /// <summary>
        /// �ʴ��ƾ�(���O/�ƦX������)
        /// </summary>
        public double? level { get; set; }
        /// <summary>
        /// ��Ʈɶ�
        /// </summary> 
        public DateTime? datatime { get; set; }
    }

    /// <summary>
    /// cpami history�Y�ɸ�ƥΨ쪺���
    /// </summary>
    public partial class RealTimeStt6
    {
        [Key]
        /// </summary>
        /// <summary>
        /// �]�ƥN�X
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// �����N�X
        /// </summary>
        public string stt_no { get; set; }
        /// <summary>
        /// ��Ʈɶ�
        /// </summary> 
        public DateTime? datatime { get; set; }
        /// <summary>
        /// �ʴ��ƾ�(���O������)
        /// </summary>
        public double? val02 { get; set; }
        /// <summary>
        /// �ʴ��ƾ�(���O/�ƦX������)
        /// </summary>
        public double? level { get; set; }
        /// <summary>
        /// �ʴ��ƾ�(�ƦX������)
        /// </summary>
        public double? val61 { get; set; }
    }


    /// <summary>
    /// Cpamiupload�Y�ɸ�ƥΨ쪺���
    /// </summary>
    public partial class RealTimeStt7
    {
        [Key]
        /// <summary>
        /// �]�ƥN�X
        /// </summary>
        [StringLength(255)]
        public string dev_id { get; set; }
        /// <summary>
        /// �����N�X
        /// </summary>
        public string stt_no { get; set; }
        /// <summary>
        /// �q���ɶ�
        /// </summary> 
        public DateTime? measure_time { get; set; }
        /// <summary>
        /// �W�Ǯɶ�
        /// </summary> 
        public DateTime? upload_time { get; set; }
        /// <summary>
        /// �ʴ��ƾ�(���O/�ƦX������)
        /// </summary>
        public double? val { get; set; }
    }
}
