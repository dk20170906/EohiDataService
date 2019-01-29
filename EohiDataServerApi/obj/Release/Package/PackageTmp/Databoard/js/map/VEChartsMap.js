(function ($) {
    /**
* 默认参数
*/
    var defaultOpts = {
        item: '',
 
    };

    var myChart = null;

    /**
 * 定义类
 */
    var VEChartsMap = function (options) {
        this.options = $.extend({}, defaultOpts, options);

        //记录item值;
        defaultOpts.item = this.options.item;
        this.init();
    }


    VEChartsMap.prototype = {
        init: function () {
            var self = this;
            var id = newGuid();
            //console.log('test');
            var pdiv = this.options.item;
            var dataPanel2 = $('<div id=' + id + ' class="data-item-mapfornum"></div>');
            var width = $(pdiv).width();
            var height = $(pdiv).height();
            dataPanel2.css({
                width: width,
                height: height,
                top: 0,
                left: 0,
                position: 'absolute',
                color: this.options.color,
                "font-size": this.options.fontsize,
            });
            self.appendHandler(dataPanel2, pdiv);
            self.initMap(id);
        },
        initMap: function (id) {
            var self = this;
            var dom = document.getElementById(id);
             myChart = echarts.init(dom);
            var viewoption = self.options.viewoption;
            var viewdata = self.options.viewdata;
            if (viewdata != undefined) {
                //设置数据;
                if (viewdata.datatype == "json") {
                    var geoCoordMap = viewdata.geoCoordMap;
                    var data = viewdata.data;
                    viewoption.series[0].data = self.convertData(geoCoordMap, data);
                    viewoption.series[1].data = viewdata.adData;
                    if (viewoption && typeof viewoption === "object") {
                        myChart.setOption(viewoption, true);
                    }
                }
                if (viewdata.datatype == "api") {

                    //检查定时器
                    self.setData_apidata();     
                }
            }       
        },
        convertData: function (geoCoordMap,data) {
            var res = [];      
            for (var i = 0; i < data.length; i++) {
                var geoCoord = geoCoordMap[data[i].name];
                if (geoCoord) {
                    res.push({
                        name: data[i].name,
                        value: geoCoord.concat(data[i].value)
                    });
                }
            }
            return res;
        },
              
        setData_apidata: function () {
            var self = this;
            var option = self.options.viewoption;
            var viewdata = self.options.viewdata;
            var url = self.options.viewdata.dataapi.url;
            //
            if (viewdata.datafor == "percentchinamapfornum") {

                //从api获取数据
                $.ajax({
                    url: url,
                    type: "post",
                    async: true,
                    data: {
                    },
                    success: function (data) {

                        self.dealData_Nomal(data);

                        //自动请求状态; intervalloading 
                        //自动请求间隔秒数;  intervalsecond
                        if (viewdata.dataapi.intervalloading && viewdata.dataapi.intervalsecond != undefined) {

                            var times = viewdata.dataapi.intervalsecond * 1000;

                            setTimeout(self.bind(self, self.setData_apidata), times);
                        }


                    },
                    error: function (e) {
                        layer.msg("错误！[" + e.status + "][" + e.statusText + "]", {
                            icon: 1,
                            time: 2000 //2秒关闭（如果不配置，默认是3秒）
                        }, function () {
                            //history.go(0);
                        });
                    }
                });

            }      
        }, 
        dealData_Nomal: function (data) {
            var self = this;
            var viewoption = self.options.viewoption;
            var viewdata = self.options.viewdata;
            var datamapping = viewdata.datamapping;
            var geoCoordMap = viewdata.geoCoordMap;
            viewoption.series[0].data = self.convertData(geoCoordMap, data);
            viewoption.series[1].data = viewdata.adData;
            if (viewoption && typeof viewoption === "object") {
                myChart.setOption(viewoption, true);
            }
        },
        /**
       * 插入容器
       */
       appendHandler: function (handlers, target) {
            for (var i = 0; i < handlers.length; i++) {
                el = handlers[i];
                $(target).append(el);
            }
        }
        ,
        bind: function (object, func) {
            return function () {
                return func.apply(object, arguments);
            }
        },
        /*
        * 更改大小
        */
        resize: function () {
            var self = this;
            //console.log('test');
            var pdiv = this.options.item;

            //self.resizeDiv = undefined;//拖拽面板
            //更改数据面板大小

            var width = $(pdiv).width();
            var height = $(pdiv).height();

            //更改div
            $(pdiv).children('div.data-item-mapfornum').css({
                width: width,
                height: height
            });
            //
            myChart.resize();

        },
        setViewoption: function (viewoption) {
            var self = this;
            self.options.viewoption = $.extend({}, self.options.viewoption, viewoption);


            var pdiv = this.options.item;
            //更改div
            var dataview = $(pdiv).children('div.data-item-mapfornum');

            
        }
    }
    window.VEChartsMap = VEChartsMap;
})(jQuery);