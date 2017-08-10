using System;
using System.Collections.Generic;

namespace Templates.Framework
{
    public interface ICodeBlock : IMultipleLines
    {
        IEnumerable<ILine> Lines { get; }

        void Add();
        void Add(string code);
    }
}
 