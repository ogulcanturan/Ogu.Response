using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    /// <summary>
    ///     Represents an error that can occur in the application, encapsulating details such as title, description, traces, and validation failures.
    ///     Implements the <see cref="IError"/> interface.
    /// </summary>
    public class JsonError : IError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonError"/> class with detailed error information.
        /// </summary>
        /// <param name="title">The title of the error.</param>
        /// <param name="description">A detailed description of the error.</param>
        /// <param name="traces">Trace information for debugging purposes.</param>
        /// <param name="code">An optional error code associated with the error.</param>
        /// <param name="helpLink">A link to help documentation related to the error.</param>
        /// <param name="failures">A list of validation failures associated with this error, if any.</param>
        /// <param name="type">The type of the error represented by <see cref="ErrorType"/>.</param>
        [JsonConstructor]
        public JsonError(string title, string description, string traces, string code, string helpLink, List<IValidationFailure> failures, ErrorType type)
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
        ///     Initializes a new instance of the <see cref="JsonError"/> class with a custom error message.
        /// </summary>
        /// <param name="error">The custom error message.</param>
        public JsonError(string error) : this(ErrorTitles.Error, error, null,  null, null, new List<IValidationFailure>(), ErrorType.Custom) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonError"/> class with a title and description.
        /// </summary>
        /// <param name="title">The title of the error.</param>
        /// <param name="description">A detailed description of the error.</param>
        public JsonError(string title, string description) : this(title, description, null, null, null, new List<IValidationFailure>(), ErrorType.Custom) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonError"/> class from a single validation failure.
        /// </summary>
        /// <param name="failure">The validation failure that caused the error.</param>
        public JsonError(IValidationFailure failure) : this(new List<IValidationFailure> { failure }) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonError"/> class with a list of validation failures.
        /// </summary>
        /// <param name="failures">A list of validation failures associated with the error.</param>
        public JsonError(List<IValidationFailure> failures) : this(ErrorTitles.ValidationError, ErrorDescriptions.OneOrMoreValidationErrorsOccurred, null, null, null, failures, ErrorType.Validation) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonError"/> class from an exception.
        /// </summary>
        /// <param name="exception">The exception that caused the error.</param>
        /// <param name="traceLevel">Indicates whether to include trace information in the traces.</param>
        public JsonError(Exception exception, ExceptionTraceLevel traceLevel) : this(
            ErrorTitles.Exception, 
            exception.Message, 
            exception.GetConcatenatedExceptionMessages(traceLevel),
            exception.HResult.ToString(), exception.HelpLink, new List<IValidationFailure>(), ErrorType.Exception) { }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Title { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Description { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Traces { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Code { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string HelpLink { get; }

        public ErrorType Type { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<IValidationFailure> ValidationFailures { get; set; }
    }
}