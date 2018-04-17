using System;
using System.Collections.Generic;
using System.Linq;

namespace Initialization
{
    class Initialization
    {
        public Initialization()
        {
            Sections = new SectionCollection();
        }

        public IEnumerator<Section> GetEnumerator()
        {
            return Sections.GetEnumerator();
        }

        public SectionCollection Sections { get; set; }
    }
}
