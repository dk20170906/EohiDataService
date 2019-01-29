(function () {
    //获取数据;
    var scenenid = "00000";         //全局
    //获取nodeid 对应的数据信息;
    scenenid = GetRequest()["flowchart_id"];
    if (scenenid == undefined) {
        //创建 flowchart_id
        scenenid = NewGuid();
    }




         //获取页面传过来的值 
    function GetRequest() {
        var url = location.search; //获取url中"?"符后的字串   
        var theRequest = new Object();
        if (url.indexOf("?") != -1) {
            var str = url.substr(1);
            strs = str.split("&");
            for (var i = 0; i < strs.length; i++) {
                theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
            }
        }
        return theRequest;
    }


 

})()