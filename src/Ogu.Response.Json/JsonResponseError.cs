using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public class JsonResponseError : IResponseError
    {
        [JsonConstructor]
        public JsonResponseError(string title, string description, string details, string code, string helpLink, List<IResponseValidationFailure> validationFailures, ErrorType type)
        {
            Title = title;
            Description = description;
            Details = details;
            Code = code; 
            HelpLink = helpLink;
            Type = type;
            ValidationFailures = validationFailures ?? new List<IResponseValidationFailure>();
        }

        public JsonResponseError(string error) : this(error, null, ErrorDetails.CustomFailureOccurred, null, null, null, ErrorType.Custom) { }

        public JsonResponseError(IResponseValidationFailure validationFailure) : this(new List<IResponseValidationFailure> { validationFailure }) { }

        public JsonResponseError(List<IResponseValidationFailure> validationFailures) : this(ErrorTitles.BadRequest, null, ErrorDetails.OneOrMoreValidationErrorsOccurred, null, null, validationFailures, ErrorType.Validation) { }

        public JsonResponseError(Exception exception, bool includeTraces) : this(exception.GetType().Name,
            exception.Message,
            includeTraces ? exception.ToString() : ErrorDetails.ExceptionOccurred,
            exception.HResult.ToString(), exception.HelpLink, null, ErrorType.Exception) { }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Title { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Description { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Details { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Code { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string HelpLink { get; }

        public ErrorType Type { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<IResponseValidationFailure> ValidationFailures { get; set; }
    }
}