/**
 * Created by 郭广平 2018.05.16
 */

(function ($) {

    /**
     * 默认参数
     */
    var defaultOpts = {
        item: '',
        html: '显示文本',
        color: '#FFFFFF'
    };

    /**
     * 定义类
     */
    var VTable = function (options) {
        this.options = $.extend({}, defaultOpts, options);

        //记录item值;
        defaultOpts.item = this.options.item;

        div = undefined;

        this.stopscrollA1 = false;
        this.marqueesHeightA1 = 285;
        //var scrollElemA1 = document.getElementById('A1');
        this.scrollElemA1 = undefined;
        this.preTopA1 = 0;
        this.currentTopA1 = 0;
        this.stoptimeA1 = 0;
        //var leftElemA2 = document.getElementById('A2');
        this.leftElemA2 = undefined;

        this.init();
    }

    VTable.prototype = {
        init: function () {
            var self = this;

            //console.log('test');
            var pdiv = this.options.item;
            var id = this.options.itemno;
            var dataPanel2 = $('<div class="data-item-table"  id="data-item-table-' + this.options.itemno + '"></div>');
            var width = $(pdiv).width();
            var height = $(pdiv).height();
            dataPanel2.css({
                width: width,
                height: height,
                //top: 0,
                //left: 0,
                //position: 'absolute',
                //color: this.options.viewoption.color,
                //"font-size": this.options.viewoption.fontsize,
            });
            self.appendHandler(dataPanel2, pdiv);
            self.div = dataPanel2;

            //初始化表格;
            self.createVTable();

            //var scrollElemA1 = document.getElementById('A1');
            debugger;
            self.scrollElemA1 = document.getElementById(this.options.itemno + 'A1');
            self.leftElemA2 = document.getElementById(this.options.itemno + 'A2');


            //self.scrollElemA1 = document.getElementById('A1');
            //self.leftElemA2 = document.getElementById( 'A2');
            //

            //
            //selft.setInterval("tableInterval()", 2000);//每隔2秒执行一次change函数，相当于table在向上滚动一样

            /**/
            //self.loopFun($('#data-item-percent-pie')[0], 60, '#ccc', '#00A0E9', '#00A0E9', '20px', 20, 60, 1500, 'linear');

            //self.loopFun($('#data-item-percent-pie')[0], 40, '#ccc', '#00A0E9', '#00A0E9', '20px', 20, 60, 1500, 'linear');

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

            //
            self.change();
            /* */
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
        createVTable: function () {




            var self = this;
            var pdiv = this.options.item;
            var tablewidth = $(pdiv).width();
            var itemno = self.options.itemno;
            $('#data-item-table-' + this.options.itemno).html("");
            //加载数据
            var viewoption = self.options.viewoption;
            if (viewoption != undefined) {
                var columns = viewoption.columns;
                var html = "";
                html += '<table border="0" width="100%" cellspacing="0" cellpadding="0">';
                html += '<tr> ';
                html += '<td  width="100%">';



                //创建表头;
                html += ' <table id="table-' + itemno + '" class="vtable"  width="' + tablewidth + '" border="0" cellpadding="0">';

                var stylethead = "";

        
                if (viewoption.tabletheadcss != undefined) {
                    var obj = viewoption.tabletheadcss;
                    for (var item in obj ) {
                        if (obj.hasOwnProperty(item)) {
                            stylethead += item + ': ' + obj[item]+";";
                        }
                    }
                }

                html += '  <tr class="titletd" style="' + stylethead + '">';








                for (var a = 0; a < columns.length; a++) {

                    var col = columns[a];
                    var width = 80;
                    if (col.width != undefined)
                        width = col.width;
                    html += '  <th nowrap width="' + width + 'px">' + col.caption + '</th>';
                   
                }

                html += '  </tr>';
                html += '  </td>';
                html += '  </table>';
                html += '</tr>';

                html += '<tr>';
                html += ' <td width="100%">';


                var str = itemno;
                var reg = new RegExp("-", "g");
                var a = str.replace(reg, "");



                html += '<div id="' + itemno + 'A1' + '" class="scrollContent">';
                html += ' <div id="' + itemno + 'A2' + '">';

                //html += '<div id="'  + 'A1' + '" class="scrollContent">';
                //html += ' <div id="'  + 'A2' + '">';
                html += '  <table width="' + tablewidth + '" border="0" cellpadding="0" cellspacing="0" class="scrollTable" >';



                var styletbody = "";
                if (viewoption.tabletbodycss != undefined) {
                    var obj = viewoption.tabletbodycss;
                    for (var item in obj) {
                        if (obj.hasOwnProperty(item)) {
                            styletbody += item + ': ' + obj[item] + ";";
                        }
                    }
                }

                html += '  <tbody class="datatd" id="tbody-' + itemno + '" style="'+styletbody+'">';

               

                html += '  </tbody>';
                html += ' </table>';
                html += '</div>';
                html += '</div>';
                html += '</td>';
                html += '</tr>';

                //
                html += '</table >'
                //alert(html);
                //将表格放入;
                $('#data-item-table-' + this.options.itemno).html(html);
            }
            else {
                //创建表头;
                var html = ' <table id="table-' + itemno + '">';
                html += '  <thead>';
                html += '  <tr>';

                html += '  <th width="50">未定义列</th>';

                html += '  </tr>';
                html += '  </thead>';
                html += '  <tbody id="tbody-' + itemno + '">';

                html += '  </tbody>';
                html += ' </table>';

                //将表格放入;
                $('#data-item-table-' + this.options.itemno).html(html);

            }
        }
        ,
        change: function () {

            var self = this;
            var itemno = self.options.itemno;

            /*
             A1,A2或B1,B2是滚动内容区域外的两个DIV的ID
             如
             <div id="B1">
             <div id="B2">
             _W为滚动内容的宽度
             _H为滚动内容的高度,必须为单元格高度的整数倍，这里每个单元格是19px高
             _T为滚动后每次停留言时间
             2006-8-25
             */

            self.a();

        }
        ,
        a: function () {
            var self = this;

            //更改数据面板大小
            var pdiv = this.options.item;
            var width = $(pdiv).width();


            self.scrollElemA1.noWrap = true;

            $(self.scrollElemA1).css({
                width: width,
                height: self.marqueesHeightA1,
                overflow: "hidden"
            })


            self.scrollElemA1.appendChild(self.leftElemA2.cloneNode(true));
            setTimeout(self.bind(self, self.init_srolltextA1), 3000);

        }
        ,
        init_srolltextA1: function () {
            debugger;
            var self = this;
            self.scrollElemA1.scrollTop = 0;
            setInterval(self.bind(self, self.scrollUpA1), 50);

        },
        scrollUpA1: function () {
            //
            var self = this;
            if (self.stopscrollA1) {
                return;
            }

            self.currentTopA1 += 1;
            if (self.currentTopA1 == (self.marqueesHeightA1 + 1)) {
                self.stoptimeA1 += 1;
                self.currentTopA1 -= 1;
                if (self.stoptimeA1 == 60) {
                    self.currentTopA1 = 0;
                    self.stoptimeA1 = 0;
                }
            } else {
                self.preTopA1 = self.scrollElemA1.scrollTop;
                self.scrollElemA1.scrollTop += 1;
                if (self.preTopA1 == self.scrollElemA1.scrollTop) {
                    self.scrollElemA1.scrollTop = 0;
                    self.scrollElemA1.scrollTop += 1;
                }

            }
        }

        /*
        * 更改大小
        */
        ,
        resize: function () {

            var self = this;
            //console.log('test');
            var pdiv = this.options.item;

            //self.resizeDiv = undefined;//拖拽面板
            //更改数据面板大小

            var width = $(pdiv).width();
            var height = $(pdiv).height();


            $('#data-item-table-' + this.options.itemno).css({
                width: width,
                height: height
            });

        },

        /*
       * 设置数据
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
            else {

            }
        }
        ,
        /*
       * 设置显示选项
       */
        setViewoption: function (viewoptions) {
            //
            this.options.viewoption = $.extend({}, this.options.viewoption, viewoptions);

            var self = this;
            //console.log('test');
            var pdiv = this.options.item;
            //更改div
            var dataview = $(pdiv).children('div.data-item-table');

            //更改文本内容
            //dataview.html(this.options.html);
          
            $(".vtable").css(this.options.viewoption.tabletheadcss);

            $(".scrollTable").css(this.options.viewoption.tabletbodycss);

            //
            self.createVTable();
          


        }
        ,
        setData_jsondata: function () {
            var self = this;
            var viewdata = self.options.viewdata;
            //x y s
            var datamapping = viewdata.datamapping;
            //从
            var data = viewdata.datajson;
            //组装 tbody;
            self.createVTBody(data);

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

                    //组装 tbody;
                    self.createVTBody(data);


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
        }
        ,
        createVTBody: function (data) {
            var self = this;
            var itemno = self.options.itemno;
            //加载数据
            var viewoption = self.options.viewoption;
            if (viewoption == undefined) return;
            if (viewoption.columns == undefined || viewoption.columns.length <= 0)
                return;

            var columns = viewoption.columns;

            var html = "";
            for (b = 0; b < data.length; b++) {
                html += "<tr>"
                for (var a = 0; a < columns.length; a++) {
                    var col = columns[a];
                    var value = data[b][col.field];
                    var width = 80;
                    if (col.width != undefined)
                        width = col.width;

                    html += '  <td  nowrap width="' + width + 'px" style="color:#fff;">' + value + '</td>';
                }

                html += "</tr>"
            }

            //  html += '  <tbody id="tbody-' + itemno + '">';
            $("#tbody-" + itemno).html(html);

            //开始滚动;
            //self.change();

        }

    }
    window.VTable = VTable;

})(jQuery);