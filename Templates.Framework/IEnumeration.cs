using System;
using System.Collections.Generic;

namespace Templates.Framework
{
    public interface IEnumeration : IComponent
    {
        IList<IEnumerationItem> Items { get; set; }
        string Name { get; }
        INamespace Namespace { get; }
        string Signature { get; }

        void Add(IEnumerationItem item);
    }

    public interface IEnumerationItem
    {
        IEnumeration Enumeration { get; set; }
        string Name { get; }
        string Value { get; }
    }
}
