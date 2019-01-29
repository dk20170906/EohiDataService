

//弹出对话框
 function layerAlert(msg)
 {
     layer.open({
         type: 1
        , offset: 'auto'
        , id: 'layerDemo_auto'  //防止重复弹出
        , content: '<div style="padding: 20px 100px;">' + msg + '</div>'
        , btn: '确定'
        , btnAlign: 'c' //按钮居中
        , shade: 0.8 //不显示遮罩
        , yes: function () {
            layer.closeAll();
        }
     });
 }