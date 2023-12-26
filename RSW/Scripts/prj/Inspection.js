$(document).ready(function () {
    //douHelper.getField(douoptions.fields, 'Prizes').formatter =
    //    douHelper.getField(douoptions.fields, 'Participants').formatter = function (v) { return v ? JSON.stringify(v) : '-'; }

    var hasChangeDetails = false;//是否已變更Details資料

    //Master編輯容器加入Detail
    douoptions.afterCreateEditDataForm = function ($container, row) {
        var isAdd = JSON.stringify(row) == '{}';
        //20231006, Add by markhong 新增功能隱藏上傳檔案欄位
        //20231215, Edit by markhong 與PM討論後開啟上傳檔案欄位
        if (isAdd) {
            console.log("ADD");
            $('div[data-field="dev_id"]').find("input:text").attr("disabled", true);
            $('[data-field="FileReport"]').hide();
            $('[data-field="FileReportUrl"]').hide();
        }

        var $_oform = $container.find(".data-edit-form-group");

        hasChangeDetails = false;

        //上傳檔案格式
        var accept = ['.pdf', '.jpg', '.jpeg'];

        $('[data-field="FileReport"]').hide();
        $('[data-field="FileReportUrl"]').hide();
        var btnUpFileReport = '<div class="form-group field-container col-md-12" data-field="FileReport"> \
                                    <label class="col-sm-12 control-label">上傳檔案 \
                                        <span class="text-danger fw-lighter ms-5">限制1個檔案，' + '類型(' + accept.join(' ') + ')' + '</span> \
                                    </label> \
                                    <div class="field-content col-sm-12"> \
                                        <a class="glyphicon ms-1 ml-1 btn btn-default change-upload-btn" title="瀏覽" onclick="dataManagerIconUploadClick.call(this);">瀏覽...</a> \
                                        <input id="upFileReport" type="file" multiple accept=' + accept.join(',') + ' style="display:none;" name="upFileReport"  /> \
                                        <span id="lnkDownload"></span> \
                                    </div> \
                               </div>';

        $(btnUpFileReport).appendTo($('.data-edit-form-group'));

        //顯示檔案
        var sss = $('[data-fn="FileReport"]').val();
        if ($('[data-fn="FileReport"]').val() != "") {
            //var a = '<a target="_blank" href="' + app.siteRoot + $('[data-fn="FileReportUrl"]').val() + '">' + sss.split('_')[1] + '</a>';
            var a = '<a target="_blank" href="' + app.siteRoot + $('[data-fn="FileReportUrl"]').val() + '">' + sss + '</a>';
            $(a).appendTo($("#lnkDownload"));
        }

        if (!window.dataManagerIconUploadClick) {
            window.dataManagerIconUploadClick = function () {
                $(this).parent().find("input:first").trigger("click");
            };
        }

        $("#upFileReport").on("change", function () {

            //刪除download
            $("#lnkDownload").empty();

            //限定檔案大小
            var maxSize = 50 * 1024 * 1024;  //50MB
            if (this.files[0].size > maxSize) {
                alert("檔案大小限制:50MB");
                return;
            };

            var fileData = new FormData();

            $.each($("#upFileReport").get(0).files, function (index, obj) {
                fileData.append(this.name, obj);
            });

            var $element = $('body');
            helper.misc.showBusyIndicator($element, { timeout: 300 * 60 * 1000 });
            $.ajax({
                url: app.siteRoot + 'OnSiteInspection/UpFile',
                datatype: "json",
                type: "POST",
                data: fileData,
                contentType: false,
                processData: false,
                success: function (data) {
                    //清空檔案
                    $("#upFileReport").val('');

                    if (data.result) {
                        //var a = '<a target="_blank" href="' + app.siteRoot + data.url + '">' + data.fileName.split('_')[1] + '</a>';
                        var a = '<a target="_blank" href="' + app.siteRoot + data.url + '">' + data.fileName + '</a>';
                        $(a).appendTo($("#lnkDownload"));
                        $('[data-fn="FileReport"]').val(data.fileName);
                        //alert("檔案挑選成功");
                    } else {
                        alert("檔案挑選失敗：\n" + data.errorMessage);
                    }

                    //helper.misc.hideBusyIndicator();
                    helper.misc.hideBusyIndicator($element, { timeout: 180000 });
                },
                complete: function () {
                    //helper.misc.hideBusyIndicator();
                    helper.misc.hideBusyIndicator($element, { timeout: 180000 });
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);
                    helper.misc.hideBusyIndicator();
                }
            });
        });
    }
    //douoptions.addable = douoptions.editable = douoptions.deleteable = false;

    //還原已變更Details資料
    douoptions.afterEditDataCancel = function (r) {
        if (hasChangeDetails) {
            console.log("haschange")
            douoptions.updateServerData(r, function (result) {
                $_masterTable.DouEditableTable('updateDatas', result.data);//取消編輯，detail有可能已做一些改變，故重刷UI
            })
        }
        $('[data-field="dev_id"]').show();
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