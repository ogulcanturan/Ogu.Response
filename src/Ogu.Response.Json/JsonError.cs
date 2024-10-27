using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public class JsonError : IError
    {
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

        public JsonError(string error) : this(ErrorTitles.Error, error, null,  null, null, null, ErrorType.Custom) { }

        public JsonError(string title, string description) : this(title, description, null, null, null, null, ErrorType.Custom) { }

        public JsonError(IValidationFailure failure) : this(new List<IValidationFailure> { failure }) { }

        public JsonError(List<IValidationFailure> failures) : this(ErrorTitles.BadRequest, ErrorDescriptions.OneOrMoreValidationErrorsOccurred, null, null, null, failures, ErrorType.Validation) { }

        public JsonError(Exception exception, bool includeTraces) : this(
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
        public List<IValidationFailure> ValidationFailures { get; set; }
    }
}