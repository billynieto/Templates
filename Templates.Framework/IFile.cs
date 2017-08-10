using System;
using System.Collections.Generic;

namespace Templates.Framework
{
    public interface IFile : IMultipleLines
    {
        Queue<IComponent> Components { get; set; }
        string Extension { get; }
        string Name { get; }
        INamespace Namespace { get; }
        IList<SortedList<string, INamespace>> UsingStatementBlocks { get; set; }

        void NewUsingStatementBlock();
        void Uses(INamespace _namepace);
    }
}
