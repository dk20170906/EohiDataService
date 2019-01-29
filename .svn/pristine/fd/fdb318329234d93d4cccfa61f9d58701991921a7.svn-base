using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EohiDataServerApi
{

    public class MyLicenseHelper
    {
        public static MyLicense Get(string LicenseFilePath)
        {
            MyLicense license = new MyLicense();
            try
            {
                string checkkey = "";
                string hardwarecode = "";
                string licenseno = "";
                DateTime licensedatestart = DateTime.MinValue;
                DateTime licensedateend = DateTime.MinValue;
                //读取授权文件;
                //string directoryPath = Server.MapPath("~/LicenseFile/");
                //string filepath = directoryPath + "/" + "License.lic";
                if (System.IO.File.Exists(LicenseFilePath))
                {
                    //读取文件内容;
                    using (StreamReader sr = new StreamReader(LicenseFilePath, System.Text.Encoding.Default))
                    {
                        //StreamReader sr = new StreamReader(LicenseFilePath, System.Text.Encoding.Default);
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string vv = line.ToString();
                            //Console.WriteLine(line.ToString());
                            if (vv.ToLower().StartsWith("hardwarecode="))
                            {
                                hardwarecode = vv.ToLower().Replace("hardwarecode=", "").Trim().ToUpper();
                            }
                            if (vv.ToLower().StartsWith("licenseno="))
                            {
                                licenseno = vv.ToLower().Replace("licenseno=", "").Trim().ToUpper();
                            }
                            if (vv.ToLower().StartsWith("datestart="))
                            {
                                licensedatestart = Convert.ToDateTime(vv.Replace("datestart=", ""));
                            }
                            if (vv.ToLower().StartsWith("dateend="))
                            {
                                licensedateend = Convert.ToDateTime(vv.Replace("dateend=", ""));
                            }

                            if (vv.ToLower().StartsWith("checkkey="))
                            {
                                checkkey = vv.ToLower().Replace("checkkey=", "").Trim().ToUpper();
                            }
                        }
                    }
                }

                //判断授权是否正确;
                string hardcode = Computer.GetBIOSInfo().ToUpper();
                hardcode = MD5.Get(hardcode).ToUpper();
                
                //硬件码一致
                if (hardcode == hardwarecode)
                {
                    string checkkeytmp="";// = Computer.GetDiskID();
                    //判断加密是否正确
                    //使用硬件码+许可号+授权开始日期+授权截止日期 md5
                    //1. 硬件码+许可号 md5
                    checkkeytmp = MD5.Get(hardcode + licenseno);
                    // + 授权开始日期
                    checkkeytmp = MD5.Get(checkkeytmp + licensedatestart.ToString("yyyy-MM-dd"));
                    // +授权截止日期
                    checkkeytmp = MD5.Get(checkkeytmp + licensedateend.ToString("yyyy-MM-dd"));
                    
                    //判断加密结果 和  检验码是否一直
                    if (checkkeytmp == checkkey)
                    {
                        //ok
                        license.licenseno = licenseno;
                        license.licensedatestart = licensedatestart;
                        license.licensedateend = licensedateend;
                    }
                    else
                    {
                        //无效的授权文件;
                    }
                }
                
            
            
            }
            catch (Exception)
            {
                //throw;
            }


            return license;
        }
    }
}