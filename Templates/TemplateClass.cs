using System;
using System.Collections.Generic;

using Templates.Framework;

namespace Templates
{
    public class TemplateClass : IClass
    {
        public static IEnumerable<IClass> SystemClasses = new List<IClass>() {
            new TemplateClass(TemplateNamespace.System, "Action"),
            new TemplateClass(TemplateNamespace.System, "Attribute"),
            new TemplateClass(TemplateNamespace.System, "Environment")
        };

        protected IList<IConstructor> constructors;
        protected IRegion currentRegion;
        protected bool currentRegionSaved;
        protected IList<IInterface> implements;
        protected string indent;
        protected IClass inheritsFrom;
        protected bool isAbstract;
        protected bool isPartial;
        protected string name;
        protected INamespace _namespace;
        protected SortedList<string, IProperty> properties;
        protected IList<IRegion> regions;
        protected string signature;
        protected string summary;
        protected IList<ITag> tags;
        protected string type;

        public IEnumerable<IConstructor> Constructors { get { return this.constructors; } }
        public IEnumerable<IInterface> Implements { get { return this.implements; } }
        public string Indent { get { return this.indent; } set { this.indent = value; } }
        public IClass InheritsFrom { get { return this.inheritsFrom; } set { this.inheritsFrom = value; } }
        public bool IsAbstract { get { return this.isAbstract; } set { this.isAbstract = value; } }
        public bool IsPartial { get { return this.isPartial; } set { this.isPartial = value; } }
        public virtual string Name { get { return this.name; } }
        public INamespace Namespace { get { return this._namespace; } }
        public IEnumerable<IProperty> Properties { get { return this.properties.Values; } }
        public IEnumerable<IRegion> Regions { get { return this.regions; } }
        public string Summary { get { return this.summary; } set { this.summary = value; } }
        public IEnumerable<ITag> Tags { get { return this.tags; } }
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

                    if (this.isAbstract)
                        this.signature += "abstract ";

                    this.signature += "class " + Type;

                    if (this.inheritsFrom != null || this.implements.Count > 0)
                    {
                        this.signature += " : ";

                        if (this.inheritsFrom != null)
                            this.signature += this.inheritsFrom.Type;

                        for (int i = 0; i < this.implements.Count; i++)
                        {
                            if (this.inheritsFrom != null || i > 0)
                                this.signature += ", ";

                            this.signature += this.implements[i].Type;
                        }
                    }
                }

