using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EohiDataServerApi.Models
{
    public enum APIEChartsTypesEnum
    {
        chart_line =0,    //拆线图
        chart_pie=1,        //饼图
        chart_bar =2,         //柱状图
        chart_radar =3,   //雷达图
        chart_liquidfill   =4,//水滴图
        percentpie=5,//进度环
        percentbar=6,//进度条
    }
}