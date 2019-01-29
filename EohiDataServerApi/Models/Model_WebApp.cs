using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EohiDataServerApi.Models
{
    public class Model_WebApp
    {


        int id;
        string webappname;
        string webappnote;
        string webapphtml;
        string webappscript;
        string mod_man;
        DateTime mod_date;



        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        public string Webappname
        {
            get { return webappname; }
            set { webappname = value; }
        }

        public string Webappnote
        {
            get { return webappnote; }
            set { webappnote = value; }
        }


        public string Webapphtml
        {
            get { return webapphtml; }
            set { webapphtml = value; }
        }
        public string Webappscript
        {
            get { return webappscript; }
            set { webappscript = value; }
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