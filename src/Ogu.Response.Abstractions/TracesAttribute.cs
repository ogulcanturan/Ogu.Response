using System;

namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Specifies a traces for a field, used as a metadata attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class TracesAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TracesAttribute"/> class.
        /// </summary>
        /// <param name="traces">Trace information for debugging purposes.</param>
        public TracesAttribute(string traces)
        {
            Traces = traces;
        }

        /// <summary>
        /// Gets the traces associated with the field.
        /// </summary>
        public string Traces { get; }
    }
}