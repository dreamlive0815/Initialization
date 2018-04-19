using System;
using System.Collections;
using System.Collections.Generic;
using SectionContainer = System.Collections.Generic.Dictionary<string, Initialization.Section>;

namespace Initialization
{
    class SectionCollection : IEnumerable<Section>
    {
        protected SectionContainer _sections;

        public SectionCollection()
        {
            _sections = new SectionContainer();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual IEnumerator<Section> GetEnumerator()
        {
            return _sections.Values.GetEnumerator();
        }

        public virtual Section GetNewSection(string name, int line, int offset)
        {
            return new Section(name, line, offset);
        }

        public Section this[string name]
        {
            get {
                if (_sections.ContainsKey(name)) {
                    return _sections[name];
                } else {
                    return null;
                }
            }
            set {
                if (name != value.Name) throw new ArgumentException("所设置的键与Section的名称不一致");
                if (_sections.ContainsKey(name)) {
                    _sections[name] = value;
                } else {
                    Add(value);
                }
            }
        }

        public virtual Section Add(Section section)
        {
            if (section == null) throw new ArgumentNullException("section不能为null");
            if (_sections.ContainsKey(section.Name)) throw new ArgumentException(string.Format("同名的Section已存在:{0}", section.Name));
            _sections.Add(section.Name, section);
            return section;
        }

        public virtual bool Remove(string key)
        {
            return _sections.Remove(key);
        }

        public bool Remove(Section section)
        {
            if (section == null) throw new ArgumentNullException("section不能为null");
            return Remove(section.Name);
        }
    }
}
