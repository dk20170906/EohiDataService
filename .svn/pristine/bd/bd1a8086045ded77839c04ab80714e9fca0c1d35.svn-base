using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Areas.Api
{
    public class JsonpResult<T> : ActionResult
    {
        public T Obj { get; set; }
        public string CallbackName { get; set; }

        public JsonpResult(T obj, string callback)
        {
            this.Obj = obj;
            this.CallbackName = callback;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var js = new System.Web.Script.Serialization.JavaScriptSerializer();
            var jsonp = this.CallbackName + "(" + js.Serialize(this.Obj) + ")";

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.Write(jsonp);
        }
    }
}