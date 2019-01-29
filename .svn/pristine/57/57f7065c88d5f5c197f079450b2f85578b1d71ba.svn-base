using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;

namespace EohiDataCenter
{
    public class Update
    {
        public Update()
        {
        }
        public static void LoadUpdateApp()
        {

            string updateexe = "WinUpdate.exe";
            CmdUtil cmdutil = new CmdUtil();
            if (cmdutil.StartApp(updateexe, "", System.Diagnostics.ProcessWindowStyle.Normal))
            {
                //退出
                //
            }



        }
        public static bool checkUpdateFiles()
        {

            //获取服务器文件列表;
            string xml = Common.WebDataHelper.GetUpdateFileList.Get();
            if (xml.StartsWith("error!"))
            {
                //
                return false;
            }
            else
            {
                //解析xml对象;
                if (xml.Length <= 0)
                    return false;
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xml);

                    XmlNodeList xmlList = xmlDoc.SelectSingleNode("files").ChildNodes;
                    foreach (XmlNode node in xmlList)
                    {
                        if (node.Name.ToLower() == "file")
                        {
                            string filename = getElInnerText(node, "filename");
                            string version = getElInnerText(node, "version");
                            string size = getElInnerText(node, "size");
                            string savepath = getElInnerText(node, "savepath");

                            //获取本地文件版本号,如果版本号不一致，写入等待更新列表；
                            string ver_local = Common.Util.LocalConfigXml.GetKey("updatefiles.xml", filename.ToLower());

                            if (ver_local.ToLower() != version.ToLower())
                            {
                                return true;

                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    //错误时
                    Common.Util.NocsMessageBox.Error("更新失败！"+ex.Message);
                    return false;
                }
            }

            return false;

        }
        private static string getElInnerText(XmlNode node, string elname)
        {
            XmlNode nodechild = node.SelectSingleNode(elname);
            if (nodechild != null)
            {
                return nodechild.InnerText;
            }
            return "";
        }
    }
}
