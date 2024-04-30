var classes_non_public = 'non-public';
var InitRainCtrl = function ($_container, options, onlycwb) {
  

    $_container.RainCtrl($.extend({
        map: app.map,
        enabledStatusFilter: true,
        canFullInfowindow: true,
        listContainer: 'inner',
        listTheme: 'none',
        autoReload: true,
        loadBase: datahelper.getRainBase,
        loadInfo: datahelper.getRainRt,
        //infoFields:fs,
        legendIcons: [
            { 'name': '正常', 'url': app.siteRoot + 'images/pin/雨量站_正常.png', 'classes': 'rain_normal', disabled: !onlycwb },
            { 'name': '大雨', 'url': app.siteRoot +'images/pin/雨量站_大雨.png', 'classes': 'rain_heavy' },
            { 'name': '豪雨', 'url': app.siteRoot +'images/pin/雨量站_豪雨.png', 'classes': 'rain_extremely' },
            { 'name': '大豪雨', 'url': app.siteRoot +'images/pin/雨量站_大豪雨.png', 'classes': 'rain_torrential' },
            { 'name': '超大豪雨', 'url': app.siteRoot +'images/pin/雨量站_超大豪雨.png', 'classes': 'rain_exttorrential' },
            { 'name': '無資料', 'url': app.siteRoot + 'images/pin/雨量站_無資料.png', 'classes': 'rain_nodata', disabled: !onlycwb }
        ],
        baseInfoMerge: {
            basekey: 'StationID', infokey: 'StationID', xfield: 'Point.Longitude', yfield: 'Point.Latitude', aftermerge: function (d) {
                //d.StationID = d.ST_NO;
                d.CName = d.StationName;

                d.Datetime = JsonDateStr2Datetime(d.InfoTime);
                d.R10M = d.Acc10MinuteRainQty;
                d.R1H = d.Acc1HourRainQty;
                d.R3H = d.Acc3HourRainQty;
                d.R6H = d.Acc6HourRainQty;
                d.R12H = d.Acc12HourRainQty;
                d.R24H = d.Acc24HourRainQty;
                d.R1D = d.Acc1DayRainQty;
                d.R2D = d.Acc2DayRainQty;
                d.R3D = d.Acc3DayRainQty;
                //if (d.Status == undefined)
                //    d.Status = $.BasePinCtrl.pinIcons.rain.noData.name;
                //20240427, edit by markhong [Tab]雨量站與綜整資訊的數據不同步
                d.Status = getStatus(JsonDateStr2Datetime(d.InfoTime), d.Acc24HourRainQt, d.Acc1HourRainQty, d.Acc3HourRainQty);
            }
        },
        hourlyFieldsInfo: { DateTime: "InfoTime", RQ: "Acc10MinuteRainQty" },
        loadHourlyInfo: datahelper.getRainSerInfo,
        checkDataStatus: function (data, index) {
            var s = '正常';
            if (!data.Datetime || (Date.now() - JsonDateStr2Datetime(data.Datetime).getTime()) >= 6 * 60 * 60 * 1000)
                s = '無資料';
            else if (data.R24H && data.R24H>=500)
                s = '超大豪雨';
            else if ((data.R24H && data.R24H >= 300) || (data.R3H && data.R3H >= 200))
                s = '大豪雨';
            else if ((data.R24H && data.R24H >= 200) || (data.R3H && data.R3H >= 100))
                s = '豪雨';
            else if ((data.R24H && data.R24H >= 80) || (data.R1H && data.R1H >= 40))
                s = '大雨';
            return $.BasePinCtrl.helper.getDataStatusLegendIcon(this.settings.legendIcons, s);
        },
    }, options));
}

function getStatus(dt, r24h, r1h, r3h) {
    var s = '正常';
    if (!dt || (Date.now() - JsonDateStr2Datetime(dt).getTime()) >= 6 * 60 * 60 * 1000)
        s = '無資料';
    else if (r24h && r24h >= 500)
        s = '超大豪雨';
    else if ((r24h && r24h >= 300) || (r3h && r3h >= 200))
        s = '大豪雨';
    else if ((r24h && r24h >= 200) || (r3h && r3h >= 100))
        s = '豪雨';
    else if ((r24h && r24h >= 80) || (r1h && r1h >= 40))
        s = '大雨';
    return s;
}

var InitWaterCtrl = function ($_container, options) {
    $_container.WaterCtrl($.extend({
        name: "水位站",
        map: app.map,
        enabledStatusFilter: true,
        canFullInfowindow:true,
        autoReload: true,
        listContainer: 'inner',
        listTheme: 'none',
        loadBase: datahelper.getWaterBase,
        loadInfo: datahelper.getWaterRt,
        legendIcons: [
            { name: '正常', url: app.siteRoot +'images/pin/水位站_正常.png', classes: 'green_status', disabled:true },
            { name: '一級', url: app.siteRoot + 'images/pin/水位站_一級.png', classes: 'red_status' },
            { name: '二級', url: app.siteRoot + 'images/pin/水位站_二級.png', classes: 'orange_status' },
            { name: '三級', url: app.siteRoot + 'images/pin/水位站_三級.png', classes: 'yellow_status' },
            { name: '無資料', url: app.siteRoot + 'images/pin/水位站_無資料.png', classes: 'gray_status', disabled: true }],
        baseInfoMerge: {
            basekey: 'StationID', infokey: 'StationID', xfield: 'Point.Longitude', yfield: 'Point.Latitude', aftermerge: function (d) {
                //d.StationID = d.ST_NO;
                d.CName = d.StationName;

                d.Datetime = JsonDateStr2Datetime(d.InfoTime);
                d.WarningLine1 = d.WarningWaterLevel && d.WarningWaterLevel.Level1 != -998 && d.WarningWaterLevel.Level1 != -999? d.WarningWaterLevel.Level1:undefined;
                d.WarningLine2 = d.WarningWaterLevel && d.WarningWaterLevel.Level2 != -998 && d.WarningWaterLevel.Level2 != -999 ? d.WarningWaterLevel.Level2 : undefined;
                d.WarningLine3 = d.WarningWaterLevel && d.WarningWaterLevel.Level3 != -998 && d.WarningWaterLevel.Level3 != -999? d.WarningWaterLevel.Level3 : undefined;
                d.Status = $.BasePinCtrl.pinIcons.water.normal.name;
                d.WaterLevel = d.WaterLevel;
                if (d.WaterLevel != undefined && d.WaterLevel != null && d.Datetime != undefined && d.Datetime != null) {
                    var dtime = Date.now() - JsonDateStr2Datetime(d.Datetime).getTime();
                    if (dtime >= 6 * 60 * 60 * 1000)
                        d.Status = $.BasePinCtrl.pinIcons.water.noData.name;
                    else if (d.WarningLine1 != undefined && d.WaterLevel >= d.WarningLine1)
                        d.Status = $.BasePinCtrl.pinIcons.water.warnLevel1.name;
                    else if (d.WarningLine2 != undefined && d.WaterLevel >= d.WarningLine2)
                        d.Status = $.BasePinCtrl.pinIcons.water.warnLevel2.name;
                    else if (d.WarningLine3 != undefined && d.WaterLevel >= d.WarningLine3)
                        d.Status = $.BasePinCtrl.pinIcons.water.warnLevel3.name;
                }
                else
                    d.Status = $.BasePinCtrl.pinIcons.water.noData.name;
            }
        },
        
        loadHourlyInfo: datahelper.getWaterSerInfo,
      
        hourlyFieldsInfo: { DateTime: "DateTime", WaterLevel: "WaterLevel" },
      
    }, options));
}

