using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EohiDataCenter
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new FormMain_Designer());
        //}
        static void Main()
        {
            //异常;
            Application.ThreadException += applicationThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += currentDomainUnhandledException;


            //风格；
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //dev 
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            //汉化
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CHS");

            //注册 remoting 通讯；
            //ChannelServices.RegisterChannel(new HttpClientChannel(), false);

            /*BinaryClientFormatterSinkProvider binaryClient = new BinaryClientFormatterSinkProvider();
            BinaryServerFormatterSinkProvider binaryServer = new BinaryServerFormatterSinkProvider();

            binaryServer.TypeFilterLevel = TypeFilterLevel.Full;
            IDictionary diction = new Hashtable();
            diction["name"] = "http";
            diction["port"] = 0;
            HttpChannel tcp = new HttpChannel(diction, binaryClient, binaryServer);
            ChannelServices.RegisterChannel(tcp, false);
            /**/



         


            //检查连接
            if (!ConnCheck())
            {
                //连接无效，设置连接；
                if (!ConnSet())
                {
                    Application.Exit();
                }
                else
                {
                    StartApp();
                }
            }
            else
            {
                StartApp();
            }
        }


        //public static FormMain _mainFrm;

        /// <summary>
        /// 连接检查
        /// </summary>
        /// <returns></returns>
        private static bool ConnCheck()
        {
            try
            {
                RemotingConfig.ReadConf();


                //string strSql = @"select getdate()";

                //Hashtable parames = new Hashtable();
                //EohiDataRemoteObject.RemotingSQLHelper remotingSQLHelper = (EohiDataRemoteObject.RemotingSQLHelper)Activator.GetObject(
                //   typeof(EohiDataRemoteObject.RemotingSQLHelper), RemotingConfig.RetmotingSqlAddress);

                //EohiDataRemoteObject.RemotingSQLResult result = remotingSQLHelper.getDataTable(strSql, parames);
                //if (result.Code > 0)
                //{
                //    return false;
                //}
                //else
                //{
                //    return true;
                //}
                return true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return false;
            }
        }

        /// <summary>
        /// 设置连接
        /// </summary>
        /// <returns></returns>
        private static bool ConnSet()
        {
            return RemotingConfig.SetConf();
            //return Common.DBHelper.SqlConn.ConnectionSetting();
        }

        /// <summary>
        /// 启动主程序
        /// </summary>
        private static void StartApp()
        {
            bool bAutoUpdate = false;
            //bAutoUpdate = true;

            if (bAutoUpdate)
            {
                //获取本地版本信息;
                if (Update.checkUpdateFiles())
                {
                    //启动更新;
                    Update.LoadUpdateApp();
                }
                else
                {
                    //启动主程序
                    LoadMainForm();
                }
            }
            else
            {
                //启动主程序
                LoadMainForm();
            }

        }
        private static void LoadMainForm()
        {
            try
            {
               
                
              
                /**/


                //Form_Main appStart = new Form_Main();
                ////appStart.ShowDialog();
                //if (appStart.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new Form_Main());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private static void doHandleException(Exception x)
        {
            /*
            if (x is ObjectDisposedException)
            {
                // Eat.
            }
            else
            {
                MessageBox.Show(x.Message);
            }
            */
            MessageBox.Show(x.Message);
        }

        private static void currentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            doHandleException((Exception)e.ExceptionObject);
        }

        private static void applicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            doHandleException(e.Exception);
        }
    }
}
