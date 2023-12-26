$(document).ready(function () {
    //douHelper.getField(douoptions.fields, 'Prizes').formatter =
    //    douHelper.getField(douoptions.fields, 'Participants').formatter = function (v) { return v ? JSON.stringify(v) : '-'; }

    var hasChangeDetails = false;//是否已變更Details資料

    //Master編輯容器加入Detail
    douoptions.afterCreateEditDataForm = function ($container, row) {

        var isAdd = JSON.stringify(row) == '{}';
        //20231006, Add by markhong 新增功能隱藏上傳檔案欄位
        if (isAdd) {
            console.log("ADD");
            $('div[data-field="dev_id"]').find("input:text").attr("disabled", true);
        }
        var $_oform = $container.find(".data-edit-form-group");
        hasChangeDetails = false;
        if (row.dev_id == undefined)
            return;

        //保留確定按鈕
        //$container.find('.modal-footer button').hide();
        //$container.find('.modal-footer').find('.btn-primary').show();
    }
    //douoptions.addable = douoptions.editable = douoptions.deleteable = false;

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

    $("#_table").DouEditableTable(douoptions); //初始dou table
});