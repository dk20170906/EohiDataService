using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Controllers
{
    public class ValidateCodeController : Controller
    {
        //
        // GET: /ValidateCode/


        //获取验证码
        public ActionResult Get()
        {
            CommonUtil.ValidateCode ValidateCode = new CommonUtil.ValidateCode();
            string code = ValidateCode.CreateValidateCode(4);//生成验证码，传几就是几位验证码
            Session["code"] = code;
            byte[] buffer = ValidateCode.CreateValidateGraphic(code);//把验证码画到画布
            return File(buffer, "image/jpeg");
        }

    }
}
