using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;

namespace IMServer.DBHelper
{
	/// <summary>
	/// Conn 的摘要说明。
	/// </summary>
	/// 
    public class SqlConn
    {
        public static string connUsedConfigFileName = "ApsConnUsed.xml";
        public static string urlConfigFileName = "Apsurl.xml";
        public static string connConfigFileName = "ApsConn.xml";
        public static string connSystemConfigFileName = "ApsSystemConn.xml";

        #region 变量

        public static string connectionString = string.Empty;       //内容数据库连接字符串
        /// <summary>
        /// 服务器名
        /// </summary>
        public static string Server = string.Empty;             //服务器名称
        /// <summary>
        /// 服务器登录端口
        /// </summary>
        public static string Port = string.Empty;            //端口
        /// <summary>
        /// 服务器登录账户
        /// </summary>
        public static string Uid = string.Empty;                 //数据库登用用户
        /// <summary>
        /// 服务器登录密码
        /// </summary>
        public static string Pwd = string.Empty;                   //数据库登录密码
        /// <summary>
        /// 数据数据库名
        /// </summary>
        public static string Database = string.Empty;               //内容数据库
        #endregion
        

        public SqlConn()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public static SqlConnection creatConnection()
        {
            SqlConnection myConn = new SqlConnection(GetConnectionString());
            return myConn;
        }
        /// <summary>
        /// 测试连接;
        /// </summary>
        /// <returns></returns>
        public static bool ConnectionOpenTest()
        {
            ReadConnConfig();

            Form_ConnTesting frm = new Form_ConnTesting();
            frm.Server = SqlConn.Server;
            frm.Port = SqlConn.Port;
            frm.Uid = SqlConn.Uid;
            frm.Pwd = SqlConn.Pwd;
            frm.Database = SqlConn.Database;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return frm.bConnAccess;
            }

            return false;
        }
        public static bool ConnectionSetting()
        {
            Form_SqlConfig frm = new Form_SqlConfig();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return true;

            return false;

        }
        public static string GetConnectionString()
        {
            if(connectionString==String.Empty)
                ReadConnConfig();
            return connectionString;
        }
      

        public static void ReadConnConfig()
        {
            /*
            Server ="127.0.0.1";
            Port ="1433";
            Uid = "sa";
            Pwd = "sa123456";
            Database = "kdata";
            */


            Server = Util.LocalConfigXml.GetKey(SqlConn.connConfigFileName, "Server");
            Port = Util.LocalConfigXml.GetKey(SqlConn.connConfigFileName, "Port");
            Uid = Util.LocalConfigXml.GetKey(SqlConn.connConfigFileName, "Uid");
            Pwd = Util.LocalConfigXml.GetKey(SqlConn.connConfigFileName, "Pwd");
            Database = Util.LocalConfigXml.GetKey(SqlConn.connConfigFileName, "Database");

            connectionString = "server=" + Server + "," + Port + ";uid=" + Uid + ";pwd=" + Pwd + ";database =" + Database;
        }
       

	}
}
