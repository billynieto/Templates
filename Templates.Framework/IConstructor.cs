using System;
using System.Collections.Generic;

namespace Templates.Framework
{
    public interface IConstructor : IMultipleLines
    {
        IList<IVariable> Base { get; set; }
        IClass Class { get; set; }
        ICodeBlock CodeBlock { get; }
        IList<IVariable> Parameters { get; }
        Privacy Privacy { get; }
        IList<IVariable> This { get; set; }

        void Add();
        void Add(string code);
        void Validate();
    }
}
