using System;

using Templates.Framework;

namespace Templates
{
    public class TemplateProperty : IProperty
    {
        protected bool canGet;
        protected bool canSet;
        protected string comment;
        protected string getter;
        protected bool isNew;
        protected bool isStatic;
        protected string name;
        protected string setter;
        protected string summary;
        protected IVariable variable;
        protected IVariableType variableType;

        public bool CanGet { get { return this.canGet; } }
        public bool CanSet { get { return this.canSet; } }
        public string Comment { get { return this.comment; } }
        public string Getter { get { return this.getter; } }
        public bool IsNew { get { return this.isNew; } set { this.isNew = value; } }
        public bool IsStatic { get { return this.isStatic; } set { this.isStatic = value; } }
        public string Name { get { return this.name; } }
        public string Setter { get { return this.setter; } }
        public string Summary { get { return this.summary; } set { this.summary = value; } }
        public IVariable Variable {  get { return this.variable; } }
        public IVariableType VariableType { get { return this.variableType; } }

        public TemplateProperty(IVariableType variableType, string name, bool canGet, bool canSet)
            : this(null, variableType, name, canGet, canSet)
        {
        }

        public TemplateProperty(IVariableType variableType, string name, string getter, string setter)
            : this(null, variableType, name, getter, setter)
        {
        }

        public TemplateProperty(string comment, IVariableType variableType, string name, bool canGet, bool canSet)
            : this(comment, variableType, name, null, canGet, null, canSet, null)
        {
        }

        public TemplateProperty(string comment, IVariableType variableType, string name, string getter, string setter)
            : this(comment, variableType, name, null, !string.IsNullOrWhiteSpace(getter), getter, !string.IsNullOrWhiteSpace(setter), setter)
        {
        }

        public TemplateProperty(IVariable variable, bool canGet, bool canSet)
            : this(variable.Reference, variable, canGet, canSet)
        {
        }

        public TemplateProperty(IVariable variable, string getter, string setter)
            : this(variable.Reference, variable, getter, setter)
        {
        }

        public TemplateProperty(string name, IVariable variable, bool canGet, bool canSet)
            : this(null, name, variable, canGet, canSet)
        {
        }

        public TemplateProperty(string name, IVariable variable, string getter, string setter)
            : this(null, name, variable, getter, setter)
        {
        }

        public TemplateProperty(string comment, string name, IVariable variable, bool canGet, bool canSet)
            : this(comment, variable.VariableType, name, variable, canGet, null, canSet, null)
        {
        }

        public TemplateProperty(string comment, string name, IVariable variable, string getter, string setter)
            : this(comment, variable.VariableType, name, variable, !string.IsNullOrWhiteSpace(getter), getter, !string.IsNullOrWhiteSpace(setter), setter)
        {
        }

        private TemplateProperty(string comment, IVariableType variableType, string name, IVariable variable, bool canGet, string getter, bool canSet, string setter)
        {
            this.canGet = canGet;
            this.canSet = canSet;
            this.comment = comment;
            this.getter = getter;
            this.name = name;
            this.setter = setter;
            this.variable = variable;
            this.variableType = variableType;

            if (this.variable != null)
                this.isStatic = this.variable.IsStatic;
        }
    }
}
