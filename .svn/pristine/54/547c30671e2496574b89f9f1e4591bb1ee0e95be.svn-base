using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using log4net;

namespace EohiQuartzService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {

            string assemblyFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string assemblyDirPath = System.IO.Path.GetDirectoryName(assemblyFilePath);
            string configFilePath = assemblyDirPath + "\\log4net.config";
            log4net.Config.DOMConfigurator.ConfigureAndWatch(new System.IO.FileInfo(configFilePath));

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Service1() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
