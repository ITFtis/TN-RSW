$(document).ready(function () {
    //douHelper.getField(douoptions.fields, 'Prizes').formatter =
    //    douHelper.getField(douoptions.fields, 'Participants').formatter = function (v) { return v ? JSON.stringify(v) : '-'; }

    var hasChangeDetails = false;//是否已變更Details資料

    //Master編輯容器加入Detail
    douoptions.afterCreateEditDataForm = function ($container, row) {
        var $_oform = $container.find(".data-edit-form-group");
        hasChangeDetails = false;
        if (row.dev_id == undefined)
            return;
        //保留確定按鈕
        //$container.find('.modal-footer button').hide();
        //$container.find('.modal-footer').find('.btn-primary').show();
    }
    douoptions.addable = douoptions.editable = douoptions.deleteable = false;

    //還原已變更Details資料
    douoptions.afterEditDataCancel = function (r) {
        if (hasChangeDetails)
            douoptions.updateServerData(r, function (result) {
                $_masterTable.DouEditableTable('updateDatas', result.data);//取消編輯，detail有可能已做一些改變，故重刷UI
            })
    }
    //UI：下載資料
    douoptions.appendCustomToolbars = [{
        item: '<span class="btn btn-primary btn-sm  glyphicon glyphicon-export" title="下載資料">下載資料</span>', event: 'click .glyphicon-sort',
        callback: function (e) {
            exportData();
        }
    }];

    function InsertTotalAfterSorted() {
        var county_code = '';
        var stt_no = '';
        var manuf = '';
        var Year = '';
        var Month = '';
        $('.filter-continer').each(function (index) {
            var $a1 = $(this).find("[data-fn='county_code']");
            var $a2 = $(this).find("[data-fn='stt_no']");
            var $a3 = $(this).find("[data-fn='manufacturer']");
            var $a4 = $(this).find("[data-fn='Year']");
            var $a5 = $(this).find("[data-fn='Month']");

            if ($a1.length > 0) {
                county_code = $a1.val();
            }
            if ($a2.length > 0) {
                stt_no = $a2.val();
            }
            if ($a3.length > 0) {
                manuf = $a3.val();
            }
            if ($a4.length > 0) {
                Year = $a4.val();
            }
            if ($a5.length > 0) {
                Month = $a5.val();
            }
        });
        //var $a1 = $(this).find("[data-fn='county_code']");
        //var $a2 = $(this).find("[data-fn='stt_no']");
        //var $a3 = $(this).find("[data-fn='manufacturer']");
        //var $a4 = $(this).find("[data-fn='Year']");
        //var $a5 = $(this).find("[data-fn='Month']");

        //var county_code = $a1.length > 0 ? $a1.val() : '';
        //var stt_no = $a2.length > 0 ? $a1.val() : ''; 
        //var manuf = $a3.length > 0 ? $a1.val() : ''; 
        //var Year = $a4.length > 0 ? $a1.val() : ''; 
        //var Month = $a5.length > 0 ? $a1.val() : ''; 

        if (county_code != '' || stt_no != '' || manuf != '' || Year != '' || Month != '') {

        }
        var fd2 = new FormData();
        if (county_code != '') { fd2.append('county_code', county_code); };
        if (stt_no != '') { fd2.append('stt_no', stt_no); };
        if (manuf != '') { fd2.append('manuf', manuf); };
        if (Year != '') { fd2.append('Year', Year); };
        if (Month != '') { fd2.append('Month', Month); };
        console.log(fd2);
        $.ajax({
            dataType: 'json',
            url: $.AppConfigOptions.baseurl + 'ReliableDay/GDataDD',
            type: "post",
            //data: new FormData($('#adj')['0']),
            data: fd2,
            processData: false,
            contentType: false,
            success: function (data) {
                //alert("儲存成功"); 
                if (data[0] != '' && data[1] != '' && data[2] != '') {
                    var $ppp = $('#_table tbody');
                    var content = '<tr data-index="9999"> \
                           <td class="dou-field-stt_no"></td> \
                           <td class="dou-field-MMDD">總計：</td> \
                           <td class="dou-field-UpdateRate_DD" style="text-align: right; ">'+ data[0] + '</td> \
                           <td class="dou-field-RealTimeRate_DD" style="text-align: right; ">'+ data[1] + '</td> \
                           <td class="dou-field-ReliableRate_DD" style="text-align: right; ">'+ data[2] + '</td> \
                       </tr>';
                    $(content).appendTo($ppp);
                }           
            },
            error: function (request) {
                //alert(request.responseJSON.Message);
                alert("error");
            }
        });
    }

    douoptions.tableOptions.onSort = function (a) {
        //alert('aaa');
        InsertTotalAfterSorted();
    }

    douoptions.tableOptions.onLoadSuccess = function (datas) {
        InsertTotalAfterSorted();
    },

    $("#_table").DouEditableTable(douoptions); //初始dou table
});