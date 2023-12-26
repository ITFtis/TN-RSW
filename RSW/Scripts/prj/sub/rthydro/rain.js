(function ($) {
    'use strict';
    var pluginName = 'rain';
    var pluginclass = function (element, e) {
        if (e) {
            e.stopPropagation();
            e.preventDefault();
        }
        this.$element = $(element);
        this.settings = { map: undefined, autoShow: true };
        this.$_pinctrl;
    };
    pluginclass.prototype = {
        constructor: pluginclass,
        init: function (options) {
            $.extend(this.settings, options);
            var that = this;
            if (this.settings.autoShow)
                that.initUi();
            else
                this.$element.on($.menuctrl.eventKeys.popu_init_before, function () {
                    that.initUi();
                });
        },
        initUi: function () {
            var that = this;

            var hasFirstRepaintPinCompleted = false;
            var $pinctrl = this.$_pinctrl = $('<div>').appendTo(this.$element).on($.BasePinCtrl.eventKeys.initUICompleted, function () {
                $pinctrl.find('.pinswitch').prop('checked', true).trigger('change');
                $pinctrl.find('.pinlist').prop('checked', true).trigger('change');
                $pinctrl.find('.ctrl').hide();
                $filterContainer.insertBefore(that.$element.find('.legend'));
                $('.search', that.$element).appendTo($('<div class="col-6">').appendTo($filterContainer));
            }).on($.BasePinCtrl.eventKeys.afterSetdata, function (e, ds, dds) {
                that.$element.trigger('get-data-complete', [ds]);
            });

            var $filterContainer = $('<div class="row filter-container">').appendTo(this.$element);

            var $districtSelect = $('<div class="col-6"><select class="form-control"></select></div>').appendTo($filterContainer).find('select');//
            $('<option value="">全區</option>').appendTo($districtSelect);
            datahelper.getTown(function (ts) {
                $.each(ts, function () {
                    $('<option value="' + this.DistrictID+'">' + this.DistrictName + '</option>').appendTo($districtSelect);//[0].coors = this.coors;
                });
                $districtSelect.on('change', function () {
                    setFilter();
                });
            })
            

            var filtercache = "-1_-1";
            var setFilter = function () {
                //helper.misc.showBusyIndicator(that.$element);
                setTimeout(function () {
                    var _district = $districtSelect.val();
                    $pinctrl.RainCtrl('setFilter', function (d) {
                        return (!_district ? true : d.DistrictID == _district);// && (!_district ? true : d._district == _district);
                    });
                }, 10);
            }

            //InitRainCtrl($pinctrl);//
            //InitRainCtrl($pinctrl, {
            //    //歷史資料區間查詢, appendQueryDurationPinInfoContent 在meterImpl.js
            //    pinInfoContent: function (data, infofields) { return appendQueryDurationPinInfoContent.call(this, data, infofields, $.RainCtrl.defaultSettings.pinInfoContent, $pinctrl, "StationID", "CName","雨量站" ) },
            //    displayChartDatas: function (ds) {
            //        var that = this;
            //        this.exportdatas = $.map(ds, function (d) { return { DATE: helper.format.JsonDateStr2Datetime(d.DATE).DateFormat('yyyy/MM/dd HH:mm:ss'), M10: d.M10 } });
            //        return ds; //如需日累計可從ds處裡
            //    }
            //});
            InitRainCtrl($pinctrl, {
                //歷史資料區間查詢, appendQueryDurationPinInfoContent 在meterImpl.js
                pinInfoContent: function (data, infofields) { return appendQueryDurationPinInfoContent.call(this, data, infofields, $.RainCtrl.defaultSettings.pinInfoContent, $pinctrl, "StationID", "CName", "雨量站") },
                displayChartDatas: function (ds) {
                    var that = this;
                    this.exportdatas = $.map(ds, function (d) { return { DATE: helper.format.JsonDateStr2Datetime(d.InfoTime).DateFormat('yyyy/MM/dd HH:mm:ss'), M10: d.Acc10MinuteRainQty } });
                    return ds; //如需日累計可從ds處裡
                }
            });
        },
        getPinCtrlInstance: function () {
            return this.$_pinctrl;
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

