using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFServerWeb
{
    class CDataManagFactory
    {
        public IDataManager CreateDataManager(DataType datatype)
        {
            switch (datatype)
            {
                case DataType.SQLSERVER:
                    CSQLSERVERHelper cssh = new CSQLSERVERHelper();
                    cssh.connstr = Global.SqlConnStr;
                    return cssh;
                case DataType.ORACLE:
                    COracleHelper coch = new COracleHelper();
                    return coch;
                case DataType.MYSQL:
                    CMySQLHelper cmsh = new CMySQLHelper();
                    return cmsh;
                case DataType.TXT:
                    CTxtHelper cth = new CTxtHelper();
                    return cth;
                case DataType.XML:
                    CXMLHelper cxlh = new CXMLHelper();
                    return cxlh;
                default:
                    CSQLSERVERHelper cssh1 = new CSQLSERVERHelper();
                    cssh1.connstr = Global.SqlConnStr;
                    return cssh1;
            }
        }
    }
}
