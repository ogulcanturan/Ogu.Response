using Ogu.Response.Abstractions;

namespace Ogu.Response
{
    public static partial class ValidationRules
    {
        /// <summary>
        /// Creates a validation rule to check if a property value is equal to a specified value.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="equalTo">The integer value the property value should be equal to.</param>
        /// <returns>
        ///  A <see cref="ValidationRule"/> that checks if the property value is equal to the specified value.
        ///  The rule will store the parsed integer value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: Validation failure occurs if the value is not equal to the specified threshold.
        /// <code>
        /// var equalToRule = ValidationRules.EqualToRule("Id", id, 10);
        /// 
        /// if (equalToRule.IsFailed())
        ///     return equalToRule.Failure.ToResponse();
        /// 
        /// var parsedId = equalToRule.GetStoredValue&lt;int&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule EqualToRule(string propertyName, string propertyValue, int equalTo)
        {
            return new ValidationRule(ValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
                (v) =>
                {
                    if (!int.TryParse(propertyValue, out var parsedValue) || parsedValue != equalTo)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is equal to a specified value.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="equalTo">The long value the property value should be equal to.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is equal to the specified value.
        /// The rule will store the parsed long value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: Validation failure occurs if the value is not equal to the specified threshold.
        /// <code>
        /// var equalToRule = ValidationRules.EqualToRule("Id", id, 10);
        /// 
        /// if (equalToRule.IsFailed())
        ///     return equalToRule.Failure.ToResponse();
        /// 
        /// var parsedId = equalToRule.GetStoredValue&lt;long&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule EqualToRule(string propertyName, string propertyValue, long equalTo)
        {
            return new ValidationRule(ValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
                (v) =>
                {
                    if (!long.TryParse(propertyValue, out var parsedValue) || parsedValue != equalTo)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }
    }
}