var InitSewerCtrl = function ($_container, options, defaultShowAllStatus) {
    $_container.WaterCtrl($.extend({
        name: "下水道",
        stTitle: function (data) { return data.stt_name },
        map: app.map,
        enabledStatusFilter: true,
        canFullInfowindow: true,
        pinInfoLabelMinWidth: '3rem',
        autoReload: true,
        listContainer: 'inner',
        listTheme: 'none',
        loadBase: datahelper.getSewerBase,
        loadInfo:  datahelper.getSewerRt,
        infoFields: [
            { field: 'stt_name', title: '站名' },
            { field: 'addr', title: '地點' },
            { field: 'datatime', title: '時間', formatter: $.waterFormatter.datetime },
            { field: 'level', title: '水位', halign: 'center', align: 'right', formatter: $.waterFormatter.float, unit: "公尺", sortable: true },
            { field: 'alarm1', title: '一級', visible: false, formatter: $.waterFormatter.float, unit: "公尺", showInList: false },
            { field: 'alarm2', title: '二級', visible: false, formatter: $.waterFormatter.float, unit: "公尺", showInList: false },
            { field: 'alarm3', title: '三級', visible: false, formatter: $.waterFormatter.float, unit: "公尺", showInList: false }
        ],
        legendIcons: [
            { 'name': '正常', 'url': app.siteRoot + 'images/pin/下水道_正常.png', 'classes': 'green_status', disabled: !defaultShowAllStatus },
            { 'name': '三級', 'url': app.siteRoot + 'images/pin/下水道_三級.png', 'classes': 'yellow_status' },
            { 'name': '二級', 'url': app.siteRoot + 'images/pin/下水道_二級.png', 'classes': 'orange_status' },
            { 'name': '一級', 'url': app.siteRoot + 'images/pin/下水道_一級.png', 'classes': 'red_status' },
            { 'name': '無資料', 'url': app.siteRoot + 'images/pin/下水道_無資料.png', 'classes': 'gray_status', disabled: !defaultShowAllStatus }
        ],
        baseInfoMerge: {
            basekey: 'dev_id', infokey: 'dev_id', xfield: 'lon', yfield: 'lat', aftermerge: function (d) {
                d.Status = $.BasePinCtrl.pinIcons.water.normal.name;
                d.WarningLine1 = d.alarm1;
                d.WarningLine2 = d.alarm2;
                d.WarningLine3 = d.alarm3;
                //if (d.level != undefined) {
                if (d.level !== undefined && d.level !== null && d.datatime !== undefined && d.datatime !== null) {
                    var dtime = Date.now() - JsonDateStr2Datetime(d.datatime).getTime();
                    if (dtime >= 1 * 60 * 60 * 1000)
                        d.Status = $.BasePinCtrl.pinIcons.water.noData.name;
                    else if (d.alarm1 != undefined && d.level >= d.alarm1)
                        d.Status = $.BasePinCtrl.pinIcons.water.warnLevel1.name;
                    else if (d.alarm2 != undefined && d.level >= d.alarm2)
                        d.Status = $.BasePinCtrl.pinIcons.water.warnLevel2.name;
                    else if (d.alarm3 != undefined && d.level >= d.alarm3)
                        d.Status = $.BasePinCtrl.pinIcons.water.warnLevel3.name;
                    else
                        d.Status = $.BasePinCtrl.pinIcons.water.normal.name;
                }
                else
                    d.Status = $.BasePinCtrl.pinIcons.water.noData.name;
            }
        },
        //loadHourlyInfo: datahelper.getSewerSerInfo,
        //hourlyFieldsInfo: { DateTime: "datatime", WaterLevel: "level" },
        getDurationOptions: function (data) { //{hourlyFieldsInfo:{DateTime:"DATE", WaterLevel:"Info"},}
            //this指的是 current
            var result = {
                seriespara:
                {
                    level: {
                        name: '水位', color: '#0000FF', type: 'line', dt: "datatime", info: "level", unit: 'm', turboThreshold: 20000
                    },
                    warn: [
                        { name: '一級', color: '#FF0000', info: 'WarningLine1' },
                        { name: '二級', color: '#FFA500', info: 'WarningLine2' },
                        { name: '三級', color: '#FFFF00', info: 'WarningLine3' },
                    ],
                    wave: { enabled: false }
                },
                chartoptions: function (_options) {
                    _options.chart.zoomType = 'x';
                    _options.xAxis.labels.formatter = function () {
                        var ff = function (s) {
                            return helper.format.paddingLeft(s, '0', 2);
                        }
                        var _date = new Date(this.value);
                        return ff(_date.getMonth() + 1) + '/' + ff(_date.getDate()) + '<br>' + ff(_date.getHours()) + ':' + ff(_date.getMinutes());
                    }
                    try {
                        _options.chart.zooming = {
                            resetButton: { position: { y: -10 } }
                        };
                    } catch (e) { }
                    //_options.lang.resetZoom = 'asssa';
                }
            };
            result.startdt = new Date(data["Datetime"]).addHours(-24);
            result.enddt = new Date(data["Datetime"]);

            result.getDurationData = datahelper.getSewerSerInfo;
            return result;
        }
    }, options));
}