                return this.signature;
            }
        }

        public TemplateClass(INamespace _namespace, string name)
        {
            this.name = name;
            this._namespace = _namespace;

            this.constructors = new List<IConstructor>();
            this.implements = new List<IInterface>();
            this.indent = "\t";
            this.isAbstract = false;
            this.isPartial = false;
            this.properties = new SortedList<string, IProperty>();
            this.regions = new List<IRegion>();
            this.tags = new List<ITag>();

            NewRegion();
        }

        public void Add(IConstructor constructor)
        {
            if (constructor == null)
                throw new ArgumentNullException("Constructor");

            this.constructors.Add(constructor);

            constructor.Class = this;
        }

        public void Add(IFunction function)
        {
            if (function == null)
                throw new ArgumentNullException("Function");

            this.currentRegion.Add(function);

            if (!this.currentRegionSaved && this.currentRegion.Functions.Count == 1)
            {
                this.regions.Add(this.currentRegion);

                this.currentRegionSaved = true;
            }
        }

        public void Add(IProperty property)
        {
            if (property == null)
                throw new ArgumentNullException("Property");

            this.properties[property.Name] = property;
        }

        public void Add(IVariable variable)
        {
            Add(variable, true, true);
        }

        public void Add(IVariable variable, bool canGet, bool canSet)
        {
            if (variable == null)
                throw new ArgumentNullException("Code Property");

            TemplateProperty property = new TemplateProperty(variable, canGet, canSet);

            this.properties[property.Name] = property;
        }

        public void Add(IVariable variable, string getter, string setter)
        {
            if (variable == null)
                throw new ArgumentNullException("Code Property");

            TemplateProperty property = new TemplateProperty(variable, getter, setter);

            this.properties[property.Name] = property;
        }

        public void Add(bool isNew, IVariable variable, bool canGet, bool canSet)
        {
            if (variable == null)
                throw new ArgumentNullException("Code Property");

            TemplateProperty property = new TemplateProperty(variable, canGet, canSet);
            property.IsNew = isNew;

            this.properties[property.Name] = property;
        }

        public void Inherits(IClass classToInherit)
        {
            if (this.inheritsFrom != null)
                throw new InvalidOperationException("Can not set " + this.name + " to inherit from " + classToInherit.Name + " because it already is set to inherit from " + this.inheritsFrom.Name + ".");

            this.inheritsFrom = classToInherit;
        }

        public void NewRegion()
        {
            NewRegion(null);
        }

        public void NewRegion(string name)
        {
            if (this.currentRegion != null
                && !this.currentRegionSaved
                && this.currentRegion.Functions.Count == 0
                && !string.IsNullOrWhiteSpace(this.currentRegion.Name))
            {
                this.regions.Add(this.currentRegion);
            }

            this.currentRegion = new TemplateRegion(name);

            if (!string.IsNullOrWhiteSpace(this.currentRegion.Name))
            {
                this.regions.Add(this.currentRegion);

                this.currentRegionSaved = true;
            }
            else
            {
                this.currentRegionSaved = false;
            }
        }

        public void WillImplement(IInterface interfaceToImplement)
        {
            if (interfaceToImplement == null)
                throw new ArgumentNullException("InterfaceToImplement");
            if (this.implements.Contains(interfaceToImplement))
                throw new ArgumentException(interfaceToImplement.Name + " interface is already implemented by " + this.name);

            this.implements.Add(interfaceToImplement);
        }

        public void Tag(string tag)
        {
            this.tags.Add(new TemplateTag(tag));
        }

        public virtual IList<ILine> ToLines()
        {
            IList<ILine> classLines = new List<ILine>();

            if (!string.IsNullOrWhiteSpace(this.summary))
            {
                classLines.Add(new TemplateLine("/// <summary>"));
                classLines.Add(new TemplateLine("/// " + this.summary));
                classLines.Add(new TemplateLine("/// </summary>"));
            }

            foreach (ITag tag in this.tags)
                classLines.Add(new TemplateLine(tag));

            classLines.Add(new TemplateLine(Signature));
            classLines.Add(new TemplateLine("{"));

            IList<ILine> meatAndPotatoLines = ListMeatAndPotatoes();
            foreach (ILine meatAndPotatoLine in meatAndPotatoLines)
                classLines.Add(new TemplateLine(this.indent + meatAndPotatoLine.Code));

            classLines.Add(new TemplateLine("}"));

            return classLines;
        }

        protected IList<ILine> ListConstructors()
        {
            IList<ILine> constructorLines = new List<ILine>();

            for (int i = 0; i < this.constructors.Count; i++)
            {
                if (i > 0)
                    constructorLines.Add(new TemplateLine());

                foreach (ILine constructorLine in constructors[i].ToLines())
                    constructorLines.Add(constructorLine);
            }

            return constructorLines;
        }

        protected IList<ILine> ListMeatAndPotatoes()
        {
            IList<ILine> meatAndPotatoesLines = new List<ILine>();

            IList<ILine> constructorLines = ListConstructors();
            IList<ILine> propertyLines = ListProperties();
            IList<ILine> regionLines = ListRegions();
            IList<ILine> variableLines = ListVariables();

            foreach (ILine variableLine in variableLines)
                meatAndPotatoesLines.Add(new TemplateLine(variableLine.Code));

            if (variableLines.Count > 0 && propertyLines.Count > 0)
                meatAndPotatoesLines.Add(new TemplateLine());

            foreach (ILine propertyLine in propertyLines)
                meatAndPotatoesLines.Add(new TemplateLine(propertyLine.Code));

            if ((variableLines.Count > 0 || propertyLines.Count > 0) && constructorLines.Count > 0)
                meatAndPotatoesLines.Add(new TemplateLine());

            foreach (ILine constructorLine in constructorLines)
                meatAndPotatoesLines.Add(new TemplateLine(constructorLine.Code));

            if ((variableLines.Count > 0 || constructorLines.Count > 0 || propertyLines.Count > 0) && regionLines.Count > 0)
                meatAndPotatoesLines.Add(new TemplateLine());

            foreach (ILine regionLine in regionLines)
                meatAndPotatoesLines.Add(new TemplateLine(regionLine.Code));

            return meatAndPotatoesLines;
        }

        protected IList<ILine> ListProperties()
        {
            IList<ILine> propertyLines = new List<ILine>();

            string temp;
            foreach (IProperty property in Properties)
            {
                if (property.CanGet || property.CanSet)
                {
                    if (!string.IsNullOrWhiteSpace(property.Summary))
                    {
                        propertyLines.Add(new TemplateLine("/// <summary>"));
                        propertyLines.Add(new TemplateLine("/// " + property.Summary));
                        propertyLines.Add(new TemplateLine("/// </summary>"));
                    }

                    if (!string.IsNullOrWhiteSpace(property.Comment))
                        propertyLines.Add(new TemplateLine("//" + property.Comment));

                    temp = "public ";

                    if(property.IsStatic)
                        temp += "static ";
                    
                    temp += (property.VariableType != null ? property.VariableType : property.Variable.VariableType).Name + " " + property.Name + " { ";

                    if (property.CanGet)
                    {
                        if (!string.IsNullOrWhiteSpace(property.Getter))
                        {
                            temp += "get { " + property.Getter + " }";
                        }
                        else if (property.Variable != null)
                        {
                            temp += "get { return ";

                            if (!property.Variable.IsStatic)
                                temp += "this.";
                            else
                                temp += this.Name + ".";

                            temp += property.Variable.InstanceName + "; }";
                        }
                        else
                        {
                            temp += "get;";
                        }
                    }

                    if (property.CanGet && property.CanSet)
                        temp += " ";

                    if (property.CanSet)
                    {
                        if (!string.IsNullOrWhiteSpace(property.Setter))
                        {
                            temp += "set { " + property.Setter + " }";
                        }
                        else if (property.Variable != null)
                        {
                            temp += "set { ";

                            if (!property.Variable.IsStatic)
                                temp += "this.";
                            else
                                temp += this.Name + ".";

                            temp += property.Variable.InstanceName + " = value; }";
                        }
                        else
                        {
                            temp += "set;";
                        }
                    }

                    temp += " }";

                    propertyLines.Add(new TemplateLine(temp));
                }
            }

            return propertyLines;
        }

        protected IList<ILine> ListRegions()
        {
            IList<ILine> regionLines = new List<ILine>();

            for (int i = 0; i < this.regions.Count; i++)
            {
                if (i > 0)
                    regionLines.Add(new TemplateLine());

                foreach (ILine regionLine in regions[i].ToLines())
                    regionLines.Add(regionLine);
            }

            return regionLines;
        }

        protected IList<ILine> ListVariables()
        {
            IList<ILine> variableLines = new List<ILine>();

            foreach (IProperty property in Properties)
            {
                if (property.Variable != null)
                {
                    string code = string.Empty;

                    code += "protected ";

                    if (property.IsNew)
                        code += "new ";

                    if (property.Variable.IsStatic)
                        code += "static ";

                    code += property.Variable.VariableType.Name + " " + property.Variable.InstanceName + ";";

                    variableLines.Add(new TemplateLine(code));
                }
            }

            return variableLines;
        }
    }
}
