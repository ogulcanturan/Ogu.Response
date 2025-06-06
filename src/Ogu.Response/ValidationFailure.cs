﻿using Ogu.Response.Abstractions;

namespace Ogu.Response
{
    /// <summary>
    /// Represents a validation failure for a property, encapsulating information about the failure.
    /// Implements the <see cref="IValidationFailure"/> interface.
    /// </summary>
    public class ValidationFailure : IValidationFailure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationFailure"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property that failed validation.</param>
        /// <param name="message">The error message describing the validation failure.</param>
        /// <param name="attemptedValue">The property value that caused the validation failure, or <c>null</c> if not applicable.</param>
        /// <param name="severity">The severity level associated with the validation failure. Default is <see cref="Severity.Error"/>.</param>
        /// <param name="code">An optional error code representing the type of validation failure.</param>
        public ValidationFailure(string propertyName, string message, object attemptedValue = null, Severity severity = Severity.Error, string code = null)
        {
            PropertyName = propertyName;
            Message = message;
            AttemptedValue = attemptedValue;
            Severity = severity;
            Code = code;
        }

        public string PropertyName { get; }

        public string Message { get; }

        public object AttemptedValue { get; }

        public Severity Severity { get; }

        public string Code { get; }
    }
}