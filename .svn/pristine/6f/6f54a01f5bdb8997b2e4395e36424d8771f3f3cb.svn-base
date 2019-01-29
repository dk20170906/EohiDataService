using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EohiDataCenter
{
    public class RemotingConfig
    {

        //public static string RetmotingSqlAddress = "http://localhost:9933/remotingsqlhelper";
        //public static string RetmotingSqlAddress = "http://localhost:39897/remotingsqlhelper.soap";
        //public static string RetmotingSqlAddress = "http://localhost:8881/remoting/SQLHelper.soap";
        public static string RetmotingSqlAddress = "tcp://localhost:9930/remotingsqlhelper";

        public static void ReadConf()
        {
            //设置;
            RetmotingSqlAddress = Common.Util.LocalConfigXml.GetKey("RemotingConf.xml", "RemotingSqlAddress");

        }

        public static bool SetConf()
        {
            //设置;
            FormRemotingConf frm = new FormRemotingConf();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RetmotingSqlAddress = Common.Util.LocalConfigXml.GetKey("RemotingConf.xml", "RemotingSqlAddress");

                return true;
            }
            return false;
        }

    }
}
