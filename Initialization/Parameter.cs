
namespace Initialization
{
    class Parameter : IPositionable
    {
        protected string _key;
        protected string _value;

        public Parameter(string key) : this(key, null)
        {
        }

        public Parameter(string key, string value)
        {
            _key = key;
            _value = value;
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

        public Comment Comment { get; set; }

        public int Line { get; }
        public int Offset { get; }
    }
}
