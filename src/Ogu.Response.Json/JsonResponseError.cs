using Ogu.Response.Abstractions;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public class JsonResponseError : IResponseError
    {
        [JsonConstructor]
        public JsonResponseError(string title, string description, string details, string code, string helpLink, IList<IResponseValidationFailure> validationFailures, ErrorType type)
        {
            Title = title;
            Description = description;
            Details = details;
            Code = code; 
            HelpLink = helpLink;
            ValidationFailures = validationFailures ?? new List<IResponseValidationFailure>();
            Type = type;
        }

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

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<IResponseValidationFailure> ValidationFailures { get; }

        public ErrorType Type { get; set; }
    }
}