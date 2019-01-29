/**
 * Created by 閮箍骞?2018.05.16
 */

(function ($) {

    /**
     * 榛樿鍙傛暟
     */
    var defaultOpts = {
        item: '',
        image: '',
        color: '#FFFFFF',
        backgroundColor: "none",   //div鑳屾櫙鑹?
        borderStyle: "solid",
        borderWidth: "14px",
    };

    /**
     * 瀹氫箟绫?
     */
    var VImageborder = function (options) {
        this.options = $.extend({}, defaultOpts, options);
        //璁板綍item鍊?
        defaultOpts.item = this.options.item;
        this.init();
    }

    VImageborder.prototype = {
        init: function () {
            var self = this;
            //console.log('test');
            var pdiv = this.options.item;
            var dataPanel2 = $('<div class="data-item-image-border"></div>');
            var width = $(pdiv).width();
            var height = $(pdiv).height();
            dataPanel2.css({
                width: width,
                height: height,
                //"border-width": "14px",
                //"border-style": "solid",
                "border-width": this.options.borderWidth,
                "border-style": this.options.borderStyle,
                top: 0,
                left: 0,
                position: 'absolute',
                "border-image-slice": "14 fill",
                "border-image-width": "initial",
                "border-image-outset": "initial",
                "border-image-repeat": "initial",
                //"background": "none",
                "background": this.options.backgroundColor,
                "border-image-source": "url('" + this.options.image + "')"
            });

            self.appendHandler(dataPanel2, pdiv);
        }
        ,
        /**
         * 鎻掑叆瀹瑰櫒
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
        }
        ,
        /*
        * 鏇存敼澶у皬
        */
        resize: function () {
  
            var self = this;
            //console.log('test');
            var pdiv = this.options.item;

            //self.resizeDiv = undefined;//鎷栨嫿闈㈡澘
            //鏇存敼鏁版嵁闈㈡澘澶у皬

            var width = $(pdiv).width();
            var height = $(pdiv).height();

            //鏇存敼div
            $(pdiv).children('div.data-item-image-border').css({
                width: width,
                height: height
            });

            //self.resizeDiv.css({
            //    display: 'none'
            //});

        },
        setOption: function (option) {
            this.options = $.extend({}, defaultOpts, option);
            var self = this;
            //console.log('test');
            var pdiv = this.options.item;
            //鏇存敼div
            var dataview = $(pdiv).children('div.data-item-image-border');
            //鏇存敼鏂囨湰鍐呭
            //dataview.html(this.options.html);
            dataview.css({
                "border-image-source": "url('" + this.options.image + "')"
            });
        }
    }
    window.VImageborder = VImageborder;

})(jQuery);