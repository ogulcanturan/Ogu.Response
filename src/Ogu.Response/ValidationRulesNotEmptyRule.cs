using Ogu.Response.Abstractions;
using System;

namespace Ogu.Response
{
    public static partial class ValidationRules
    {
        /// <summary>
        /// Creates a validation rule to check if a property value is empty.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The property value to be validated.</param>
        /// <returns></returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is null or white space.
        /// <code>
        /// var notEmptyRule = ValidationRules.NotEmptyRule("Data", data);
        ///
        /// if(notEmptyRule.IsFailed())
        ///     return notEmptyRule.Failure.ToResponse();
        ///
        /// ...
        /// </code>
        /// </remarks>
        public static ValidationRule NotEmptyRule(string propertyName, string propertyValue)
        {
            return new ValidationRule(ValidationFailures.NotEmpty(propertyName), () => string.IsNullOrWhiteSpace(propertyValue));
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is empty.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The property value to be validated.</param>
        /// <returns></returns>
        public static ValidationRule NotEmptyRule(string propertyName, Guid propertyValue)
        {
            return new ValidationRule(ValidationFailures.NotEmpty(propertyName), () => propertyValue == Guid.Empty);
        }
    }
}