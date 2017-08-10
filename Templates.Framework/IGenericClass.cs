using System;
using System.Collections.Generic;

namespace Templates.Framework
{
    public interface IGenericClass : IClass
    {
        IList<IVariableType> GenericTypes { get; }
        Dictionary<IVariableType, IInterface> WhereClause { get; }

        void Where(IVariableType genericType, IInterface isOfInterface);
    }
}
