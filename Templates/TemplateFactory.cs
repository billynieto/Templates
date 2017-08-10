using System;
using System.Collections.Generic;

using Templates.Framework;

namespace Templates
{
    public class TemplateFactory : ICodeFactory
    {
        public IClass GenerateClass(INamespace _namespace, string name)
        {
            return new TemplateClass(_namespace, name);
        }

        public ICodeBlock GenerateCodeBlock()
        {
            return new TemplateCodeBlock();
        }

        public IConstructor GenerateConstructor()
        {
            return new TemplateConstructor();
        }

        public IConstructor GenerateConstructor(IList<IVariable> parameters)
        {
            return new TemplateConstructor(parameters);
        }

        public IConstructor GenerateConstructor(Privacy privacy, IList<IVariable> parameters, IList<IVariable> secondaryParameters)
        {
            return new TemplateConstructor(privacy, parameters, secondaryParameters);
        }

        public IEnumeration GenerateEnumeration(INamespace _namespace, string name)
        {
            return new TemplateEnumeration(_namespace, name);
        }

        public IFile GenerateFile(INamespace _namespace, string name, string extension)
        {
            return new TemplateFile(_namespace, name, extension);
        }

        public IFunction GenerateFunction(Privacy privacy, string returnType, string name)
        {
            return new TemplateFunction(privacy, returnType, name);
        }

        public IFunction GenerateFunction(Privacy privacy, IVariableType returnType, string name)
        {
            return new TemplateFunction(privacy, returnType, name);
        }

        public IFunction GenerateFunction(Privacy privacy, string returnType, string name, IList<IVariable> parameters)
        {
            return new TemplateFunction(privacy, returnType, name, parameters);
        }

        public IFunction GenerateFunction(Privacy privacy, IVariableType returnType, string name, IList<IVariable> parameters)
        {
            return new TemplateFunction(privacy, returnType, name, parameters);
        }

        public IFunction GenerateFunction(Privacy privacy, Overridability overridability, string returnType, string name, IList<IVariable> parameters)
        {
            return new TemplateFunction(privacy, overridability, returnType, name, parameters);
        }

        public IFunction GenerateFunction(Privacy privacy, Overridability overridability, IVariableType returnType, string name, IList<IVariable> parameters)
        {
            return new TemplateFunction(privacy, overridability, returnType, name, parameters);
        }

        public IGenericClass GenerateGenericClass(INamespace _namespace, string name, IList<string> types)
        {
            return new TemplateGenericClass(_namespace, name, types);
        }

        public IGenericClass GenerateGenericClass(INamespace _namespace, string name, IList<IVariableType> types)
        {
            return new TemplateGenericClass(_namespace, name, types);
        }

        public IGenericInterface GenerateGenericInterface(INamespace _namespace, string name, IList<string> types)
        {
            return new TemplateGenericInterface(_namespace, name, types);
        }

        public IGenericInterface GenerateGenericInterface(INamespace _namespace, string name, IList<IVariableType> types)
        {
            return new TemplateGenericInterface(_namespace, name, types);
        }

        public IInterface GenerateInterface(INamespace _namespace, string name)
        {
            return new TemplateInterface(_namespace, name);
        }

        public ILine GenerateLine()
        {
            return new TemplateLine();
        }

        public ILine GenerateLine(string code)
        {
            return new TemplateLine(code);
        }

        public INamespace GenerateNamespace(string name)
        {
            return new TemplateNamespace(name);
        }

        public IProperty GenerateProperty(IVariable variable, bool canGet, bool canSet)
        {
            return new TemplateProperty(variable, canGet, canSet);
        }

        public IRegion GenerateRegion()
        {
            return new TemplateRegion();
        }

        public IRegion GenerateRegion(string name)
        {
            return new TemplateRegion(name);
        }

        public IVariable GenerateVariable(IVariableType _type)
        {
            return new TemplateVariable(_type);
        }

        public IVariable GenerateVariable(IVariableType _type, string instanceName)
        {
            return new TemplateVariable(_type, instanceName);
        }

        public IVariableType GenerateVariableType(string name)
        {
            return new TemplateVariableType(name);
        }
    }
}
