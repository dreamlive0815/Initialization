
namespace Initialization
{
    class Parameter : IPositionable
    {
        protected string _key;
        protected string _value;
        protected Comment _comment;

        protected int _line;
        protected int _offset;

        public Parameter(string key, string value)
        {
            _key = key;
            _value = value;
        }

        public Parameter(string key, string value, int line, int offset) : this(key, value)
        {
            _line = line;
            _offset = offset;
        }

        public string Key 
        {
            get { return _key; }
            set { _key = value; }
        }

        public virtual string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Comment Comment
        {
            get { return _comment; }
            set { _comment = value; }
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

        public virtual string ToString(bool withComment)
        {
            withComment = withComment && (Comment != null);
            return string.Format("{0}={1}{3}{2}", Key, Value, withComment ? Comment.ToString() : null, withComment ? "\t" : "");
        }
    }
}
