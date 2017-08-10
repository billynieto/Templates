using System;
using System.Collections.Generic;
using System.Text;

using Templates.Framework;

namespace Templates
{
    public class TemplateGenericVariableType : TemplateVariableType, IGenericVariableType
    {
        public IEnumerable<IVariableType> Parameters { get; set; }

        public TemplateGenericVariableType(string name, IEnumerable<IVariableType> parameters)
            : base(name)
        {
            Parameters = parameters;
        }

        public override string ToString()
        {
            int count = 0;
            string spacer = ", ";
            StringBuilder text = new StringBuilder().Append(Name).Append("<");

            foreach (IVariableType parameter in this.Parameters)
            {
                if (count++ > 0)
                    text.Append(spacer);

                text.Append(parameter);
            }

            text.Append(">");

            return text.ToString();
        }
    }
}