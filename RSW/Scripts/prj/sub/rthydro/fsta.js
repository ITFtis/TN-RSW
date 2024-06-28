﻿
(function ($) {
    'use strict';
    var pluginName = 'fsta';
    var pluginclass = function (element, e) {
        if (e) {
            e.stopPropagation();
            e.preventDefault();
        }
        this.$element = $(element);
        this.settings = { };
        this.currentevent = undefined;
        this.rainInfo = {
            title: '雨量站', total: '-',
            legends: [app.siteRoot + 'images/pin/雨量站_超大豪雨.png', app.siteRoot + 'images/pin/雨量站_大豪雨.png', app.siteRoot + 'images/pin/雨量站_豪雨.png', app.siteRoot + 'images/pin/雨量站_大雨.png'],
            snames: ['超大豪雨', '大豪雨', '豪雨', '大雨'],
            counts: ['0', '0', '0', '0']
        }
        this.waterInfo = {
            title: '水位站', total: '-',
            legends: [app.siteRoot + 'images/pin/水位站_一級.png', app.siteRoot + 'images/pin/水位站_二級.png', app.siteRoot + 'images/pin/水位站_三級.png'],
            snames: ['一級', '二級', '三級'],
            counts: ['0', '0', '0']
        }
        this.fsInfo = {
            title: '淹水感測器', total: '-',
            legends: [app.siteRoot + 'images/pin/fsensor_50.png', app.siteRoot + 'images/pin/fsensor_30.png', app.siteRoot + 'images/pin/fsensor_10.png'],
            snames: ['50公分↑', '30公分↑', '10公分↑'],
            counts: ['0', '0', '0']
        }
        this.sewerInfo = {
            title: '下水道監測站', total: '-',
            legends: [app.siteRoot + 'images/pin/下水道_一級.png', app.siteRoot + 'images/pin/下水道_二級.png', app.siteRoot + 'images/pin/下水道_三級.png'],
            snames: ['一級', '二級', '三級'],
            counts: ['0', '0', '0']
        }
    };
    pluginclass.prototype = {
        constructor: pluginclass,
        init: function (options) {

            $.extend(this.settings, options);
            var that = this;
            this.$_r_sta_c;
            this.$_w_sta_c;
            this.$_f_sta_c;
            this.$_s_sta_c;
            that.initUi();
        },
        initUi: function () {
            var that = this;
            /*$('<div class="sta-c-title"><label>目前事件:<label/><label class="event-name">--</label></div>').appendTo(this.$element).find('.event-name');*/
            this.$_r_sta_c = $('<div class="r-sta-c sta-c"></div>').appendTo(this.$element);//雨量
            this.$_w_sta_c = $('<div class="w-sta-c sta-c"></div>').appendTo(this.$element);//水位
            this.$_f_sta_c = $('<div class="f-sta-c sta-c"></div>').appendTo(this.$element);//淹水感測
            this.$_s_sta_c = $('<div class="s-sta-c sta-c"></div>').appendTo(this.$element);//通報災情

            this._setSatInfo(this.$_r_sta_c, this.rainInfo);
            this._setSatInfo(this.$_w_sta_c, this.waterInfo);
            this._setSatInfo(this.$_f_sta_c, this.fsInfo);
            this._setSatInfo(this.$_s_sta_c, this.sewerInfo);
            this.$element.find('.sta-c').on('click', function () {
                that.$element.trigger("change-tab-index", that.$element.find('.sta-c').index(this)+1);
            });
        },
        _setSatInfo: function ($_c, i) {
            $_c.empty();
            $('<div class="total"></div><div class="legend"></div><div  class="status-name"></div><div class="count"></div>').appendTo($_c);
            $('<div class="title">' + i.title + '</div>').appendTo($_c.find('.total'));
            $('<div class="t-count">' + i.total + '</div>').appendTo($_c.find('.total'));
            $.each(i.legends, function () {
                $('<img src="' + this + '">').appendTo($_c.find('.legend'));
            });
            $.each(i.snames, function () {
                $('<div>' + this + '</div>').appendTo($_c.find('.status-name'));
            });
            $.each(i.counts, function () {
                $('<div>' + this + '</div>').appendTo($_c.find('.count'));
            });
        },
        //setEvent: function (ev) {
        //    if (ev)
        //        this.$element.find('.event-name').text(ev.EventName);
        //},
        setRainData: function (ds) {
            var t = 0, heavy = 0, extremely = 0, torrential = 0, exttorrential=0;
            if (ds) {
                $.each(ds, function () {
                    //20240617, add by markhong 總整資訊的加總，原透過status改由讀取各時段雨量來判斷加總
                    if (this.R24H && this.R24H >= 500)
                        exttorrential++;
                    else if ((this.R24H && this.R24H >= 300) || (this.R3H && this.R3H >= 200))
                        torrential++;
                    else if ((this.R24H && this.R24H >= 200) || (this.R3H && this.R3H >= 100))
                        extremely++;
                    else if ((this.R24H && this.R24H >= 80) || (this.R1H && this.R1H >= 40))
                        heavy++;
                });
                t = heavy + extremely + torrential + exttorrential;
            }
            this._setSatInfo(this.$_r_sta_c, $.extend(this.rainInfo, { total: t, counts: [exttorrential, torrential, extremely, heavy] }));
        },
        setWaterData: function (ds) {
            var t = 0, c1 = 0, c2= 0, c3 = 0;
            if (ds) {
                $.each(ds, function () {
                    if (this.Status) {
                        if (this.Status == $.BasePinCtrl.pinIcons.water.warnLevel1.name)
                            c1++;
                        else if (this.Status == $.BasePinCtrl.pinIcons.water.warnLevel2.name)
                            c2++;
                        else if (this.Status == $.BasePinCtrl.pinIcons.water.warnLevel3.name)
                            c3++;
                    }
                });
                t = c1 + c2+c3;
            }
            this._setSatInfo(this.$_w_sta_c, $.extend(this.waterInfo, { total: t, counts: [c1, c2, c3] }));
        },
        setFloorData: function (ds) {
            var t = 0,c0_10=0, c10 = 0, c30 = 0, c50 = 0;
            if (ds) {
                $.each(ds, function () {
                    if (!this.InfoTime || this.WaterDepth == undefined)// || this.Depth < 10)
                        return;
                    if (this.WaterDepth >= 50)
                        c50++;
                    else if (this.WaterDepth >= 30)
                        c30++;
                    else if (this.WaterDepth >= 10)
                        c10++;
                    else
                        c0_10++;
                    t++;
                });
            }
            this._setSatInfo(this.$_f_sta_c, $.extend(this.fsInfo, { total: c10+c30+c50, counts: [c50, c30, c10] }));
        },
        setSewerData: function (ds) {
            var t = 0, c1 = 0, c2 = 0, c3 = 0;
            if (ds) {
                $.each(ds, function () {
                    if (this.Status) {
                        if (this.Status == $.BasePinCtrl.pinIcons.water.warnLevel1.name)
                            c1++;
                        else if (this.Status == $.BasePinCtrl.pinIcons.water.warnLevel2.name)
                            c2++;
                        else if (this.Status == $.BasePinCtrl.pinIcons.water.warnLevel3.name)
                            c3++;
                    }
                });
                t = c1 + c2+c3;
            }
            this._setSatInfo(this.$_s_sta_c, $.extend(this.sewerInfo, { total: t, counts: [c1, c2, c3] }));
        }
    }

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

})(jQuery);