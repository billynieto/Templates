using System;
using System.Collections.Generic;

namespace Templates.Framework
{
    public enum Overridability
    {
        None,
        Overridable,
        Overriding,
        Static
    }

    public interface IFunction : IMultipleLines
    {
        ICodeBlock CodeBlock { get; }
        IList<IConstructor> Constructors { get; set; }
        bool IsCommentedOut { get; set; }
        bool IsNew { get; set; }
        string Name { get; }
        Overridability Overridability { get; }
        IList<IVariable> Parameters { get; }
        Privacy Privacy { get; }
        IVariableType ReturnType { get; }
        string Signature { get; }
        string SortBy { get; }
        string Summary { get; set; }
        IList<ITag> Tags { get; }

        void Add();
        void Add(string code);
        void Tag(string tag);
    }
}
