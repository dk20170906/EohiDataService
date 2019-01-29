using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Admin/Login/
        //
        // GET: /Admin/Login/
        public ActionResult Index()
        {
            //ssGetRegionCode();
            ViewBag.ReturnUrl = Request.QueryString["ReturnUrl"];
            if (TempData.ContainsKey("loginerr"))
            {
                string err = TempData["loginerr"].ToString();
                string errmsg = TempData["loginerrmsg"].ToString();
                ViewBag.err = err;
                ViewBag.errmsg = errmsg;
            }
            return View();
            //return PartialView();//会将页面的Layout自动设为null
        }
        private void GetList()
        {
            
        }
        public ActionResult Login(Models.AdminAccount adminAccount, string ReturnUrl, string code)
        {

            if (string.IsNullOrEmpty(code) || Session["code"] == null)
            {
                TempData["loginerr"] = "err";
                TempData["loginerrmsg"] = "验证码已失效";
                return RedirectToAction("Index", "Login", ViewBag);
            }
            else
            {
                if (code.ToLower() != Session["code"].ToString().ToLower())
                {
                    TempData["loginerr"] = "err";
                    TempData["loginerrmsg"] = "验证码错误";
                    return RedirectToAction("Index", "Login", ViewBag);
                }
            }

            Models.AdminAccount user = AdminAccountService.GetbyUserId(adminAccount.User_id);
            if (user == null)
            {
                //登陆失败;
                //ViewBag.err = "err";
                //ViewBag.errmsg = "账号未注册！";
                TempData["loginerr"] = "err";
                TempData["loginerrmsg"] = "账号未注册";
                return RedirectToAction("Index", "Login", ViewBag);
            }
            else
            {
                //判断密码;
                if (user.User_password != adminAccount.User_password)
                {
                    //登陆失败
                    TempData["loginerr"] = "err";
                    TempData["loginerrmsg"] = "密码错误";
                    return RedirectToAction("Index", "Login", ViewBag);
                }
                else
                {
                    //登陆成功;
                    CreateSession(user);
                    var ggg = Request.UrlReferrer.OriginalString;
                    Session["RegionCode"] = null;
                    //跳转
                    if (string.IsNullOrEmpty(ReturnUrl))
                    {

                        return RedirectToAction("Index", "Home");//如果登录成功跳转的页面。

                    }
                    else
                    {
                        return Redirect(ReturnUrl);

                    }
                }
            }

        }



        private void CreateSession(Models.AdminAccount user)
        {
            //session处理;
            Models.AdminAccount entity = new Models.AdminAccount();
            entity.User_name = user.User_name;
            //entity.pwd = "";
            HttpContext.Session["admin"] = entity;
        }


        public ActionResult Logout()
        {
            //
            HttpContext.Session["admin"] = null;

            return RedirectToAction("Index", "Login");
        }

    }
}