var InitFLCtrl = function ($_container, options, defaultShowAllStatus) {
    var $_p = $('<div class="row"><div class="col-md-12"></div></div>').appendTo($_container);
    $_p.WaterCtrl($.extend({
        name: "流速計",
        stTitle: function (data) { return data.stt_name },
        map: app.map,
        enabledStatusFilter: true,
        canFullInfowindow: true,
        pinInfoLabelMinWidth: '3rem',
        autoReload: true,
        //listContainer: 'inner',
        //listTheme: 'none',
        loadBase: datahelper.getFLStations,
        loadInfo: datahelper.getFLRealtime,
        infoFields: [
            { field: 'stt_name', title: '站名' },
            { field: 'addr', title: '地點' },
            { field: 'datatime', title: '時間', formatter: $.waterFormatter.datetime },
            { field: 'FL', title: '流速', halign: 'center', align: 'right', formatter: $.waterFormatter.float, unit: "", sortable: true }
            
        ],
        legendIcons: [
            { 'name': '正常', 'url': app.siteRoot + 'images/pin/流速計_正常.png', 'classes': 'green_status' },
            { 'name': '無資料', 'url': app.siteRoot + 'images/pin/流速計_無資料.png', 'classes': 'gray_status' }
        ],
        baseInfoMerge: {
            basekey: 'dev_id', infokey: 'dev_id', xfield: 'lon', yfield: 'lat', aftermerge: function (d) {
                d.Status = $.BasePinCtrl.pinIcons.water.normal.name;
               
                if (d.FL !== undefined && d.FL !== null && d.datatime !== undefined && d.datatime !== null) {
                    var dtime = Date.now() - JsonDateStr2Datetime(d.datatime).getTime();
                    if (dtime >= 1 * 60 * 60 * 1000)
                        d.Status = $.BasePinCtrl.pinIcons.water.noData.name;
                    else
                        d.Status = $.BasePinCtrl.pinIcons.water.normal.name;
                }
                else
                    d.Status = $.BasePinCtrl.pinIcons.water.noData.name;
            }
        },
        getDurationOptions: function (data) { //{hourlyFieldsInfo:{DateTime:"DATE", WaterLevel:"Info"},}
            //this指的是 current
            var result = {
                seriespara:
                {
                    level: {
                        name: '流速', color: '#0000FF', type: 'line', dt: "datatime", info: "level", unit: 'm/s', turboThreshold: 20000
                    },
                    warn: [],
                    wave: { enabled: false }
                },
                chartoptions: function (_options) {
                    _options.chart.zoomType = 'x';
                    _options.yAxis.title.text = '流速m/s';
                    _options.xAxis.labels.formatter = function () {
                        var ff = function (s) {
                            return helper.format.paddingLeft(s, '0', 2);
                        }
                        var _date = new Date(this.value);
                        return ff(_date.getMonth() + 1) + '/' + ff(_date.getDate()) + '<br>' + ff(_date.getHours()) + ':' + ff(_date.getMinutes());
                    }
                    try {
                        _options.chart.zooming = {
                            resetButton: { position: { y: -10 } }
                        };
                    } catch (e) { }
                    //_options.lang.resetZoom = 'asssa';
                }
            };
            result.startdt = new Date(data["Datetime"]).addHours(-24);
            result.enddt = new Date(data["Datetime"]);

            result.getDurationData = datahelper.getFLSerInfo;
            return result;
        }
    }, options));
    return $_p;
}


var InitFloodSensor = function ($_container, options) {
    FloodSensorOptions.map = FloodSensorOptions.map || app.map;
    //$_container.PinCtrl($.extend(FloodSensorOptions, options));
    $_container.WaterCtrl($.extend({},FloodSensorOptions, options));
}

var InitReportDisaster = function ($_container, options) {
    $_container.PinCtrl($.extend(floodPinOptions, { map: app.map, enabledStatusFilter: true, autoReload: true, listContainer: 'inner', listTheme:'none'}, options));
}

