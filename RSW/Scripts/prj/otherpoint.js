//防汛點位
(function ($) {
    'use strict';
    var pluginName = 'otherpoint';

    var pluginclass = function (element, e) {
        if (e) {
            e.stopPropagation();
            e.preventDefault();
        }
        this.$element = $(element);
        this.settings = { map:undefined };


    };
    pluginclass.prototype = {
        constructor: pluginclass,
        init: function (options) {

            var current = this;

            $.extend(this.settings, options);
            current.initUi();

        },
        initUi: function () {
            var that = this;
            $('<div class="other-clear-all-layer glyphicon glyphicon-erase btn btn-sm btn-outline-info" style="position:fixed;right:3rem;padding:1px 3px;top:4px;">關閉圖層</div>')
                .appendTo(this.$element).on('click', function () {
                that.$element.find('.pinswitch:checked').prop("checked", false).trigger('change');
            });

            $('<div class="item-title">氣象圖資</div>').appendTo(this.$element);
            Init雷達迴波圖(this.$element).on($.BasePinCtrl.eventKeys.initUICompleted, function () {
                //$(this).find('.pinswitch').prop('checked', true).trigger('change');
            });
            Init累積雨量圖(this.$element).on($.BasePinCtrl.eventKeys.initUICompleted, function () {
                //$(this).find('.pinswitch').prop('checked', true).trigger('change');
            });

            InitQpf060minRt(this.$element);

            $('<div class="item-title">點位</div>').appendTo(this.$element);
            var $_fl =InitFLCtrl(this.$element,
                {
                    //歷史資料區間查詢, appendQueryDurationPinInfoContent 在meterImpl.js
                    pinInfoContent: function (data, infofields) { return appendQueryDurationPinInfoContent.call(this, data, infofields, $.WaterCtrl.defaultSettings.pinInfoContent, $_fl, "stt_no", "stt_name", "流速計") },
                    displayChartDatas: function (ds) {
                        var that = this;
                        this.exportdatas = $.map(ds, function (d) { return { DateTime: helper.format.JsonDateStr2Datetime(d.datatime).DateFormat('yyyy/MM/dd HH:mm:ss'), Level: d.level || '-' } });
                        return ds.filter(d => d.level != null); //如需日累計可從ds處裡
                    }
                }
            );
            InitMovingPumpCtrl(this.$element);
            InitReservoirCtrl(this.$element);
            InitTideCtrl(this.$element);

            $('<div class="item-title">CCTV</div>').appendTo(this.$element);
            Init水利局CCTVCtrl(this.$element);

            $('<div class="item-title">雨水下水道圖資</div>').appendTo(this.$element);
            datahelper.getTnwrbarcgislegend(undefined, function (ls) {
                $.each(ls, function () {
                    var l = this;
                    var legendIcons = $.map(this.legend, function (i) {
                        if (i.label == '<all other values>')
                            return;
                        return { 'name': i.label, 'url': 'data:image/png;base64,' + i.imageData, classes: '' }
                    });
                    var $_m = $('<div class="col-md-12" data-layer="' + l.layerId + '"></div>').appendTo(that.$element).PinCtrl({
                        map: app.map, name: this.layerName, useLabel: false, useList: false, minZoom: this.minScale==8000?18:0,
                        polyStyles: [{ name: '圖例', strokeColor: '#FF0000', strokeOpacity: 1, strokeWeight: 1, fillColor: '#FF0000', fillOpacity: .7, classes: 'water_normal' }],
                        legendIcons: legendIcons
                    }).on($.BasePinCtrl.eventKeys.initUICompleted, function () {
                        var $_this = $(this);
                        $_this.find('.pinswitch').off('change').on('change', function () {
                            $_this.toggleClass('show');
                            changeLayer();
                        });
                       
                    });
                    var dynamicMapLayer = L.dou.popup.esri.dynamicMapLayerAutoPopup({ map: app.map, popup: undefined, layerOptions: { url: datahelper.tnwrbarcgisUrl, layers: [l.layerId], proxy: undefined } })
                        .dynamicMapLayer;
                    dynamicMapLayer.setOpacity(0.9);
                    var changeLayer = function () {
                        //var asd = dynamicMapLayer.getLayers();
                        //var _layers = $.map($_c.find('.pinctrl.show'), function (el) {
                        //    return parseInt($(el).attr('data-layer'));
                        //});
                        //dynamicMapLayer.setLayers(_layers);
                        //dynamicMapLayer.redraw();

                        if ($_m.hasClass('show')) {
                            dynamicMapLayer.addTo(app.map);
                            dynamicMapLayer.bringToFront();
                        } else dynamicMapLayer.remove();
                    }
                });
            });
                    
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