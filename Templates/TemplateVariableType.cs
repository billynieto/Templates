using System;
using System.Collections.Generic;

using Templates.Framework;

namespace Templates
{
    public class TemplateVariableType : IVariableType
    {
        public static IVariableType Boolean = new TemplateVariableType(TemplateNamespace.System, "bool");
        public static IVariableType DataSet = new TemplateVariableType(TemplateNamespace.System_Data, "DataSet");
        public static IVariableType DateTime = new TemplateVariableType(TemplateNamespace.System, "DateTime");
        public static IVariableType Guid = new TemplateVariableType(TemplateNamespace.System, "Guid");
        public static IVariableType Int = new TemplateVariableType(TemplateNamespace.System, "int");
        public static IVariableType Int32 = new TemplateVariableType(TemplateNamespace.System, "int32");
        public static IVariableType Int64 = new TemplateVariableType(TemplateNamespace.System, "int64");
        public static IVariableType Object = new TemplateVariableType(TemplateNamespace.System, "object");
        public static IVariableType String = new TemplateVariableType(TemplateNamespace.System, "string");
        public static IVariableType TimeSpan = new TemplateVariableType(TemplateNamespace.System, "TimeSpan");
        public static IVariableType Void = new TemplateVariableType(TemplateNamespace.System, "void");
        
        public static IEnumerable<IVariableType> SystemTypes = new List<IVariableType>() {
            Boolean, DataSet, DateTime, Guid, Int, Int32, Int64, Object, String, TimeSpan, Void
        };

        protected string name;
        protected INamespace _namespace;
        
        public string Name { get { return this.name; } }

        public TemplateVariableType(string name)
            :this(null, name)
        {
        }
        
        public TemplateVariableType(INamespace _namespace, string name)
        {
            this.name = name;
            this._namespace = _namespace;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
