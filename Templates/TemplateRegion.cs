using System;
using System.Collections.Generic;

using Templates.Framework;

namespace Templates
{
    public class TemplateRegion : IRegion
    {
        protected string indent;
        protected string name;
        protected SortedList<string, IFunction> functions;

        public string Indent { get { return this.indent; } set { this.indent = value; } }
        public string Name { get { return this.name; } }
        public SortedList<string, IFunction> Functions { get { return this.functions; } }

        public TemplateRegion()
            : this(null)
        {
        }

        public TemplateRegion(string name)
        {
            this.name = name;

            this.functions = new SortedList<string, IFunction>();
        }

        public void Add(IFunction function)
        {
            if (function == null)
                throw new ArgumentNullException("Function");
            if (function.Name == null)
                throw new ArgumentNullException("Function Name");

            this.functions.Add(function.SortBy, function);
        }

        public virtual IList<ILine> ToLines()
        {
            IList<ILine> regionLines = new List<ILine>();

            IList<ILine> functionLines = ListFunctions();

            if (!string.IsNullOrWhiteSpace(this.name))
            {
                regionLines.Add(new TemplateLine("#region " + this.name));
                regionLines.Add(new TemplateLine());
            }

            foreach (ILine functionLine in functionLines)
                regionLines.Add(functionLine);

            if (!string.IsNullOrWhiteSpace(this.name))
            {
                if (this.functions.Count > 0)
                    regionLines.Add(new TemplateLine());

                regionLines.Add(new TemplateLine("#endregion " + this.name));
            }

            return regionLines;
        }

        private IList<ILine> ListFunctions()
        {
            IList<ILine> functionLines = new List<ILine>();

            foreach(IFunction function in this.functions.Values)
            {
                if (functionLines.Count > 0)
                    functionLines.Add(new TemplateLine());

                if (!string.IsNullOrWhiteSpace(function.Summary))
                {
                    functionLines.Add(new TemplateLine("/// <summary>"));
                    functionLines.Add(new TemplateLine("/// " + function.Summary));
                    functionLines.Add(new TemplateLine("/// </summary>"));
                    foreach (IVariable parameter in function.Parameters)
                        functionLines.Add(new TemplateLine("/// </param name=\"" + parameter.VariableType.Name + "\">" + parameter.Summary + "</param>"));
                }

                foreach (ILine functionLine in function.ToLines())
                    functionLines.Add(functionLine);
            }

            return functionLines;
        }
    }
}
