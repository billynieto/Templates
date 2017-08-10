using System;
using System.Collections.Generic;

using Templates.Framework;

namespace Templates
{
    public class TemplateFunction : IFunction
    {
        protected ICodeBlock codeBlock;
        protected IList<IConstructor> constructors;
        protected string indent;
        protected bool isCommentedOut;
        protected bool isNew;
        protected string name;
        protected Overridability overridability;
        protected IList<IVariable> parameters;
        protected Privacy privacy;
        protected IVariableType returnType;
        protected string signature;
        protected string sortBy;
        protected string summary;
        protected IList<ITag> tags;

        public ICodeBlock CodeBlock { get { return this.codeBlock; } }
        public IList<IConstructor> Constructors { get { return this.constructors; } set { this.constructors = value; } }
        public string Indent { get { return this.indent; } set { this.indent = value; } }
        public bool IsCommentedOut { get { return this.isCommentedOut; } set { this.isCommentedOut = value; } }
        public bool IsNew { get { return this.isNew; } set { this.isNew = value; } }
        public string Name { get { return this.name; } }
        public Overridability Overridability { get { return this.overridability; } }
        public IList<IVariable> Parameters { get { return this.parameters; } }
        public Privacy Privacy { get { return this.privacy; } }
        public IVariableType ReturnType { get { return this.returnType; } }
        public string Summary { get { return this.summary; } set { this.summary = value; } }
        public IList<ITag> Tags { get { return this.tags; } }

        public string Signature
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.signature))
                {
                    this.signature = this.returnType + " " + this.name + "(";

                    for (int i = 0; i < this.parameters.Count; i++)
                    {
                        if (i > 0)
                            this.signature += ", ";

                        this.signature += this.parameters[i].VariableType + " " + this.parameters[i].InstanceName;
                    }

                    this.signature += ")";
                }

                return this.signature;
            }
        }

        public string SortBy
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.sortBy))
                {
                    this.sortBy = this.name + ": ";

                    for (int i = 0; i < this.parameters.Count; i++)
                    {
                        if (i > 0)
                            this.sortBy += ", ";

                        this.sortBy += this.parameters[i].VariableType.Name + " " + this.parameters[i].InstanceName;
                    }
                }

                return this.sortBy;
            }
        }

        public TemplateFunction(Privacy privacy, string returnType, string name)
            : this(privacy, Overridability.None, new TemplateVariableType(returnType), name, new List<IVariable>())
        {
        }

        public TemplateFunction(Privacy privacy, IVariableType returnType, string name)
            : this(privacy, Overridability.None, returnType, name, new List<IVariable>())
        {
        }

        public TemplateFunction(Privacy privacy, string returnType, string name, IList<IVariable> parameters)
            : this(privacy, Overridability.None, new TemplateVariableType(returnType), name, parameters)
        {
        }

        public TemplateFunction(Privacy privacy, IVariableType returnType, string name, IList<IVariable> parameters)
            : this(privacy, Overridability.None, returnType, name, parameters)
        {
        }

        public TemplateFunction(Privacy privacy, Overridability overridability, string returnType, string name)
            : this(privacy, overridability, new TemplateVariableType(returnType), name, new List<IVariable>())
        {
        }

        public TemplateFunction(Privacy privacy, Overridability overridability, IVariableType returnType, string name)
            : this(privacy, overridability, returnType, name, new List<IVariable>())
        {
        }

        public TemplateFunction(Privacy privacy, Overridability overridability, string returnType, string name, IList<IVariable> parameters)
            : this(privacy, overridability, new TemplateVariableType(returnType), name, parameters)
        {
        }

        public TemplateFunction(Privacy privacy, Overridability overridability, IVariableType returnType, string name, IList<IVariable> parameters)
        {
            this.overridability = overridability;
            this.name = name;
            this.parameters = parameters;
            this.privacy = privacy;
            this.returnType = returnType;

            this.codeBlock = new TemplateCodeBlock();
            this.indent = "\t";
            this.tags = new List<ITag>();
        }

        public void Add()
        {
            this.codeBlock.Add(string.Empty);
        }

        public void Add(string code)
        {
            this.codeBlock.Add(code);
        }

        public void Tag(string tag)
        {
            this.tags.Add(new TemplateTag(tag));
        }

        public virtual IList<ILine> ToLines()
        {
            IList<ILine> functionLines = new List<ILine>();

            foreach (ITag tag in this.tags)
                functionLines.Add(new TemplateLine(tag));

            string temp = string.Empty;
            if (this.privacy == Privacy.Public)
                temp += "public";
            else if (this.privacy == Privacy.Protected)
                temp += "protected";
            else if (this.privacy == Privacy.Private)
                temp += "private";

            if (this.isNew)
                temp += " new";

            if (this.overridability != Overridability.None)
            {
                temp += " ";

                if (this.overridability == Overridability.Overridable)
                    temp += "virtual";
                else if (this.overridability == Overridability.Overriding)
                    temp += "override";
                else
                    temp += "static";
            }

            temp += " " + Signature;

            string prefix = this.isCommentedOut ? "//" : string.Empty;

            functionLines.Add(new TemplateLine(prefix + temp));
            functionLines.Add(new TemplateLine(prefix + "{"));

            foreach (ILine line in this.codeBlock.Lines)
                functionLines.Add(new TemplateLine(prefix + this.indent + line.Code));

            functionLines.Add(new TemplateLine(prefix + "}"));
            
            return functionLines;
        }
    }
}
