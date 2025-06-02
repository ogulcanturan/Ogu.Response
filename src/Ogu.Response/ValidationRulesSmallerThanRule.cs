using Ogu.Response.Abstractions;

namespace Ogu.Response
{
    public static partial class ValidationRules
    {
        /// <summary>
        /// Creates a validation rule that checks if a property value is smaller than a specified threshold.
        /// <para>
        /// The rule parses the property value as an integer and compares it against the given threshold (`smallerThan`).
        /// If the value is smaller than the threshold, it is considered valid, and the parsed integer is stored, which can be
        /// retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        /// </para>
        /// <para>
        /// If the property value is not a valid integer or the value is not smaller than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="smallerThan">The integer value the property value should be smaller than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is smaller than the specified threshold.
        /// The rule will store the parsed integer value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if value is 0 or above.
        /// <code>
        /// var idSmallerThanRule = ValidationRules.SmallerThanRule("Id", id, 0);
        ///
        /// if(idSmallerThanRule.IsFailed())
        ///     return idSmallerThanRule.Failure.ToResponse();
        ///
        /// var parsedId = idSmallerThanRule.GetStoredValue&lt;int&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule SmallerThanRule(string propertyName, string propertyValue, int smallerThan)
        {
            return new ValidationRule(ValidationFailures.SmallerThan(propertyName, propertyValue, smallerThan),
                (v) =>
                {
                    if (!int.TryParse(propertyValue, out var parsedValue) || parsedValue >= smallerThan)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule that checks if a property value is smaller than a specified threshold.
        /// <para>
        /// The rule parses the property value as a long and compares it against the given threshold (`smallerThan`).
        /// If the value is smaller than the threshold, it is considered valid, and the parsed long is stored, which can be
        /// retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        /// </para>
        /// <para>
        /// If the property value is not a valid long or the value is not smaller than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="smallerThan">The long value the property value should be smaller than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is smaller than the specified threshold.
        /// The rule will store the parsed long value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if value is 0 or above.
        /// <code>
        /// var idSmallerThanRule = ValidationRules.SmallerThanRule("Id", id, 0);
        ///
        /// if(idSmallerThanRule.IsFailed())
        ///     return idSmallerThanRule.Failure.ToResponse();
        ///
        /// var parsedId = idSmallerThanRule.GetStoredValue&lt;int&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule SmallerThanRule(string propertyName, string propertyValue, long smallerThan)
        {
            return new ValidationRule(ValidationFailures.SmallerThan(propertyName, propertyValue, smallerThan),
                (v) =>
                {
                    if (!long.TryParse(propertyValue, out var parsedValue) || parsedValue >= smallerThan)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }
    }
}