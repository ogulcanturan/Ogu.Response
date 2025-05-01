using System.Collections.Generic;

namespace Ogu.Response.Abstractions
{
    public static class ResponseDefaults
    {
        public static readonly HashSet<int> NoResponseStatusCodes = new HashSet<int> { 204, 205, 304 };

        public static class ErrorTitles
        {
            public const string Exception = "Exception";

            public const string ValidationError = "Validation Error";

            public const string Error = "Error";
        }

        public static class ErrorDescriptions
        {
            public const string OneOrMoreValidationErrorsOccurred = "One or more validation errors occurred.";
        }
    }
}