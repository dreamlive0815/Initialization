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

        public IEnumerator<Section> GetEnumerator()
        {
            return _sections.Values.GetEnumerator();
        }

        public Section this[string key]
        {
            get {
                if (_sections.ContainsKey(key)) {
                    return _sections[key];
                } else {
                    return null;
                }
            }
            set {
                if (key != value.Name) throw new ArgumentException("所设置的键与Section的名称不一致");
                if (_sections.ContainsKey(key)) {
                    _sections[key] = value;
                } else {
                    Add(value);
                }
            }
        }

        public virtual void Add(Section section)
        {
            if (section == null) throw new ArgumentNullException("section不能为null");
            if (_sections.ContainsKey(section.Name)) throw new ArgumentException(string.Format("同名的Section已存在:{0}", section.Name));
            _sections.Add(section.Name, section);
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
