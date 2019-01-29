using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFServerWeb
{
    class CDataManagFactory
    {
        public IDataManager CreateDataManager(EDataType datatype)
        {
            switch (datatype)
            {
                case EDataType.SQLSERVER:
                    CSQLSERVERHelper cssh = new CSQLSERVERHelper();
                    cssh.connstr = WFGlobal.SqlConnStr;
                    return cssh;
                case EDataType.ORACLE:
                    COracleHelper coch = new COracleHelper();
                    return coch;
                case EDataType.MYSQL:
                    CMySQLHelper cmsh = new CMySQLHelper();
                    return cmsh;
                case EDataType.TXT:
                    CTxtHelper cth = new CTxtHelper();
                    return cth;
                case EDataType.XML:
                    CXMLHelper cxlh = new CXMLHelper();
                    return cxlh;
                default:
                    CSQLSERVERHelper cssh1 = new CSQLSERVERHelper();
                    cssh1.connstr = WFGlobal.SqlConnStr;
                    return cssh1;
            }
        }
    }
}
