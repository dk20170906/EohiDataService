using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace EohiQuartzManager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StartApp();
        }

        public static void StartApp()
        {
            Common.DBHelper.SqlConn.ReadConnConfig();

            //连接无效，设置连接；
            if (Conn_Test())
            {
                Application.Run(new FormMain());
              
            }
            else
            {
                Application.Exit();
               
            }
        }

        private static bool Conn_Test()
        {

                string connString = Common.DBHelper.SqlConn.GetConnectionString();
                SqlConnection myConn = new SqlConnection(connString);
                try
                {
                    myConn.Open();

                    return true;
                }
                catch (SqlException exp)
                {
                   

                    if (Common.DBHelper.SqlConn.ConnectionSetting() == false)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                finally
                {
                    myConn.Close();
                    myConn.Dispose();
                }

        }
       

        /// <summary>
        /// 设置连接
        /// </summary>
        /// <returns></returns>
        private static bool ConnSet()
        {
            return Common.DBHelper.SqlConn.ConnectionSetting();
        }
    }
}
