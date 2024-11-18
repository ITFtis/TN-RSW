$(document).ready(function () {

    const select_disaster = function () {
        var selected = $("#select-disaster option:selected");
        $(".disaster-content").html("<h5>" + selected.text() + "</h5><hr/>" + selected.data("desc"));
        $(".disaster-summary").html("");
        scan_st();
        changeSewer(data.currentSewer, true);
    }
    $("#select-disaster").off("change").on("change", select_disaster);

    const get_disaster_time = function () {
        var selected = $("#select-disaster option:selected");
        var d = selected.val().split('-');

        qsdt = new Date(d[1].substring(0, 4), d[1].substring(4, 6) - 1, d[1].substring(6, 8)).DateFormat("yyyy-MM-ddT00:00:00");
        qedt = new Date(d[2].substring(0, 4), d[2].substring(4, 6) - 1, (d[2].substring(6, 8) * 1) + 1.0).DateFormat("yyyy-MM-ddT00:00:00");
        return [qsdt, qedt];
    }

    window.defaultPinOpacity = .9;
    var isfromsensorshowInfoWindow;
    var $_sewerpinctrl, $_waterpinctrl, $_rainpinctrl;
    var data = data || {};
    data.currentSewer = undefined;
    data.currentWater = undefined;
    data.currentRain = undefined;
    data.sewer = [];
    data.legend_filter = [];
    data.water;
    data.rain;
    var _legendIcons = [
        { 'name': '正常', 'url': app.siteRoot + 'images/pin/下水道_正常.png', 'classes': 'green_status' },
        { 'name': '三級', 'url': app.siteRoot + 'images/pin/下水道_三級.png', 'classes': 'yellow_status' },
        { 'name': '二級', 'url': app.siteRoot + 'images/pin/下水道_二級.png', 'classes': 'orange_status' },
        { 'name': '一級', 'url': app.siteRoot + 'images/pin/下水道_一級.png', 'classes': 'red_status' },
        { 'name': '無資料', 'url': app.siteRoot + 'images/pin/下水道_無資料.png', 'classes': 'gray_status' }
    ];
    var $_sewercontent = $('.main-content > .display-cell > .sewer-content');
    var $_watercontent = $('.main-content > .display-cell > .water-content');
    var $_raincontent = $('.main-content > .display-cell > .rain-content');
    var _sewerchart, _waterchart, _rainchart;
    var querySewerId = helper.misc.getRequestParas()['sid'];

    //setTimeout(function () {
    //    if (querySewerId)
    //        $('.resize-ctrl').trigger('click');
    //});

    $('<div id="basemapDiv">').appendTo($('body')).hide();
    mapHelper.createMap('leaflet', function () {
        //if (querySewerId)
        //    $('.resize-ctrl').trigger('click');
        initSewer();
    });

    var scan_st = function () {
        const findmax = function (prev, current) {
            return (prev && prev.val61 > current.val61) ? prev : current
        }; //returns object
        if (data.sewer) {
            let alarm1 = 0;
            let alarm2 = 0;
            let alarm3 = 0;
            $_sewerpinctrl.find('.fixed-table-body td:nth-child(3)').hide();
            $_sewerpinctrl.find('.fixed-table-body td:nth-child(4)').hide();
            $.each(data.sewer, function (_i, _d) {
                var d = get_disaster_time();
                const s = { ..._d };
                s.qsdt = d[0];
                s.qedt = d[1];
                var tr = $(".pin-list-container .fixed-table-body tbody tr[data-index=" + _i + "]:eq(0)");
                tr.removeClass('green_status yellow_status orange_status red_status gray_status');
                datahelper.getSewerSerInfo(s, function (ds) {
                    var max_rec = ds.reduce(findmax, {});
                    if (max_rec && max_rec.val61) {
                        tr.find('td:nth-child(3)').html('<div style="white-space:nowrap">' + max_rec.datatime.replace('T','<br/>') + '</div>').show();
                        tr.find('td:nth-child(4)').text(max_rec.val61).show();
                        if (s.alarm1 && max_rec.val61 > s.alarm1) {
                            alarm1++;
                            tr.addClass("red_status");
                        } else if (s.alarm2 && max_rec.val61 > s.alarm2) {
                            alarm2++;
                            tr.addClass("orange_status");
                        } else if (s.alarm3 && max_rec.val61 > s.alarm3) {
                            alarm3++;
                            tr.addClass("yelllow_status");
                        }
                    }
                    if (_i == data.sewer.length - 1) {
                        $(".disaster-summary").html(
                            (alarm1 ? "下水道感應達一級警戒 " + alarm1 + "站<br/>" : "")
                            + (alarm2 ? "下水道感應達二級警戒 " + alarm2 + "站<br/>" : "")
                            + (alarm3 ? "下水道感應達三級警戒 " + alarm3 + "站<br/>" : "")
                        );
                        window.dispatchEvent(new Event('resize'));
                    }
                });
            });
        }
    }

    //改變下水道
    var changeSewer = function (s, c) {
        data.currentSewer = s;
        if ($_sewerpinctrl) {
            //if (isfromsensorshowInfoWindow)
            $_sewerpinctrl.instance.__pinctrl.instance._mapctrl.offFocusRowIndex();
            if (c)
                app.map.flyTo([s.lat, s.lon]);

            //$(".pin-list-container .fixed-table-body tbody tr").removeClass('meter-active');
            $.each(data.sewer, function (_i, _d) {
                if (_d.stt_no == data.currentSewer.stt_no) {
                    if (isfromsensorshowInfoWindow) {
                        var tr = $(".pin-list-container .fixed-table-body tbody tr[data-index=" + _i + "]:eq(0)");
                        if (tr && tr.length > 0)
                            tr.addClass("meter-active");
                    }
                    else {
                        $_sewerpinctrl.instance.__pinctrl.instance._mapctrl.onFocusRowIndex(_i);
                    }
                }
            });
        }


        isfromsensorshowInfoWindow = false;
        if (data.water)
            changeWater(findnearest(s, data.water));
        if (data.rain)
            changeRain(findnearest(s, data.rain));

        sewerChart(s);
        //showActivedpin($_sewerpinctrl, s, 'StationID', 100);
        showActivedpin($_sewerpinctrl, s, 'stt_no', 100);
    }

    var showActivedpin = function (pinctrl, s, sf, z) {
        var plusclass = ' actived-pin';
        $.each(pinctrl.instance.__pinctrl.instance._mapctrl.graphics, function () {
            var _icon = this.getIcon() || this;//g.getIcon() 20220915加入， 解決_icon.options.iconSize==null(因該新版leafleft問題), pin label位置一直動
            //_icon.shadowUrl = "http://localhost:54188/Scripts/gis/images/camera.png";
            var cns = _icon.options.className;//.replace(plusclass);
            var c = cns.indexOf(plusclass) >= 0 || this.attributes[sf] == s[sf];
            cns = cns.replace(plusclass, '');

            if (this.attributes[sf] == s[sf]) {
                cns += plusclass;
                //this.setZIndexOffset(window.pinLeafletMaxZIndex++);
                this.setZIndexOffset(window.pinLeafletMaxZIndex + 100000 + (z || 0));
            }
            if (c) {
                _icon.options.className = cns;
                this.setIcon(_icon);
            }
        })
    }

    var sewerChart = function (s) {
        var $_c = $_sewercontent.empty();
        var d = get_disaster_time();
        s.qsdt = d[0];
        s.qedt = d[1];
        helper.misc.showBusyIndicator($_sewercontent);
        datahelper.getSewerSerInfo(s, function (ds) {
            helper.misc.hideBusyIndicator($_sewercontent);
            var seriesdef = {
                level: {
                    name: '水位', color: '#0000FF', type: 'line', dt: "datatime", info: "level", unit: 'm', turboThreshold: 5000
                },
                warn: [
                    { name: '一級', color: '#FF0000', info: 'WarningLine1', turboThreshold: 5000 },
                    { name: '二級', color: '#FFA500', info: 'WarningLine2', turboThreshold: 5000 },
                    { name: '三級', color: '#FFFF00', info: 'WarningLine3', turboThreshold: 5000 },
                ],
                wave: { enabled: false }
            }
            //if (app.flood24hr && app.flood24hr.length > 1000)//turboThreshold預設1000，超過整個series就不會呈現
            //    seriesdef.level.turboThreshold = app.flood24hr.length;
            _sewerchart = charthelper.showMeterChart($_c, s, ds, s.stt_name + '-水位歷線圖', '水深(cm)', 0, seriesdef, function (chartoptions) {
                chartoptions.legend.enabled = false;
            }, function (idx, x, y, p) {
                charthelper.chartGearingByX(x, [_waterchart, _rainchart]);
            });
        });
    }

    //改變水位站
    var changeWater = function (s) {
        console.log('water:' + JSON.stringify(s));
        data.currentWater = s;
        var $_c = $_watercontent.empty();
        if (s == null)
            return;
        var d = get_disaster_time();
        s.qsdt = d[0];
        s.qedt = d[1];
        _waterchart = datahelper.getWaterSerInfo(s, function (ds) {
            var seriesdef = {
                level: {
                    name: '水位', color: '#0000FF', type: 'line', dt: "DateTime", info: "WaterLevel", unit: 'm', turboThreshold: 5000
                },
                warn: [
                    { name: '一級', color: '#FF0000', info: 'WarningLine1', turboThreshold: 5000 },
                    { name: '二級', color: '#FFA500', info: 'WarningLine2', turboThreshold: 5000 },
                    { name: '三級', color: '#FFFF00', info: 'WarningLine3', turboThreshold: 5000 },
                ],
                wave: { enabled: false }
            }
            //if (app.flood24hr && app.flood24hr.length > 1000)//turboThreshold預設1000，超過整個series就不會呈現
            //    seriesdef.level.turboThreshold = app.flood24hr.length;
            //20240808, edit by markhong 水深(cm)=>水位高程(M)
            _waterchart = charthelper.showMeterChart($_c, s, ds, s.StationName + '-水位歷線圖', '水位高程(M)', 0, seriesdef, function (chartoptions) {
                chartoptions.legend.enabled = false;
            }, function (idx, x, y, p) {
                charthelper.chartGearingByX(x, [_sewerchart, _rainchart]);
            });
        });
        showActivedpin($_waterpinctrl, s, 'StationID');
    }
    //改變雨量站
    var changeRain = function (s) {
        console.log('rain:' + JSON.stringify(s));
        data.currentRain = s;
        var $_c = $_raincontent.empty();
        if (s == null)
            return;
        var d = get_disaster_time();
        s.qsdt = d[0];
        s.qedt = d[1];
        var m10f = "Acc10MinuteRainQty";
        datahelper.getRainSerInfo(s, function (ds) {
            var cds = $.grep(ds, function (d) { if (d[m10f] < 0) d[m10f] = 0; return d[m10f] >= 0; });
            var seriesdef = {
                warn: [],
                level: { name: '雨量', color: '#0000FF', type: 'column', yAxis: 1, dt: 'InfoTime', info: m10f, unit: 'mm', turboThreshold: 5000 },
                sumlevel: { name: '累計雨量', color: '#FF0000', type: 'line', threshold: 0, unit: 'mm', marker: { enabled: true, states: { hover: { enabled: false }, turboThreshold: 5000 } } },
                wave: { enabled: false }
            }
            //if (app.flood24hr && app.flood24hr.length > 1000)//turboThreshold預設1000，超過整個series就不會呈現
            //    seriesdef.level.turboThreshold = app.flood24hr.length;
            _rainchart = charthelper.showMeterChart($_c, s, cds, s.StationName+ '-雨量歷線圖', '雨量(mm)', 0, seriesdef,
                function (chartoptions) {
                    chartoptions.legend.enabled = false;
                    //console.log(infos);
                    chartoptions.yAxis.title.margin = 0;
                    chartoptions.yAxis.title.tickPixelInterval = 40;
                    //chartoptions.xAxis.labels.formatter = function () {
                    //    var ff = function (s) {
                    //        return helper.format.paddingLeft(s, '0', 2);
                    //    }
                    //    var _date = new Date(this.value);
                    //    return ff(_date.getHours()) + ':' + ff(_date.getMinutes());
                    //}

                    chartoptions.chart.backgroundColor = undefined;
                    chartoptions.shared = false;
                    chartoptions.crosshairs = false;


                    chartoptions.legend.itemMarginBottom = 3;
                    chartoptions.legend.itemMarginTop = 1;

                    var _max1H = 0, _sum = 0;
                    $.each(ds, function () {
                        var _r = this[m10f] < 0 ? 0 : this[m10f];
                        _max1H = _max1H < _r ? _r : _max1H;
                        _sum += _r;
                    });
                    chartoptions.xAxis = [chartoptions.xAxis, { lineWidth: 1, opposite: true }];
                    chartoptions.yAxis = [{
                        title: {
                            text: '累計雨量(mm)',
                            margin: 4
                        },
                        labels: {
                            x: -4,
                            formatter: function () {
                                //return this.value < _sum + this.axis.tickInterval ? this.value : '';
                                return this.value < _sum + (_sum % this.axis.tickInterval == 0 ? 0 : this.axis.tickInterval) ? this.value : '';
                            }
                        },
                        max: _sum == 0 ? 1 : _sum * 2.1,
                        min: 0,
                        lineWidth: 1,
                        gridLineWidth: 0,
                        gridLineDashStyle: 'dash',
                        startOnTick: false,
                        endOnTick: false,
                        //minTickInterval: 1,
                        //tickAmount: 4,
                    },
                    {
                        title: {
                            text: '雨量(mm)',
                            margin: 4
                        },
                        labels: {
                            x: 4,
                            formatter: function () {
                                return this.value < _max1H + (_max1H % this.axis.tickInterval == 0 ? 0 : this.axis.tickInterval) ? this.value : '';
                            }
                        },
                        max: _max1H == 0 ? 1 : _max1H * 2.1,
                        min: 0,
                        lineWidth: 1,
                        reversed: true,
                        gridLineWidth: 0,
                        gridLineDashStyle: 'dash',
                        startOnTick: false,
                        endOnTick: false,
                        opposite: true,
                        //tickAmount: 12
                    }
                    ];
                }, function (idx, x, y, p) {

                    charthelper.chartGearingByX(x, [_sewerchart, _waterchart]);
                }
            );
        });
        showActivedpin($_rainpinctrl, s, 'StationID');
    }

    //找出最近點(確定source, targets得座標欄位是lon,lat)
    var findnearest = function (source, targets, lon, lat) {
        if (!source || !targets)
            return null;
        lon = lon || 'X';
        lat = lat || 'Y';
        var r = null;
        var d = 9999999999;
        $.each(targets, function () {
            if (!this)
                return;
            var t = this;
            var _d = helper.gis.pointDistance([source.X, source.Y], [eval('this.' + lon), eval('this.' + lat)]);
            if (_d < d) {
                d = _d;
                r = t;
            }
        });
        return r;
    }

    //******資料初始化*******
    //下水道
    helper.misc.showBusyIndicator($_sewercontent);
    var isInitSewer = false;
    var initSewer = function () {
        $_sewerpinctrl = $('.disaster-panel > .meter').sewer({ map: app.map, defaultShowAllStatus: true, autoReload: { auto: false } }).on('get-data-complete', function (e, ds) {
            data.sewer = ds;/*$_fsta.fsta('setSewerData', ds);*/
            setTimeout(select_disaster, 0);
            $(window).on('resize', function () {
                setTimeout(() => {
                    $('.jsPanel-content').css('padding', '2px');
                    $_sewerpinctrl.find('.fixed-table-header th:nth-child(3)').text('發生時間').css('width', '40%');
                    $_sewerpinctrl.find('.fixed-table-header th:nth-child(4)').text('最高水位').css('width', '15%');
                }, 100);
            });
            $_sewerpinctrl.find('.legend').css('position', 'absolute').css('left', '8%');
            $_sewerpinctrl.find('.legend .legend-icon').off('click').on('click', function () {
                const t = $(this).text();
                if (data.legend_filter.find(x => x.name == t)) {
                    data.legend_filter = data.legend_filter.filter(x => x.name != t);
                } else {
                    data.legend_filter.push(_legendIcons.find(x => x.name == t));
                }
                if (data.legend_filter.length) {
                    $_sewerpinctrl.find('.legend .legend-icon').css('filter', 'grayscale(1.2)').css('font-weight','').css('text-decoration','');
                    $_sewerpinctrl.find('.fixed-table-body tbody tr').hide();
                    for (let f of data.legend_filter) {
                        $_sewerpinctrl.find('.legend .legend-icon').each(function () {
                            if ($(this).text() == f.name) {
                                $(this).css('filter', '').css('font-weight', '800').css('text-decoration', 'underline');
                            }
                        });
                        $_sewerpinctrl.find('.fixed-table-body tr.' + f.classes).show();
                    }
                } else {
                    $_sewerpinctrl.find('.fixed-table-body tbody tr').show();
                    $_sewerpinctrl.find('.legend .legend-icon').css('filter', '').css('font-weight', '').css('text-decoration', '');
                }
            });
            $_sewerpinctrl.find('.filter-container').hide();
            $_sewerpinctrl.find('.glyphicon-search').hide();
            $_sewerpinctrl.find('.fixed-table-body').css('overflow-y','scroll').css('max-height','calc(100vh - 480px)');
            $_sewerpinctrl.find('.fixed-table-body td:nth-child(3)').hide();
            $_sewerpinctrl.find('.fixed-table-body td:nth-child(4)').hide();

            if (!isInitSewer) {
                $_sewerpinctrl.WaterCtrl('fitBounds', { padding: [20, 20] });
                initWater();
                initRain();
                //預設呈現資料
                var temps;
                if (querySewerId)
                    temps = $.grep(ds, function (d) { return d.StationID == querySewerId; });

                if (!temps || temps.length == 0)
                    temps = ds;
                data.currentSewer = temps[0];
                $('.fix-popu-ctrl-toleft').removeClass('fixed-show');
                helper.misc.hideBusyIndicator($_sewercontent);
            }
            else {
                showActivedpin($_sewerpinctrl, data.currentSewer, 'StationID');
            }

            $('.query-time').text(new Date().DateFormat('yyyy/MM/dd HH:mm:ss'));

            isInitSewer = true;

        }).sewer('getPinCtrlInstance');
        //$_sewerpinctrl = $(this).find('.pinctrl');
        $_sewerpinctrl.instance.__pinctrl.instance._mapctrl.showInfoWindow = function (g, c) { //點選清單後會觸發
            isfromsensorshowInfoWindow = true;
            changeSewer(g.attributes, c);
            //$('.fix-popu-ctrl-toleft').removeClass('fixed-show');
        }
    }
    //初始水位站
    helper.misc.showBusyIndicator($_watercontent);
    var isInitWater = false;
    var initWater = function () {
        $_waterpinctrl = $('<div>').appendTo('body').water({ map: app.map, defaultShowAllStatus: true }).on('get-data-complete', function (e, ds) {
            data.water = ds;
            if (!isInitWater) {
                $_waterpinctrl.find('.legend-disable').trigger('click');
                changeWater(findnearest(data.currentSewer, ds));
                helper.misc.hideBusyIndicator($_watercontent);
            }
            else {
                showActivedpin($_waterpinctrl, data.currentWater, 'StationID');
            }
            isInitWater = true;
        }).water('getPinCtrlInstance').hide();
        $_waterpinctrl.instance.__pinctrl.instance._mapctrl.showInfoWindow = function (g, c) { //點選清單後會觸發
            changeWater(g.attributes);
        }
    }
    //初始雨量站
    helper.misc.showBusyIndicator($_raincontent);
    var isInitRain = false;
    var initRain = function () {
        $_rainpinctrl = $('<div class="meter">').appendTo('body').rain({ map: app.map }).on('get-data-complete', function (e, ds) {
            data.rain = ds;
            if (!isInitRain) {
                $_rainpinctrl.find('.legend-disable').trigger('click');
                changeRain(findnearest(data.currentSewer, ds));
                helper.misc.hideBusyIndicator($_raincontent);
            }
            else
                showActivedpin($_rainpinctrl, data.currentRain, 'StationID');
            isInitRain = true;
        }).rain('getPinCtrlInstance').hide();;
        $_rainpinctrl.instance.__pinctrl.instance._mapctrl.showInfoWindow = function (g, c) { //點選清單後會觸發
            changeRain(g.attributes);
        }
    }
});