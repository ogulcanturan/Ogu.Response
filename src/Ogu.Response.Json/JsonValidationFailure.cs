using Ogu.Response.Abstractions;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public class JsonValidationFailure : IResponseValidationFailure
    {
        public JsonValidationFailure(string propertyName, string message, object attemptedValue = null, Severity severity = Severity.Error, string code = null)
        {
            PropertyName = propertyName;
            Message = message;
            AttemptedValue = attemptedValue;
            Severity = severity;
            Code = code;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string PropertyName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object AttemptedValue { get; set; }

        public Severity Severity { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Code { get; set; }
    }
}