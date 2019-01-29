using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EohiDataCenter
{
    public class ParameterManager
    {
        public ParameterManager()
        {
        }

        public Dictionary<string, Parameter> _dic = new Dictionary<string, Parameter>();

        public void Add(string key, object value)
        {
            Parameter par = new Parameter();
            par.parname = key;
            par.value = value;
          
            _dic.Add(key, par);
        }

        public object Get(string key)
        {
            if (_dic.ContainsKey(key))
            {
                Parameter par = _dic[key];
                if (par == null)
                    return null;

                return par.value;
            }
            return "";
        }


    }
}
