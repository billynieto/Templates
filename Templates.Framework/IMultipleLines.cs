using System;
using System.Collections.Generic;

namespace Templates.Framework
{
    public interface IMultipleLines
    {
        string Indent { get; set; }

        IList<ILine> ToLines();
    }
}
