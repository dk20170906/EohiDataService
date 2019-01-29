using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Areas.Admin.Controllers
{
    public class UpdateFileController : Controller
    {
        EFDBHelper<Models.a_system_updatefile> dbhelper = new EFDBHelper<Models.a_system_updatefile>();
        //
        // GET: /Admin/Version/

        public ActionResult Index()
        {
         
            return View();
        }


        /// <summary>
        /// 获取api列表;
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public JsonResult getlist(int page, int limit)
        {
            //int rows = 0;
            //var list = ApiItemService.GetList();
            //Models.kailifonEntities E = new Models.kailifonEntities();
            //var list = E.api_items.Select();

            int rows = 0;
            var list = dbhelper.Page(page, limit, out rows, u => u.filename, null);

            //Models.api_items
            return Json(new
            {
                code = 0,
                msg = "",
                count = rows,
                data = list
            },
            JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            Models.a_system_updatefile updateFile = new Models.a_system_updatefile();
            if (id <= 0)
            {
                //新增;
                updateFile.versionno = "";
                updateFile.savedir = "";

                ViewBag.entity = updateFile;
            }
            else
            {
                updateFile = dbhelper.FindById(id);
                ViewBag.entity = updateFile;
            }
            return View();//
        }

        [HttpPost]
        public JsonResult dataSave(Models.a_system_updatefile entity)
        {
            try
            {
                if (entity != null)
                {
                    if (entity.versionno == null)
                        entity.versionno = "";
                }

                entity.uptime = DateTime.Now;
                if (entity.id <= 0)
                {
                    dbhelper.Insert(entity);
                    dbhelper.SaveChanges();
                    //dbhelper.SaveChanges();
                }
                else
                {
                    //删除原有的文件;
                    //FileDelete(entity.id);
                    Models.a_system_updatefile oldEntity = dbhelper.FindById(entity.id);
                    if (oldEntity.fileurl != entity.fileurl)
                    {
                        //删除原有文件;
                        FileDelete(oldEntity.fileurl);
                    }


                    dbhelper.Update(entity);
                    dbhelper.SaveChanges();
                }
                return Json(new { access = true });

            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }

        }


        public JsonResult Del(int id)
        {
            try
            {
                //删除原有的文件;
                Models.a_system_updatefile oldEntity = dbhelper.FindById(id);
                FileDelete(oldEntity.fileurl);

                //删除记录
                Models.a_system_updatefile updateFile = dbhelper.FindById(id);
                dbhelper.Delete(updateFile);
                dbhelper.SaveChanges();

                return Json(new { access = true });

            }
            catch (Exception exp)
            {
                return Json(new { access = false, msg = exp.Message });
            }

        }




        public ActionResult Save(Models.UpdateFile updateFile )
        {
            if (updateFile != null)
            {
                if (updateFile.Savedir == null)
                    updateFile.Savedir = "";
                if (updateFile.Versionno == null)
                    updateFile.Versionno = "";
            }

            if (updateFile.Id <= 0)
            {
                //新增;
                UpdateFileService.Add(updateFile);
            }
            else
            {
                UpdateFileService.Update(updateFile);
            }

            //将文件从Datatrans\update\tmp文件夹移出至Datatrans\update
            var originalDirectory = new DirectoryInfo(string.Format("{0}DataTrans\\update", Server.MapPath(@"\")));
            string pathString1 = System.IO.Path.Combine(originalDirectory.ToString(), "tmp");
            string pathString2 = System.IO.Path.Combine(originalDirectory.ToString(), "");

            var path1 = string.Format("{0}\\{1}", pathString1, updateFile.Filename);
            var path2 = string.Format("{0}\\{1}", pathString2, updateFile.Filename);

            if (System.IO.File.Exists(path2))
                System.IO.File.Delete(path2);
            System.IO.File.Move(path1, path2);

            return RedirectToAction("Index", "UpdateFile");//跳转
        }

       
        public ActionResult FileSave()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}DataTrans\\update", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "tmp");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);

                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                        //保存
                        file.SaveAs(path);
                    }
                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

        public void FileDelete(string fileurl)
        {
            if (System.IO.File.Exists(Server.MapPath("~/") + fileurl))
                System.IO.File.Delete(Server.MapPath("~/") + fileurl);
        }
    }
}
