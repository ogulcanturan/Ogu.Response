using Ogu.Response.Abstractions;

namespace Ogu.Response
{
    public static partial class ValidationRules
    {
        /// <summary>
        /// Creates a validation rule to check if an attempted value is not null.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The attempted value to be validated.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is null.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is null.
        /// <code>
        /// var notNullRule = ValidationRules.NotNullRule("Data", data);
        ///
        /// if(notNullRule.IsFailed())
        ///     return notNullRule.Failure.ToResponse();
        ///
        /// ...
        /// </code>
        /// </remarks>
        public static ValidationRule NotNullRule(string propertyName, object attemptedValue)
        {
            return new ValidationRule(() => ValidationFailures.NotNull(propertyName), () => attemptedValue != null);
        }
    }
}