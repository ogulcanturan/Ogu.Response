using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public class JsonResponseError : IResponseError
    {
        [JsonConstructor]
        public JsonResponseError(string title, string description, string traces, string code, string helpLink, List<IResponseValidationFailure> validationFailures, ErrorType type)
        {
            Title = title;
            Description = description;
            Traces = traces;
            Code = code; 
            HelpLink = helpLink;
            Type = type;
            ValidationFailures = validationFailures ?? new List<IResponseValidationFailure>();
        }

        public JsonResponseError(string error) : this(ErrorTitles.BadRequest, error, null,  null, null, null, ErrorType.Custom) { }

        public JsonResponseError(IResponseValidationFailure validationFailure) : this(new List<IResponseValidationFailure> { validationFailure }) { }

        public JsonResponseError(List<IResponseValidationFailure> validationFailures) : this(ErrorTitles.BadRequest, ErrorDescriptions.OneOrMoreValidationErrorsOccurred, null, null, null, validationFailures, ErrorType.Validation) { }

        public JsonResponseError(Exception exception, bool includeTraces) : this(
            ErrorTitles.InternalServerError, 
            exception.Message, 
            includeTraces ? $"{exception.GetType().Name} - {exception}": exception.GetType().Name, 
            exception.HResult.ToString(), exception.HelpLink, null, ErrorType.Exception) { }

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
        public List<IResponseValidationFailure> ValidationFailures { get; set; }
    }
}