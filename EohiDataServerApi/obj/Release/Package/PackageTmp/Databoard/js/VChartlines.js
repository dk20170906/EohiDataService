/**
 * Created by 郭广平 2018.05.16
 */

(function ($) {

    /**
     * 默认参数
     */
    var defaultOpts = {
        item: '',
    };

    /**
     * 定义类
     */
    var VChartlines = function (options) {
        this.options = $.extend({}, defaultOpts, options);

        //记录item值;
        defaultOpts.item = this.options.item;

        this.chart = undefined;

        this.dataIndex = 0;
        this.dataCount = 0;


        this.init();
    }

    VChartlines.prototype = {
        init: function () {
            var self = this;
            //console.log('test');
            var pdiv = this.options.item;
            var dataPanel2 = $('<div class="data-item-chart"></div>');
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
            //
            //dataPanel2.html(this.options.html)

            // 基于准备好的dom，初始化echarts实例
            var myChart = echarts.init(dataPanel2[0], "chalk");
            //var myChart = echarts.init(dataPanel2[0]);
            // var myChart = echarts.init(dataPanel[0]);
            self.chart = myChart;
            // 指定图表的配置项和数据
            debugger;
            var option = self.options.viewoption;
            var viewdata = self.options.viewdata;
            if (viewdata.datatype == "api") {
                //检查定时器
                self.setData_apidata();
            } else {
                if (option.series.length > 0 && option.series[0].data != null && option.series[0].data.length > 0) {
                    myChart.setOption(option);
                } else {
                    if (viewdata != undefined) {
                        if (viewdata.datatype == "json") {
                            self.setData_jsondata();
                        }
                    }
                }
            }
        }
        ,
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
            $(pdiv).children('div.data-item-chart').css({
                width: width,
                height: height
            });
            //
            self.chart.resize();

        }
        ,
        setData_jsondata: function () {
            debugger;
            var self = this;
            var option = self.options.viewoption;
            var viewdata = self.options.viewdata;
            if (viewdata.datafor == "chartlines") {
                //从
                var data = viewdata.datajson;
                self.dealData_Nomal(data);
            }

        }
        ,
        setData_apidata: function () {
            var self = this;
            var option = self.options.viewoption;
            var viewdata = self.options.viewdata;
            var url = self.options.viewdata.dataapi.url;
            //
            if (viewdata.datafor == "chartlines") {

                //从api获取数据
                $.ajax({
                    url: url,
                    type: "post",
                    async: true,
                    data: {
                    },
                    success: function (data) {

                        self.dealData_Nomal(data);

                        if (viewdata.dataapi.intervalloading && viewdata.dataapi.intervalsecond != undefined) {

                            var times = viewdata.dataapi.intervalsecond * 1000;

                            setTimeout(self.bind(self, self.setData_apidata), times);
                        }


                    },
                    error: function (e) {
                        if (viewdata.dataapi.intervalloading && viewdata.dataapi.intervalsecond != undefined) {

                            var times = viewdata.dataapi.intervalsecond * 1000;

                            setTimeout(self.bind(self, self.setData_apidata), times);
                        }

                    }
                });

            }

        }
        ,
        dealData_Nomal: function (data) {
            var self = this;
            var option = self.options.viewoption;
            var viewdata = self.options.viewdata;
            var datamapping = viewdata.datamapping;
            //var datamapping = viewdata.datamapping;
            //data=d;
            var dx = [];
            var dy = [];
            var ds = [];

            //以dx的值为标准顺序获取ds数组，
            for (var m = 0; m < viewdata.datajson.length; m++) {
                var mdd = viewdata.datajson[m];
                if (ds.indexOf(mdd["s"]) < 0) {
                    ds.push(mdd["s"]);

                }
                if (dx.indexOf(mdd["x"]) < 0) {
                    dx.push(mdd["x"]);
                }
            }
            //得到y轴数据集合 （令多条）
            if (ds.length > 0) {
                for (var m = 0; m < ds.length; m++) {
                    var mserise = [];
                    var msertype = "";
                    for (var n = 0; n < viewdata.datajson.length; n++) {
                        var mdd = viewdata.datajson[n];
                        if (mdd["s"] == ds[m]) {
                            mserise.push(mdd["y"]);
                            msertype = mdd["t"];
                        }
                    }
                    dy.push({ name: ds[m], type: msertype, data: mserise });
                }
            }
            //得到json
            var jsondata = JSON.stringify(dy);
            option.legend.data = ds;
            //将数据绑定到系列中
            for (var k = 0; k < dy.length; k++) {
                option.series[k] = {
                    name: dy[k].name,
                    type: dy[k].type,
                    itemStyle: {
                        normal: {
                            //好，这里就是重头戏了，定义一个list，然后根据所以取得不同的值，这样就实现了，
                            color: function (params) {
                                // build a color map as your need.
                                var colorList = [
                                    '#C1232B', '#B5C334', '#FCCE10', '#E87C25', '#27727B',
                                    '#FE8463', '#9BCA63', '#FAD860', '#F3A43B', '#60C0DD',
                                    '#D7504B', '#C6E579', '#F4E001', '#F0805A', '#26C0C0'
                                ];
                                return colorList[params.dataIndex]
                            },
                            //以下为是否显示，显示位置和显示格式的设置了
                            label: {
                                show: true,
                                position: 'top',
                                //                             formatter: '{c}'
                                formatter: '{b}\n{c}'
                            }
                        }
                    },
                    //设置柱的宽度，要是数据太少，柱子太宽不美观~
                    barWidth: 70,
                    data: dy[k].data,
                    markPoint: {},
                    //值范围最大值
                    //markPoint: {
                    //    data: [
                    //        { type: 'max', name: '最大值' },
                    //        { type: 'min', name: '最小值' }
                    //    ]
                    //},
                    markLine: {},          //平均
                    //markLine: {
                    //    data: [
                    //        { type: 'average', name: '平均值' }
                    //    ]
                    //}


                }
            }
            option.xAxis.data = dx;
            self.chart.setOption(option);
        }
        ,


    }
    window.VChartlines = VChartlines;

})(jQuery);