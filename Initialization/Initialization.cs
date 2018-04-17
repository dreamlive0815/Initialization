using System;
using System.Collections;
using System.Collections.Generic;

namespace Initialization
{
    class Initialization : IEnumerable<Section>
    {
        protected SectionCollection _sections;
        protected List<Comment> _comments;

        public Initialization()
        {
            _sections = new SectionCollection();
            _comments = new List<Comment>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Sections.GetEnumerator();
        }

        public IEnumerator<Section> GetEnumerator()
        {
            return Sections.GetEnumerator();
        }

        public string FileName { get; set; }

        public SectionCollection Sections
        {
            get { return _sections; }
            set { _sections = value; }
        }

        public List<Comment> Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }
    }
}
