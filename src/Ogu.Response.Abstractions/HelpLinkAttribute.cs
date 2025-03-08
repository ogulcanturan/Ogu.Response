using System;

namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Specifies a help link for a field, used as a metadata attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class HelpLinkAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpLinkAttribute"/> class.
        /// </summary>
        /// <param name="helpLink">The URL or link to the help documentation associated with the field.</param>
        public HelpLinkAttribute(string helpLink)
        {
            HelpLink = helpLink;
        }

        /// <summary>
        /// Gets the help link associated with the field.
        /// </summary>
        public string HelpLink { get; }
    }
}