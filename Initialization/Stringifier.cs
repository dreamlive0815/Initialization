using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Initialization
{
    class Stringifier
    {
        protected Initialization _ini;

        public Stringifier(Initialization ini)
        {
            if(ini == null) throw new ArgumentNullException("ini对象不能为空");
            _ini = ini;
        }

        public bool WithComment { get; set; } = true;

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public SectionOrder SectionOrder { get; set; } = SectionOrder.Inherit;

        public ParameterOrder ParameterOrder { get; set; } = ParameterOrder.Inherit;

        public virtual string Stringify()
        {
            var sb = new StringBuilder();
            var nl  = Environment.NewLine;
            var _withComment = WithComment;

            if(SectionOrder == SectionOrder.Name || ParameterOrder == ParameterOrder.Key) _withComment = false;

            if(_withComment) {
                foreach(var comment in _ini.Comments) {
                    sb.Append(';');
                    sb.Append(comment.Content);
                    sb.Append(nl);
                }
            }

            IEnumerable<Section> sections = _ini.Sections;
            if(SectionOrder == SectionOrder.Name) sections = sections.OrderBy(i => i.Name);
            foreach(var section in sections) {

                sb.Append('['); sb.Append(section.Name); sb.Append(']');
                sb.Append(nl);

                if(ParameterOrder == ParameterOrder.Key) {
                    IEnumerable<Parameter> paramaters = section.Parameters;
                    paramaters = paramaters.OrderBy(i => i.Key);
                    foreach(var paramater in paramaters) {

                    }
                }

            }


            return sb.ToString();
        }



    }

    enum SectionOrder
    {
        Inherit,
        Name,
    }

    enum ParameterOrder
    {
        Inherit,
        Key,
    }
}