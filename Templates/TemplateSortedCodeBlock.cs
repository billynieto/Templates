using System;
using System.Collections.Generic;

using Templates.Framework;

namespace Templates
{
    public class TemplateSortedCodeBlock : ISortedCodeBlock
    {
        protected string indent;
        protected SortedList<string, ILine> lines;

        public string Indent { get { return this.indent; } set { this.indent = value; } }
        public IEnumerable<ILine> Lines { get { return this.lines.Values; } }

        public TemplateSortedCodeBlock()
        {
            this.indent = "\t";
            this.lines = new SortedList<string, ILine>();
        }

        public void Add()
        {
            Add(string.Empty);
        }

        public void Add(string code)
        {
            string lastKey = this.lines.Keys[this.lines.Keys.Count];

            this.lines[lastKey] = new TemplateLine(code);
        }

        public void Add(string sortBy, string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException("Code");

            this.lines[sortBy] = new TemplateLine(code);
        }

        public virtual IList<ILine> ToLines()
        {
            throw new NotImplementedException();
        }
    }
}
