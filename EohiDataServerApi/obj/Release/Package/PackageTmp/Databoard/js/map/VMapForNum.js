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
    var VMapForNum = function (options) {
        this.options = $.extend({}, defaultOpts, options);

        //记录item值;
        defaultOpts.item = this.options.item;

        this.chart = undefined;

        this.dataIndex = 0;
        this.dataCount = 0;


        this.init();
    }

    VMapForNum.prototype = {
        init: function () {
            var self = this;
            //console.log('test');
            var pdiv = this.options.item;
            var dataPanel2 = $('<div class="data-item-mapfornum"></div>');
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
            var myChart = echarts.init(dataPanel2[0]);
            // var myChart = echarts.init(dataPanel[0]);
            self.chart = myChart;
            // 指定图表的配置项和数据
            var option = self.options.viewoption;
            var viewdata = self.options.viewdata;
            if (viewdata != undefined) {
                //设置数据;
                if (viewdata.datatype == "json") {
                    var datajson = self.options.viewdata.datajson;
                    option.series.data = datajson;
                    myChart.setOption(option);
                    return;
                }
                if (viewdata.datatype == "api") {

                    //检查定时器
                    self.setData_apidata();
                }
            }
            else
                myChart.setOption(option);

   
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
            $(pdiv).children('div.data-item-mapfornum').css({
                width: width,
                height: height
            });
            //
            self.chart.resize();

        },
        setViewoption: function (viewoption) {
            var self = this;
            self.options.viewoption = $.extend({}, self.options.viewoption, viewoption);


            var pdiv = this.options.item;
            //更改div
            var dataview = $(pdiv).children('div.data-item-text');

            //更改文本内容
            //dataview.html(this.options.html);
            //dataview.css({
            //    color: this.options.color,
            //    "font-size": this.options.fontsize,
            //});

        }
        ,

        setData_apidata: function () {
            var self = this;
            var option = self.options.viewoption;
            var viewdata = self.options.viewdata;
            var url = self.options.viewdata.dataapi.url;
            //

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
                    layer.msg("错误！[" + e.status + "][" + e.statusText + "]", {
                        icon: 1,
                        time: 2000 //2秒关闭（如果不配置，默认是3秒）
                    }, function () {
                        //history.go(0);
                    });
                }
            });

        }
        ,
        dealData_Nomal: function (data) {
            var self = this;
            var option = self.options.viewoption;
            option.series.data = data;
            self.chart.setOption(option);
        }
    }
    window.VMapForNum = VMapForNum;

})(jQuery);