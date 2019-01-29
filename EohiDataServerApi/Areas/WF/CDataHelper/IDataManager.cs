using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
namespace WFServerWeb
{
    public interface IDataManager
    {
        int ExecuteNonQuery(string cmdText);
        DataTable GetDataTable(string cmdText);
    }
}
