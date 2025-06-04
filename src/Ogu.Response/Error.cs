using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ogu.Response
{
    /// <summary>
    /// Represents an error that can occur in the application, encapsulating details such as title, description, traces, and validation failures.
    /// Implements the <see cref="IError"/> interface.
    /// </summary>
    public class Error : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class with detailed error information.
        /// </summary>
        /// <param name="title">The title of the error.</param>
        /// <param name="description">A detailed description of the error.</param>
        /// <param name="traces">Trace information for debugging purposes.</param>
        /// <param name="code">An optional error code associated with the error.</param>
        /// <param name="helpLink">A link to help documentation related to the error.</param>
        /// <param name="failures">A list of validation failures associated with this error, if any.</param>
        /// <param name="type">The type of the error represented by <see cref="ErrorType"/>.</param>
        public Error(string title, string description, string traces, string code, string helpLink, List<IValidationFailure> failures, ErrorType type)
        {
            Title = title;
            Description = description;
            Traces = traces;
            Code = code; 
            HelpLink = helpLink;
            Type = type;
            ValidationFailures = failures ?? new List<IValidationFailure>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class with a custom error message.
        /// </summary>
        /// <param name="error">The custom error message.</param>
        public Error(string error) : this(ResponseDefaults.ErrorTitles.Error, error, null,  null, null, new List<IValidationFailure>(), ErrorType.Custom) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class with a title and description.
        /// </summary>
        /// <param name="title">The title of the error.</param>
        /// <param name="description">A detailed description of the error.</param>
        public Error(string title, string description) : this(title, description, null, null, null, new List<IValidationFailure>(), ErrorType.Custom) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class from a single validation failure.
        /// </summary>
        /// <param name="failure">The validation failure that caused the error.</param>
        public Error(IValidationFailure failure) : this(new List<IValidationFailure> { failure }) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class with a list of validation failures.
        /// </summary>
        /// <param name="failures">A list of validation failures associated with the error.</param>
        public Error(List<IValidationFailure> failures) : this(ResponseDefaults.ErrorTitles.ValidationError, ResponseDefaults.ErrorDescriptions.OneOrMoreValidationErrorsOccurred, null, null, null, failures, ErrorType.Validation) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class from an exception.
        /// </summary>
        /// <param name="exception">The exception that caused the error.</param>
        /// <param name="traceLevel">Indicates whether to include trace information in the traces.</param>
        public Error(Exception exception, ExceptionTraceLevel traceLevel)
        {
            Title = ResponseDefaults.ErrorTitles.Exception;
            ValidationFailures = new List<IValidationFailure>();
            Type = ErrorType.Exception;

            switch (traceLevel)
            {
                case ExceptionTraceLevel.None:
                default:
                    Description = ResponseDefaults.ErrorDescriptions.SomethingWentWrong;
                    break;
                case ExceptionTraceLevel.Basic:
                    Description = exception.Message;
                    break;
                case ExceptionTraceLevel.Summary:
                    Description = exception.Message;
                    Code = $"{exception.HResult}";
                    HelpLink = exception.HelpLink;

                    var result = new StringBuilder(); // Todo: Use -> StringBuilderPool

                    var ex = exception;

                    while (ex != null)
                    {
                        result.Append(ex.GetType().Name);
                        result.Append(": ");
                        result.Append(ex.Message);
                        result.Append(ExceptionSeparator);

                        ex = ex.InnerException;
                    }

                    if (result.Length >= ExceptionSeparator.Length)
                    {
                        result.Length -= ExceptionSeparator.Length;
                    }

                    Traces = result.ToString();

                    break;

                case ExceptionTraceLevel.Full:
                    Description = exception.Message;
                    Code = $"{exception.HResult}";
                    HelpLink = exception.HelpLink;
                    Traces = exception.ToString();

                    break;
            }
        }

        private const string ExceptionSeparator = " -> ";

        public string Title { get; }

        public string Description { get; }

        public string Traces { get; }

        public string Code { get; }

        public string HelpLink { get; }

        public ErrorType Type { get; }

        public List<IValidationFailure> ValidationFailures { get; }
    }
}