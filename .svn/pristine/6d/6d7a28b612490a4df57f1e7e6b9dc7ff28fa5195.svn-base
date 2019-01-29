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
    var VIndexdiagram = function (options) {
        this.options = $.extend({}, defaultOpts, options);

        //记录item值;
        defaultOpts.item = this.options.item;

        this.chart = undefined;

        this.dataIndex = 0;
        this.dataCount = 0;


        this.init();
    }

    VIndexdiagram.prototype = {
        init: function () {
            var self = this;
            //console.log('test');
            var pdiv = this.options.item;
            var dataPanel2 = $('<div class="data-item-VIndexdiagram"></div>');
            var width = $(pdiv).width();
            var height = $(pdiv).height();
            dataPanel2.css({
                width: width,
                height: height,
                top: 0,
                left: 0,
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
            }
            if (viewdata.datatype == "json") {
                self.setData_jsondata();
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
            $(pdiv).children('div.data-item-VIndexdiagram').css({
                width: width,
                height: height
            });
            //
            self.chart.resize();

        }
        ,



       
        /*
       * 设置数据选项
       */
        setViewdata: function (viewdata) {
            var self = this;
            self.options.viewdata = $.extend({}, self.options.viewdata, viewdata);
            //加载数据
            var viewdata = self.options.viewdata;
            if (viewdata != undefined) {

                //设置数据;
                if (viewdata.datatype == "json") {
                    self.setData_jsondata();
                }
                if (viewdata.datatype == "api") {
                    //检查定时器
                    self.setData_apidata();
                }
            }
            else
                self.div.html(this.options.viewoption.html)
        }
        ,
        /*
        * 设置显示选项;
        */
        setViewoption: function (viewoption) {
            //this.options = $.extend({}, defaultOpts, option);
            var self = this;
            self.options.viewoption = $.extend({}, self.options.viewoption, viewoption);

            var self = this;
            //console.log('test');
            var pdiv = this.options.item;
            //更改div
            var dataview = $(pdiv).children('div.data-item-VIndexdiagram');

            var width = $(pdiv).width();
            var height = $(pdiv).height();

            //更改div
            dataview.css({
                width: width,
                height: height
            });
            var viewdata = self.options.viewdata;
            if (viewdata != undefined) {

                //设置数据;
                if (viewdata.datatype == "json") {
                    self.setData_jsondata();
                }
                if (viewdata.datatype == "api") {
                    //检查定时器
                    self.setData_apidata();
                }
            }
        },

        setData_jsondata: function () {
            debugger;
            var self = this;
            var option = self.options.viewoption;
            var viewdata = self.options.viewdata;
            if (viewdata.datafor == "VIndexdiagram") {
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
            if (viewdata.datafor == "VIndexdiagram") {

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
            var mdata = [];
            option.series[0].data = null;
            //以dx的值为标准顺序获取ds数组，
            for (var m = 0; m < data.length; m++) {
                var mdd = data[m];
                if (ds.indexOf(mdd["s"]) < 0) {
                    ds.push(mdd["s"]);

                }
                if (dx.indexOf(mdd["x"]) < 0) {
                    dx.push(mdd["x"]);
                }
            }
            //得到y轴数据集合
            if (ds.length > 0) {
                for (var m = 0; m < ds.length; m++) {
                    var mserise = [];
                    var msertype = "line";
                    for (var n = 0; n < data.length; n++) {
                        var mdd = data[n];
                        if (mdd["s"] == ds[m]) {
                            mserise.push(mdd["y"]);
                        }
                    }
                }
            }
      //为系列赋y值 
            option.series[0].data = mserise;
            //平均值，最大值，最小值 ，分段
            if (option.series[0].markLine.data != undefined) {
                mdata = option.series[0].markLine.data;
            }
            if (data[0].max != undefined) {
                var yaxis = { yAxis: data[0].max}
                mdata.push(yaxis);
            }
            if (data[0].min != undefined) {
                mdata.push({ yAxis: data[0].min });
            }
            if (data[0].ave != undefined) {
                mdata.push({ yAxis: data[0].ave });
            }
            option.xAxis.data = dx;
            option.series[0].markLine.data = mdata;
            self.chart.setOption(option);
        }
        ,


    }
    window.VIndexdiagram = VIndexdiagram;

})(jQuery);