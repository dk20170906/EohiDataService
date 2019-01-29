using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EohiDataRemoteObject
{
    [Serializable]
    public class NFXmlSql
    {
        public string cmd = "";
        public string id = "";
        public string ErrorMessage = "";
        public string resultClass = "";

        public bool HasError = false;

        public List<SqlParameter> parsList = new List<SqlParameter>();


        public string toXml()
        {
            //<![CDATA[红旗&CA72]]>
            string xml = "";
            xml += "<cmd><![CDATA[" + this.cmd + "]]></cmd>";
            xml += "<table><![CDATA[" + this.id + "]]></table>";
            xml += "<cmdpars>";

            for (int i = 0; i < parsList.Count; i++)
            {
                xml += "<parameter name=\"" + parsList[i].ParameterName + "\"><![CDATA[" + parsList[i].Value + "]]></parameter>";
            }
            xml += "</cmdpars>";

            return xml;
        }
    }
}
