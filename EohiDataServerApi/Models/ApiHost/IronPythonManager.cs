using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EohiDataServerApi
{
    public class IronPythonManager
    {
        private static ScriptEngine engine=null;
        public IronPythonManager()
        {
          
        }

        public static ScriptEngine CreateScriptEngine()
        {
            if(engine==null)
            {
                Console.Write("创建 ScriptEngine ");
                engine = IronPython.Hosting.Python.CreateEngine();
            }
            return engine;
        }

        public static object ScriptExec(ApiHost runtimeHost, string scrpitTxt)
        {
            //if (runtimeHost._isDesign)
            //{
            //    runtimeHost.printout(scrpitTxt);
            //}
            //创建一个IpyRunTime，需要2-3秒时间。建议进入全局时加载，此为演示
            var engine = CreateScriptEngine();// IronPython.Hosting.Python.CreateEngine();
            var code = engine.CreateScriptSourceFromString(scrpitTxt);

            //设置参数;
            ScriptScope scope = engine.CreateScope();
            scope.SetVariable("apihost", runtimeHost);

            var actual = code.Execute<object>(scope);
           
            
            return actual;
        }
    }
}