var FloodSensorOptions = {
    stTitle: function (d) { return d.StationName },
    useTimeSeriesData: true, enabledStatusFilter: true, autoReload: true,
    listContainer: 'inner',
    listTheme: 'none',
    pinInfoLabelMinWidth: '68px;vertical-align: top',
    name: '淹水感測設備',
    canFullInfowindow : true,
    infoFields: [
        //{ field: 'SensorUUID', title: 'ID' },
        { field: 'StationName', title: '站名' },
        { field: 'InfoTime', title: '時間', formatter: function (v, r) { return v ? JsonDateStr2Datetime(v).DateFormat('MM/dd HH:mm') : '-' } },
        { field: 'WaterDepth', title: '淹水深度', formatter: function (v, r) { return (v != undefined ? v.toFixed(0) : '-') + '公分'; } },
        { field: 'Owner', title: '建置單位'}
    ],
    legendIcons: [{ name: '正常', url: app.siteRoot + 'images/pin/Flood_b_7.png', classes: 'blue_status', disabled: true },
        { name: '淹水', url: app.siteRoot + 'images/pin/Flood_r_7.png', classes: 'red_status' },
        { name: '淹水10↑', url: app.siteRoot + 'images/pin/fsensor_10.png', classes: 'red_status' },
        { name: '淹水30↑', url: app.siteRoot + 'images/pin/fsensor_30.png', classes: 'red_status' },
        { name: '淹水50↑', url: app.siteRoot + 'images/pin/fsensor_50.png', classes: 'red_status' },
        { name: '無資料', url: app.siteRoot + 'images/pin/Flood_g_7.png', classes: 'gray_status', disabled: true  }],
    checkDataStatus: function (data, index) {
        var _i = 0;
        if (!data.InfoTime)// || (Date.now() - JsonDateStr2Datetime(data.SourceTime).getTime()) >= 24 * 60 * 60 * 1000)
            _i = 5;
        else if (data.WaterDepth && data.WaterDepth >= 50)
            _i = 4;
        else if (data.WaterDepth && data.WaterDepth >= 30)
            _i = 3;
        else if (data.WaterDepth && data.WaterDepth >= 10)
            _i = 2;
        else if (data.WaterDepth && data.WaterDepth >= 0)
            _i = 1;
        return this.settings.legendIcons[_i];
    },
    loadBase: window.datahelper.getFloodWaterBase,
    loadInfo: window.datahelper.getFloodWaterRt,
    transformData: function (_base, _info) {
        var that = this;
        var datas = [];
        $.each(_base, function () {
            var _i = JSON.parse(JSON.stringify(this));
            if (this.Point && this.Point.Latitude != undefined) {
                _i.X = this.Point.Longitude;
                _i.Y = this.Point.Latitude;
            }
            var _if = $.grep(_info, function (_in) { return _in.StationID == _i.StationID });
            if (_if.length > 0)
                _i = $.extend(_i, _if[0]);
            datas.push(_i);
        });
        datas.sort(function (a, b) {
            var av = a.Depth, bv = b.Depth;
            if (!a.InfoTime || (Date.now() - JsonDateStr2Datetime(a.InfoTime).getTime()) >= 24 * 60 * 60 * 1000)
                av = -999;
            if (!b.InfoTime || (Date.now() - JsonDateStr2Datetime(b.InfoTime).getTime()) >= 24 * 60 * 60 * 1000)
                bv = -999;
            return bv - av; //清單淹水的在前面
        });
        window.pinLeafletMaxZIndex += datas.length;
        $.each(datas, function (idx, d) { //淹水的pin pinZIndex越大，才會在上面
            d.pinZIndex = window.pinLeafletMaxZIndex - idx;
        });
        window.ALLDATAS = datas;//選鄉鎮select filter
        return datas;
    },
    pinInfoContent: function (data) {
        var that = this;
        //var $_c = $($.BasePinCtrl.defaultSettings.pinInfoContent.call(this, data));
        var $_c = $($.WaterCtrl.defaultSettings.pinInfoContent.call(this, data));
        return $_c[0].outerHTML;

        var eurl = encodeURI(app.CSgdsRoot+ 'FDashboard.html?sensoruuid=' + data.SensorUUID);
        console.log("eurl:" + eurl);
        var $_info = $('<div style="font-size:1.4rem;padding:0 2rem 0 .4rem;text-align:end;"></div>').appendTo($_c);
        $('<a href="' + eurl + '" target="_FDashboard"><sapn class="glyphicon glyphicon-info-sign" title="淹水感測整合資訊"></a>').appendTo($_info);
        if (data.Depth && data.Depth >=10 && !data.floodarea) {
            var _gid = 'cal-' + helper.misc.geguid();
            $('<sapn id="' + _gid + '" style="font-size:1.2rem;margin-right:1rem;cursor:pointer" class="glyphicon glyphicon-alert" title="淹水範圍推估"></span>').prependTo($_info);
            setTimeout(function () {
                if (_gid) {
                    $('#' + _gid).on('click', function () {
                        var $_this = $(this);
                        $_this.off('click').css('opacity', .3)[0].style.cursor = 'not-allowed';//.addClass('btn disabled');

                        helper.misc.showBusyIndicator($_this.closest('.meterInfoTemplateContent'), { content: '演算中...' });
                        datahelper.estimateFloodingComputeForLightweightDatas([{
                            PK_ID: data.SensorUUID + Date.now(),
                            X: data.Point.Longitude,
                            Y: data.Point.Latitude,
                            DATE: helper.format.JsonDateStr2Datetime(data.SourceTime),
                            CREATE_DATE: helper.format.JsonDateStr2Datetime(data.SourceTime),
                            Sources: "淹水感測器即時推估_KHSFC",
                            EMISTYPE: '淹水感測器',
                            DEPTH: data.Depth,
                            SourceCode: 7,
                            TOWN_NAME: data.TownCode,
                            LOCATION_DESCRIPTION: data.Address,
                            Described: '-'
                        }], function (fds) {
                            helper.misc.hideBusyIndicator($_this.closest('.meterInfoTemplateContent'));
                            if (fds && fds.floodarea.length > 0) {
                                //console.log(fds.floodarea[0]);
                                if (fds.floodarea == undefined || fds.floodarea.length == 0 || fds.floodarea[0].Image_Data == undefined)
                                    return;
                                data.floodarea = fds.floodarea[0];
                                that.$element.trigger('add-floodarea', [data]);
                                return;
                                var fa = fds.floodarea[0];

                                data._floodImageOverlay = L.imageOverlay(app.CSgdsRoot + fa.Image_Data.Url, [[fa.Image_Data.MaxY, fa.Image_Data.MaxX], [fa.Image_Data.MinY, fa.Image_Data.MinX]], { interactive: true, zIndex: 201 }).addTo(app.map);
                                data._floodImageOverlay.on('click', function (e) {
                                    var popup = data._floodImageOverlay.unbindPopup().bindPopup('<div class="leaflet-infowindow-title  status-blue">' + data.SensorName + '</div>' + "<div>" + floodPinOptions.pinInfoContent.call({ settings: floodPinOptions }, fa) + "</div>",
                                        $.extend({ closeOnClick: true, autoClose: true, className: 'leaflet-infowindow', minWidth: 250 },
                                            {}))
                                        .openPopup(e.latlng).getPopup();

                                    var _color = '#0000FF';
                                    var $_pop = $(popup.getElement());
                                    var _statusstyle = window.getComputedStyle($_pop.find('.leaflet-infowindow-title')[0]);
                                    var $_title = $_pop.find('.leaflet-infowindow-title');
                                    var _cbtn = $_pop.find('.leaflet-popup-close-button')[0];
                                    var _content = $_pop.find('.leaflet-popup-content')[0];
                                    $_title.css('background-color', _color).css('color', '#fff').css('box-shadow', $_title.css('box-shadow').replace('#999', _color).replace('rgb(153, 153, 153)', _color));
                                    _content.style.borderColor = _cbtn.style.borderColor = _cbtn.style.color = _color;
                                    $_pop.find('.leaflet-popup-tip')[0].style.boxShadow = " 3px 3px 15px " + _color;
                                });
                            }
                        });
                    })
                }
            });
        }

        return $_c[0].outerHTML;
    },
    getDurationOptions: function (data) { //{hourlyFieldsInfo:{DateTime:"DATE", WaterLevel:"Info"},}
        //this指的是 current
        var result = {
            seriespara:
            {
                level: {
                    name: '水位', color: '#0000FF', type: 'line', dt: "InfoTime", info: "WaterDepth", unit: 'cm', turboThreshold:5000
                },
                wave: { enabled: false }
            },
            chartoptions: function (_options) {
                _options.chart.zoomType = 'x';
                _options.yAxis.title.text="水深cm"
                _options.yAxis.minRange = 1; //如全0就不會畫在中間
                _options.yAxis.min = 0;
                _options.xAxis.labels.formatter = function () {
                    var ff = function (s) {
                        return helper.format.paddingLeft(s, '0', 2);
                    }
                    var _date = new Date(this.value);
                    return ff(_date.getMonth() + 1) + '/' + ff(_date.getDate()) + '<br>' + ff(_date.getHours()) + ':' + ff(_date.getMinutes());
                }
            }
        };

        result.startdt = helper.format.JsonDateStr2Datetime(data["InfoTime"]).addHours(-24);
        result.enddt = helper.format.JsonDateStr2Datetime(data["InfoTime"]);
        result.stationNo = data["StationID"]
        result.getDurationData = datahelper.getFloodWaterSerInfo;
        return result;
    }
};

var Init水利局CCTVCtrl = function ($_container, options) {
    var $_p = $('<div class="row"><div class="col-md-12"></div></div>').appendTo($_container);
    var pOptins = {
        map: app.map, useSearch: false,
        useLabel: true, useList: true,
        infoFields: [{ field: 'name', title: '位置' }, { field: 'urls', title: '攝影機數', formatter: function (v, d) { return v.length } }],
        listTheme: 'success',
        name: "水利局",
        canFullInfowindow: { "max-width": 366 },
        stTitle: function (data) { return data.name },
        loadBase: datahelper.getTnWrbCCTV,
        //pinsize: { x: 10, y: 10, minx: 8, maxx: 26, miny: 10, maxy: 26, step: 2, anchor: "cneter" },
        //legendIcons: [{ 'name': '水利局', 'url': app.siteRoot + '/Images/CCTV.png', 'classes': 'green_status' }],
    };
    pOptins = $.extend(pOptins, options);
    $_p.CctvCtrl(pOptins);
    //return Init高雄自建CCTV($_container, $.extend({ name: '河川水位', loadBase: datahelper.get河川水位CCTV}, options))
    return $_p;
}


