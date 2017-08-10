using System;
using System.Collections.Generic;
using Templates.Framework;

namespace Templates
{
    public class TemplateTag : ITag
    {
        protected string text;

        public string Text { get { return this.text; } }

        public TemplateTag(string text)
        {
            this.text = text;
        }
    }
}
