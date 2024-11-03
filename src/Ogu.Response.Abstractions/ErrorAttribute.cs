using System;

namespace Ogu.Response.Abstractions
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ErrorAttribute : Attribute
    {
        public ErrorAttribute(string title) : this(title, null) { }
        public ErrorAttribute(string title, string description) : this(title, description, null) { }
        public ErrorAttribute(string title, string description, string traces) : this(title, description, traces, null) { }
        public ErrorAttribute(string title, string description, string traces, string helpLink)
        {
            Title = title;
            Description = description;
            Traces = traces;
            HelpLink = helpLink;
        }

        public string Title { get; }

        public string Description { get; }

        public string Traces { get; }

        public string HelpLink { get; }
    }
}