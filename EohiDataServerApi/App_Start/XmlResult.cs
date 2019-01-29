﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace EohiDataServerApi
{
    public class XmlResult : ActionResult
    {
        // 可被序列化的内容
        object Data { get; set; }

        // Data的类型
        Type EDataType { get; set; }

        // 构造器
        public XmlResult(object data, Type type)
        {
            Data = data;
            EDataType = type;
        }

        // 主要是重写这个方法
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            // 设置 HTTP Header 的 ContentType
            response.ContentType = "text/xml";

            if (Data != null)
            {
                // 序列化 Data 并写入 Response
                XmlSerializer serializer = new XmlSerializer(EDataType);
                MemoryStream ms = new MemoryStream();
                serializer.Serialize(ms, Data);
                response.Write(System.Text.Encoding.UTF8.GetString(ms.ToArray()));
            }
        }
    }

}