using System;

namespace Templates.Framework
{
    public interface IProperty
    {
        bool CanGet { get; }
        bool CanSet { get; }
        string Comment { get; }
        string Getter { get; }
        bool IsNew { get; set; }
        bool IsStatic { get; set; }
        string Name { get; }
        string Setter { get; }
        string Summary { get; }
        IVariable Variable { get; }
        IVariableType VariableType { get; }
    }
}
