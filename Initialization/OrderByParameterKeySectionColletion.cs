using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Initialization
{
    class OrderByParameterKeySectionColletion : SectionCollection
    {
        public override Section GetNewSection(string name, int line, int offset)
        {
            return new OrderByParameterKeySection(name, line, offset);
        }
    }

    class OrderByParameterKeySection : Section
    {
        public OrderByParameterKeySection(string name) : base(name)
        {
        }

        public OrderByParameterKeySection(string name, int line, int offset) : base(name, line, offset)
        {
        }

        public override string ToString(bool withComment)
        {
            var sb = new StringBuilder();
            var nl = Environment.NewLine;
            sb.Append('['); sb.Append(_name); sb.Append(']');
            sb.Append(nl);
            var list = _parameters.OrderBy(i => i.Key);
            var lineList = list.Select(i => i.Line).OrderBy(i => i);
            foreach(var item in list)
            {
                if(withComment) {
                    var preLines = lineList.Where(i => i < item.Line);
                    var preId = preLines.Count() > 0 ? preLines.Max() : 0;
                    var comments = _comments.Where(i => i.Line > preId && i.Line < item.Line);
                    foreach(var comment in comments) {
                        sb.Append(comment.ToString(withComment));
                        sb.Append(nl);
                    }
                }
                sb.Append(item.ToString(withComment));
                sb.Append(nl);
            }
            if(withComment) {
                var maxLine = lineList.Count() > 0 ? lineList.Max() : 0;
                var lastComments = _comments.Where(i => i.Line > maxLine);
                foreach(var comment in lastComments) {
                    sb.Append(comment.ToString(withComment));
                    sb.Append(nl);
                }
            }

            return sb.ToString();
        }
    }
    
}