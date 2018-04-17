using System;
using System.Collections.Generic;
using System.Linq;

namespace Initialization
{
    class Section
    {
        protected string _name;

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }

    class SectionCollection
    {
        protected Dictionary<string, Section> _sections;

        public SectionCollection()
        {
            _sections = new Dictionary<string, Section>();
        }

        public virtual IEnumerator<Section> GetEnumerator()
        {
            return _sections.Values.GetEnumerator();
        }

        public virtual void Add(Section section)
        {

        }

        public virtual void Remove(Section section)
        {

        }
    }
}