var InitReservoirCtrl = function ($_container, options) {
    var $_p = $('<div class="row"><div class="col-md-12"></div></div>').appendTo($_container);
    $_p.PinCtrl($.extend({
        map: app.map, stTitle: function (d) { return d.StationName },
        name: '水庫水情',
        classes: classes_non_public,
        //enabledStatusFilter: true,
        autoReload: true,
        listTheme: 'gbright',
        pinInfoLabelMinWidth: "5rem",
        loadBase: datahelper.getReservoirBase,
        loadInfo: datahelper.getReservoirRt,
        infoFields: [
            { field: 'StationName', title: '水庫名稱' },
            { field: 'Time', title: '時間', formatter: function (v, r) { return v ? JsonDateStr2Datetime(v).DateFormat('yyyy/MM/dd HH:mm') : '-' } },
            { field: 'WaterHeight', title: '水位', unit: '公尺', formatter: function (v) { return v || '-' }},
            { field: 'EffectiveStorage', title: '有效蓄水量', unit: '萬噸', formatter: function (v) { return v || '-' }},
            { field: 'PercentageOfStorage', title: '蓄水量百分比', unit: '%', formatter: function (v) { return v? helper.format.formatNumber(v,2) : '-' }},
            { field: 'Discharge', title: '放流量', unit: 'CMS', formatter: function (v) { return v || '-' } },
            { field: 'StatusN', title: '狀態'}
        ],
        legendIcons: [
            { 'name': '未放水', 'url': app.siteRoot + 'images/pin/水庫_未放水.png', 'classes': 'blue_status' },
            { 'name': '預計放水', 'url': app.siteRoot + 'images/pin/水庫_預計放水.png', 'classes': 'orange_status' },
            { 'name': '放水中', 'url': app.siteRoot + 'images/pin/水庫_放水中.png', 'classes': 'red_status' }
        ],
        checkDataStatus: function (d, i) {
            return $.BasePinCtrl.helper.getDataStatusLegendIcon(this.settings.legendIcons, d.StatusN);
        },
        baseInfoMerge: {
            basekey: 'StationNo', infokey: 'StationNo', xfield: 'Point.Longitude', yfield: 'Point.Latitude', aftermerge: function (d) {
                d.StatusN = (d.Status == 0 ? '預計放水' : (d.Status == 1 ? '放水中' : '未放水'))
            }
        }
    }, options));
    return $_p;
}

var nullformatter = formatter = function (v, data) {
    return v==undefined || v=='null' || v=='' ? '-':v;
};
var InitMovingPumpCtrl = function ($_container, options) {
    var $_p = $('<div class="row"><div class="col-md-12"></div></div>').appendTo($_container);
    $_p.PinCtrl($.extend({
        map: app.map, useSearch: true,
        //minZoom: app.defaultVisibleZoomLevel,
        listTheme: 'gbright',
        name: "移動式抽水機",
        stTitle: function (data) { return data.StationName },
        pinInfoLabelMinWidth: "5rem",
        infoFields: [
            { field: 'StationID', title: '機組編號' },
            { field: 'StationName', title: '機組名稱' },
            { field: 'InfoTime', title: '資料時間' },
            { field: 'RSSI', title: '訊號強度', showInList: false, formatter: nullformatter },
            { field: 'PumpStatus', title: '運轉狀態', showInList: false, formatter: nullformatter },// formatter: function (v, data) { return v == undefined ? '---' : '<img src="Images/' + (v == 1 ? 'red' : 'green') + '.png"></img>'; } },
            { field: 'LowOilStatus', title: '低油位訊號', showInList: false, formatter: nullformatter },// formatter: function (v, data) { return v == undefined ? '---' : '<img src="Images/' + (v == 1 ? 'red' : 'green') + '.png"></img>'; } },
            //{ field: 'OutputStatus', title: '出水狀態', showInList: false },// formatter: function (v, data) { return v == undefined ? '---' : '<img src="Images/' + (v == 1 ? 'green' : 'gray') + '.png"></img>'; } },
            //{ field: 'MoveStatus', title: 'GPS位置狀態', showInList: false }//, formatter: function (v, data) { return v == 1 ? '運送中' : '靜止' } },
            { field: 'BatteryVoltage', title: '電池電壓', formatter: nullformatter },
            { field: 'SolarVoltage', title: '太陽能電壓', formatter:nullformatter },
        ],
        transformData: function (_base, _info) {
            var r = [];
            $.each(_base, function (idxi, _b) {
                if (!_b.IsEnabled)
                    return;
                var b = JSON.parse(JSON.stringify(_b));
                b.X =0;
                b.Y = 0;
                var ifs = $.grep(_info, function (_i) {
                    return _i.StationID == b.StationID;
                });
                if (ifs.length > 0) {
                    b= $.extend(b, ifs[0]);
                    b.X = b.Point.Longitude;
                    b.Y = b.Point.Latitude;
                }
                r.push(b);
            });
            
            return r;
        },
        legendIcons: [{ 'name': '待命', 'url': app.siteRoot + 'images/pin/抽水機_待命.png', 'classes': 'green_status' }, { 'name': '運轉', 'url': app.siteRoot + 'images/pin/抽水機_抽水中.png', 'classes': 'red_status' }, { 'name': '其他', 'url': app.siteRoot + 'images/pin/抽水機_其他.png', 'classes': 'gray_status' }],
        checkDataStatus: function (data, index) {
            var status = "其他";
            if (data.PumpStatus == '0')
                status = '待命';
            else if (data.PumpStatus == '1')
                status = '運轉';
            return $.BasePinCtrl.helper.getDataStatusLegendIcon(this.settings.legendIcons, status);
        },
        loadBase: datahelper.getMovingPumpBase,
        loadInfo: datahelper.getMovingPumpRt,
    }, options));

    return $_p;
}

var InitTideCtrl = function ($_container, options) {
    var $_p = $('<div class="row"><div class="col-md-12"></div></div>').appendTo($_container);
    $_p.WaterCtrl($.extend({
        map: app.map, stTitle: function (d) { return d.id + '(' + d.name + ')' },
        name: '潮位站',
        classes: classes_non_public,
        canFullInfowindow:true,
        autoReload: true,
        listTheme: 'gbright',
        loadBase: datahelper.getTideBase,
        loadInfo: datahelper.getTideRt,
        infoFields: [
            { field: 'id', title: '站名', formatter: function (v, r) { return v + '('+r.name+')' }},
            { field: 'time', title: '時間', formatter: function (v, r) { return v ? JsonDateStr2Datetime(v).DateFormat('yyyy/MM/dd HH:mm') : '-' } },
            { field: 'height', title: '潮高', unit: 'm' },
            { field: 'status', title: '漲退潮'}
        ],
        legendIcons: [
            { 'name': '待命', 'url': app.siteRoot + 'images/pin/潮位站.png', 'classes': 'blue_status' }
        ],
        checkDataStatus: function (d, i) {
            return this.settings.legendIcons[0];
        },
        baseInfoMerge: {basekey: 'id', infokey: 'id', xfield: 'lon', yfield: 'lat'},
        getDurationOptions: function (data) { //{hourlyFieldsInfo:{DateTime:"DATE", WaterLevel:"Info"},}
            //this指的是 current
            var result = {
                seriespara:
                {
                    level: {
                        name: '潮位', color: '#0000FF', type: 'line', dt: "time", info: "height", unit: 'm'
                    },
                    warn: [],
                    wave: { enabled: false }
                }
            };

            result.startdt = new Date(data["time"]).addHours(-24);
            result.enddt = new Date(data["time"]);
            result.stationNo = data["id"]
            result.getDurationData = function (_d,callback) {
                callback( data.sers);
            };
            return result;
        }
    }, options));
    return $_p;
}

