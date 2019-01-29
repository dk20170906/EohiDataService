using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EohiDataServerApi.Models
{
    [Serializable]
    public class AdminMenu : Entity
    {

        private int id;
        private string menucode;
        private string menuname;
        private string menuurl;
        private string parentcode;
        private int reorder;
        private int isuse;
        private int menulevel;
        private string menunote;
        private string cre_man;
        private DateTime cre_date;
        private string mod_man;
        private DateTime mod_date;

        private int childcount = 0;


        public List<AdminMenu> childMenus = new List<AdminMenu>();

        public int Childcount
        {
            get { return childMenus.Count; }
           
        }

        /// <summary>
        /// id
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        public string Menucode
        {
            get { return menucode; }
            set { menucode = value; }
        }

        public string Menuname
        {
            get { return menuname; }
            set { menuname = value; }
        }
       

        public string Menuurl
        {
            get { return menuurl; }
            set { menuurl = value; }
        }
        

        public string Parentcode
        {
            get { return parentcode; }
            set { parentcode = value; }
        }
        

        public int Reorder
        {
            get { return reorder; }
            set { reorder = value; }
        }
       

        public int Isuse
        {
            get { return isuse; }
            set { isuse = value; }
        }
       

        public int Menulevel
        {
            get { return menulevel; }
            set { menulevel = value; }
        }
       

        public string Menunote
        {
            get { return menunote; }
            set { menunote = value; }
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



       

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Cre_date
        {
            get { return cre_date; }
            set { cre_date = value; }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Cre_man
        {
            get { return cre_man; }
            set { cre_man = value; }
        }
    }
}
