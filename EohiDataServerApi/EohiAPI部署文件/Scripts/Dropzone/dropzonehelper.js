

var Dropzonehelpers = {
    modalid :"fileupload",//
    callback :"fileuploadcallback",//
    imginptid :"Brandlogo",//图片值控件id
    imgviewareaid: "imgs",//图片显示区控件id
    imgappend:"add",//图片加入方式add,replace
    init: function (setting) {
        //debugger;
        this.modalid = setting.modalid;
        this.callback = setting.callback;
        this.imginptid = setting.imginptid;
        this.imgviewareaid = setting.imgviewareaid
        this.imgappend = setting.imgappend;
    },
    loadimg: function () {
        var filehtml = '';

        var files = $('#' + this.imginptid).val();
        if (files != null && files != '') {
            var filelist = files.split(';');
            for (a = 0; a < filelist.length; a++) {
                filehtml += this.addimg(filelist[a], 'load');
            }
            $('#' + this.imgviewareaid).html(filehtml);
        }
    },
    fileuploadlogo:function() {
        $('#' + this.modalid).modal();
        window.frames["ad_img"].clearfiles();
    },
    fileuploadcallback:function(data) {
        $('#' + this.modalid).modal('hide');
        var img = "";
        //商品图片;
        if (this.imgappend == "add")
            img = $('#' + this.imgviewareaid).html();

        
        for (var i = 0; i < data.files.length; i++) {
            img += this.addimg(data.files[i].url);
        }
        $('#' + this.imgviewareaid).html(img);
    },
    //增加图片，url为相对路径；
    addimg:function(url, type) {
        if (!url || url.length < 5)
            return '';
        var type = type || '';
        var img = '';
        img += '<div class="goodsmodel" onmouseover="Dropzonehelpers.onMoveOver(this);" onmouseout="Dropzonehelpers.onMoveOut(this);"><img  class="goodsimgitem selectedImg" src="' + url + '" filepath="' + url + '" is_primary="0"/>';
        img += '<div class="goodsclose"><a href="#" onclick="Dropzonehelpers.onDelClick(this);return false;">移除&nbsp;</a><a href="' + url + '" target="_blank">浏览</a></div></div>';
        return img;
    },
    onMoveOver:function(obj) {
        $(obj).children('.goodsclose').css({ display: 'block' });
    },
    onMoveOut:function(obj) {
        $(obj).children('.goodsclose').css({ display: 'none' });
    },
    onDelClick:function(obj) {
       $(obj).parent().parent().remove();
    },
    getfiles: function () {
        //组装图片；
        var objImgs = $("#imgs .goodsimgitem");
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
    getfilescount: function () {
        //组装图片；
        var objImgs = $("#imgs .goodsimgitem");
        if (objImgs && objImgs.length > 0) {
            return objImgs.length;
        }
        else
            return 0;
    },
    setimg: function () {
        var filestxt = this.getfiles();
        $('#' + this.imginptid).val(filestxt);
    }
    
    
}

