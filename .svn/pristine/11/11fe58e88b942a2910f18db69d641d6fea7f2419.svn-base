using EohiDataServerApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Areas.ThreeD.Controllers
{
    /// <summary>
    ///用户登录
    /// </summary>
    public class LoginController : Controller
    {
        EFDBHelper<api_user> dbapiuser = new EFDBHelper<api_user>();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="adminAccount"></param>
        /// <param name="ReturnUrl"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Login(string user_id, string password)
        {
            Models.AdminAccount user = AdminAccountService.GetbyUserId(user_id);
            if (user == null)
            {
                return Json(
                    new
                    {
                        statusCode = 0,
                        msg = "用户未注册！",
                    }
                , JsonRequestBehavior.AllowGet);
            }
            if (user.User_password.Equals(MD5.Get(password).ToLower())
            || user.User_password == password)
            {

                //CreateSession(user);
                return Json(new { statusCode = 200, msg = "登录成功！" }, JsonRequestBehavior.AllowGet);
            }
            return Json(
                new
                {
                    statusCode = -1,
                    msg = "未知错误!",
                }
            , JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ApiLogin(string user_name, string password)
        {
            api_user user = dbapiuser.GetAll(u => u.user_name == user_name, u => u.id).FirstOrDefault();
            if (user == null)
            {
                return Json(
                    new
                    {
                        statusCode = 0,
                        msg = "用户未注册！",
                    }
                , JsonRequestBehavior.AllowGet);
            }
            if (user.user_password.Equals(MD5.Get(password).ToLower())
            || user.user_password == password)
            {

                CreateSession(user);
                return Json(new { statusCode = 200, msg = "登录成功！" }, JsonRequestBehavior.AllowGet);
            }
            return Json(
                new
                {
                    statusCode = -1,
                    msg = "未知错误!",
                }
            , JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 保存登录状态
        /// </summary>
        /// <param name="user"></param>
        private void CreateSession(Models.api_user user)
        {
            //entity.pwd = "";
            HttpContext.Session["admin"] = user;
            HttpContext.Session.Timeout = 30000;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
          public JsonResult ChangePwd(string userid,string password)
        {
            Models.AdminAccount user=   AdminAccountService.UpdatePwd(userid, MD5.Get( password).ToLower());
            if (user.Id>0)
            {
                return Json(new
                {
                    statusCode = 200,
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                statusCode = 0,
            }, JsonRequestBehavior.AllowGet);
        }


          /// <summary>
          /// 获取当前用户
          /// </summary>
          /// <returns></returns>
        public JsonResult AccountLogin()
        {
            return Json(HttpContext.Session["admin"], JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoginSession()
        {
            return View();
        }

    }
}
