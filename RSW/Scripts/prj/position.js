(function ($) {
    'use strict';

    var pluginName = 'position';
    var pluginclass = function (element, e) {
        if (e) {
            e.stopPropagation();
            e.preventDefault();
        }
        this.$element = $(element);
        this.settings = undefined;
    };

    pluginclass.prototype = {
        constructor: pluginclass,
        init: function (options) {
            this.settings = $.extend(this.settings, options);
            var that = this;
            this.$element.on($.menuctrl.eventKeys.popu_init_before, function () {
                //定位
                var $positionDiv = that.$element;
                $('<div class="position-title">座標定位</div>').appendTo($positionDiv);
                $positionDiv.BasePosition({ map: app.map }); //座標定位
               
                $('<div class="position-title">行政區、村里定位</div>').appendTo($positionDiv);
                $positionDiv.village_position({ map: app.map }).on($.village_position.eventKeys.initUICompleted, function () { //村里定位
                    $('<div class="btn btn-default clear-positon  kk">清除定位資料<div>').appendTo($positionDiv).on('click', function () {
                        $positionDiv.find('.btn-default:not(.clear-positon)').trigger('click');
                        $positionDiv.addressGeocode('clear');
                        $positionDiv.addressGeocode('_showAddressSelect', false);
                        $positionDiv.cadastre_position('clear');
                    });
                });

                $('<div class="position-title">地籍定位</div>').appendTo($positionDiv);
                $positionDiv.cadastre_position({ map: app.map });//地籍地位

                $('<div class="position-title">地址定位</div>').appendTo($positionDiv);
                $positionDiv.addressGeocode({ map: app.map });
                
            });
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