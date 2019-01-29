using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace EohiDataServerApi.CommonUtil
{
    public class ToolUnit
    {

        /// <summary>
        /// 根据路径删除文件
        /// </summary>
        /// <param name="path"></param>
        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static List<FileSystemInfo> SelectDirFiles(string srcPath, string fileSuffix)
        {
            List<FileSystemInfo> list = new List<FileSystemInfo>();
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                    }
                    else
                    {
                        if (i.Extension==fileSuffix&&!list.Contains(i))
                        {
                            list.Add(i);
                        }
                        else
                        {
                            return fileinfo.ToList();
                        }
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                throw;
            }
        }


        /// <summary>    
        /// datatable=>list
        /// </summary>    
        public class ModelConvertHelper<T> where T : new()
        {
            public static IList<T> ConvertToModel(DataTable dt)
            {
                // 定义集合    
                IList<T> ts = new List<T>();

                // 获得此模型的类型   
                Type type = typeof(T);
                string tempName = "";

                foreach (DataRow dr in dt.Rows)
                {
                    T t = new T();
                    // 获得此模型的公共属性      
                    PropertyInfo[] propertys = t.GetType().GetProperties();
                    foreach (PropertyInfo pi in propertys)
                    {
                        tempName = pi.Name;  // 检查DataTable是否包含此列    

                        if (dt.Columns.Contains(tempName))
                        {
                            // 判断此属性是否有Setter      
                            if (!pi.CanWrite) continue;

                            object value = dr[tempName];
                            if (value != DBNull.Value)
                                pi.SetValue(t, value, null);
                        }
                    }
                    ts.Add(t);
                }
                return ts;
            }
        }
    }
}