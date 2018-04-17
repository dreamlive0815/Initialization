using System;
using System.Collections.Generic;

namespace Initialization
{
    class Parameter
    {
        protected string _key;
        protected string _value;

        public virtual string Key 
        {
            get { return _key; }
            set { _key = value; }
        }

        public virtual string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
