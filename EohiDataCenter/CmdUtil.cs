using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EohiDataCenter
{
    
    /// <summary> 
    /// CmdUtility 的摘要说明。 
    /// </summary> 
    public class CmdUtil
    {

        /// <summary> 
        /// 执行cmd.exe命令 
        /// </summary> 
        /// <param name="commandText">命令文本</param> 
        /// <returns>命令输出文本</returns> 
        public string ExeCommand(string commandText)
        {
            return ExeCommand(new string[] { commandText });
        }
        /// <summary> 
        /// 执行多条cmd.exe命令 
        /// </summary> 
        /// <param name="commandArray">命令文本数组</param> 
        /// <returns>命令输出文本</returns> 
        public string ExeCommand(string[] commandTexts)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            string strOutput = null;
            try
            {
                p.Start();
                foreach (string item in commandTexts)
                {
                    p.StandardInput.WriteLine(item);
                }
                p.StandardInput.WriteLine("exit");
                strOutput = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();
            }
            catch (Exception e)
            {
                strOutput = e.Message;
            }
            return strOutput;
        }
        /// <summary> 
        /// 启动外部Windows应用程序，隐藏程序界面 
        /// </summary> 
        /// <param name="appName">应用程序路径名称</param> 
        /// <returns>true表示成功，false表示失败</returns> 
        public bool StartApp(string appName)
        {
            return StartApp(appName, ProcessWindowStyle.Hidden);
        }
        /// <summary> 
        /// 启动外部应用程序 
        /// </summary> 
        /// <param name="appName">应用程序路径名称</param> 
        /// <param name="style">进程窗口模式</param> 
        /// <returns>true表示成功，false表示失败</returns> 
        public bool StartApp(string appName, ProcessWindowStyle style)
        {
            return StartApp(appName, null, style);
        }
        /// <summary> 
        /// 启动外部应用程序，隐藏程序界面 
        /// </summary> 
        /// <param name="appName">应用程序路径名称</param> 
        /// <param name="arguments">启动参数</param> 
        /// <returns>true表示成功，false表示失败</returns> 
        public bool StartApp(string appName, string arguments)
        {
            return StartApp(appName, arguments, ProcessWindowStyle.Hidden);
        }
        /// <summary> 
        /// 启动外部应用程序 
        /// </summary> 
        /// <param name="appName">应用程序路径名称</param> 
        /// <param name="arguments">启动参数</param> 
        /// <param name="style">进程窗口模式</param> 
        /// <returns>true表示成功，false表示失败</returns> 
        public bool StartApp(string appName, string arguments, ProcessWindowStyle style)
        {
            bool blnRst = false;
            Process p = new Process();
            p.StartInfo.FileName = appName;//exe,bat and so on 
            p.StartInfo.WindowStyle = style;
            p.StartInfo.Arguments = arguments;
            try
            {
                p.Start();
                //p.WaitForExit();
                //p.Close();
                blnRst = true;

                
            }
            catch
            {
            }
            return blnRst;
        }

    } 
}
