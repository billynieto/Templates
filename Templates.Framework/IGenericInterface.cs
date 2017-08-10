using System;
using System.Collections.Generic;

namespace Templates.Framework
{
    public interface IGenericInterface : IInterface
    {
        IList<IVariableType> GenericTypes { get; }
    }
}
