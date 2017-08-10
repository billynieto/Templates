using System;
using System.Collections.Generic;

namespace Templates.Framework
{
    public interface IInterface : IComponent
    {
        IEnumerable<IInterface> Implements { get; }
        bool IsPartial { get; set; }
        IEnumerable<IFunction> Methods { get; }
        string Name { get; }
        INamespace Namespace { get; }
        IEnumerable<IProperty> Properties { get; }
        string Signature { get; }
        string Summary { get; set; }
        string Type { get; }

        void Add(IFunction function);
        void Add(IVariable variable);
        void Add(IVariable property, bool canGet, bool canSet);
        void WillImplement(IInterface _interface);
    }
}
