using System;

namespace Ogu.Response.Abstractions
{
    [AttributeUsage(AttributeTargets.Field)]
    public class HelpLinkAttribute : Attribute
    {
        public HelpLinkAttribute(string helpLink)
        {
            HelpLink = helpLink;
        }

        public string HelpLink { get; }
    }
}