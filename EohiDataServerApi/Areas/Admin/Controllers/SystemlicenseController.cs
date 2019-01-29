using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Areas.Admin.Controllers
{
    public class SystemlicenseController : Controller
    {
        //
        EFDBHelper<Models.a_systeminfo> dbhelper = new EFDBHelper<Models.a_systeminfo>();

        //
        // GET: /Admin/Systemlicense/

        public ActionResult Index()
        {
            var item = new Models.a_systeminfo();
            var list = dbhelper.GetAll(u => u.id);
            if (list.Count > 0)
            {
                item= list.FirstOrDefault();
            }
            else
            {
                item = new Models.a_systeminfo();
                item.system_id = Guid.NewGuid().ToString().ToUpper();
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
            string filepath = directoryPath +  "License.lic";
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

            string hardcode = Computer.GetBIOSInfo().ToUpper();
            hardcode = DESEncrypt.md5(hardcode, 32).ToUpper();

            ViewBag.system_hardwarecode = hardcode;

            return View();
        }


        public JsonResult GetLicense()
        {
            string directoryPath = Server.MapPath("~/LicenseFile/");
            string filepath = directoryPath + "/" + "License.lic";

            MyLicense license = MyLicenseHelper.Get(filepath);

            return Json(new
            {
                LicenseNo = license.licenseno,
                LicenseEffdateStart = license.licensedatestart.ToString("yyyy-MM-dd"),
                LicenseEffdateEnd = license.licensedateend.ToString("yyyy-MM-dd")
            },
            JsonRequestBehavior.AllowGet);
        }
    }
}
