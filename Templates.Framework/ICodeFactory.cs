using System;
using System.Collections.Generic;

namespace Templates.Framework
{
    public interface ICodeFactory
    {
        IClass GenerateClass(INamespace _namespace, string name);
        ICodeBlock GenerateCodeBlock();
        IConstructor GenerateConstructor();
        IConstructor GenerateConstructor(IList<IVariable> parameters);
        IConstructor GenerateConstructor(Privacy privacy, IList<IVariable> parameters, IList<IVariable> secondaryParameters);
        IEnumeration GenerateEnumeration(INamespace _namespace, string name);
        IFile GenerateFile(INamespace _namespace, string name, string extension);
        IFunction GenerateFunction(Privacy privacy, string returnType, string name);
        IFunction GenerateFunction(Privacy privacy, IVariableType returnType, string name);
        IFunction GenerateFunction(Privacy privacy, string returnType, string name, IList<IVariable> parameters);
        IFunction GenerateFunction(Privacy privacy, IVariableType returnType, string name, IList<IVariable> parameters);
        IFunction GenerateFunction(Privacy privacy, Overridability overridability, string returnType, string name, IList<IVariable> parameters);
        IFunction GenerateFunction(Privacy privacy, Overridability overridability, IVariableType returnType, string name, IList<IVariable> parameters);
        IGenericClass GenerateGenericClass(INamespace _namespace, string name, IList<string> genericTypes);
        IGenericClass GenerateGenericClass(INamespace _namespace, string name, IList<IVariableType> genericTypes);
        IGenericInterface GenerateGenericInterface(INamespace _namespace, string name, IList<string> genericTypes);
        IGenericInterface GenerateGenericInterface(INamespace _namespace, string name, IList<IVariableType> genericTypes);
        IInterface GenerateInterface(INamespace _namespace, string name);
        ILine GenerateLine();
        ILine GenerateLine(string code);
        INamespace GenerateNamespace(string name);
        IProperty GenerateProperty(IVariable variable, bool canGet, bool canSet);
        IRegion GenerateRegion();
        IRegion GenerateRegion(string name);
        IVariable GenerateVariable(IVariableType _type);
        IVariable GenerateVariable(IVariableType _type, string instanceName);
        IVariableType GenerateVariableType(string name);
    }
}
