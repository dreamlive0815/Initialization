using System;
using System.Text;

namespace Initialization
{
    class Writer
    {
        protected Initialization _ini;
        protected Encoding _encoding;
        protected bool _withComment;

        public Writer(Initialization ini)
        {
            if(ini == null) throw new ArgumentNullException("ini对象不能为空");
            _ini = ini;
        }

        public bool WithComment
        {
            get { return _withComment; }
            set { _withComment = value; }
        }

        public Encoding Encoding
        {
            get { return _encoding; }
            set { _encoding = value; }
        }

        public virtual string Parse()
        {
            return "";
        }



    }
}