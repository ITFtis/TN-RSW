(function ($) {
    'use strict';
    $.village_position = {
        eventKeys: {
            initUICompleted: "village_position-initUICompleted"
        }
    };
    var pluginName = 'village_position';
    var pluginclass = function (element, e) {
        if (e) {
            e.stopPropagation();
            e.preventDefault();
        }
        this.$element = $(element);
        this.$containter = $('<div>').appendTo(this.$element);;
        this.settings = undefined;
        this.vdatas = [];
        this.currentVillage = undefined;
        this.currentTownAllVillage = [];
        this.currentVillagePolyline = undefined;
        this.currentTownAllVillagePolyline = undefined;
    };

    pluginclass.prototype = {
        constructor: pluginclass,
        init: function (options) {
            this.settings = $.extend(this.settings, options);
            var that = this;
            //this.$element.on($.menuctrl.eventKeys.popu_init_after, function () {
            helper.misc.showBusyIndicator(that.$containter);
                var _ctrl = { settings: $.extend({ map: app.map }, $.KmlCtrl.defaultSettings) };
                _ctrl.settings.descriptionParser = app.villageDescriptionParser;
                setTimeout(function () {
                    app.getVillageGeojsonData.call(_ctrl, function (ds) {
                        helper.misc.hideBusyIndicator(that.$containter);
                        var _temp = {};
                        $.each(ds, function () {
                            //this.geometry.type = "Polyline"; //原Polygon改Polyline
                            //var _d = $.parserStringToObject(this.geojson.properties.description, "<BR>", "：");
                            //$.extend(this, _d);
                            if (!_temp.hasOwnProperty(this.TOWNNAME))
                                _temp[this.TOWNNAME] = [];
                            _temp[this.TOWNNAME].push(this);
                        });
                        //排序
                        //var _temp = [];
                        for (var t in _temp) {

                            _temp[t].sort(function (a, b) {
                                return a.VILLNAME.localeCompare(b.VILLNAME, "zh-Hant"); //chrome有問題
                            });
                            that.vdatas.push({ town: t, villages: _temp[t] });

                        }
                        that.vdatas.sort(function (a, b) {
                            return a.town.localeCompare(b.town, "zh-Hant"); //chrome有問題
                        });


                        that.initUI();
                        helper.misc.hideBusyIndicator(that.$element);
                    });
                }, 400);
            //});
        },
        initUI: function () {
            var that = this;
            var $c = $('<form class="form-inline village-p">').appendTo(this.$containter);
            var $townSelect = $('<select class="form-control" >').appendTo($c);
            var $villageSelect = $('<select class="form-control v">').appendTo($c);
            var $confirm = $('<div class="btn btn-success p">定位</div>').appendTo($c);
            var $cancel = $('<div class="btn btn-default " style="width:20%;display:none;" disabled="disabled">清除</div>').appendTo($c);

            $villageSelect.on('change', function () {
                that.currentVillage = $villageSelect.find('> option[value="' + $villageSelect.val() + '"]')[0].village;
            });

            $townSelect.on('change', function () {
                $villageSelect.empty();
                var t = $townSelect.find('> option[value="' + $townSelect.val() + '"]')[0].town;
                that.currentTownAllVillage = t.villages;
                $('<option value="all">全區</option>').appendTo($villageSelect)[0].village = undefined;
                $.each(t.villages, function () {
                    $('<option value="' + this.VILLNAME + '">' + this.VILLNAME + '</option>').appendTo($villageSelect)[0].village = this;
                });
                $villageSelect.trigger('change');
            });
            $.each(this.vdatas, function () {
                $('<option value="' + this.town + '">' + this.town + '</option>').appendTo($townSelect)[0].town = this;
            });
            $townSelect.trigger('change');

            $confirm.on('click', function () {
                removePolyline();
                that.currentVillagePolyline = undefined;
                that.currentTownAllVillagePolyline = L.featureGroup();
                $.each(that.currentTownAllVillage, function () {

                    var _lopts = { dashArray: '10, 4, 1, 4, 1, 4', color: '#333', weight: 2, opacity: .8, fillColor: '#ff0000', fillOpacity: .08, bubblingMouseEvents: false };
                    if (that.currentVillage != undefined) {
                        if (this == that.currentVillage)
                            that.currentVillagePolyline = L.geoJSON(this.geojson, _lopts);
                    }
                    else {  //呈現行政區全區村里
                        //_lopts = $.extend(_lopts, { weight: 1, opacity: .4 });
                        _lopts = $.extend(_lopts, { weight: 2, opacity: .4 });
                        that.currentTownAllVillagePolyline.addLayer(L.geoJSON(this.geojson, _lopts));
                    }
                });
                that.currentTownAllVillagePolyline.addTo(that.settings.map);
                if (that.currentVillagePolyline)
                    that.currentVillagePolyline.addTo(that.settings.map).bringToFront();

                $cancel.removeAttr("disabled");
                if (that.currentVillagePolyline)
                    that.settings.map.flyToBounds(that.currentVillagePolyline.getBounds(), { duration: .5 });
                else
                    that.settings.map.flyToBounds(that.currentTownAllVillagePolyline.getBounds(), { duration: .5 });
            });
            var removePolyline = function () {
                $cancel.attr("disabled", '');
                if (that.currentVillagePolyline)
                    that.currentVillagePolyline.remove();
                if (that.currentTownAllVillagePolyline)
                    that.currentTownAllVillagePolyline.remove();
            }
            $cancel.on('click', function () {
                removePolyline();
            });

            this.$element.trigger($.village_position.eventKeys.initUICompleted);
        }

    };


    $.fn[pluginName] = function (arg) {

        var args, instance;

        if (!(this.data(pluginName) instanceof pluginclass)) {

            this.data(pluginName, new pluginclass(this[0]));
        }

        instance = this.data(pluginName);


        if (typeof arg === 'undefined' || typeof arg === 'object') {

            if (typeof instance.init === 'function') {
                instance.init(arg);
            }
            this.instance = instance;
            return this;

        } else if (typeof arg === 'string' && typeof instance[arg] === 'function') {

            args = Array.prototype.slice.call(arguments, 1);

            return instance[arg].apply(instance, args);

        } else {

            $.error('Method ' + arg + ' does not exist on jQuery.' + pluginName);

        }
    };
}(jQuery));