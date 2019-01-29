(function ($) {

    /**
     * 默认参数
     */
    var defaultOpts = {
        item: '',
        html: '显示文本',
        color: '#FFFFFF',
        fontsize: "32px"

    };




    /**
     * 定义类
     */
    var VButton = function (options) {

        //var itemno = self.options.itemno;

        this.options = $.extend({}, defaultOpts, options);

        this.designMode = true;
        //记录item值;
        defaultOpts.item = this.options.item;

        div = undefined;
       
        this.init();
    }

    VButton.prototype = {      
        init: function () {
            var self = this;
            //console.log('test');
            var pdiv = this.options.item;
            var dataPanel2 = $('<div class="data-item-text" > <div class="databoard_btn"> <a id="" href = "" class= "button_a" style = "" onclick = "" > 超链接</a></div ></div>');
            var width = $(pdiv).width();
            var height = $(pdiv).height();

            self.options.viewoption.width = width;
            self.options.viewoption.height = height;
             self.options.viewoption["line-height"] = height+"px";
            self.appendHandler(dataPanel2, pdiv);
            //

            self.div = dataPanel2;



            self.div.css(self.options.viewoption);
            //指定button的text
           // self.div.html(this.options.viewoption.fonttext);
            //self.div.onclick = objclick();


            //处于运行模式时，有效 
            if (this.designMode) {

                $(dataPanel2).bind('click', function () {
                    //  alert(1);
                });

            }
      

            //记载数据
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
                dataPanel2.html(this.options.viewoption.html)
        }
        ,
        objclick: function () {
            alert(1234566);
            alert(this.value);
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
            $(pdiv).children('div.data-item-text').css({
                width: width,
                height: height  ,
                "line-height": height+"px"
            });

            //self.resizeDiv.css({
            //    display: 'none'
            //});

        },
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
            var dataview = $(pdiv).children('div.data-item-text');

            //更改文本内容
            dataview.html(this.options.fonttext);
            dataview.css(self.options.viewoption);
        }

        ,
        setData_jsondata: function () {
            var self = this;
            var viewdata = self.options.viewdata;
            self.onclick = viewdata.datajson.value;
        }
        ,
        bind: function (object, func) {
            return function () {
                return func.apply(object, arguments);
            }
        }
        ,
        setData_apidata: function () {
            var self = this;

            var viewdata = self.options.viewdata;
            var url = viewdata.dataapi.url;

            //从api获取数据
            $.ajax({
                url: url,
                type: "post",
                async: true,
                data: {
                },
                success: function (data) {
                    // self.onclick = data.value;
                    //自动请求状态; intervalloading 
                    //自动请求间隔秒数;  intervalsecond
                    if (viewdata.dataapi.intervalloading && viewdata.dataapi.intervalsecond != undefined) {

                        var times = viewdata.dataapi.intervalsecond * 1000;

                        setTimeout(self.bind(self, self.setData_apidata), times);
                    }
                },
                error: function (e) {
                    // self.onclick = data.value;
                    //自动请求状态; intervalloading 
                    //自动请求间隔秒数;  intervalsecond
                    if (viewdata.dataapi.intervalloading && viewdata.dataapi.intervalsecond != undefined) {

                        var times = viewdata.dataapi.intervalsecond * 1000;

                        setTimeout(self.bind(self, self.setData_apidata), times);
                    }
                }
            });
        }
    }
    window.VButton = VButton;

})(jQuery);