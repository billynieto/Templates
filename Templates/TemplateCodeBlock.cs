using System;
using System.Collections.Generic;

using Templates.Framework;

namespace Templates
{
    public class TemplateCodeBlock : ICodeBlock
    {
        protected string indent;
        protected IList<ILine> lines;

        public string Indent { get { return this.indent; } set { this.indent = value; } }
        public IEnumerable<ILine> Lines { get { return this.lines; } }

        public TemplateCodeBlock()
        {
            this.indent = "\t";
            this.lines = new List<ILine>();
        }

        public void Add()
        {
            Add(string.Empty);
        }

        public void Add(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                this.lines.Add(new TemplateLine());
            else
                this.lines.Add(new TemplateLine(code));
        }

        public virtual IList<ILine> ToLines()
        {
            throw new NotImplementedException();
        }
    }
}
