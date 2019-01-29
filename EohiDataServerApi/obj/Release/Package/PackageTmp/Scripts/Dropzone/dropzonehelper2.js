
function Dropzonehelpers() {
    this.modalid = "fileupload",//
     this.callback = "fileuploadcallback",//
     this.imginptid = "Brandlogo",//图片值控件id
     this.imgviewareaid = "imgs",//图片显示区控件id
     this.imgappend = "add",//图片加入方式add,replace
    this.init = function (setting) {
        //debugger;
        this.modalid = setting.modalid;
        this.callback = setting.callback;
        this.imginptid = setting.imginptid;
        this.imgviewareaid = setting.imgviewareaid
        this.imgappend = setting.imgappend;
    },
    this.loadimg = function () {
        var filehtml = '';

        var files = $('#' + this.imginptid).val();
        if (files != null && files != '') {
            var filelist = files.split(';');
            for (a = 0; a < filelist.length; a++) {
                filehtml += this.addimg(filelist[a], 'load');
            }
            $('#' + this.imgviewareaid).html(filehtml);

            //为goodsmodel对象添加事件 onmouseover，onmouseout
            $('.goodsmodel').on('mouseover', function () {
                //function code here.
                $(this).children('.goodsclose').css({ display: 'block' });
            });
            $('.goodsmodel').on('mouseout', function () {
                //function code here.
                $(this).children('.goodsclose').css({ display: 'none' });
            });
            $('.goodsclose').on('click', '.a_delimg', function () {
                //function code here.
                $(this).parent().parent().remove();
            });

        }
        else {
            $('#' + this.imgviewareaid).html('');
        }
    },
    this.fileuploadlogo = function () {
        $('#' + this.modalid).modal();
        window.frames["ad_img"].clearfiles();
    },
    this.fileuploadcallback = function (data) {
        $('#' + this.modalid).modal('hide');
        var img = "";
        //商品图片;
        if (this.imgappend == "add")
            img = $('#' + this.imgviewareaid).html();


        for (var i = 0; i < data.files.length; i++) {
            img += this.addimg(data.files[i].url);
        }
        $('#' + this.imgviewareaid).html(img);
        //为goodsmodel对象添加事件 onmouseover，onmouseout
        $('.goodsmodel').on('mouseover',  function () {
            //function code here.
            $(this).children('.goodsclose').css({ display: 'block' });
        });
        $('.goodsmodel').on('mouseout', function () {
            //function code here.
            $(this).children('.goodsclose').css({ display: 'none' });
        });
        $('.goodsclose').on('click', '.a_delimg', function () {
            //function code here.
            $(this).parent().parent().remove();
        });
       
        
    },
    //增加图片，url为相对路径；
    this.addimg = function (url, type) {
        if (!url || url.length < 5)
            return '';
        var type = type || '';
        var img = '';
        img += '<div class="goodsmodel" ><img  class="goodsimgitem selectedImg" src="' + url + '" filepath="' + url + '" is_primary="0"/>';
        img += '<div class="goodsclose"><a href="#" class="a_delimg" >移除&nbsp;</a><a href="' + url + '" target="_blank">浏览</a></div></div>';
        return img;
    },
    //this.onMoveOver = function (obj) {
    //    $(obj).children('.goodsclose').css({ display: 'block' });
    //},
    //this.onMoveOut = function (obj) {
    //    $(obj).children('.goodsclose').css({ display: 'none' });
    //},
    //this.onDelClick = function (obj) {
    //    $(obj).parent().parent().remove();
    //},
    this.getfiles = function () {
        //组装图片；
        var objImgs = $("#" + this.imgviewareaid + " .goodsimgitem");
        var valueimgs = "";
        if (objImgs && objImgs.length > 0) {
            for (var i = 0 ; i < objImgs.length; i++) {
                if (valueimgs.length > 0) {
                    valueimgs += ";";
                }
                valueimgs += $(objImgs[i]).attr("filepath")
            }
        }
        return valueimgs;
    },
    this.getfilescount = function () {
        //组装图片；
        var objImgs = $("#" + this.imgviewareaid + " .goodsimgitem");
        if (objImgs && objImgs.length > 0) {
            return objImgs.length;
        }
        else
            return 0;
    },
    this.setimg = function () {
        var filestxt = this.getfiles();
        $('#' + this.imginptid).val(filestxt);
    }
}
