using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Scripting.Hosting;
namespace WFServerWeb
{
    public class CScriptOpe
    {
        private static ScriptEngine engine = null;
        private static ScriptEngine CreateScriptEngine()
        {
            try
            {
                if (engine == null)
                {
                    Console.Write("创建 ScriptEngine ");
                    engine = IronPython.Hosting.Python.CreateEngine();
                }
                return engine;
            }
            catch(Exception ex)
            {
                CLog.PutDownErrInfo("创建脚本引擎操作异常。");
                throw ex;
            }
        }

        /// <summary>
        /// 运行Python脚本
        /// </summary>
        /// <param name="scrpitTxt"></param>
        /// <param name="InstanceID"></param>
        /// <returns></returns>
        public static object ScriptExec(string scrpitTxt, string InstanceID)
        {
            try
            {
                //创建一个IpyRunTime，需要2-3秒时间。建议进入全局时加载，此为演示
                var engine = CreateScriptEngine();// IronPython.Hosting.Python.CreateEngine();
                var code = engine.CreateScriptSourceFromString(scrpitTxt);

                //设置参数;
                ScriptScope scope = engine.CreateScope();
                CFuncForPython cffp = new CFuncForPython();
                scope.SetVariable("wfhost", cffp);
                scope.SetVariable("InstanceID", InstanceID);

                var actual = code.Execute<object>(scope);

                return actual;
            }
            catch (Exception ex)
            {
                CLog.PutDownErrInfo("脚本执行异常。实例ID：" + InstanceID + "，异常信息：" + ex.Message.ToString());
                throw ex;
            }
        }
    }
}