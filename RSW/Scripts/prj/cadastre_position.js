(function ($) {
    'use strict';
    $.cadastre_position = {
        eventKeys: {
            initUICompleted: "cadastre_position_initUICompleted"
        }
    };
    var pluginName = 'cadastre_position';
    var pluginclass = function (element, e) {
        if (e) {
            e.stopPropagation();
            e.preventDefault();
        }
        this.$element = $(element);
        this.$containter = $('<div>').appendTo(this.$element);
        this.settings = undefined;
        this.allCadastreSecCode;
        this.secNocache = {};
        this.currentPolygon = null;
    };

    pluginclass.prototype = {
        constructor: pluginclass,
        init: function (options) {
            this.settings = $.extend(this.settings, options);
            var that = this;
            var _ctrl = { settings: $.extend({ map: app.map }, $.PolygonCtrl.defaultSettings) };
            datahelper.getCadastreSecCode(function (secs) {
                that.allCadastreSecCode = secs;
                that.initUI();
            })
        },
        initUI: function () {
            var that = this;
            var $c = $('<form class="form-inline cadastre-p d-flex">').appendTo(this.$containter);
            var $townSelect = $('<select class="form-control" style="width:80px;;display:inline;" >').appendTo($c);
            var $secSelect = $('<select class="form-control" style="width:calc( 100% - 246px );display:inline;margin-left:4px" >').appendTo($c);
            var $noInput = $('<input class="form-control" list="no-list" style="width:96px;;margin-left:4px" />').appendTo($c);
            var $nochoice = $('<datalist id="no-list">').appendTo($c);
            var $confirm = $('<div class="btn btn-success p " style=";margin-left:4px;">定位</div>').appendTo($c);
            var $cancel = $('<div class="btn btn-default clear" style="width:20%;display:none;" disabled="disabled">清除</div>').appendTo($c);


            $secSelect.on('change', function () {
                helper.misc.showBusyIndicator(that.$containter);
                datahelper.getCadastreSecAllNo($secSelect.val(), function (n) {
                    helper.misc.hideBusyIndicator(that.$containter);
                    $nochoice.empty();
                    $.each(n.Data, function () {
                        //$('<option value="' + this.地號全碼 + '">' + this.地號全碼 + '</option>').appendTo($nochoice)[0]._no = this;
                        $('<option value="' + this.地號 + '">' + this.地號 + '</option>').appendTo($nochoice)[0]._no = this;
                    });
                });
            });

            $townSelect.on('change', function () {
                $secSelect.empty();
                var secs = that.allCadastreSecCode[$townSelect.val()];//.find('> option[value="' + $townSelect.val() + '"]')[0].town;
                //that.currentTownAllVillage = secs.villages;
                $.each(secs, function () {
                    $('<option value="' + this.段代碼 + '">' + this.地段 + '</option>').appendTo($secSelect);//[0].village = this;
                });
                $secSelect.trigger('change');
            });
            for (var t in this.allCadastreSecCode) {
                $('<option value="' + t + '">' + t + '</option>').appendTo($townSelect);//[0].town = this;
            };
            $townSelect.trigger('change');

            $confirm.on('click', function () {
                removePolygon();
                //alert($noInput.val());
                var _no = $nochoice.find('[value="' + $noInput.val() + '"]')[0]._no;
                var latlngs = [];
                $.each(_no.PosList, function () {
                    latlngs.push([this.Y, this.X]);
                });
                that.currentPolygon = L.polygon(latlngs, { color: 'red' }).addTo(that.settings.map);
                that.settings.map.flyToBounds(that.currentPolygon.getBounds(), { duration: .5 });
                
            });
            var removePolygon = function () {
                if (that.currentPolygon)
                    that.currentPolygon.remove();
                $cancel.attr("disabled", '');
            }
            $cancel.on('click', function () {
                removePolygon();
            });

            this.$element.trigger($.cadastre_position.eventKeys.initUICompleted);
        },
        clear: function () {
            this.$containter.find('clear').trigger('click');
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