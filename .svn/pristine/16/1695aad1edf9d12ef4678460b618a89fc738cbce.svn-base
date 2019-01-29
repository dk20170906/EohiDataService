/*
 * VRowScrollText - 多行文本滚动组件
 *
 * 作者:
 * 版本:
 * 创建日期:
 * 
 * 使用说明：
 * { 使用前请引入jQuery, 文本长度大于父级元素才可激活滚动，否则无滚动效果 }
 */

; (function ($) {
    "use strict";
    var defaultOpts = {
        item: '',
        viewoption: {
            rollingspeed: 30, //横向滚动速度
            rotesecond: 5, //每行翻转停留时间
            pagerowcount: -1, //每页条数，-1表示不分页
            sleepspace: 2,//停止间隔时间2s
            pagesecond: 20, //每页显示停留时间
        }
    };
    var VRowScrollText = function (options) {
        //this.options = $.extend({}, defaultOpts, options);
        this.options = $.extend({}, defaultOpts, options);
        //记录item值;
        defaultOpts.item = this.options.item;

        this.div = undefined;
        this.pagingperpageinterval = undefined;//控件分页定时器
        this.pageindex = 0;//第几页
        this.pagecount = 1;//
        this.pagedata = [];
        this.init();

    }
    //多行滚动文本
    VRowScrollText.prototype = {
        init: function () {
            var self = this;
            //
            var pdiv = this.options.item;
            var dataPanel2 = $('<div class="table-scoll"></div>');
            var width = $(pdiv).width();
            var height = $(pdiv).height();
            dataPanel2.css({
                width: width,
                height: height,
                top: 0,
                left: 0
            });
            self.appendHandler(dataPanel2, pdiv);

            self.div = dataPanel2;

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


            // self.item_init();

            //设置重新翻转
            setTimeout(self.bind(self, self.ReInneranimation), 1000 * self.options.viewoption.rotesecond);
        },
        /**
         * 插入容器
         */
        appendHandler: function (handlers, target) {
            for (var i = 0; i < handlers.length; i++) {
                el = handlers[i];
                $(target).append(el);
            }
        },
        /*
         * 更改大小
         */
        resize: function () {

            var self = this;
            var pdiv = this.options.item;
            var width = $(pdiv).width();
            var height = $(pdiv).height();
            //更改div
            $(pdiv).children('div.table-scoll').css({
                width: width,
                height: height
            });
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
        },
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
            //$(pdiv).children('div.table-scoll').css(self.options.viewoption.alldivcss);
            $('.table-scoll', self).css(self.options.viewoption.alldivcss);
            $('.table-index', self).css(self.options.viewoption.keycss);
            $('.scrollbox', self).css(self.options.viewoption.valuecss);
            $('.txt', self).css(self.options.viewoption.valuecss);
            $('txt-clone', self).css(self.options.viewoption.valuecss);




            ////更改文本内容
            //dataview.css({
            //    color: this.options.viewoption.color,
            //    "font-size": this.options.viewoption.fontsize
            //});
        }
        ,
        setData_jsondata: function () {
            var self = this;
            var viewdata = self.options.viewdata;
            //x y s
            var datamapping = viewdata.datamapping;
            //从
            var data = viewdata.datajson;
            var dx = [];
            if (data.length > 0) {
                for (var b = 0; b < data.length; b++) {
                    var di = data[b];
                    dx.push(di[datamapping.y]);
                }
                self.pagedata = dx;
                //当每页显示的个数少于总条数  >0
                if (self.options.viewoption.pagerowcount > 0 && self.options.viewoption.pagerowcount < dx.length) {
                    //var indexallcount = Math.ceil(dx.length / self.options.viewoption.pagerowcount);
                    self.pagecount = Math.ceil(dx.length / self.options.viewoption.pagerowcount);
                    //
                    self.pageindex = 0;
                    //
                    self.Piecewise_item_init();
                } else {
                    self.item_init(dx);
                }
            }
        },
        //allpage共多少页面
        Piecewise_item_init: function () {
            var self = this;
            var timespan = 1000 * self.options.viewoption.pagesecond;
            if (self.pageindex < self.pagecount) {
                // debugger;
                //console.log(123);
                self.item_init(self.getPiecewiseValue());
                self.pageindex++;
                setTimeout(self.bind(self, self.Piecewise_item_init), timespan);
                //setTimeout(self.Piecewise_item_init(allpage, obj), timespan);
            } else {
                self.pageindex = 0;
                // setTimeout(self.Piecewise_item_init(allpage, obj), timespan);
                setTimeout(self.bind(self, self.Piecewise_item_init), timespan);
            }
        },

        //分段取值index 第几段，obj 总集合
        getPiecewiseValue: function () {
            var self = this;
            return self.pagedata.slice(self.pageindex * self.options.viewoption.pagerowcount, (self.pageindex + 1) * self.options.viewoption.pagerowcount);
        },


        bind: function (object, func) {
            return function () {
                return func.apply(object, arguments);
            }
        },
        setData_apidata: function () {
            var self = this;

            var viewdata = self.options.viewdata;
            var url = viewdata.dataapi.url;

            //从api获取数据
            $.ajax({
                url: url,
                type: "post",
                async: true,
                data: {},
                success: function (data) {
                    //x y s
                    var datamapping = viewdata.datamapping;
                    //data=d;
                    var dx = [];
                    for (var b = 0; b < data.length; b++) {
                        var di = data[b];
                        dx.push(di[datamapping.y]);
                    }
                    self.pagedata = dx;

                    //当每页显示的个数少于总条数  >0
                    if (self.options.viewoption.pagerowcount > 0 && self.options.viewoption.pagerowcount < dx.length) {
                        //var indexallcount = Math.ceil(dx.length / self.options.viewoption.pagerowcount);
                        self.pagecount = Math.ceil(dx.length / self.options.viewoption.pagerowcount);
                        //
                        self.pageindex = 0;
                        //
                        self.Piecewise_item_init();
                    } else {
                        self.item_init(self.pagedata);
                    }
                    //  self.div.html(dx[0]);

                    //自动请求状态; intervalloading
                    //自动请求间隔秒数;  intervalsecond
                    if (viewdata.dataapi.intervalloading && viewdata.dataapi.intervalsecond != undefined) {

                        var times = viewdata.dataapi.intervalsecond * 1000;

                        setTimeout(self.bind(self, self.setData_apidata), times);
                    }
                },
                error: function (e) {
                    //自动请求状态; intervalloading
                    //自动请求间隔秒数;  intervalsecond
                    if (viewdata.dataapi.intervalloading && viewdata.dataapi.intervalsecond != undefined) {

                        var times = viewdata.dataapi.intervalsecond * 1000;

                        setTimeout(self.bind(self, self.setData_apidata), times);
                    }
                }
            });
        },
        //重新翻转方法
        ReInneranimation: function () {
            var self = this;
            //移除翻转样式
            $('.table-line', self).each(function (index) {
                $(this).removeClass("inneranimation");
            });

            //开始翻转处理
            setTimeout(self.bind(self, self.AddInneranimation), self.options.viewoption.rotesecond);

        },
        AddInneranimation: function () {
            var self = this;
            //console.log('reInneranimation');
            $('.table-line', self).each(function (index) {
                var $this = $(this);
                setTimeout(function () {
                    //debugger;
                   // console.log(index);
                    var tbline = $this;
                    //tbline.removeClass("inneranimation");
                    tbline.addClass("inneranimation");
                }, 50 * index);
            });
            //加载重新翻转方法
            setTimeout(self.bind(self, self.ReInneranimation), 1000 * self.options.viewoption.rotesecond);
        },
        //创建单个行dom树,
        init_dom_tree: function (obj) {
            var self = this;
            if (self.options.pagerowcount < 0) {
                self.options.pagerowcount = 0;
            }
            var htmlStr = '';
            if (obj.length > 0) {
                $.each(obj, function (i, item) {
                    var iindex = parseInt(self.options.viewoption.pagerowcount * self.pageindex + i) + 1;
                    htmlStr += '<div class="table-line">';
                    htmlStr += '<div class="demo-cont">';
                    htmlStr += '<div class="table-index" style="">No.' + iindex + '</div>';
                    htmlStr += '<div class="table-content txt-scroll-default">';
                    htmlStr += '<div class="scrollbox">';
                    htmlStr += '<div class="txt" style="">';
                    htmlStr += '' + item +'&nbsp;&nbsp;&nbsp;&nbsp;';
                    htmlStr += '</div>';
                    htmlStr += ' </div>';
                    htmlStr += '</div>';
                    htmlStr += '</div>';
                    htmlStr += '</div>';
                });
            }
            return htmlStr;
        }
        ,
        //创建多列滚动表格
        item_init: function (obj) {
            var self = this;
            self.div.empty();
            //创建 html dom对像
            var htmlStrDom = $(self.init_dom_tree(obj)).css(self.options.viewoption.alldivcss);
            self.div.append(htmlStrDom);
            var tablelines = htmlStrDom.get();
            $.each(tablelines, function (i, item) {
                // alert(index);
                //debugger;
                $('.table-index', item).css(self.options.viewoption.keycss);
                var scrollbox = $('.scrollbox', item).css(self.options.viewoption.valuecss);
                var txt_begin = $('.txt', item).css(self.options.viewoption.valuecss);
                var txt_end = $('<div class="txt-clone"></div>').css(self.options.viewoption.valuecss);


                var scrollVaue = 0;
                //翻转 逐行翻转 间隔300毫秒
                setTimeout(function () {
                    var tbline = item;
                    tbline.addClass("inneranimation");
                }, 50 * i);

                //如果 文字div 的宽度 大于容器div的宽度，需要滚动处理
                if (txt_begin.width() > scrollbox.width()) {
                    txt_end.html(txt_begin.html());
                    scrollbox.append(txt_end);
                    setTimeout(marquee, self.options.viewoption.sleepspace * 1000);
                }
                function marquee() {
                    //scrollLeft() 方法返回或设置匹配元素的滚动条的水平位置。
                    //滚动条的水平位置指的是从其左侧滚动过的像素数。当滚动条位于最左侧时，位置是 0。
                    //文字宽度 < 滚动
                    if (txt_end.width() - scrollbox.scrollLeft() <= 0) {

                        //重置位置
                        //停留2秒
                        setTimeout(marquee, self.options.viewoption.sleepspace * 1000);

                        scrollVaue = scrollbox.scrollLeft() - txt_begin.width();
                        scrollbox.scrollLeft(scrollVaue);

                    } else {
                        //移动
                        scrollVaue = scrollVaue + 1;
                        scrollbox.scrollLeft(scrollVaue);

                        setTimeout(marquee, self.options.viewoption.rollingspeed);
                    }
                }
            });



            //tablelines.each(function (index) {
            //    // alert(index);
            //    //debugger;
            //    var $this = $(this);
            //    $('.table-index', $this).css(self.options.viewoption.keycss);
            //    var scrollbox = $('.scrollbox', $this).css(self.options.viewoption.valuecss);
            //    var txt_begin = $('.txt', $this).css(self.options.viewoption.valuecss);
            //    var txt_end = $('<div class="txt-clone"></div>').css(self.options.viewoption.valuecss);


            //    var scrollVaue = 0;
            //    //翻转 逐行翻转 间隔300毫秒
            //    setTimeout(function () {
            //        var tbline = $this;
            //        tbline.addClass("inneranimation");
            //    }, 50 * index);

            //    //如果 文字div 的宽度 大于容器div的宽度，需要滚动处理
            //    if (txt_begin.width() > scrollbox.width()) {
            //        txt_end.html(txt_begin.html());
            //        scrollbox.append(txt_end);
            //        setTimeout(marquee, self.options.viewoption.sleepspace*1000);
            //    }
            //    function marquee() {
            //        //scrollLeft() 方法返回或设置匹配元素的滚动条的水平位置。
            //        //滚动条的水平位置指的是从其左侧滚动过的像素数。当滚动条位于最左侧时，位置是 0。
            //        //文字宽度 < 滚动
            //        if (txt_end.width() - scrollbox.scrollLeft() <= 0) {

            //            //重置位置
            //            //停留2秒
            //            setTimeout(marquee, self.options.viewoption.sleepspace*1000);

            //            scrollVaue = scrollbox.scrollLeft() - txt_begin.width();
            //            scrollbox.scrollLeft(scrollVaue);

            //        } else {
            //            //移动
            //            scrollVaue = scrollVaue + 1;
            //            scrollbox.scrollLeft(scrollVaue);

            //            setTimeout(marquee, self.options.viewoption.rollingspeed);
            //        }
            //    }
            //});

           
        },
        
    }
    window.VRowScrollText = VRowScrollText;
})(jQuery);