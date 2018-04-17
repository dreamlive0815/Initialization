using System;
using System.Collections;
using System.Collections.Generic;

namespace Initialization
{
    class Section : IPositionable
    {
        protected string _name;
        protected ParameterCollection _parameters;
        protected List<Comment> _comments;

        public Section(string name)
        {
            _name = name;
            _parameters = new ParameterCollection();
            _comments = new List<Comment>();
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public ParameterCollection Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        public List<Comment> Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        public int Line { get; }
        public int Offset { get; }
    }
}
