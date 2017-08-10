using System;

namespace Templates.Framework
{
    public interface IVariable
    {
        string InstanceName { get; }
        string InstanceNameReference { get; }
        bool IsStatic { get; }
        string ListInstanceName { get; }
        string ListInstanceNameReference { get; }
        string Reference { get; }
        string Summary { get; set; }
        IVariableType VariableType { get; }
    }
}
