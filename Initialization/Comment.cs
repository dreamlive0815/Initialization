
namespace Initialization
{
    class Comment : IPositionable
    {
        protected string _content;

        protected int _line;
        protected int _offset;

        public Comment(string content)
        {
            _content = content;
        }

        public Comment(string content, int line, int offset) : this(content)
        {
            _line = line;
            _offset = offset;
        }

        public string Content
        {
            get { return _content; }
            set { _content = value; }
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
            return string.Format(";{0}", _content);
        }

    }
}
