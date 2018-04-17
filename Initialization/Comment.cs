
namespace Initialization
{
    class Comment : IPositionable
    {
        protected string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public int Line { get; }
        public int Offset { get; }

    }
}
