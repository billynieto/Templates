using System;
using System.Collections.Generic;

namespace Templates.Framework
{
    public enum Privacy
    {
        Public,
        Protected,
        Private
    }

    public interface IClass : IComponent
    {
        IEnumerable<IConstructor> Constructors { get; }
        IEnumerable<IInterface> Implements { get; }
        IClass InheritsFrom { get; set; }
        bool IsAbstract { get; set; }
        bool IsPartial { get; set; }
        string Name { get; }
        INamespace Namespace { get; }
        IEnumerable<IProperty> Properties { get; }
        IEnumerable<IRegion> Regions { get; }
        string Signature { get; }
        string Summary { get; set; }
        IEnumerable<ITag> Tags { get; }
        string Type { get; }

        void Add(IConstructor IConstructor);
        void Add(IFunction codeFunction);
        void Add(IProperty property);
        void Add(IVariable variable);
        void Add(IVariable variable, bool canGet, bool canSet);
        void Add(bool isNew, IVariable variable, bool canGet, bool canSet);
        void Inherits(IClass classToInherit);
        void NewRegion();
        void NewRegion(string name);
        void WillImplement(IInterface interfaceToImplement);
        void Tag(string tag);
    }
}
