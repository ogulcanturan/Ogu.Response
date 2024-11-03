using System;

namespace Ogu.Response.Abstractions
{
    /// <summary>
    ///     Specifies a title for a field, used as a metadata attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class TitleAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TitleAttribute"/> class.
        /// </summary>
        /// <param name="title">The title to associate with the field.</param>
        public TitleAttribute(string title)
        {
            Title = title;
        }

        /// <summary>
        ///     Gets the title associated with the field.
        /// </summary>
        public string Title { get; }
    }
}