using System;

namespace Ogu.Response.Abstractions
{
    [AttributeUsage(AttributeTargets.Field)]
    public class TitleAttribute : Attribute
    {
        public TitleAttribute(string title)
        {
            Title = title;
        }

        public string Title { get; }
    }
}