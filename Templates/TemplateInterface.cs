using System;
using System.Collections.Generic;

using Templates.Framework;

namespace Templates
{
    public class TemplateInterface : IInterface
    {
        public static IInterface Disposable = new TemplateInterface(TemplateNamespace.System, "IDisposable");
        public static IInterface DbTransaction = new TemplateInterface(TemplateNamespace.System_Data, "IDbTransaction");
        
        protected IList<IInterface> implements;
        protected bool isPartial;
        protected string indent;
        protected SortedList<string, IFunction> methods;
        protected string name;
        protected INamespace _namespace;
        protected SortedList<string, IProperty> properties;
        protected string signature;
        protected string summary;
        protected string type;

        public IEnumerable<IInterface> Implements { get { return this.implements; } }
        public bool IsPartial { get { return this.isPartial; } set { this.isPartial = value; } }
        public string Indent { get { return this.indent; } set { this.indent = value; } }
        public IEnumerable<IFunction> Methods { get { return this.methods.Values; }  }
        public virtual string Name { get { return this.name; } }
        public virtual INamespace Namespace { get { return this._namespace; } }
        public IEnumerable<IProperty> Properties { get { return this.properties.Values; } }
        public string Summary { get { return this.summary; } set { this.summary = value; } }
        public virtual string Type { get { if (this.type == null) this.type = Name; return this.type; } }

        public virtual string Signature
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.signature))
                {
                    this.signature = "public ";

                    if (this.isPartial)
                        this.signature += "partial ";
                    
                    this.signature += "interface " + Type;

                    if (this.implements.Count > 0)
                    {
                        this.signature += " : ";
                        
                        for (int i = 0; i < this.implements.Count; i++)
                        {
                            if (i > 0)
                                this.signature += ", ";

                            this.signature += this.implements[i].Type;
                        }
                    }
                }

                return this.signature;
            }
        }

        public TemplateInterface(INamespace _namespace, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name");

            this.name = name;
            this._namespace = _namespace;

            this.implements = new List<IInterface>();
            this.indent = "\t";
            this.methods = new SortedList<string, IFunction>();
            this.properties = new SortedList<string, IProperty>();
        }

        public void Add(IFunction function)
        {
            if (function == null)
                throw new ArgumentNullException("Function");

            //TODO: This is a temporary fix.  Sometimes the methods are being duplicated erroneously, most likely caused by Microsoft coding errors.
            //TODO: This needs to be changed to throw a proper error if the key is already established.
            if (!this.methods.Keys.Contains(function.SortBy))
                this.methods[function.SortBy] = function;
        }

        public void Add(IVariable variable)
        {
            Add(variable, true, true);
        }

        public void Add(IVariable variable, bool canGet, bool canSet)
        {
            this.properties[variable.Reference] = new TemplateProperty(variable, canGet, canSet);
        }

        public void WillImplement(IInterface interfaceToImplement)
        {
            if (this.implements.Contains(interfaceToImplement))
                throw new ArgumentException(interfaceToImplement.Name + " Interface is already implemented by " + this.name);

            this.implements.Add(interfaceToImplement);
        }

        public virtual IList<ILine> ToLines()
        {
            IList<ILine> interfaceLines = new List<ILine>();

            if (!string.IsNullOrWhiteSpace(this.summary))
            {
                interfaceLines.Add(new TemplateLine("/// <summary>"));
                interfaceLines.Add(new TemplateLine("/// " + this.summary));
                interfaceLines.Add(new TemplateLine("/// </summary>"));
            }

            interfaceLines.Add(new TemplateLine(Signature));
            interfaceLines.Add(new TemplateLine("{"));

            IList<ILine> propertyLines = ListProperties();
            IList<ILine> methodLines = ListMethods();

            foreach (ILine propertyLine in propertyLines)
                interfaceLines.Add(new TemplateLine(this.indent + propertyLine.Code));

            if (propertyLines.Count > 0 && methodLines.Count > 0)
                interfaceLines.Add(new TemplateLine());

            foreach (ILine methodLine in methodLines)
                interfaceLines.Add(new TemplateLine(this.indent + methodLine.Code));

            interfaceLines.Add(new TemplateLine("}"));

            return interfaceLines;
        }

        protected virtual IList<ILine> ListMethods()
        {
            IList<ILine> methodLines = new List<ILine>();

            foreach (IFunction method in this.methods.Values)
            {
                if (!string.IsNullOrWhiteSpace(method.Summary))
                {
                    methodLines.Add(new TemplateLine("/// <summary>"));
                    methodLines.Add(new TemplateLine("/// " + method.Summary));
                    methodLines.Add(new TemplateLine("/// </summary>"));
                    foreach(IVariable parameter in method.Parameters)
                        methodLines.Add(new TemplateLine("/// </param name=\"" + parameter.VariableType.Name + "\">" + parameter.Summary + "</param>"));
                }

                methodLines.Add(new TemplateLine(method.Signature + ";"));
            }

            return methodLines;
        }

        protected virtual IList<ILine> ListProperties()
        {
            IList<ILine> propertyLines = new List<ILine>();

            string temp;
            foreach (IProperty property in this.properties.Values)
            {
                if (!string.IsNullOrWhiteSpace(property.Summary))
                {
                    propertyLines.Add(new TemplateLine("/// <summary>"));
                    propertyLines.Add(new TemplateLine("/// " + property.Summary));
                    propertyLines.Add(new TemplateLine("/// </summary>"));
                }

                if (!string.IsNullOrWhiteSpace(property.Comment))
                    propertyLines.Add(new TemplateLine("//" +  property.Comment));

                temp = property.Variable.VariableType.Name + " " + property.Variable.Reference + " { ";

                if (property.CanGet)
                    temp += "get;";
                if (property.CanGet && property.CanSet)
                    temp += " ";
                if (property.CanSet)
                    temp += "set;";

                temp += " }";

                propertyLines.Add(new TemplateLine(temp));
            }

            return propertyLines;
        }
    }
}
