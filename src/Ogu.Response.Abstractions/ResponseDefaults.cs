using System.Collections.Generic;

namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Contains default values and constants used when building responses,
    /// particularly for error handling and status code behavior.
    /// </summary>
    public static class ResponseDefaults
    {
        /// <summary>
        /// HTTP status codes that should not include a response body.
        /// Commonly includes 204 (No Content), 205 (Reset Content), and 304 (Not Modified).
        /// </summary>
        public static readonly HashSet<int> NoResponseStatusCodes = new HashSet<int> { 204, 205, 304 };

        /// <summary>
        /// Contains default error titles used in standardized error responses.
        /// </summary>
        public static class ErrorTitles
        {
            /// <summary>
            /// Default title used when an unhandled exception occurs.
            /// </summary>
            public const string Exception = "Exception";

            /// <summary>
            /// Title used when a validation error is returned.
            /// </summary>
            public const string ValidationError = "Validation Error";

            /// <summary>
            /// General-purpose error title.
            /// </summary>
            public const string Error = "Error";
        }

        /// <summary>
        /// Contains default error descriptions used in standardized error responses.
        /// </summary>
        public static class ErrorDescriptions
        {
            /// <summary>
            /// Description used when one or more validation errors are present in the request.
            /// </summary>
            public const string OneOrMoreValidationErrorsOccurred = "One or more validation errors occurred.";

            /// <summary>
            /// Generic fallback description used when an unexpected error occurs.
            /// </summary>
            public const string SomethingWentWrong = "Oops! Something went wrong.";
        }
    }
}