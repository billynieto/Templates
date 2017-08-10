using System;
using System.Collections.Generic;

using Templates.Framework;

namespace Templates
{
    public class TemplateEnumeration : IEnumeration
    {
        protected string indent;
        protected IList<IEnumerationItem> items;
        protected string name;
        protected INamespace _namespace;
        protected string signature;

        public string Indent { get { return this.indent; } set { this.indent = value; } }
        public IList<IEnumerationItem> Items { get { return this.items; } set { this.items = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public INamespace Namespace { get { return this._namespace; } }
        public virtual string Signature { get { if (string.IsNullOrWhiteSpace(this.signature)) { this.signature = "public enum " + name; } return this.signature; } }

        public TemplateEnumeration(INamespace _namespace, string name)
        {
            this.indent = "\t";

            this.name = name;
            this._namespace = _namespace;

            this.items = new List<IEnumerationItem>();
        }

        public void Add(IEnumerationItem item)
        {
            this.items.Add(item);

            item.Enumeration = this;
        }

        public IList<ILine> ToLines()
        {
            IList<ILine> lines = new List<ILine>();

            lines.Add(new TemplateLine(Signature));
            lines.Add(new TemplateLine("{"));

            for (int i = 0; i < this.items.Count; i++)
            {
                string itemText = items[i].Name;

                if (!string.IsNullOrWhiteSpace(items[i].Value))
                    itemText += " = " + items[i].Value;

                lines.Add(new TemplateLine(this.indent + itemText + (i == this.items.Count - 1 ? string.Empty : ",")));
            }

            lines.Add(new TemplateLine("}"));

            return lines;
        }
    }

    public class TemplateEnumerationItem : IEnumerationItem
    {
        protected IEnumeration enumeration;
        protected string name;
        protected string value;

        public IEnumeration Enumeration { get { return this.enumeration; } set { this.enumeration = value; } }
        public string Name { get { return this.name; } }
        public string Value { get { return this.value; } }

        public TemplateEnumerationItem(string name)
            : this(name, null)
        {
        }

        public TemplateEnumerationItem(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
