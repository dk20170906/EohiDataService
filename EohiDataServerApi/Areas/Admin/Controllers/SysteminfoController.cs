using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Areas.Admin.Controllers
{
    public class SysteminfoController : Controller
    {
        EFDBHelper<Models.a_systeminfo> dbhelper = new EFDBHelper<Models.a_systeminfo>();

        //
        // GET: /Admin/Systeminfo/

        public ActionResult Index()
        {
            var item = new Models.a_systeminfo();
            var list = dbhelper.GetAll(u=>u.id);
            if (list.Count > 0)
            {
                item = list.FirstOrDefault();
            }
            else
            {
                item = new Models.a_systeminfo();
                item.system_id =  Guid.NewGuid().ToString().ToUpper();
               
                item.system_name = "";
                item.system_worksitename = "";

                item.company_name = "";
                item.company_linkman = "";
                item.company_tel = "";
                item.company_address = "";

                item.system_licenseno = "未授权";
                item.system_effdate_e = "未授权";
                item.system_effdate_s = "未授权";

              
            }

            string directoryPath = Server.MapPath("~/LicenseFile/");
            string filepath = directoryPath + "/" + "License.lic";
            MyLicense license = MyLicenseHelper.Get(filepath);

            if (license.licenseno == "")
            {
                item.system_licenseno = "未授权";
                item.system_effdate_e = "未授权";
                item.system_effdate_s = "未授权";
            }
            else
            {
                item.system_licenseno = license.licenseno;
                item.system_effdate_s = license.licensedatestart.ToString("yyyy-MM-dd");
                item.system_effdate_e = license.licensedateend.ToString("yyyy-MM-dd");
            }
            
            ViewBag.entity = item;

            return View();
        }

        public ActionResult Save(Models.a_systeminfo entity)
        {
            if (entity.id<=0 )
            {
                dbhelper.Insert(entity);
                dbhelper.SaveChanges();
            }
            else
            {
                var olditem = dbhelper.FindById(entity.id);
                if (olditem != null)
                {
                    //olditem. = System.Data.EntityState.Detached; 
                    //这个是在同一个上下文能修改的关键
                    dbhelper.GetDbContext().Entry<Models.a_systeminfo>(olditem).State = System.Data.EntityState.Detached;
                  
                    entity.system_effdate_e = olditem.system_effdate_e;
                    entity.system_effdate_s = olditem.system_effdate_s;
                    entity.system_licenseno = olditem.system_licenseno;
                }

                //if (olditem != null)
                //{
                //    olditem.system_id = entity.system_id;
                //    olditem.system_name = entity.system_name;
                //    olditem.system_worksitename = entity.system_worksitename;
                //}
                //dbhelper.Update(olditem);
                dbhelper.Update(entity);
                dbhelper.SaveChanges();
            }

            return RedirectToAction("Index", "Systeminfo");
        }
    }
}
