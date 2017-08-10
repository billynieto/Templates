using System;
using System.Collections.Generic;

using Templates.Framework;

namespace Templates
{
    public class TemplateConstructor : IConstructor
    {
        protected IList<IVariable> baseParameters;
        protected ICodeBlock codeBlock;
        protected IClass _class;
        protected string indent;
        protected IList<IVariable> parameters;
        protected Privacy privacy;
        protected IList<IVariable> thisParameters;

        public IList<IVariable> Base { get { return this.baseParameters; } set { this.baseParameters = value; } }
        public ICodeBlock CodeBlock { get { return this.codeBlock; } }
        public IClass Class { get { return this._class; } set { this._class = value; } }
        public string Indent { get { return this.indent; } set { this.indent = value; } }
        public IList<IVariable> Parameters { get { return this.parameters; } }
        public Privacy Privacy { get { return this.privacy; } }
        public IList<IVariable> This { get { return this.thisParameters; } set { this.thisParameters = value; } }

        public TemplateConstructor()
            : this(new List<IVariable>())
        {
        }

        public TemplateConstructor(IList<IVariable> parameters)
        {
            this.parameters = parameters;

            this.privacy = Privacy.Public;
            this.codeBlock = new TemplateCodeBlock();
            this.indent = "\t";
        }

        public TemplateConstructor(IList<IVariable> parameters, IList<IVariable> secondaryParameters)
            : this(Privacy.Public, parameters, secondaryParameters)
        {
        }

        public TemplateConstructor(Privacy privacy, IList<IVariable> parameters, IList<IVariable> secondaryParameters)
        {
            this.privacy = privacy;

            this.codeBlock = new TemplateCodeBlock();
            this.indent = "\t";
            this.parameters = new List<IVariable>();

            SortedList<string, IVariable> sortedParameters = new SortedList<string, IVariable>();
            foreach (IVariable parameter in parameters)
                sortedParameters.Add(parameter.InstanceName, parameter);

            foreach (IVariable parameter in sortedParameters.Values)
                this.parameters.Add(parameter);

            if (secondaryParameters != null && secondaryParameters.Count > 0)
            {
                sortedParameters.Clear();
                foreach (IVariable secondaryParameter in secondaryParameters)
                    sortedParameters.Add(secondaryParameter.InstanceName, secondaryParameter);

                foreach (IVariable parameter in sortedParameters.Values)
                    this.parameters.Add(parameter);
            }
        }

        public void Add()
        {
            this.codeBlock.Add(string.Empty);
        }

        public void Add(string code)
        {
            if (code == null)
                throw new ArgumentNullException("Code");

            this.codeBlock.Add(code);
        }

        public void Validate()
        {
            if (this.baseParameters != null && this.thisParameters != null)
                throw new InvalidOperationException("Constructors must either only use the ' : base()' or ' : this()' calling feature to reference other constructores.");

            IList<IVariable> temp = null;
            if(this.baseParameters != null)
                temp =  new List<IVariable>(this.baseParameters);
            else if(this.thisParameters != null)
                temp = new List<IVariable>(this.thisParameters);

            if (temp != null)
            {
                foreach (IVariable parameter in temp)
                {
                    bool found = false;
                    foreach(IVariable passedInParameter in this.parameters)
                    {
                        if(parameter.VariableType.Name == passedInParameter.VariableType.Name)
                        {
                            if(parameter.VariableType != passedInParameter.VariableType)
                                throw new InvalidOperationException("Parameter " + parameter.VariableType.Name + " does not share a common Type with the parameter of the same name that was passed to the constructor. (" + parameter.VariableType + " != " + passedInParameter.VariableType + ")");

                                found = true;

                                break;
                        }
                    }

                    if(!found)
                        throw new InvalidOperationException("Parameter " + parameter.VariableType.Name + " that is being passed to another constructor was not found in the list of parameters it was passed.");
                }
            }
        }

        public virtual IList<ILine> ToLines()
        {
            IList<ILine> constructorLines = new List<ILine>();

            constructorLines.Add(new TemplateLine("public " + this._class.Name + "(" + TemplatesHelper.ListOutParameters(this.parameters) + ")"));

            if (this.baseParameters != null)
                constructorLines.Add(new TemplateLine("\t: base(" + TemplatesHelper.ListOutVariables(this.baseParameters) + ")"));
            else if(this.thisParameters != null)
                constructorLines.Add(new TemplateLine("\t: this(" + TemplatesHelper.ListOutVariables(this.thisParameters) + ")"));

            constructorLines.Add(new TemplateLine("{"));

            foreach (ILine line in this.codeBlock.Lines)
                constructorLines.Add(new TemplateLine(this.indent + line.Code));

            constructorLines.Add(new TemplateLine("}"));

            return constructorLines;
        }
    }
}
