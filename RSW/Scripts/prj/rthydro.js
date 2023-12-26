window.app = window.app || {};
app.villageDescriptionParser = function (desc) {
    return $.parserStringToObject(desc, "<BR>", "：");
};
app.villageGeojsonData= undefined;
app.getVillageGeojsonData = function (callback) {
    var _kmlCtrl = this;
    if (app.villageGeojsonData)
        callback(JSON.parse(JSON.stringify(app.villageGeojsonData)));
    else {
        var _kc = helper.misc.localCache.get(datahelper.villagekml() + app.kmlVersion);
        if (_kc) {
            app.villageGeojsonData = JSON.parse(_kc);
            app.getVillageGeojsonData(callback);
        }
        else {
            var kcl = new LKmlCtrl(_kmlCtrl, function () {
                kcl.loadKml(datahelper.villagekml(), function (ds) {
                    app.villageGeojsonData = ds;
                    callback(JSON.parse(JSON.stringify(app.villageGeojsonData)));
                });
            });
        }
    }
};
app.kmlVersion = "1.1";
$(document).ready(function () {
    var currentevent = undefined;
    mapHelper.createMap('leaflet', function () {
        setTimeout(function () {
            $('#basemapDiv').on($.MapBaseLayer.events.initUICompleted, function () {
                $('#basemapDiv').MapBaseLayer('setDisplayLayer', '黑階');
            });
        });

        var $_mainContainer = $('#main-ctrl');

        //組tab
        var $_tab = helper.bootstrap.genBootstrapTabpanel($_mainContainer, undefined, undefined,
            ['綜整資訊', '雨量站', '水位站', '淹水感測', '下水道', '&nbsp;&nbsp;災 情&nbsp;&nbsp;'],
            ['<div class="fsta-c">', '<div class="rain-c meter">', '<div class="water-c meter">', '<div class="fsensor-c meter">', '<div class="sewer-c meter">', '<div class="rdisaster-c meter">']
        );
        $_tab.appendTo($_mainContainer).find('.nav').addClass('nav-fill');

        //統計
        var $_fsta= $_tab.find('.fsta-c').fsta().on('change-tab-index', function (e, i) {
            $_tab.find('.nav-item>.nav-link:eq('+i+')').tab("show");
        });

        //雨量
        $_tab.find('.rain-c').rain({ map: app.map }).on('get-data-complete', function (e, ds) {
            $_fsta.fsta('setRainData', ds); //get-data-complete取雨量資料完 , 計算雨量統計資料
        });
        //水位
        $_tab.find('.water-c').water({ map: app.map }).on('get-data-complete', function (e, ds) {
            $_fsta.fsta('setWaterData', ds);
        });
        //淹水感測
        $_tab.find('.fsensor-c').fsensor({ map: app.map }).on('get-data-complete', function (e, ds) {
            $_fsta.fsta('setFloorData', ds);
        });
        //下水道
        $_tab.find('.sewer-c').sewer({ map: app.map }).on('get-data-complete', function (e, ds) {
            $_fsta.fsta('setSewerData', ds);
        });
        //通報災情
        $_tab.find('.rdisaster-c').rdisaster({ map: app.map }).on('get-data-complete', function (e, ds) {
        });

        //災情
        var initCurrentEvent = function () { 
            if (!currentevent)
                return;
            $_tab.find('.rdisaster-c').rdisaster('setEvent', currentevent);
        }
        //取水利署事件
        window.datahelper.loadWraEvents(function (d) {
            var _eventid = helper.misc.getRequestParas()['eventid'];
            //_eventid = 'T2004';
            //_eventid = 'R00648';// 'T2004';
            if (_eventid) {
                currentevent = $.grep(d, function (d) { return d.EventNo == _eventid; })[0];
                currentevent.Enabled = true;
            }
            else
                currentevent = d[0];
            if (currentevent.Enabled) {
                initCurrentEvent();
            }
        });
        //
        //$('#bgg-ctrl').on($.menuctrl.eventKeys.popu_init_before, function () {
        //    alert('$.menuctrl.eventKeys.popu_init_before');
        //}).on($.menuctrl.eventKeys.init_ctrl_menu_completed, function () {
        //    alert('$.menuctrl.eventKeys.init_ctrl_menu_completed');
        //});

        //劃圖工具
        if ($("#drawDiv").length > 0)
            $('#drawDiv').gisdrawtool({
                map: app.map, initEvent: $.menuctrl.eventKeys.popu_init_before, colorSelector: true,
                types: [$.gisdrawtool.types.FREEHAND_POLYLINE, $.gisdrawtool.types.POINT, $.gisdrawtool.types.POLYLINE, $.gisdrawtool.types.POLYGON]
            });

        //匯入KML
        $("#importKmlDiv").on($.menuctrl.eventKeys.popu_init_before, function () {
            $("#importKmlDiv").LocalKml({ map: app.map, useKmlCtrl: true });//, server:"http://210.61.23.112/TNCM/WS/KmlUpload.ashx" });
        });
        //定位
        var $positionDiv = $('#positionDiv').position();
        $('#_export').on('click', function () {
            helper.misc.showBusyIndicator();
            var m = $('#map')[0];
            
            $('body').addClass('print');
            
            //多一層包map，為了避免匯出圖時右邊及下方會多出邊界(因該是圖關係)
            domtoimage.toJpeg(document.getElementById('map-container'), { quality: 0.95 })
                .then(function (dataUrl) {
                    var link = document.createElement('a');
                    link.download = 'map.jpg';
                    link.href = dataUrl;
                    link.click();
                    helper.misc.hideBusyIndicator();
                    $('body').removeClass('print');
                }).catch(function (error) {
                    console.error('oops, something went wrong!', error);
                });
        });
      
    });
})
