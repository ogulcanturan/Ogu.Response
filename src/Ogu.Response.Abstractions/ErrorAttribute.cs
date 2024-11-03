using System;

namespace Ogu.Response.Abstractions
{
    /// <summary>
    ///     Specifies an error attribute for a field, providing metadata related to errors.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ErrorAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ErrorAttribute"/> class with a title.
        /// </summary>
        /// <param name="title">The title of the error.</param>
        public ErrorAttribute(string title) : this(title, null) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ErrorAttribute"/> class with a title and description.
        /// </summary>
        /// <param name="title">The title of the error.</param>
        /// <param name="description">A description of the error.</param>
        public ErrorAttribute(string title, string description) : this(title, description, null) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ErrorAttribute"/> class with a title, description, and trace information.
        /// </summary>
        /// <param name="title">The title of the error.</param>
        /// <param name="description">A description of the error.</param>
        /// <param name="traces">Trace information for debugging.</param>
        public ErrorAttribute(string title, string description, string traces) : this(title, description, traces, null) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ErrorAttribute"/> class with a title, description, trace information, and help link.
        /// </summary>
        /// <param name="title">The title of the error.</param>
        /// <param name="description">A description of the error.</param>
        /// <param name="traces">Trace information for debugging.</param>
        /// <param name="helpLink">A link to help documentation related to the error.</param>
        public ErrorAttribute(string title, string description, string traces, string helpLink)
        {
            Title = title;
            Description = description;
            Traces = traces;
            HelpLink = helpLink;
        }

        /// <summary>
        ///     Gets the title of the error.
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     Gets the description of the error.
        /// </summary>
        public string Description { get; }

        /// <summary>
        ///     Gets the trace information for debugging purposes.
        /// </summary>
        public string Traces { get; }

        /// <summary>
        ///     Gets the help link associated with the error.
        /// </summary>
        public string HelpLink { get; }
    }
}