var Init雷達迴波圖 = function ($_meter) {
    var _layer = undefined;
    var $_silder = undefined;
    var _timer = Date.now();
    var $_legend = $('#rader-legend');
    var _legendColorDef = [{ min: 65, max: 9999, color: "#FFFFFF" },
        { min: 60, max: 65, color: "#9C31CE" }, { min: 55, max: 60, color: "#FF00FF" },
        { min: 50, max: 55, color: "#D50000" }, { min: 45, max: 50, color: "#FF6363" },
        { min: 40, max: 45, color: "#FF9400" }, { min: 35, max: 40, color: "#E7C600" },
        { min: 30, max: 35, color: "#FFFF00" }, { min: 25, max: 30, color: "#009400" },
        { min: 20, max: 25, color: "#00B400" }, { min: 15, max: 20, color: "#00E900" },
        { min: 10, max: 15, color: "#005BFF" }, { min: 5, max: 10, color: "#00B6FF" }, {min: 0, max: 5, color: "#00FFFF" }];

    var $_p = $('<div class="row"><div class="col-md-12"></div></div>').appendTo($_meter);
    $_p.PinCtrl({
        map: app.map, name: '雷達迴波圖', useLabel: false, useList: false, autoReload: { auto: false, interval: 10 * 1000 }
    }).on($.BasePinCtrl.eventKeys.initUICompleted, function () {
        var _timerflag = undefined;
        var timerreload = function () {
            clearInterval(_timerflag);
            _timerflag = setInterval(function () {
                $_p.find('.pinswitch').trigger('change');
            }, 5*60 * 1000);
        }
        $_p.find('.pinswitch').off('change').on('change', function () {
            var s = $(this).is(':checked');
            if (Date.now() - _timer >= 60 * 1000) {
                if (_layer) {
                    _layer.remove();
                    _layer = null;
                    $_silder.remove();
                }
                _timer = Date.now();
            }
            if (!_layer && s) {
                helper.misc.showBusyIndicator();
                //$.getJSON('https://cwbopendata.s3.ap-northeast-1.amazonaws.com/MSC/O-A0058-005.json', function (r) {
                //    helper.misc.hideBusyIndicator();
                //    var url = r.cwbopendata.dataset.resource.uri;
                //    var time = r.cwbopendata.dataset.time.obsTime;
                //    var x1 = parseFloat(r.cwbopendata.dataset.datasetInfo.parameterSet.parameter[1].parameterValue.split('-')[0]);
                //    var x2 = parseFloat(r.cwbopendata.dataset.datasetInfo.parameterSet.parameter[1].parameterValue.split('-')[1]);
                //    var y1 = parseFloat(r.cwbopendata.dataset.datasetInfo.parameterSet.parameter[2].parameterValue.split('-')[0]);
                //    var y2 = parseFloat(r.cwbopendata.dataset.datasetInfo.parameterSet.parameter[2].parameterValue.split('-')[1]);

                //    var imageBounds = [[y1, x1], [y2, x2]];
                //    app.map.createPane('cloudimage1').style.zIndex = 350;
                //    //var newOverlay = new google.maps.GroundOverlay(url, imageBounds, { map: this.map, opacity: this.currentOpacity }); //1.東南亞imageBounds對不齊??;2.GroundOverlay zoom in out會卡卡的
                //    _layer = L.imageOverlay(url, imageBounds, { opacity: 1, pane: 'cloudimage1' }).addTo(app.map);
                //    $_silder = bindSilder($_p, _layer);
                //    $("#other-layer-info .rader-layer").removeClass('offdisplay');
                //    $("#other-layer-info .rader-layer-time").text(helper.format.JsonDateStr2Datetime(time).DateFormat('MM/dd HH:mm:ss'));
                //    $_legend.empty().show();
                //    L.DouLayer.Qqesums.genLegend($_legend, _legendColorDef, 24, 16, '毫米(mm)', '雷達迴波');
                //});
                var gparas = JSON.parse(JSON.stringify(L.DouLayer.Qqesums.DefaulParas));
                gparas.url = app.siteRoot + 'api/rad/rt';
                gparas.parser = L.DouLayer.Qqesums.Parser.sqpe;
                $.getJSON(gparas.url, function (r) {
                    helper.misc.hideBusyIndicator();
                    if (!r) {
                        $("#other-layer-info .rader-layer").removeClass('offdisplay');//.text(_linfo);
                        $("#other-layer-info .rader-layer-time").text('-無資料');
                        return;
                    }
                    gparas.parser(gparas, r.Content);

                    _layer = L.DouLayer.gridRectCanvas({ 'opacity': 1, ycellsize: gparas.ycellsize, xcellsize: gparas.xcellsize, noMask: true });//.addTo(app.map);
                    _layer.on('mousemove', function (evt) {
                        if (!L.Browser.mobile)
                            _layer.bindTooltip('dBZ:' + evt.griddata.val, { className: "qpesums_tooltip" }).openTooltip(evt.latlng);
                    }).on('mouseout', function (evt) {
                        if (!L.Browser.mobile && _layer.getTooltip() != null)
                            _layer.closeTooltip();
                    });
                    //用$.map 資料太大會有Maximum call stack size exceeded
                    var gs = L.DouLayer.Qqesums.gridData2cellData(gparas.datas, gparas.colorDef);//$.map(gdata.datas, function (val, i) {return { lng: val[0], lat: val[1], color: getcolor(gdata.colorDef, val[2]) }});

                    $_legend.empty().show();
                    L.DouLayer.Qqesums.genLegend($_legend, gparas.colorDef, 24, 16, 'dBZ', '雷達迴波');
                    _layer.setData(gs);
                    $_silder = bindSilder($_p, _layer);

                    $("#other-layer-info .rader-layer").removeClass('offdisplay');//.text(_linfo);
                    $("#other-layer-info .rader-layer-time").text(helper.format.JsonDateStr2Datetime(r.Datetime).DateFormat('MM/dd HH:mm:ss'));
                    _layer.addTo(app.map);
                });
                timerreload();
            } else {
                if (_layer) {
                    if (s) {
                        _layer.addTo(app.map);
                        $_silder.removeClass('offdisplay');
                        $("#other-layer-info .rader-layer").removeClass('offdisplay');
                    }
                    else {
                        _layer.remove();
                        $_silder.addClass('offdisplay');
                        $("#other-layer-info .rader-layer").addClass('offdisplay');
                    }
                }
                else
                    $("#other-layer-info .rader-layer").addClass('offdisplay');
            }
            if ($("#other-layer-info .rader-layer").hasClass('offdisplay'))
                $_legend.hide();
            else
                $_legend.show();
        });
    });
   

    return $_p;
}

