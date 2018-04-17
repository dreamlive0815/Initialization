using System;
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

        public IEnumerator<Parameter> GetEnumerator()
        {
            return _parameters.Values.GetEnumerator();
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
                if (key != value.Key) throw new ArgumentException("所设置的键与Parameter的键名不一致");
                if (_parameters.ContainsKey(key)) {
                    _parameters[key] = value;
                } else {
                    Add(value);
                }
            }
        }

        public virtual void Add(Parameter parameter)
        {
            if (_parameters.ContainsKey(parameter.Key)) throw new ArgumentException("相同键名的Parameter已存在");
            _parameters.Add(parameter.Key, parameter);
        }

        public virtual bool Remove(string key)
        {
            return _parameters.Remove(key);
        }

        public bool Remove(Parameter parameter)
        {
            return Remove(parameter.Key);
        }
    }
}
