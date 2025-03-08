using Ogu.Response.Abstractions;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    /// <summary>
    /// Represents a validation failure for a property, encapsulating information about the failure.
    /// Implements the <see cref="IValidationFailure"/> interface.
    /// </summary>
    public class JsonValidationFailure : IValidationFailure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonValidationFailure"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property that failed validation.</param>
        /// <param name="message">The error message describing the validation failure.</param>
        /// <param name="attemptedValue">The property value that caused the validation failure, or <c>null</c> if not applicable.</param>
        /// <param name="severity">The severity level associated with the validation failure. Default is <see cref="Severity.Error"/>.</param>
        /// <param name="code">An optional error code representing the type of validation failure.</param>
        [JsonConstructor]
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