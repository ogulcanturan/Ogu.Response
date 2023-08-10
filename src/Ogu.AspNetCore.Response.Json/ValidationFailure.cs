using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Text.Json.Serialization;

namespace Ogu.AspNetCore.Response.Json
{
    public class ValidationFailure : IValidationFailure
    {
        public ValidationFailure(string propertyName, string message, object attemptedValue = null, Severity severity = Severity.Error, string code = null)
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

        public static IValidationFailure[] ToValidationFailures(ModelStateDictionary modelState)
        {
           return modelState.Select(x => x.Value?.Errors.Select(y => new
                    ValidationFailure(x.Key, y.ErrorMessage, x.Value.AttemptedValue)))
                .SelectMany(x => x ?? Enumerable.Empty<IValidationFailure>()).ToArray();
        }
    }
}