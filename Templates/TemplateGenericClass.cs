using System;
using System.Collections.Generic;

using Templates.Framework;

namespace Templates
{
    public class TemplateGenericClass : TemplateClass, IGenericClass
    {
        protected IList<IVariableType> genericTypes;
        protected Dictionary<IVariableType, IInterface> whereClause;

        public IList<IVariableType> GenericTypes { get { return this.genericTypes; } }
        public Dictionary<IVariableType, IInterface> WhereClause { get { return this.whereClause; } }

        public override string Type
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.type))
                {
                    this.type = base.Name + "<";

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
        
        public TemplateGenericClass(INamespace _namespace, string name, IList<string> genericTypes)
            : base(_namespace, name)
        {
            this.genericTypes = new List<IVariableType>();
            this.whereClause = new Dictionary<IVariableType, IInterface>();

            foreach (string genericType in genericTypes)
                Add(genericType);
        }

        public TemplateGenericClass(INamespace _namespace, string name, IList<IVariableType> genericTypes)
            : base(_namespace, name)
        {
            this.genericTypes = genericTypes;

            this.whereClause = new Dictionary<IVariableType, IInterface>();
        }

        public void Where(IVariableType genericType, IInterface isOfInterface)
        {
            if (genericType == null)
                throw new ArgumentNullException("Generic Type");
            if (isOfInterface == null)
                throw new ArgumentNullException("Is Of Interface");

            if (this.whereClause.ContainsKey(genericType))
                throw new ArgumentException("Generic Type (" + genericType.Name + ") Where already set.");

            this.whereClause[genericType] = isOfInterface;
        }

        protected void Add(string genericType)
        {
            if (string.IsNullOrWhiteSpace(genericType))
                throw new ArgumentNullException("CodeGenericType");

            this.genericTypes.Add(new TemplateVariableType(genericType));
        }

        public override IList<ILine> ToLines()
        {
            IList<ILine> genericClassLines = new List<ILine>();

            genericClassLines.Add(new TemplateLine(Signature));

            if (this.whereClause != null && this.whereClause.Count > 0)
            {
                foreach (IVariableType key in this.whereClause.Keys)
                {
                    string code = this.indent + "where " + key.Name + " : " + this.whereClause[key].Type;

                    genericClassLines.Add(new TemplateLine(code));
                }
            }

            genericClassLines.Add(new TemplateLine("{"));

            IList<ILine> meatAndPotatoLines = ListMeatAndPotatoes();
            foreach (ILine meatAndPotatoLine in meatAndPotatoLines)
                genericClassLines.Add(new TemplateLine(this.indent + meatAndPotatoLine.Code));

            genericClassLines.Add(new TemplateLine("}"));

            return genericClassLines;
        }
    }
}
