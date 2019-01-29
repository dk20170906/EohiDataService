using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EohiDataServerApi.Models
{
    public class UpdateFile
    {


        int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        string filename;

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        string versionno;

        public string Versionno
        {
            get { return versionno; }
            set { versionno = value; }
        }
        int filesize;

        public int Filesize
        {
            get { return filesize; }
            set { filesize = value; }
        }
        string savedir;

        public string Savedir
        {
            get { return savedir; }
            set { savedir = value; }
        }
        DateTime uptime;

        public DateTime Uptime
        {
            get { return uptime; }
            set { uptime = value; }
        }
    }
}