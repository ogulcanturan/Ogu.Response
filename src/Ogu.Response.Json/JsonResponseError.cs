using Ogu.Response.Abstractions;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public class JsonResponseError : IResponseError
    {
        public JsonResponseError(string title, string description, string details, string code, string helpLink, IResponseValidationFailure[] validationFailures, ErrorType type)
        {
            Title = title;
            Description = description;
            Details = details;
            Code = code; 
            HelpLink = helpLink;
            ValidationFailures = validationFailures;
            Type = type;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Title { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Description { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Details { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Code { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string HelpLink { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IResponseValidationFailure[] ValidationFailures { get; set; }

        public ErrorType Type { get; set; }

        public static IResponseErrorBuilder Builder => new JsonResponseErrorBuilder();
    }
}