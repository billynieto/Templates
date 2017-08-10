using System;

using Templates.Framework;

namespace Templates
{
    public class TemplateVariable : IVariable
    {
        public static IVariable Guid_Empty = new TemplateVariable(TemplateVariableType.Guid, "Guid.Empty");

        protected string instanceName;
        protected string instanceNameReference;
        protected bool isStatic;
        protected string listInstanceName;
        protected string listInstanceNameReference;
        protected string reference;
        protected string summary;
        protected IVariableType _type;

        public string InstanceName { get { if (this.instanceName == null) this.instanceName = _type.Name.Substring(0, 1).ToLower() + _type.Name.Substring(1, _type.Name.Length - 1); return this.instanceName; } }
        public string InstanceNameReference { get { if (this.instanceNameReference == null) this.instanceNameReference = InstanceName.Substring(0, 1).ToUpper() + InstanceName.Substring(1, InstanceName.Length - 1); return this.instanceNameReference; } }
        public bool IsStatic { get { return this.isStatic; } }
        public string ListInstanceName { get { if (this.listInstanceName == null) { this.listInstanceName = TemplatesHelper.Pluralize(InstanceName); } return this.listInstanceName; } }
        public string ListInstanceNameReference { get { if (this.listInstanceNameReference == null) this.listInstanceNameReference = ListInstanceName.Substring(0, 1).ToUpper() + ListInstanceName.Substring(1, ListInstanceName.Length - 1); return this.listInstanceNameReference; } }
        public string Reference { get { if (this.reference == null) this.reference = InstanceName.Substring(0, 1).ToUpper() + InstanceName.Substring(1, InstanceName.Length - 1); return this.reference; } }
        public string Summary { get { return this.summary; } set { this.summary = value; } }
        public IVariableType VariableType { get { return this._type; } }

        public TemplateVariable(string _type)
            : this(false, new TemplateVariableType(_type), null)
        {
        }

        public TemplateVariable(IVariableType _type)
            : this(false, _type, null)
        {
        }
        
        public TemplateVariable(string _type, string instanceName)
            : this(false, new TemplateVariableType(_type), instanceName)
        {
        }

        public TemplateVariable(IVariableType _type, string instanceName)
            : this(false, _type, instanceName)
        {
        }
        
        public TemplateVariable(bool isStatic, string _type, string instanceName)
            : this(isStatic, new TemplateVariableType(_type), instanceName)
        {
        }

        public TemplateVariable(bool isStatic, IVariableType _type, string instanceName)
        {
            if (!string.IsNullOrWhiteSpace(instanceName))
            {
                string prefix = instanceName.Substring(0, 1).ToLower();
                string suffix = instanceName.Substring(1, instanceName.Length - 1);

                this.instanceName = prefix + suffix;
            }
            else
            {
                this.instanceName = null;
            }
            
            this.isStatic = isStatic;
            this._type = _type;
        }
    }
}
