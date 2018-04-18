
namespace Initialization
{
    class Comment : IPositionable
    {
        protected string _content;

        public Comment(string content)
        {
            _content = content;
        }

        public Comment(string content, int line, int offset) : this(content)
        {
            Line = line;
            Offset = offset;
        }

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public int Line { get; }
        public int Offset { get; }

        public override string ToString()
        {
            return ToString(true);
        }

        public string ToString(bool withComment)
        {
            return string.Format(";{0}", _content);
        }

    }
}
