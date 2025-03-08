using System.Collections.Generic;

namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Represents an error in the application.
    /// </summary>
    public interface IError
    {
        /// <summary>
        /// The title of the error.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// The description of the error.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The trace information for debugging.
        /// </summary>
        string Traces { get; }

        /// <summary>
        /// The unique code associated with the error.
        /// </summary>
        string Code { get; }

        /// <summary>
        /// A link to further information about the error.
        /// </summary>
        string HelpLink { get; }

        /// <summary>
        /// The type of the error.
        /// <list>
        ///     <item>Custom (0): Custom-defined errors.</item>
        ///     <item>Validation (1): Errors related to validation failures.</item>
        ///     <item>Exception (2): Standard exception that occur during processing.</item>
        /// </list>
        /// </summary>
        ErrorType Type { get; }

        /// <summary>
        /// A list of validation failures associated with the error.
        /// </summary>
        List<IValidationFailure> ValidationFailures { get; set; }
    }
}