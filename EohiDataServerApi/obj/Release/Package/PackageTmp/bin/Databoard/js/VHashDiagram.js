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
    var VHashDiagram = function (options) {
        this.options = $.extend({}, defaultOpts, options);

        //记录item值;
        defaultOpts.item = this.options.item;

        this.chart = undefined;


        this.t_data = undefined;


        this.init();
    }

    VHashDiagram.prototype = {
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
            var option = self.options.viewoption;
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
            $(pdiv).children('div.data-item-chart').css({
                width: width,
                height: height
            });
            //
            self.chart.resize();

        },
        setViewoption: function (viewoption) {
            var self = this;
            self.options.viewoption = $.extend({}, self.options.viewoption, viewoption);

            var option = self.options.viewoption;
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
                myChart.setOption(option);



        }
        ,
        setData_jsondata: function () {
            var self = this;
            var option = self.options.viewoption;
            var viewdata = self.options.viewdata;
      
                var data = viewdata.datajson;
                self.dealData_Nomal(data);                   
          
        }
        ,
        setData_apidata: function () {
            var self = this;
            var option = self.options.viewoption;
            var viewdata = self.options.viewdata;
            var url = self.options.viewdata.dataapi.url;

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
                            //清理;
                            if (self.t_data != undefined) {
                                clearTimeout(self.t_data);
                            }
                            self.t_data = setTimeout(self.bind(self, self.setData_apidata), times);
                        }


                    },
                    error: function (e) {
                        //自动请求状态; intervalloading 
                        //自动请求间隔秒数;  intervalsecond
                        if (viewdata.dataapi.intervalloading && viewdata.dataapi.intervalsecond != undefined) {

                            var times = viewdata.dataapi.intervalsecond * 1000;
                            //清理;
                            if (self.t_data != undefined) {
                                clearTimeout(self.t_data);
                            }
                            self.t_data = setTimeout(self.bind(self, self.setData_apidata), times);
                        }
                    }
                });               
        }
        ,
        dealData_Nomal: function (data) {
            if (data.length>0) {
                var self = this;
                var option = self.options.viewoption;
                option.series = [];
                for (var i = 0; i < data.length; i++) {
                    debugger
  
                        var serise = data[i];
                        option.series.push(serise);
                 
                
                }
                
            }
            //var self = this;
            //var option = self.options.viewoption;
            //var viewdata = self.options.viewdata;
            //var datamapping = viewdata.datamapping;
            //if (viewdata.length > 0) {
            //    for (var i = 0; i < viewdata.length; i++) {
            //        debugger
            //        var serise = viewdata[i];
            //        option.series.add(serise);
            //    }
            //}



            //if (option.series.length > 0) {
            //    for (var i = 0; i < option.series.length; i++) {
            //        var series = option.series[i];
            //        series.data = viewdata.datajson[series.name].data;
            //    }
            //}
            self.chart.setOption(option);
        },

        
  

    }
    window.VHashDiagram = VHashDiagram;

})(jQuery);