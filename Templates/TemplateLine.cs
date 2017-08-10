using System;

using Templates.Framework;

namespace Templates
{
    public class TemplateLine : ILine
    {
        protected string code;

        public string Code { get { return this.code; } }

        public TemplateLine()
            : this(string.Empty)
        {
        }

        public TemplateLine(string code)
        {
            this.code = code;
        }

        public TemplateLine(ITag tag)
        {
            this.code = tag.Text;
        }
    }
}