var Init累積雨量圖 = function ($_meter) {
    //https://cwbopendata.s3.ap-northeast-1.amazonaws.com/DIV2/O-A0040-003.kmz
    var _layer = undefined;
    var $_silder = undefined;
    var _timer = Date.now();
    var $_legend = $('#dayrainfall-legend');
    var _legendColorDef = [{ min: 300, max: 5000, color: "#FFD6FE" }, { min: 200, max: 300, color: "#FE38FB" },
    { min: 150, max: 200, color: "#DB2DD2" }, { min: 130, max: 150, color: "#AB1FA2" },
    { min: 110, max: 130, color: "#AA1801" }, { min: 90, max: 110, color: "#D92203" },
    { min: 70, max: 90, color: "#FF2A06" }, { min: 50, max: 70, color: "#FFA81D" },
    { min: 40, max: 50, color: "#FFD328" }, { min: 30, max: 40, color: "#FEFD31" },
    { min: 20, max: 30, color: "#00FB30" }, { min: 15, max: 20, color: "#26A31B" },
    { min: 10, max: 15, color: "#0177FC" }, { min: 6, max: 10, color: "#00A5FE" },
    { min: 2, max: 6, color: "#01D2FD" }, { min: 1, max: 2, color: "#9EFDFF" }, { min: 0, max: 1, color: "#EDEDE6" }];
    var $_p = $('<div class="row"><div class="col-md-12"></div></div>').appendTo($_meter);
    var $_p = $('<div class="row"><div class="col-md-12"></div></div>').appendTo($_meter);
    $_p.PinCtrl({
        map: app.map, name: '累積雨量圖', useLabel: false, useList: false
    }).on($.BasePinCtrl.eventKeys.initUICompleted, function () {
        var _timerflag = undefined;
        var timerreload = function () {
            clearInterval(_timerflag);
            _timerflag = setInterval(function () {
                $_p.find('.pinswitch').trigger('change');
            }, 5*60 * 1000);
        }
        $_p.find('.pinswitch').off('change').on('change', function () {
            var s = $(this).is(':checked');
            if (Date.now() - _timer >= 60 * 1000) {
                if (_layer) {
                    _layer.remove();
                    _layer = null;
                    $_silder.remove();
                }
                _timer = Date.now();
            }
            if (!_layer && s) {
                helper.misc.showBusyIndicator();
                $.getJSON(app.siteRoot + 'api/rain/img/dayrainfall', function (r) {
                    helper.misc.hideBusyIndicator();
                    var url = r.Url;
                    //var time = r.cwbopendata.dataset.time.obsTime;
                    var x1 = r.MaxX;
                    var x2 = r.MinX;
                    var y1 = r.MaxY;
                    var y2 = r.MinY;
                    var _linfo = (r.Time ? helper.format.JsonDateStr2Datetime(r.Time).DateFormat('MM/dd HH:mm:ss') : '');

                    var imageBounds = [[y1, x1], [y2, x2]];
                    app.map.createPane('cloudimage1').style.zIndex = 350;
                    //var newOverlay = new google.maps.GroundOverlay(url, imageBounds, { map: this.map, opacity: this.currentOpacity }); //1.東南亞imageBounds對不齊??;2.GroundOverlay zoom in out會卡卡的
                    _layer = L.imageOverlay(url, imageBounds, { opacity: 1, pane: 'cloudimage1' }).addTo(app.map);
                    $_silder = bindSilder($_p, _layer);
                    $("#other-layer-info .sum-rainfall-layer").removeClass('offdisplay');//.text(_linfo);
                    $("#other-layer-info .sum-rainfall-layer-time").text(_linfo);
                    $_legend.empty().show();

                    L.DouLayer.Qqesums.genLegend($_legend, _legendColorDef, 24, 16, '毫米(mm)', '累積雨量');
                });
                timerreload();
            } else {
                if (_layer) {
                    if (s) {
                        _layer.addTo(app.map);
                        $_silder.removeClass('offdisplay');
                        $("#other-layer-info .sum-rainfall-layer").removeClass('offdisplay');
                    }
                    else {
                        _layer.remove();
                        $_silder.addClass('offdisplay');
                        $("#other-layer-info .sum-rainfall-layer").addClass('offdisplay');
                    }
                }
                else
                    $("#other-layer-info .rader-layer").addClass('offdisplay');
            }
            if ($("#other-layer-info .sum-rainfall-layer").hasClass('offdisplay'))
                $_legend.hide();
            else
                $_legend.show();
        });
    });

    return $_p;
}

var InitQpf060minRt = function ($_meter) {
    //https://cwbopendata.s3.ap-northeast-1.amazonaws.com/DIV2/O-A0040-003.kmz
    var _layer = undefined;
    var $_silder = undefined;
    var _timer = Date.now();
    var $_legend = $('#qpesums-legend');
    var $_p = $('<div class="row"><div class="col-md-12"></div></div>').appendTo($_meter);
    $_p.PinCtrl({
        map: app.map, name: '預報1小時降雨', useLabel: false, useList: false
    }).on($.BasePinCtrl.eventKeys.initUICompleted, function () {
        var _timerflag = undefined;
        var timerreload = function () {
            clearInterval(_timerflag);
            _timerflag = setInterval(function () {
                $_p.find('.pinswitch').trigger('change');
            }, 5 * 60 * 1000);
        }
        $_p.find('.pinswitch').off('change').on('change', function () {
            var s = $(this).is(':checked');
            if (Date.now() - _timer >= 60 * 1000) {
                if (_layer) {
                    _layer.remove();
                    _layer = null;
                    $_silder.remove();
                }
                _timer = Date.now();
            }
            if (!_layer && s) {
                helper.misc.showBusyIndicator();
                var gparas = JSON.parse(JSON.stringify(L.DouLayer.Qqesums.DefaulParas));
                gparas.url = app.siteRoot + 'api/qpesums/qpf060min/rt';
                gparas.parser = L.DouLayer.Qqesums.Parser.sqpe;
                $.getJSON(gparas.url, function (r) {
                    helper.misc.hideBusyIndicator();
                    if (!r) {
                        $("#other-layer-info .qpfqpe60-layer").removeClass('offdisplay');//.text(_linfo);
                        $("#other-layer-info .qpfqpe60-layer-time").text('-無資料');
                        return;
                    }
                    gparas.parser(gparas, r.Content);

                    _layer = L.DouLayer.gridRectCanvas({ 'opacity': 1, ycellsize: gparas.ycellsize, xcellsize: gparas.xcellsize, noMask: true });//.addTo(app.map);
                    _layer.on('mousemove', function (evt) {
                        if (!L.Browser.mobile)
                            _layer.bindTooltip('降雨量:' + evt.griddata.val, { className:"qpesums_tooltip"}).openTooltip(evt.latlng);
                            //_layer.bindTooltip(JSON.stringify(evt.latlng) + "<br>" + JSON.stringify(evt.griddata)).openTooltip(evt.latlng);
                    }).on('mouseout', function (evt) {
                        if (!L.Browser.mobile && _layer.getTooltip() != null)
                            _layer.closeTooltip();
                    });
                    //用$.map 資料太大會有Maximum call stack size exceeded
                    var gs = L.DouLayer.Qqesums.gridData2cellData(gparas.datas, gparas.colorDef);//$.map(gdata.datas, function (val, i) {return { lng: val[0], lat: val[1], color: getcolor(gdata.colorDef, val[2]) }});
                    //_layer._map = app.map;
                    //_layer.addTo(app.map);

                    $_legend.empty().show();
                    L.DouLayer.Qqesums.genLegend($_legend, gparas.colorDef, 24, 16, '毫米(mm)', '預報雨量');
                    _layer.setData(gs);
                    $_silder = bindSilder($_p, _layer);

                    $("#other-layer-info .qpfqpe60-layer").removeClass('offdisplay');//.text(_linfo);
                    $("#other-layer-info .qpfqpe60-layer-time").text(helper.format.JsonDateStr2Datetime( r.Datetime).DateFormat('MM/dd HH:mm:ss'));
                    _layer.addTo(app.map);
                });

                timerreload();
            } else {
                if (_layer) {
                    if (s) {
                        _layer.addTo(app.map);
                        $_silder.removeClass('offdisplay');
                        //$_legend.removeClass('offdisplay');
                        $("#other-layer-info .qpfqpe60-layer").removeClass('offdisplay');
                    }
                    else {
                        _layer.remove();
                        $_silder.addClass('offdisplay');
                        //$_legend.addClass('offdisplay');
                        $("#other-layer-info .qpfqpe60-layer").addClass('offdisplay');
                    }
                }
                else
                    $("#other-layer-info .rader-layer").addClass('offdisplay');
            }
            if ($("#other-layer-info .qpfqpe60-layer").hasClass('offdisplay'))
                $_legend.hide();
            else
                $_legend.show();
        });
    });

    return $_p;
}

