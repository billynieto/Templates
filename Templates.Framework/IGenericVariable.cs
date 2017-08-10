using System;

namespace Templates.Framework
{
    public interface IGenericVariable : IVariable
    {
        new IGenericVariableType VariableType { get; }
    }
}
