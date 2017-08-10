using System;
using System.Collections.Generic;

namespace Templates.Framework
{
    public interface IRegion : IMultipleLines
    {
        SortedList<string, IFunction> Functions { get; }
        string Name { get; }

        void Add(IFunction codeFunction);
    }
}