var bindSilder = function ($_c,g) {
    $_c.find('.opacity-container').remove();
    $_oslider = $('<div class="col-12 opacity-container"><div class="opacity-slider" title="透明度"></div></div>').appendTo($_c).find('.opacity-slider')
        .gis_layer_opacity_slider({
            map: app.map,
            range: "min",
            max: 100,
            min: 5,
            value: $_c[0].currentOpacity || 90,
            setOpacity: function (_op) {
                $_c[0].currentOpacity = _op * 100;
                g.setOpacity(_op);
            }
        });//.addClass('offdisplay');
    return $_oslider;
}

var InitDistrictCtrl = function ($_container, options) {
    var $_p = $('<div class="row"><div class="col-md-12"></div></div>').appendTo($_container);
    var tboundary = undefined;
    $_p.PinCtrl($.extend({
        map: app.map, useLabel: false, useList: false,name: '行政區'
    }, options)).on($.BasePinCtrl.eventKeys.initUICompleted, function () {
        $_p.find('.pinswitch').off('change').on('change', function () {
            var s = $(this).is(':checked');
            if (tboundary)
                tboundary.clear();
            if (s) {
                //高雄市boundary
                tboundary = new boundary.LineBoundary({
                    map: app.map, data: boundary.data.Town, ids: function (b) { return b.Other.indexOf('高雄市') >= 0; }, autoFitBounds: false,
                    style: {
                        strokeWeight: 2, dashArray: '2, 6', strokeColor: "#888888"
                    }
                });
            }
        });
    });
}

var appendQueryDurationPinInfoContent = function (data, infofields, oldPinInfoContent, $_pinctrl, idf, namef, dname) {
    var that = this;
    if (!oldPinInfoContent)
        return;
    var $_c = $(oldPinInfoContent.call(this, data, infofields)).addClass('history-data-chart');
    var $_dc = $('<div class="date-dur-ctrl row">' +
        '<div class="input-group  col"><span class="input-group-text">開始時間</span><input type="date" class="form-control"></div>' +
        '<div class="input-group  col"><span class="input-group-text">結束時間</span><input type="date" class="form-control"></div>' +
        '<span class="btn btn-sm btn-default glyphicon glyphicon-search  col-2">查詢</span>' +
        '<span class="btn btn-link glyphicon glyphicon-cloud-download  col-2">下載</span></div>').appendTo($_c.find('.carousel-item:eq(1)'));
    if (!data.qedt) {
        data.qsdt = new Date().addHours(- 24).DateFormat('yyyy-MM-dd');
        data.qedt = new Date().DateFormat('yyyy-MM-dd');
    }
    $_dc.find('input:eq(0)').attr('value', helper.format.JsonDateStr2Datetime(data.qsdt).DateFormat('yyyy-MM-dd') );
    $_dc.find('input:eq(1)').attr('value', helper.format.JsonDateStr2Datetime(data.qedt).DateFormat('yyyy-MM-dd')); //如有呼叫queryHistory後qedt會變成+TT23:59:59, set value會有問題

    var _carouselId = $_c.attr('id');
    var cdata = data;
    setTimeout(function () {
        var $_carousel = $('#' + _carouselId);

        var queryHistory = function () {
            cdata.qsdt = $_carousel.find('.date-dur-ctrl input:eq(0)').val() + 'T00:00:00';
            var sdd = $_carousel.find('.date-dur-ctrl input:eq(1)').val();
            cdata.qedt = $_carousel.find('.date-dur-ctrl input:eq(1)').val()+'T23:59:59';
            $_carousel.find('.carousel-item').addClass('active');

            $_carousel.trigger('slide.bs.carousel');
            setTimeout(function () {
                $_carousel.trigger('slid.bs.carousel');
            }, 100);
        }

        $_carousel.find('.date-dur-ctrl .glyphicon-search').on('click', function () {
            queryHistory();
        });

        that.$element.off('abc____').on('abc____', function () { //來置放大
            queryHistory();
        });
        $_carousel.find('.date-dur-ctrl .glyphicon-cloud-download').on('click', function () {
            var wb = {
                SheetNames: [data[idf]],//, 'Sheet4'],
                //Sheets: { Sheet1: XLSX.utils.json_to_sheet(sdata.data), Sheet4: XLSX.utils.json_to_sheet(ldata.data) },
                Sheets: {},
                Props: {}
            };
            wb.Sheets[data[idf]] = XLSX.utils.json_to_sheet(that.exportdatas, { skipHeader: true });//sdata.data);
            XLSX.writeFile(wb, '[' + data[idf] + ']測站' + dname + '觀測(' + cdata.qsdt + '_' + cdata.qedt + ')' + '.xlsx', { cellStyles: true, compression: true });
        });
    });
    if ($_pinctrl)
        $_pinctrl.off($.BasePinCtrl.eventKeys.fullInfowindowChange).on($.BasePinCtrl.eventKeys.fullInfowindowChange, function (e, er, z) {
            if (z)
                $(this).trigger('abc____')
        })
    return $_c[0].outerHTML;
}

