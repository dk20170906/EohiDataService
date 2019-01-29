
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using log4net;

namespace EohiQuartzService
{
    ////窗体控制->委托
    //public delegate void ServiceHeplerHandler(string[] args);           //开始
    //public delegate void ServiceHeplerStopHandler();                            //停止
    //public delegate void ServiceHeplerShowTaskHandler(string url);                  //显示远程控制页面
    public partial class Service1 : ServiceBase
    {

        //定义Timer类
        static System.Timers.Timer timer;
        //定义委托
        public delegate void SetControlValue(string value);
        //日志
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        //public bool ServiceRunIsTrue { get; set; }    //服务运行状态
        //public  ServiceHeplerHandler ServiceStratHandler;
        //public  ServiceHeplerStopHandler ServiceStopHandler;
        //public ServiceHeplerShowTaskHandler ServiceShowTaskHandler;
        public Service1()
        {
            QuarztHelper.Start();
            //  ServiceRunIsTrue = QuarztHelper.ServiceRunIsTrue;
            //ServiceStratHandler += OnStart;
            //ServiceStopHandler += OnStop;
            //ServiceShowTaskHandler += ShowTaskManagers;
            InitializeComponent();
            string start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), "服务初始化；");
            log.Info(start);
        }

        protected override void OnStart(string[] args)
        {
            QuarztHelper.Start();
            //ServiceRunIsTrue = QuarztHelper.ServiceRunIsTrue;
            string start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), "服务启动，ONSTART");
            log.Info(start);


            ///3秒后有多少任务在执行
            InitTimer();
            timer.Start();
        }

        protected override void OnStop()
        {
            QuarztHelper.Stop();
            //ServiceRunIsTrue = QuarztHelper.ServiceRunIsTrue;
            string start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), "服务停止，ONSTOP");
            log.Info(start);
        }
        protected void ShowTaskManagers(string url)
        {
            System.Diagnostics.Process.Start(url);
            string start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), "跳转远程管理页面，CrystalQuartzPanel.acd");
            log.Info(start);
        }



        /// <summary>
        /// 初始化Timer控件
        /// </summary>
        private static void InitTimer()
        {
            //设置定时间隔(毫秒为单位)
            int interval = 3000;
            timer = new System.Timers.Timer(interval);
            //设置执行一次（false）还是一直执行(true)
            timer.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
            //绑定Elapsed事件
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerUp);
        }

        /// <summary>
        /// Timer类执行定时到点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void TimerUp(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                string start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), "》》》》》》》》》》》当前Quartz服务正在执行的任务总有" +"---------------------------------------"+ "条任务");
                log.Info(start);
                if (QuarztHelper.scheduler != null)
                {
                    IList<IJobExecutionContext> jobContexts = QuarztHelper.scheduler.GetCurrentlyExecutingJobs();
                    if (jobContexts != null && jobContexts.ToList().Count > 0)
                    {
                         start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), "》》》》》》》》》》》当前Quartz服务正在执行的任务总有" + jobContexts.Count + "条任务");
                        log.Info(start);
                    }
                }
            }
            catch (Exception ex)
            {
                //初始化log4net
                string start = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), "启动Quartz服务失败:" + ex.Message);
                log.Info(start);
            }
        }


    }
}
