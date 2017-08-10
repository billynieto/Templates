using System;
using System.Collections.Generic;

namespace Templates.Framework
{
    public interface ISortedCodeBlock : ICodeBlock, IMultipleLines
    {
        void Add(string sortBy, string code);
    }
}
