using System;
using System.Collections.Generic;

using Templates.Framework;

namespace Templates
{
    public class TemplateGenericInterface : TemplateInterface, IGenericInterface
    {
        protected IList<IVariableType> genericTypes;

        public IList<IVariableType> GenericTypes { get { return this.genericTypes; } }

        public override string Type
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.type))
                {
                    this.type = Name + "<";

                    for (int i = 0; i < this.genericTypes.Count; i++)
                    {
                        if (i > 0)
                            this.type += ", ";

                        this.type += this.genericTypes[i].Name;
                    }

                    this.type += ">";
                }

                return this.type;
            }
        }

        public TemplateGenericInterface(INamespace _namespace, string name, IEnumerable<string> genericTypes)
            : base(_namespace, name)
        {
            this.genericTypes = new List<IVariableType>();

            foreach (string genericType in genericTypes)
                Add(genericType);
        }

        public TemplateGenericInterface(INamespace _namespace, string name, IEnumerable<IVariableType> genericTypes)
            : base(_namespace, name)
        {
            this.genericTypes = new List<IVariableType>(genericTypes);
        }

        protected void Add(string genericType)
        {
            if (string.IsNullOrWhiteSpace(genericType))
                throw new ArgumentNullException("CodeGenericType");

            this.genericTypes.Add(new TemplateVariableType(genericType));
        }

        public override IList<ILine> ToLines()
        {
            throw new NotImplementedException();
        }
    }
}
