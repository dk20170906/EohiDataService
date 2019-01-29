using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace IMServer.Util
{
    /// <summary>
    /// 本地配置信息保存类
    /// </summary>
    public class LocalConfigXml
    {
        public LocalConfigXml()
        {

        }
        /// <summary>
        /// 保存本地配置信息
        /// </summary>
        /// <param name="filename">文件名  如:a.xml.该文件存在运行目录\\config\\xml 目录下.</param>
        /// <param name="key">key 如:name</param>
        /// <param name="value">value 如:管理员</param>
        /// <returns></returns>
        public static bool SetKey(string filename, string key, string value)
        {
            try
            {
                string str_path = "";
                //获取登录用户配置绝对路径
                str_path = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\xml\\";
                str_path += filename;

                //判断文件是否存在，如果不存在，则创建
                if (!File.Exists(str_path))
                {
                    CreateXml(filename);
                }

                if (File.Exists(str_path))
                {
                    DataSet ds = new DataSet();
                    FileStream fin = new FileStream(str_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    ds.ReadXml(fin);
                    fin.Close();
                    fin.Dispose();

                    if (ds.Tables.Count <= 0)
                    {
                        DataTable dt = new DataTable();
                        ds.Tables.Add(dt);
                    }


                    //如果没有行记录，则增加行记录
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        ds.Tables[0].Rows.Add(dr);
                    }

                    //
                    if (ds.Tables[0].Columns.Contains(key))
                    {
                        ds.Tables[0].Rows[0][key] = DESEncrypt.Encrypt(value);
                    }
                    else
                    {
                        ds.Tables[0].Columns.Add(key, typeof(string));
                        ds.Tables[0].Rows[0][key] = DESEncrypt.Encrypt(value);
                    }


                    //XmlDataDocument datadoc = new XmlDataDocument(ds);
                    ds.WriteXml(str_path, XmlWriteMode.IgnoreSchema);

                    //释放资源
                    ds.Clear();
                    ds.Dispose();

                    return true;
                }
            }

            catch (Exception exp)
            {
                // Response.Write(ex.Message);
                //throw new ArgumentNullException("HistroyNodeTextXmlWrite", "配置信息保存失败!" + exp.Message);
                //MessageBox.Show("保存登录用户信息异失败");
            }

            return false;

        }



        public static bool SetKey(string filename, string key, string value, bool bEncrypt)
        {
            try
            {
                if(bEncrypt)
                    value = DESEncrypt.Encrypt(value);


                string str_path = "";
                //获取登录用户配置绝对路径
                str_path = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\xml\\";
                str_path += filename;

                //判断文件是否存在，如果不存在，则创建
                if (!File.Exists(str_path))
                {
                    CreateXml(filename);
                }

                if (File.Exists(str_path))
                {
                    DataSet ds = new DataSet();
                    FileStream fin = new FileStream(str_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    ds.ReadXml(fin);
                    fin.Close();
                    fin.Dispose();

                    if (ds.Tables.Count <= 0)
                    {
                        DataTable dt = new DataTable();
                        ds.Tables.Add(dt);
                    }


                    //如果没有行记录，则增加行记录
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        ds.Tables[0].Rows.Add(dr);
                    }

                    //
                    if (ds.Tables[0].Columns.Contains(key))
                    {

                        ds.Tables[0].Rows[0][key] = value;
                    }
                    else
                    {
                        ds.Tables[0].Columns.Add(key, typeof(string));
                        ds.Tables[0].Rows[0][key] = value;
                    }


                    //XmlDataDocument datadoc = new XmlDataDocument(ds);
                    ds.WriteXml(str_path, XmlWriteMode.IgnoreSchema);

                    //释放资源
                    ds.Clear();
                    ds.Dispose();

                    return true;
                }
            }

            catch (Exception exp)
            {
                // Response.Write(ex.Message);
                //throw new ArgumentNullException("HistroyNodeTextXmlWrite", "配置信息保存失败!" + exp.Message);
                //MessageBox.Show("保存登录用户信息异失败");
            }

            return false;

        }


        private static bool CreateXml(string filename)
        {
            //
            string str_path = AppDomain.CurrentDomain.BaseDirectory + "\\Config";

            if (!Directory.Exists(str_path))
                Directory.CreateDirectory(str_path);

            str_path = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\xml";
            if(!Directory.Exists(str_path))
                Directory.CreateDirectory(str_path);

            str_path = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\xml\\" + filename;
            if (File.Exists(str_path))
            {
                try
                {
                    File.Delete(str_path);
                }
                catch (Exception exp)
                {
                    return false;
                    // throw;
                }
            }

            // 
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds.Tables.Add(dt);
                //
                ds.WriteXml(str_path, XmlWriteMode.IgnoreSchema);
            }
            catch (Exception exp2)
            {
                return false;
                // throw;
            }

            return true;
        }

        /// <summary>
        /// 读取本地配置信息
        /// </summary>
        /// <param name="filename">文件名  如:a.xml.该文件存在运行目录\\config\\xml 目录下</param>
        /// <param name="key"></param>
        /// <returns>返回文本</returns>
        public static string GetKey(string filename, string key)
        {
             DataSet ds = new DataSet();
            try
            {
                string str_path = "";
                //获取登录用户配置绝对路径
                str_path = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\xml\\";
                str_path += filename;

                //判断文件是否存在，如果不存在，则创建
                if (!File.Exists(str_path))
                {
                    return "";
                }

                if (File.Exists(str_path))
                {
                   

                    FileStream fin = new FileStream(str_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    ds.ReadXml(fin);
                    fin.Close();
                    fin.Dispose();

                    if (ds.Tables.Count <= 0)
                    {
                        return "";
                    }

                    //如果没有行记录，则增加行记录
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        return "";
                    }

                    if (!ds.Tables[0].Columns.Contains(key))
                    {
                        return "";
                    }


                    return DESEncrypt.Decrypt(ds.Tables[0].Rows[0][key].ToString());

                }
            }

            catch (Exception exp)
            {
                // Response.Write(ex.Message);
                //throw new ArgumentNullException("HistroyNodeTextXmlWrite", "配置信息保存失败!" + exp.Message);
                //MessageBox.Show("保存登录用户信息异失败");
            }
            finally
            {
                ds.Clear();
                ds.Dispose();
            }
            return "";
        }


        /// <summary>
        /// 读取本地配置信息
        /// </summary>
        /// <param name="filename">文件名  如:a.xml.该文件存在运行目录\\config\\xml 目录下</param>
        /// <param name="key"></param>
        /// <returns>返回文本</returns>
        public static string GetKey(string filename, string key, bool bEncrypt)
        {
            DataSet ds = new DataSet();
            try
            {
                string str_path = "";
                //获取登录用户配置绝对路径
                str_path = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\xml\\";
                str_path += filename;

                //判断文件是否存在，如果不存在，则创建
                if (!File.Exists(str_path))
                {
                    return "";
                }

                if (File.Exists(str_path))
                {


                    FileStream fin = new FileStream(str_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    ds.ReadXml(fin);
                    fin.Close();
                    fin.Dispose();

                    if (ds.Tables.Count <= 0)
                    {
                        return "";
                    }

                    //如果没有行记录，则增加行记录
                    if (ds.Tables[0].Rows.Count <= 0)
                    {
                        return "";
                    }

                    if (!ds.Tables[0].Columns.Contains(key))
                    {
                        return "";
                    }
                    string vv = ds.Tables[0].Rows[0][key].ToString();
                    if(bEncrypt)
                        vv = DESEncrypt.Decrypt(ds.Tables[0].Rows[0][key].ToString());
                    return vv;

                }
            }

            catch (Exception exp)
            {
                // Response.Write(ex.Message);
                //throw new ArgumentNullException("HistroyNodeTextXmlWrite", "配置信息保存失败!" + exp.Message);
                //MessageBox.Show("保存登录用户信息异失败");
            }
            finally
            {
                ds.Clear();
                ds.Dispose();
            }
            return "";
        }

    }
}
