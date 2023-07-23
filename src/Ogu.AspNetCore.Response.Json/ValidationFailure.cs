using System.Text.Json.Serialization;

namespace Ogu.AspNetCore.Response.Json
{
    public class ValidationFailure : IValidationFailure
    {
        public ValidationFailure(string propertyName, string message) : this(propertyName, message, null, Severity.Error, null) { }

        public ValidationFailure(string propertyName, string message, object attemptedValue, Severity severity, string code)
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