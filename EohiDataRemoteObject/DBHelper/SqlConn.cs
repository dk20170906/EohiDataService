using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
namespace EohiDataRemoteObject
{
	/// <summary>
	/// Conn 的摘要说明。
	/// </summary>
	/// 
    public class SqlConn
    {
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



        public static bool bConnIsAceess = false;//网络情况;


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
            //Server = Common.Util.LocalConfigXml.GetKey("Conn.xml", "Server");
            //Port = Common.Util.LocalConfigXml.GetKey("Conn.xml", "Port");
            //Uid = Common.Util.LocalConfigXml.GetKey("Conn.xml", "Uid");
            //Pwd = Common.Util.LocalConfigXml.GetKey("Conn.xml", "Pwd");
            //Database = Common.Util.LocalConfigXml.GetKey("Conn.xml", "Database");

            //EohiDataRemoteObject.SqlConn.Server = "121.40.200.42";
            //EohiDataRemoteObject.SqlConn.Port = "13533";
            //EohiDataRemoteObject.SqlConn.Uid = "sa";
            //EohiDataRemoteObject.SqlConn.Pwd = "sa123456";
            //EohiDataRemoteObject.SqlConn.Database = "u3dDesigner";
            //EohiDataRemoteObject.SqlConn.ReadConnConfig();
            //ConfigurationManager
            connectionString = "server=" + Server + "," + Port + ";uid=" + Uid + ";pwd=" + Pwd + ";database =" + Database;
            
        }
       

	}
}
