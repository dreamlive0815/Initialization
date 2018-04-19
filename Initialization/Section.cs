using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Initialization
{
    class Section : IPositionable
    {
        protected string _name;
        protected ParameterCollection _parameters;
        protected List<Comment> _comments;

        protected int _line;
        protected int _offset;

        public Section(string name)
        {
            _name = name;
            _parameters = new ParameterCollection();
            _comments = new List<Comment>();
        }

        public Section(string name, int line, int offset) : this(name)
        {
            _line = line;
            _offset = offset;
        }

        public Parameter this[string key]
        {
            get { return _parameters[key]; }
            set { _parameters[key] = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public ParameterCollection Parameters
        {
            get { return _parameters; }
            set {
                if(_parameters == null) throw new ArgumentNullException("Parameters不能为null");
                _parameters = value;
            }
        }

        public List<Comment> Comments
        {
            get { return _comments; }
            set {
                if(_comments == null) throw new ArgumentNullException("Comments不能为null");
                _comments = value;
            }
        }

        public virtual int Line
        {
            get { return _line; }
        }
        public virtual int Offset
        {
            get { return _offset; }
        }

        public override string ToString()
        {
            return ToString(true);
        }

        protected virtual IOrderedEnumerable<IPositionable> OrderBy(IEnumerable<IPositionable> items)
        {
            return items.OrderBy(i => i.Line);
        }

        public virtual string ToString(bool withComment)
        {
            var sb = new StringBuilder();
            var nl = Environment.NewLine;
            sb.Append('['); sb.Append(_name); sb.Append(']');
            sb.Append(nl);
            var list = _parameters.Cast<IPositionable>().ToList();
            if (withComment) list.AddRange(_comments);
            var items = OrderBy(list).ToList();
            foreach(var item in items)
            {
                sb.Append(item.ToString(withComment));
                sb.Append(nl);
            }
            return sb.ToString();
        }
    }
}
