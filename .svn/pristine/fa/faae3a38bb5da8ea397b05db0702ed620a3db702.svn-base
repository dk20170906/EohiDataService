using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Areas.Admin.Controllers
{
    public class RmtSQLConfigController : Controller
    {
        //
        // GET: /Admin/RmtSQLConfig/

        public ActionResult Index()
        {

            //
            ViewBag.remotingsqlconn_server = ConfigurationManager.AppSettings["remotingsqlconn_server"];
            ViewBag.remotingsqlconn_port = ConfigurationManager.AppSettings["remotingsqlconn_port"];
            ViewBag.remotingsqlconn_uid = ConfigurationManager.AppSettings["remotingsqlconn_uid"];
            ViewBag.remotingsqlconn_pwd = ConfigurationManager.AppSettings["remotingsqlconn_pwd"];
            ViewBag.remotingsqlconn_database = ConfigurationManager.AppSettings["remotingsqlconn_database"];

            //
            return View();
        }
        public ActionResult Save()
        {

            //
            ViewBag.remotingsqlconn_server = ConfigurationManager.AppSettings["remotingsqlconn_server"];
            ViewBag.remotingsqlconn_port = ConfigurationManager.AppSettings["remotingsqlconn_port"];
            ViewBag.remotingsqlconn_uid = ConfigurationManager.AppSettings["remotingsqlconn_uid"];
            ViewBag.remotingsqlconn_pwd = ConfigurationManager.AppSettings["remotingsqlconn_pwd"];
            ViewBag.remotingsqlconn_database = ConfigurationManager.AppSettings["remotingsqlconn_database"];


            //重新启动网站;

            //
            return View();
        }

    }
}
