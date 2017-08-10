using System;

using Templates.Framework;

namespace Templates
{
    public class TemplateNamespace : INamespace
    {
        public static INamespace System = new TemplateNamespace("System");
        public static INamespace System_Collections_Generic = new TemplateNamespace("System.Collections.Generic");
        public static INamespace System_Data = new TemplateNamespace("System.Data");
        public static INamespace System_Data_SqlClient = new TemplateNamespace("System.Data.SqlClient");
        public static INamespace System_Linq = new TemplateNamespace("System.Linq");
        public static INamespace System_Text = new TemplateNamespace("System.Text");
        public static INamespace Templates = new TemplateNamespace("Templates");
        public static INamespace Templates_Framework = new TemplateNamespace("Templates.Framework");

        protected string name;

        public string Name { get { return this.name; } }

        public TemplateNamespace(string name)
        {
            this.name = name;
        }

        public TemplateNamespace(INamespace parent, string name)
        {
            if (parent == null)
                throw new ArgumentNullException("Parent");
            if (name == null)
                throw new ArgumentNullException("Name");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is empty.");

            if (!name.StartsWith("."))
                name = "." + name;

            this.name = parent.Name + name;
        }
    }
}
