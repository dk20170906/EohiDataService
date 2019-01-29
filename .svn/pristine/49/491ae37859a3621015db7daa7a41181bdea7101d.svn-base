using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {

        public ActionResult NavBar()
        {
            //获取Session;
            //如果无Session
           
            return View();
        }



        public ActionResult LeftMenu()
        {
            //获取用户菜单;
            //List<Models.MenuEntity> menu = new List<Models.MenuEntity>();
            //if (HttpContext.Session["menu"] != null)
            //{
            //    menu = HttpContext.Session["menu"] as  List<Models.MenuEntity>;
            //}

            List<Models.AdminMenu> menuList = new List<Models.AdminMenu>();


            string url = Request.Url.ToString();
            //获取用户菜单;
            Models.AdminMenu menu = new Models.AdminMenu();
            menu.Menucode = "0";
            menu.Parentcode = "";
            menu.Menuname = "系统设置";
            menu.Menuurl = "";


            //子菜单;
            Models.AdminMenu childmenu = new Models.AdminMenu();
            childmenu.Menucode = "001";
            childmenu.Parentcode = "0";
            childmenu.Menuname = "系统信息";
            childmenu.Menuurl = "Admin/Systeminfo/Index";
            menu.childMenus.Add(childmenu);


            //子菜单;
            childmenu = new Models.AdminMenu();
            childmenu.Menucode = "002";
            childmenu.Parentcode = "0";
            childmenu.Menuname = "文件更新";
            childmenu.Menuurl = "Admin/UpdateFile/Index";
            menu.childMenus.Add(childmenu);

            childmenu = new Models.AdminMenu();
            childmenu.Menucode = "003";
            childmenu.Parentcode = "0";
            childmenu.Menuname = "许可证";
            childmenu.Menuurl = "Admin/Systemlicense/Index";
            menu.childMenus.Add(childmenu);



            childmenu = new Models.AdminMenu();
            childmenu.Menucode = "0004";
            childmenu.Parentcode = "0";
            childmenu.Menuname = "企业动态";
            childmenu.Menuurl = "Admin/Article/Index";
            menu.childMenus.Add(childmenu);


            menuList.Add(menu);


            //获取用户菜单;
            //数据交互，API管理
            menu = new Models.AdminMenu();
            menu.Menucode = "1";
            menu.Parentcode = "";
            menu.Menuname = "数据交互";
            menu.Menuurl = "";

            childmenu = new Models.AdminMenu();
            childmenu.Menucode = "004";
            childmenu.Parentcode = "1";
            childmenu.Menuname = "API管理";
            childmenu.Menuurl = "Admin/API/Index";
            menu.childMenus.Add(childmenu);


            childmenu = new Models.AdminMenu();
            childmenu.Menucode = "005";
            childmenu.Parentcode = "1";
            childmenu.Menuname = "WEBAPP管理";
            childmenu.Menuurl = "Admin/WebApp/Index";
            menu.childMenus.Add(childmenu);


            childmenu = new Models.AdminMenu();
            childmenu.Menucode = "006";
            childmenu.Parentcode = "1";
            childmenu.Menuname = "连接管理";
            childmenu.Menuurl = "Admin/ApiLink/Index";
            menu.childMenus.Add(childmenu);


            childmenu = new Models.AdminMenu();
            childmenu.Menucode = "007";
            childmenu.Parentcode = "1";
            childmenu.Menuname = "3D文件上传";
            childmenu.Menuurl = "Admin/ThreeDUploadFiles/Index";
            menu.childMenus.Add(childmenu);

            menuList.Add(menu);



            //获取用户菜单;
            //数据看板
            menu = new Models.AdminMenu();
            menu.Menucode = "4";
            menu.Parentcode = "";
            menu.Menuname = "数据看板";
            menu.Menuurl = "";


            childmenu = new Models.AdminMenu();
            childmenu.Menucode = "4001";
            childmenu.Parentcode = "4";
            childmenu.Menuname = "看板定义";
            childmenu.Menuurl = "Admin/DataBoardDesigner/Index";
            menu.childMenus.Add(childmenu);


            menuList.Add(menu);


            //获取用户菜单;
            //定时调度
            menu = new Models.AdminMenu();
            menu.Menucode = "2";
            menu.Parentcode = "";
            menu.Menuname = "定时调度";
            menu.Menuurl = "";

            childmenu = new Models.AdminMenu();
            childmenu.Menucode = "007";
            childmenu.Parentcode = "2";
            childmenu.Menuname = "定时任务";
            childmenu.Menuurl = "Admin/QuartzNet/Index";
            menu.childMenus.Add(childmenu);


            childmenu = new Models.AdminMenu();
            childmenu.Menucode = "008";
            childmenu.Parentcode = "2";
            childmenu.Menuname = "定时任务查看";
            childmenu.Menuurl = "CrystalQuartzPanel.axd";
            menu.childMenus.Add(childmenu);

            menuList.Add(menu);

            ViewBag.menus = menuList;
            ViewBag.currenturl = url;
            //string html = getMenuHtmlCode(menu);
            //ViewBag.menuhtml = html;
            return View();
        }


        public ActionResult NavFooter()
        {
            return View();
        }
       
    }
}
