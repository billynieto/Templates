using System;
using System.Collections.Generic;

namespace Templates.Framework
{
    public interface IGenericVariableType : IVariableType
    {
        IEnumerable<IVariableType> Parameters { get; set; }
    }
}
