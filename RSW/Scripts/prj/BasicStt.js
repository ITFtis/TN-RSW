$(document).ready(function () {
    var hasChangeDetails = false;//是否已變更Details資料
    var ca = $('#isAdmin').val() === 'true';
    var b = {};//遠端控制參數
    b.item = function (v, r) {
        var btn = "";
        if (!r.IsImport) {
            btn = '<span class="btn btn-default btn-sm glyphicon glyphicon-list-alt" title="遠端控制參數"></span>';
        }
        return btn;
    }
    b.event = 'click .glyphicon-list-alt';
    b.callback = function importQdate(evt, value, row, index) {
        ShowRPT(row);
    };
    if (ca)
        douoptions.appendCustomFuncs = [b];

    //Master編輯容器加入Detail
    douoptions.afterCreateEditDataForm = function ($container, row) {
        var $_oform = $container.find(".data-edit-form-group");
        hasChangeDetails = false;
        if (row.stt_no == undefined)
            return;
        //保留確定按鈕
        //$container.find('.modal-footer button').hide();
        //$container.find('.modal-footer').find('.btn-primary').show();

        var $s = $container.find('.modal-footer').find('.btn-primary').parent();
        var btnBack = '<button id="btnBack" class="btn btn-secondary" onclick="resetedit(0)">初始設定</button>';
        $(btnBack).appendTo($s);
        vSttName = row.stt_name;
        vLon = row.lon;
        vLat = row.lat;
        vRefDevId = row.ref_dev_id;
    }
    douoptions.addable = douoptions.deleteable = false;

    //還原已變更Details資料
    douoptions.afterEditDataCancel = function (r) {
        if (hasChangeDetails)
            douoptions.updateServerData(r, function (result) {
                $_masterTable.DouEditableTable('updateDatas', result.data);//取消編輯，detail有可能已做一些改變，故重刷UI
            })
    }
    //20230916, add by markhong UI：新增說明文字
    douoptions.appendCustomToolbars = [{
        item: '<label style="color:red">設備異常：電壓<3.0V，訊號強度 < -95dbm</label>'
    }];

    $("#_table").DouEditableTable(douoptions); //初始dou table
});
var vSttName, vLon, vLat, vRefDevId;
var be, al1, al2, al3;
//細項popup
function ShowRPT(row) {
    var devid = row["dev_id"];
    $("#dev_id").val(devid);
    //利用devid去[BasicDev]取下列資料
    var fd1 = new FormData();
    fd1.append('dev_id', devid);
    console.log(fd1);
    $.ajax({
        dataType: 'json',
        url: $.AppConfigOptions.baseurl + 'Station/GData1',
        type: "post",
        //data: new FormData($('#adj')['0']),
        data: fd1,
        processData: false,
        contentType: false,
        success: function (data) {
            //alert("儲存成功");
            console.log(data[1]);
            $("#base_elev").val(data[0]);
            $("#alarm1").val(data[1]);
            $("#alarm2").val(data[2]);
            $("#alarm3").val(data[3]);
            be = data[0]
            al1 = data[1]
            al2 = data[2]
            al3 = data[3]
            console.log(be, al1, al2, al3);
            //location.reload();          
        },
        error: function (request) {
            //alert(request.responseJSON.Message);
            alert("error");
        }
    });

    $("#ITPopUp").modal('show');
}
//細項取消
function closeDialog() {
    console.log("closed");
    $("#ITPopUp").modal('hide');
}
//細項編輯
function editInfo() {
    let confirmAction = confirm("[非同步]變更下控設備設定，確認要執行嗎？");
    if (confirmAction) {
        //alert("已執行");
        //儲存DB
        saveData();
        
    }
}

function saveData() {
    var fd1 = new FormData();
    fd1.append('dev_id', $("#dev_id").val());
    fd1.append('base_elev', $("#base_elev").val());
    fd1.append('alarm1', $("#alarm1").val());
    fd1.append('alarm2', $("#alarm2").val());
    fd1.append('alarm3', $("#alarm3").val());
    fd1.append('rRate', $("#rRate").val());
    fd1.append('beUpdate', 'true');
    fd1.append('alUpdate', 'true');
    fd1.append('rrUpdate', 'true');
    $.ajax({
        dataType: 'json',
        url: $.AppConfigOptions.baseurl + 'Station/SaveData',
        type: "post",
        //data: new FormData($('#adj')['0']),
        data: fd1,
        processData: false,
        contentType: false,
        success: function (data) {
            $.ajax({
                dataType: 'json',
                url: $.AppConfigOptions.baseurl + 'Station/SaveData_Dev',
                type: "post",
                data: fd1,
                processData: false,
                contentType: false,
            });
            if (data == 'success') {
                closeDialog();
                alert("儲存成功");
            }
        },
        error: function (request) {
            //alert(request.responseJSON.Message);
            alert("error");
            closeDialog();
        }
    });


}
//初始化編輯UI
function resetedit(action) {
    switch (action) {
        case 0: {
            document.querySelector('input[data-fn="stt_name"]').value = vSttName;
            document.querySelector('input[data-fn="lon"]').value = vLon;
            document.querySelector('input[data-fn="lat"]').value = vLat;
            document.querySelector('input[data-fn="ref_dev_id"]').value = vRefDevId;
            break;
        }
        case 1: {
            $("#base_elev").val(be);
            $("#alarm1").val(al1);
            $("#alarm2").val(al2);
            $("#alarm3").val(al3);
            break;
        }
        default: {
            break;
        }
    }
}