using System;

using Templates.Framework;

namespace Templates
{
    public class TemplateGenericVariable : TemplateVariable, IGenericVariable
    {
        protected IGenericVariableType genericVariableType;

        public new IGenericVariableType VariableType { get { return this.genericVariableType; } }

        public TemplateGenericVariable(IGenericVariableType genericVariableType)
            : base(genericVariableType)
        {
            this.genericVariableType = genericVariableType;
        }

        public TemplateGenericVariable(IGenericVariableType genericVariableType, string instanceName)
            : base(genericVariableType, instanceName)
        {
            this.genericVariableType = genericVariableType;
        }
    }
}
