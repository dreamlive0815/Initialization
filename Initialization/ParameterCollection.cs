using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ParameterContainer = System.Collections.Generic.Dictionary<string, Initialization.Parameter>;

namespace Initialization
{
    class ParameterCollection : IEnumerable<Parameter>
    {
        protected ParameterContainer _parameters;

        public ParameterCollection()
        {
            _parameters = new ParameterContainer();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual IEnumerator<Parameter> GetEnumerator()
        {
            return _parameters.Values.GetEnumerator();
        }

        public virtual Parameter GetNewParameter(string key, string value, int line, int offset)
        {
            return new Parameter(key, value, line, offset);
        }

        public Parameter this[string key]
        {
            get {
                if (_parameters.ContainsKey(key)) {
                    return _parameters[key];
                } else {
                    return null;
                }
            }
            set {
                if (value == null) throw new ArgumentNullException("Parameter不能为null");
                if (key != value.Key) throw new ArgumentException("所设置的键与Parameter的键名不一致");
                if (_parameters.ContainsKey(key)) {
                    _parameters[key] = value;
                } else {
                    Add(value);
                }
            }
        }

        public virtual Parameter Add(Parameter parameter)
        {
            if (parameter == null) throw new ArgumentNullException("Parameter不能为null");
            if (_parameters.ContainsKey(parameter.Key)) throw new ArgumentException(string.Format("相同键名的Parameter已存在:{0}", parameter.Key));
            _parameters.Add(parameter.Key, parameter);
            return parameter;
        }

        public virtual bool Remove(string key)
        {
            return _parameters.Remove(key);
        }

        public bool Remove(Parameter parameter)
        {
            if (parameter == null) throw new ArgumentNullException("parameter不能为null");
            return Remove(parameter.Key);
        }
    }
}
