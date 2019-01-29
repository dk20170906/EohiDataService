using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EohiDataServerApi.Models
{
    public class Model_ApiItem
    {


        int id=0;
        string apiname="";
        string apistatus = "";
        string apinote = "";
        string apipars = "";
        string apiscript = "";
        string mod_man = "";
        DateTime mod_date;




        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        public string Apiname
        {
            get { return apiname; }
            set { apiname = value; }
        }

        public string Apistatus
        {
            get { return apistatus; }
            set { apistatus = value; }
        }


        public string Apinote
        {
            get { return apinote; }
            set { apinote = value; }
        }
        public string Apiscript
        {
            get { return apiscript; }
            set { apiscript = value; }
        }

        public string Apipars
        {
            get { return apipars; }
            set { apipars = value; }
        }

        public string Mod_man
        {
            get { return mod_man; }
            set { mod_man = value; }
        }

        public DateTime Mod_date
        {
            get { return mod_date; }
            set { mod_date = value; }
        }
    }
}