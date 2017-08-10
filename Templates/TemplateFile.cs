using System;
using System.Collections.Generic;

using Templates.Framework;

namespace Templates
{
    public class TemplateFile : IFile
    {
        protected Queue<IComponent> components; //Try to come up with a better name, Geesh!
        protected SortedList<string, INamespace> currentUsingStatementBlock;
        protected string extension;
        protected string indent;
        protected string name;
        protected INamespace _namespace;
        protected IList<SortedList<string, INamespace>> usingStatementBlocks;

        public Queue<IComponent> Components { get { return this.components; } set { this.components = value; } }
        public string Extension { get { return this.extension; } }
        public string Indent { get { return this.indent; } set { this.indent = value; } }
        public string Name { get { return this.name; } }
        public INamespace Namespace { get { return this._namespace; } }
        public IList<SortedList<string, INamespace>> UsingStatementBlocks { get { return this.usingStatementBlocks; } set { this.usingStatementBlocks = value; } }

        public TemplateFile(INamespace _namespace, string name, string extension)
        {
            this.extension = extension;
            if (this.extension == null)
                throw new ArgumentNullException("Extension");

            this._namespace = _namespace;
            if (this._namespace == null)
                throw new ArgumentNullException("Namespace");

            this.components = new Queue<IComponent>();
            this.indent = "\t";
            this.name = name;
            this.usingStatementBlocks = new List<SortedList<string, INamespace>>();

            NewUsingStatementBlock();
        }

        public void Uses(INamespace _namespace)
        {
            if (this.currentUsingStatementBlock != null && this.currentUsingStatementBlock.Count == 0)
                this.usingStatementBlocks.Add(this.currentUsingStatementBlock);

            this.currentUsingStatementBlock.Add(_namespace.Name, _namespace);
        }

        public void NewUsingStatementBlock()
        {
            this.currentUsingStatementBlock = new SortedList<string, INamespace>();
        }

        public virtual IList<ILine> ToLines()
        {
            IList<ILine> lines = new List<ILine>();

            foreach (SortedList<string, INamespace> usingStatementBlock in this.usingStatementBlocks)
            {
                foreach (INamespace _namespace in usingStatementBlock.Values)
                    lines.Add(new TemplateLine("using " + _namespace.Name + ";"));

                lines.Add(new TemplateLine());
            }

            lines.Add(new TemplateLine("namespace " + this._namespace.Name));
            lines.Add(new TemplateLine("{"));

            int i = 0;
            while(this.components.Count > 0)
            {
                if (i++ > 0)
                    lines.Add(new TemplateLine());

                foreach (ILine nestedLine in components.Dequeue().ToLines())
                    lines.Add(new TemplateLine(this.indent + nestedLine.Code));
            }

            lines.Add(new TemplateLine("}"));

            return lines;
        }
    }
}
