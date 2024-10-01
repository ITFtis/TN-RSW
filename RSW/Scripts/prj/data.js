window.app = window.app || {};

app.CSgdsRoot = "https://www.dprcflood.org.tw/SGDS/";
var fmgcctvtoken = 'eXysP97yhN';
var fmgcctvsource = 'https://fmg.wra.gov.tw/swagger/api/';
var sewerapisource = 'https://waterinfo.tainan.gov.tw/RSW2/';
if (location.href.indexOf('.ftis.') >= 0 || location.href.indexOf('localhost') >= 0)
    sewerapisource = 'https://pj.ftis.org.tw/RSW2/';
(function (window) {

    var getData = function (url, paras, callback, option) {
        var _ajaxoptions = $.extend({
            url: url,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8", //加contentType IE有問題，但放在server同一domain是OK的
            //async: _async,
            data: paras
        }, option);

        //console.log(url + '參數:' + JSON.stringify(paras));
        $.ajax(_ajaxoptions)
            .done(function (dat, status) {
                var d = dat.d ? dat.d : (dat.Data ? dat.Data : dat);
                d = d.Data ? d.Data: d;
                callback(d); //dat.Data是fly v2
            }).fail(function (dat, status) {
                console.log('error:' + dat.responseText);
            });
    };

    //土地利用類型
    var getLandUseType = function () {
        if (!window.landUseType) {
            getData(app.CSgdsRoot + "WS/FloodComputeWS.asmx/LandUseType", undefined, function (d) {
                window.landUseType = d;
            }, { type: "POST", async: false });
        }
        return window.landUseType;
    };
    //取水利署事件清單 >>災情資訊查詢用
    var loadWraEvents = function (callback) {
        if (!window.waEvents) {
            getData(app.CSgdsRoot + "WS/FloodComputeWS.asmx/WraEvents", undefined, function (d) {
                window.waEvents = d;
                callback(window.waEvents);
            }, { type: "POST", async: false });
        }
        else
            callback(window.waEvents);
    }
    
    //取單一演算結果DTM
    var loadDEMCalculateData = function (_FloodingClass, callback) {
        var _cd = $.extend({}, _FloodingClass);
        _cd.DATE = JsonDateStr2Datetime(_cd.DATE);
        if (_cd.Recession_DATE) _cd.Recession_DATE = JsonDateStr2Datetime(_cd.Recession_DATE);
        if (_cd.CREATE_DATE) _cd.CREATE_DATE = JsonDateStr2Datetime(_cd.CREATE_DATE);
        if (_cd.MODIFY_DATE) _cd.MODIFY_DATE = JsonDateStr2Datetime(_cd.MODIFY_DATE);
        getData(app.CSgdsRoot + "WS/FloodComputeWS.asmx/GetDEMCalculateData", JSON.stringify({ computeDistance: 500, ds: _cd }), function (d) {
            callback(d);
        }, { type: "POST"});
    };

    var convertFloodToUiObject = function (ds) {
        console.log('淹水點:' + ds.length);
        //flood:來至水利署(同一網格只用淹水+高程最大資料取代影響戶數及土地利用); floodarea:同一網格只用淹水+高程最大資料; handdrawflood:人工圈繪; statistics:僅用來至水利署且同一網格只用淹水+高程最大資料
        var _r = { flood: [], floodarea: [], handdrawflood: [], statistics: [] };
        var _floodkey = [];
        var _floodGroup = [];
        var _handdrawfloodkey = [];
        var _handdrawfloodGroup = [];

        var _handdrawfloodGridId = [];
        $.each(ds, function () {
            if (!this.NOTIFICATION_Data) {
                console.log(this.EffectAddress);
                return;
            }
            $.extend(this, this.NOTIFICATION_Data);
            this.Described = this.NOTIFICATION_Data.Described;

            var _key = (this.GridId == 0 ? this.PK_ID : this.GridId) + this.COUNTY_NAME + this.TOWN_NAME;
            //if (Math.random() % 5 == 0)
            //    this.IsFromWraEMIS = false;
            if (this.IsFromWraEMIS) {
                //組group
                if (_floodkey.indexOf(_key) < 0) {
                    _floodkey.push(_key);
                    _floodGroup.push({ key: _key, g: [this] });
                }
                else {
                    var _g = $.grep(_floodGroup, function (_gg) {
                        return _gg.key == _key;
                    })[0];
                    _g.g.push(this);

                }
                _r.flood.push(this);
            }
            else {
                //組group
                if (_handdrawfloodkey.indexOf(_key) < 0) {
                    _handdrawfloodkey.push(_key);
                    _handdrawfloodGroup.push({ key: _key, g: [this] });
                }
                else {
                    var _g = $.grep(_handdrawfloodGroup, function (_gg) {
                        return _gg.key == _key;
                    })[0];
                    _g.g.push(this);

                }
                _r.handdrawflood.push(this);
            }


        });
        //flood找出最大淹水深度+高程,並改同一group計算值
        $.each(_floodGroup, function () {
            var maxdata = this.g[0];
            var maxdataidx = 0;
            if (this.g.length != 1) {
                ////找出最大淹水深度+高程

                $.each(this.g, function (_idx) {
                    if ((this.DEPTH / 100 + this.Z_D) > (maxdata.DEPTH / 100 + maxdata.Z_D)) {
                        maxdata = this;
                        maxdataidx = _idx;
                    }
                });

            }
            $.each(this.g, function (_idx) {
                if (_idx != maxdataidx) {
                    this._Land = this.Land;
                    this.Land = maxdata.Land;
                    this._AffectStat = this.AffectStat;
                    this.AffectStat = maxdata.AffectStat;
                }

                //infoField用
                this.AffectHouse = maxdata.AffectStat ? maxdata.AffectStat.TotalHouse : 0;
                this.AffectArea = maxdata.AffectStat ? maxdata.AffectStat.TotalArea : 0;
                this.AffectHouse30cmUp = maxdata.AffectStat ? maxdata.AffectStat.TotalHouse30cmUp : 0;
                this.AffectArea30cmUp = maxdata.AffectStat ? maxdata.AffectStat.TotalArea30cmUp : 0;
                this.AffectHouse50cmUp = maxdata.AffectStat ? maxdata.AffectStat.TotalHouse50cmUp : 0;
                this.AffectArea50cmUp = maxdata.AffectStat ? maxdata.AffectStat.TotalArea50cmUp : 0;
            });
            _r.statistics.push(maxdata); //加入statistics
            _r.floodarea.push(maxdata);     //加入floodarea
        });
        //handdrawflood找出最大淹水深度+高程,並改同一group計算值
        $.each(_handdrawfloodGroup, function () {
            var maxdata = this.g[0];
            if (this.g.length != 1) {
                ////找出最大淹水深度+高程

                $.each(this.g, function () {
                    if ((this.DEPTH / 100 + this.Z_D) > (maxdata.DEPTH / 100 + maxdata.Z_D))
                        maxdata = this;
                });
            }
            $.each(this.g, function () {
                this._Land = this.Land;
                this.Land = maxdata.Land;
                this._AffectStat = this.AffectStat;
                this.AffectStat = maxdata.AffectStat;

                //infoField用
                this.AffectHouse = maxdata.AffectStat ? maxdata.AffectStat.TotalHouse : 0;
                this.AffectArea = maxdata.AffectStat ? maxdata.AffectStat.TotalArea : 0;
                this.AffectHouse30cmUp = maxdata.AffectStat ? maxdata.AffectStat.TotalHouse30cmUp : 0;
                this.AffectArea30cmUp = maxdata.AffectStat ? maxdata.AffectStat.TotalArea30cmUp : 0;
                this.AffectHouse50cmUp = maxdata.AffectStat ? maxdata.AffectStat.TotalHouse50cmUp : 0;
                this.AffectArea50cmUp = maxdata.AffectStat ? maxdata.AffectStat.TotalArea50cmUp : 0;
            });
            _r.floodarea.push(maxdata);     //加入floodarea
        });

        return _r;
    }

    //
    var estimateFloodingComputeForLightweightDatas = function (floodings, callback) {
        getData(app.CSgdsRoot + "WS/FloodComputeWS.asmx/EstimateFlooding", JSON.stringify({ floodings: floodings }), function (d) {
            callback(convertFloodToUiObject(d));
        }, { type: "POST" });
    }

    //取淹水演算結果
    var loadFloodComputeForLightweightDatas = function (st, et, countyID, datatype, callback) {
        //st = '2018/08/22 16:55:00';
        //et = '2018/08/31 17:30:00';
        //datatype = 0;
        countyID = 17;
        getData(app.CSgdsRoot + "WS/FloodComputeWS.asmx/GetFloodComputeForLightweightData", JSON.stringify({ beginDT: st, endDT: et, computeDistance: 500, CountyID: countyID, dataType: datatype }), function (d) {
            callback(convertFloodToUiObject(d));
        }, { type: "POST" });
    }


    var getFHYFloodSensorInfoLast24Hours_Address = function (address, callback) {
        getData(app.CSgdsRoot + "WS/FHYBrokerWS.asmx/GetFHYFloodSensorInfoLast24Hours_Address", JSON.stringify({ 'address': address }), function (d) {
            callback(d);
        }, { type: "POST" });
    }
    var getFHYFloodSensorStation = function (callback) {
        getData(app.CSgdsRoot + 'WS/FHYBrokerWS.asmx/GetFHYFloodSensorStationByCityCode', JSON.stringify({ cityCode:64}), callback, { type: 'POST' });
    }
    var getFHYFloodSensorInfoRt = function (dt, callback) {
        getData(app.CSgdsRoot + 'WS/FHYBrokerWS.asmx/GetFHYFloodSensorInfoRt', undefined, callback, { type: 'POST' });
    }
    var getFHYFloodSensorInfoLast24Hours = function (id, callback) {
        getData(app.CSgdsRoot + 'WS/FHYBrokerWS.asmx/GetFHYFloodSensorInfoLast24Hours', JSON.stringify({ sensorUUID: id }), callback, { type: 'POST' });
    }
    var getFHYFloodSensorInfoByDuration = function (d, callback) {
        var s = new Date(d.qsdt).DateFormat('yyyy/MM/dd HH:mm:ss');
        var e = new Date(d.qedt).DateFormat('yyyy/MM/dd HH:mm:ss');
        getData(app.CSgdsRoot + 'WS/FHYBrokerWS.asmx/GetFHYFloodSensorInfoByDuration', JSON.stringify({ sensorUUID: d.SensorUUID, start: s, end: e  }), callback, { type: 'POST' });
    }
  
    var getFHYTown = function (cityCode, callback) {
        var k = 'fhyTown' + cityCode;
        if (window[k]) {
            callback(window[k]);
        }
        else
            getData(app.CSgdsRoot + "WS/FHYBrokerWS.asmx/GetFHYTown", JSON.stringify({ cityCode: cityCode }), function (d) {
                window[k] = d;
                callback(window[k])
            }, { type: 'POST' });
    }

    var getRainBase = function (callback) {
        helper.data.get(app.siteRoot + "api/rain/base", callback);
    }

    var getRainRt = function (dt, callback) {
        helper.data.get(app.siteRoot + "api/TainanApi/Rain/Infos/Accumulation/Realtime", callback);
    }

    var getRainSerInfo = function (d, callback) {
        //var u = app.siteRoot + "api/rain/timeser/" + d.ST_NO + "/" + d.Source + (d.qsdt? "/" + d.qsdt + "/" + d.qedt:"") ;
        var et = helper.format.JsonDateStr2Datetime(d.InfoTime);
        var st = helper.format.JsonDateStr2Datetime(et + "").addHours(-24);
        if (d.qsdt) {
            st = helper.format.JsonDateStr2Datetime(d.qsdt);
            et = helper.format.JsonDateStr2Datetime(d.qedt);
        }
        var u = app.siteRoot + "api/TainanApi/Rain/Info/Accumulation/History/" + d.StationID + "/" + + st.DateFormat('yyyyMMddHHmm') + "/" + et.DateFormat('yyyyMMddHHmm') ;
        helper.data.get(u, callback);
    }

    var getFloodWaterBase = function (callback) {
        helper.data.get(app.siteRoot + "api/floodwater/base", callback);
    }
    var getFloodWaterRt = function (dt, callback) {
        helper.data.get(app.siteRoot + "api/TainanApi/FloodWater/Infos/Realtime", callback);
    }
    var getFloodWaterSerInfo = function (d, callback) {
        //var u = app.siteRoot + "api/rain/timeser/" + d.ST_NO + "/" + d.Source + (d.qsdt? "/" + d.qsdt + "/" + d.qedt:"") ;
        var et = helper.format.JsonDateStr2Datetime(d.InfoTime);
        var st = helper.format.JsonDateStr2Datetime(et + "").addHours(-24);
        var u = app.siteRoot + "api/TainanApi/FloodWater/Infos/Last24Hours/" + d.StationID;
        helper.data.get(u, callback);
    }

    var getWaterBase = function (callback) {
        helper.data.get(app.siteRoot + "api/water/base", callback);
    }
    var getWaterRt = function (dt, callback) {
        helper.data.get(app.siteRoot + "api/TainanApi/Water/Infos/Realtime", callback);
    }
    var getWaterSerInfo = function (d, callback) {
        var st = new Date(d["Datetime"]).addHours(-24);
        var et = new Date(d["Datetime"]);
        if (d.qsdt) {
            st = helper.format.JsonDateStr2Datetime(d.qsdt);
            et = helper.format.JsonDateStr2Datetime(d.qedt);
        }
        helper.data.get("https://water.tainan.gov.tw/WebServices/WaterService.asmx/GetHourInfos",
            function (r) {
                callback(r.d);
            }, {
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: "{ 'stationID':'" + d["StationID"] + "', 'beginDateTime':'" + st.DateFormat("yyyy/MM/dd HH:mm:ss") + "', 'endDateTime':'" + et.DateFormat("yyyy/MM/dd HH:mm:ss") + "' }"
            });
    }

    var getSewerBase = function (callback) {
        helper.data.get(sewerapisource + 'sewerwater/stations', callback, { dataType: "json"});
    }
    var getSewerRt = function (dt, callback) {
        helper.data.get(sewerapisource + 'sewerwater/Realtime', callback);
    }

    var getSewerSerInfo = function (d, callback) {
        var st = new Date().addHours(-24);
        var et = new Date();
        if (d.qsdt) {
            st = helper.format.JsonDateStr2Datetime(d.qsdt);
            et = helper.format.JsonDateStr2Datetime(d.qedt);
        }
        helper.data.get(sewerapisource + 'sewerwater/History/' + d.dev_id + '/' + st.DateFormat('yyyyMMddHHmm') + '/' + et.DateFormat('yyyyMMddHHmm'), callback);
    }

    var getFLStations = function (callback) {
        helper.data.get(sewerapisource + 'sewerwater/FLStations', callback, { dataType: "json" });
    }
    var getFLRealtime = function (dt, callback) {
        helper.data.get(sewerapisource + 'sewerwater/FLRealtime', callback);
    }

    var getFLSerInfo = function (d, callback) {
        var st = new Date().addHours(-24);
        var et = new Date();
        if (d.qsdt) {
            st = helper.format.JsonDateStr2Datetime(d.qsdt);
            et = helper.format.JsonDateStr2Datetime(d.qedt);
        }
        helper.data.get(sewerapisource + 'sewerwater/History/' + d.dev_id + '/' + st.DateFormat('yyyyMMddHHmm') + '/' + et.DateFormat('yyyyMMddHHmm'), callback);
        //    function (ds) {
        //    callback(  ds.filter(d => d.level != null));
        //});
    }

    var getMovingPumpRt = function (dt, callback) {
        helper.data.get(app.siteRoot + "api/TainanApi/MobilePumper/Infos/Realtime", callback);
    }
    var getMovingPumpBase = function (callback) {
        helper.data.get(app.siteRoot + "api/TainanApi/MobilePumper/Stations" ,callback);
    }
    var qs = "StationNo eq '30303' or StationNo eq '30401' or StationNo eq '30402' or StationNo eq '30403' or StationNo eq '30501' or StationNo eq '30502' or StationNo eq '30503' or StationNo eq '30504' or StationNo eq '30601' or StationNo eq '30602' or StationNo eq '30603'";

    var getReservoirBase = function (callback) {
        $.BasePinCtrl.helper.getWraFhyApi("Reservoir/Station?$filter=" + qs, undefined, function (d) {
            callback(d.Data);
        })
    }
    var getReservoirRt = function (dt, callback) {
        $.BasePinCtrl.helper.getWraFhyApi("Reservoir/Info/RealTime?$filter=" + qs, undefined, function (d) {
            callback(d.Data);
        })
    }

    var getTnWrbCCTV = function (callback) {
        helper.data.get(app.siteRoot + "api/cctv/tnwrb/base", callback);
    }
    

    var get即時監視影像CCTV = function (callback) {

        var cscounttemp = 0;
        var k = 'getOtherHCCTV';
        if (window[k]) {
            callback(window[k]);
        }
        else {
            var cctvbase = [];
            var ddd = Date.now();
            var fmgok = false, cextraok = false;
            //fmgok = true;
            $.get(app.siteRoot + 'api/fmg/get/allstabase', function (ss) {
                $.each(ss, function () {
                    if (this.x_tm97 != undefined) {

                        var coor = helper.gis.TWD97ToWGS84(this.x_tm97, this.y_tm97);
                        this.type = 1;
                        this.X = coor.lon;
                        this.Y = coor.lat;
                        this.org = this.source_name;
                    }
                });
                cctvbase = cctvbase.concat(ss);
                fmgok = true;
                if (fmgok && cextraok) {
                    window[k] = cctvbase;
                    callback(cctvbase);
                }
            });
            //getJson(fmgcctvsource + 'source?token=' + fmgcctvtoken, function (ss) {
            //    $.each(ss.cctvSource, function () {
            //        var s = this;
            //        console.log(fmgcctvsource + 'cctv_station?counid=2&sourceid=' + s.sourceid + '&token=' + fmgcctvtoken);
            //        getJson(fmgcctvsource + 'cctv_station?counid=2&sourceid=' + s.sourceid + '&token=' + fmgcctvtoken, function (sts) {
            //            var cts = [];
            //            $.each(sts.cctvs, function () {
            //                if (this.x_tm97 != undefined) {
                                
            //                    var coor = helper.gis.TWD97ToWGS84(this.x_tm97, this.y_tm97);
            //                    this.type = 1;
            //                    this.sourceid = s.sourceid;
            //                    this.X = coor.lon;
            //                    this.Y = coor.lat;
            //                    this.org = this.source_name;
            //                    cts.push(this);//改及時抓API
            //                }
            //            });
            //            cctvbase = cctvbase.concat(cts);
            //            cscounttemp++;
            //            console.log(s.name + "完成" + cscounttemp + "/" + ss.cctvSource.length);
            //            if (cscounttemp == ss.cctvSource.length) {
            //                console.log("抓cctv時間:" + (Date.now() - ddd) + " 共" + ss.cctvSource.length + "筆");
            //                fmgok = true;
            //                if (fmgok && cextraok) {
            //                    window[k] = cctvbase;
            //                    callback(cctvbase);
            //                }
            //            }
            //        });
                    
            //    })
                
            //});
            $.get(app.CSgdsRoot + "kml/cctv_extra.json", function (d) {
                var cts = [];
                $.each(d, function () {
                    if (this.lon && this.city == '高雄市') {// helper.gis.pointInPolygon([this.lon, this.lat], app.hkbounds)) {
                        this.type = 99;
                        this.X = this.lon;
                        this.Y = this.lat;
                        this.counname = this.city;
                        this.town_name = this.town;
                        this.source_name = this.org;
                        this.urls = [{ id: this.id, name: this.name, url: this.url }];
                        //this.isvideo = this.url.indexOf('/jpg.php') < 0 && this.url.indexOf('/rt.jpg') < 0 && this.url.indexOf('/Image') < 0 && this.url.indexOf('/images') < 0;
                        this.isvideo = this.url.indexOf('/jpg.php') < 0 && !this.url.endsWith('.jpg') && this.url.indexOf('/Image') < 0 && this.url.indexOf('/images') < 0;
                        cts.push(this);
                    }
                })
                cctvbase = cctvbase.concat(cts);
                cextraok = true;
                if (fmgok && cextraok) {
                    window[k] = cctvbase;
                    callback(cctvbase);
                }
            });

        }

    }

    var getFmgOneCCTV = function (id, sourceid) {
        var r;
        helper.data.get(app.siteRoot + 'api/fmg/get/cctv/' + id + '/' + sourceid, function (c) {
            r = c;
        }, { async: false },true);
        //getJson(fmgcctvsource + 'cctv/' + id + '?sourceid=' + sourceid + '&token=' + fmgcctvtoken, function (c) {
        //    r = c;
        //}, { async: false });
        return r;
    }

    var getTideBase = function (callback) {
        //helper.data.get(app.siteRoot + encodeURIComponent("Data/台南潮位站.json"), callback, { contentType:undefined});
        helper.data.get(app.siteRoot + "Data/Tide.json", callback, { contentType: undefined });
    }

    var getTideRt = function (dt, callback) {
        helper.data.get("https://opendata.cwb.gov.tw/api/v1/rest/datastore/O-B0075-001?Authorization=CWB-3ADFC2B7-CF1B-42BF-969D-28CCC65F2720&format=JSON&StationID=C4N01,11781,46778A,4A&sort=DataTime", function (d) {
            var rs = [];
            if (d.Success) {
                $.each(d.Records.SeaSurfaceObs.Location, function () {
                    //排除無資料
                    var sds = $.grep(this.StationObsTimes.StationObsTime, function (s) {
                        return s.WeatherElements.TideHeight != 'None';
                    });
                    var rt = sds[sds.length - 1];//最新一筆
                    if (!rt)
                        return;
                    var t = { id: this.Station.StationID, time: rt.DateTime, height: rt.WeatherElements.TideHeight, status: rt.WeatherElements.TideLevel, sers: [] };
                    //時間序列資料
                    t.sers = $.map(sds, function (_t) { return { time: _t.DateTime, height: parseFloat( _t.WeatherElements.TideHeight) } });
                    rs.push(t);
                });
            }
            callback(rs)
        });
    }


    var getTown = function (callback) {
        helper.data.get(app.siteRoot + "api/TainanApi/Basic/District", function (ts) {
            callback($.grep(ts, function (t) { //排除嘉義及高雄
                return t.DistrictID && (t.DistrictID + "").indexOf('7') == 0;
            }));
        },undefined, true)
    }

    var getTnwrbarcgislegend = function (_server, callback) {
        _server = datahelper.tnwrbarcgisUrl;// 'https://water.tainan.gov.tw/tnwrbarcgis104/rest/services/TNRW/IISI_rainwater/MapServer';

        //if (!_server.startsWith(datahelper.proxy))
        //    _server = datahelper.proxy + '?' + _server;
        helper.data.get((_server.substr(_server.length - 2) == '/' ? _server : _server + '/') + 'legend?f=pjson', function (d) {
            callback(JSON.parse(d).layers)
        });
    }

    //預抓資料
    var preInitData = function () {
        //var options = {
        //    url: "https://wrbweb.tainan.gov.tw/TainanApi/Basic/District", type: "GET",// dataType: "json",// crossDomain: true,
        //    headers: { 'apikey': 'F2AC69CA-CBF6-4851-9B26-3961D2EA3D1A' }
        //};
        //$.ajax(options).done(function (data, status) {
        //    callback(data);
        //}).fail(function (data, status) {
        //    callback(data, status);
        //});

        //var sdd = JsonDateStr2Datetime('2020/10/01 00:00:01').toJSON();
        //getTideBase();
        getTown();

        getSewerBase(function (dsdf, sda) {
            var as = dsdf;
        });
        //boundary.helper.GetBoundaryData(boundary.data.County, function (b) { });
        //boundary.helper.GetBoundaryData(boundary.data.Town, function (b) { });
        datahelper.getLandUseType();
        $('body').trigger('preInitData-completed');
    }
    var getCadastreSecCode = function (callback) {
        helper.data.get(app.siteRoot + "Data/CadastreSecCode.json", callback);
    }
    var getCadastreSecAllNo = function (sec, callback) {
        helper.data.get('https://waterinfo.tainan.gov.tw/RSW/api/cadastre/'+sec, callback,undefined, true);
    }

    window.datahelper = {
        preInitData: preInitData,                                                   //初始基本資料

        //getKhfloodinfoToke: getKhfloodinfoToke,                                     //取KhfloodinfoToke，CCTV用

        getRainBase: getRainBase,                                                   //雨量站站資訊
        getRainRt: getRainRt,                                                       //雨量站即時資訊
        getRainSerInfo: getRainSerInfo,                                             //雨量站歷線資料

        getFloodWaterBase: getFloodWaterBase,                                       //淹水感測資訊
        getFloodWaterRt: getFloodWaterRt,                                           //淹水感測即時資訊
        getFloodWaterSerInfo: getFloodWaterSerInfo,                                 //淹水感測24小時歷線資料


        getWaterBase: getWaterBase,
        getWaterRt: getWaterRt,                                                     //水位站資訊(含水利署、水利局)
        getWaterSerInfo: getWaterSerInfo,                                           //水位歷線資料


        
        getTnWrbCCTV: getTnWrbCCTV,
        getFmgOneCCTV: getFmgOneCCTV,
        //getAllCCTV: getAllCCTV,                                                   //六宮格用

        getMovingPumpBase: getMovingPumpBase,                                       //移動抽水機即時資料
        getMovingPumpRt: getMovingPumpRt,                                           //移動抽水機歷史資料

        getSewerBase: getSewerBase,                                                 //下水道基本資料
        getSewerRt: getSewerRt,                                                     //下水道即時資料
        getSewerSerInfo: getSewerSerInfo,                                           //下水道歷史區間資料

        getFLStations: getFLStations,
        getFLRealtime: getFLRealtime,
        getFLSerInfo: getFLSerInfo,

        getTideBase: getTideBase,                                                   //潮位基本資料
        getTideRt: getTideRt,                                                       //潮位即時資料

        getReservoirBase: getReservoirBase,                                         //水庫基本資料
        getReservoirRt: getReservoirRt,                                             //水庫即時資料

       /*水利署災情用*/
        getLandUseType: getLandUseType,                                             //土地利用種類
        loadWraEvents: loadWraEvents,                                               //取水利署事件清單

        loadFloodComputeForLightweightDatas: loadFloodComputeForLightweightDatas,   //取積淹水演算資料(不含DEM,Adress)
        loadDEMCalculateData: loadDEMCalculateData,                                 //取淹水網格DEM資料
        estimateFloodingComputeForLightweightDatas: estimateFloodingComputeForLightweightDatas, //災情預估

        //getFHYTown: getFHYTown,
        getTown: getTown,
        AllTW: '全臺',

        //台南 Gis
        getTnwrbarcgislegend: getTnwrbarcgislegend,
        tnwrbarcgisUrl: 'https://water.tainan.gov.tw/tnwrbarcgis104/rest/services/TNRW/IISI_rainwater/MapServer',

        getCadastreSecCode: getCadastreSecCode,                                     //取所有地段碼
        getCadastreSecAllNo: getCadastreSecAllNo,                                   //所以地段下所以地號資料
        villagekml: function () { return app.siteRoot +'Data/Tainan_village.kmz'}
    };

})(window);
