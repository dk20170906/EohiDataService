using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EohiDataServerApi.Areas.Chart.Controllers
{
    public class DoController : Controller
    {
        public string GetRomotingAddress()
        {
            return ConfigurationManager.AppSettings["remotingsqladdress"];
        
        }

        //
        // GET: /Chart/Do/

        public ActionResult Get()
        {


            string id = Request.QueryString["id"];
            string webappname = Request.QueryString["webappname"];
            if (id == null && webappname == null)
            {
                ViewBag.webapphtml = "找到不预定的参数 id 或者 webappname";
                ViewBag.webappscript = "";
            }

            else
            {
                try
                {
                    DataTable dt = null;
                    
                    string strSql = @"";
                    if (id != null)
                    {
                        strSql = @"select * from  api_webapp where id=@id";
                        SqlParameter[] parames = new SqlParameter[]
                        {
                            new SqlParameter("@id", id.ToString())
                        };
                        dt = DBHelper.getDataTable(strSql, parames);
                    }
                    else
                    {
                        strSql = @"select * from  api_webapp where webappname=@webappname";
                        SqlParameter[] parames = new SqlParameter[]
                        {
                            new SqlParameter("@webappname", webappname.ToString())
                        };
                        dt = DBHelper.getDataTable(strSql, parames);
                    }
                   
                    
                    //DataTable dt = DBHelper.getDataTable(strSql, parames);


                    //this.gridControl_report.DataSource = result.DataTable;
                    string html = "";
                    string javascript = "";

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        html = dt.Rows[0]["webapphtml"].ToString();
                        javascript = dt.Rows[0]["webappscript"].ToString();
                    }
                    else
                    {
                        html = "信息获取失败";
                        //Common.Util.NocsMessageBox.Message("信息获取失败！");
                    }

                    ViewBag.webapphtml = html;
                    ViewBag.webappscript = javascript;


                }
                catch (Exception exp)
                {
                    ViewBag.webapphtml = exp.Message;
                    ViewBag.webappscript = "";
                }
            }


            return View();
        }

        public ActionResult GetCleanByName()
        {
            //string id = Request.QueryString["id"];
            string webappname = Request.QueryString["webappname"];
            //if(string.IsNullOrEmpty(id)&

            try
            {

                string strSql = @"select * from  api_webapp where webappname=@webappname";
                SqlParameter[] parames = new SqlParameter[]
                {
                    new SqlParameter("@webappname", webappname)
                };
                DataTable dt = DBHelper.getDataTable(strSql, parames);


                //this.gridControl_report.DataSource = result.DataTable;
                string html = "";
                string javascript = "";

                if (dt != null && dt.Rows.Count > 0)
                {
                    html = dt.Rows[0]["webapphtml"].ToString();
                    javascript = dt.Rows[0]["webappscript"].ToString();
                }
                else
                {
                    html = "信息获取失败";
                    //Common.Util.NocsMessageBox.Message("信息获取失败！");
                }

                ViewBag.webapphtml = html;
                ViewBag.webappscript = javascript;


            }
            catch (Exception exp)
            {
                ViewBag.webapphtml = exp.Message;
                ViewBag.webappscript = "";
            }

            return View();
        }
    }
}
