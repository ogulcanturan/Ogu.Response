using Ogu.Response.Abstractions;

namespace Ogu.Response
{
    public static partial class ValidationRules
    {
        /// <summary>
        /// Creates a validation rule that checks if an attempted value is smaller than a specified threshold.
        /// <para>
        /// The rule parses the property value as a short and compares it against the given threshold (`smallerThan`).
        /// If the value is smaller than the threshold, it is considered valid, and the parsed short is stored, which can be
        /// retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        /// </para>
        /// <para>
        /// If the property value is not a valid short or the value is not smaller than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="smallerThan">The short value the attempted value should be smaller than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is smaller than the specified threshold.
        /// The rule will store the parsed short value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if value is 0 or above.
        /// <code>
        /// var idSmallerThanRule = ValidationRules.SmallerThanRule("Id", id, (short)0);
        ///
        /// if(idSmallerThanRule.IsFailed())
        ///     return idSmallerThanRule.Failure.ToResponse();
        ///
        /// var parsedId = idSmallerThanRule.GetStoredValue&lt;short&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule SmallerThanRule(string propertyName, string attemptedValue, short smallerThan)
        {
            return new ValidationRule(() => ValidationFailures.SmallerThan(propertyName, attemptedValue, smallerThan),
                (v) =>
                {
                    if (!short.TryParse(attemptedValue, out var parsedValue) || parsedValue >= smallerThan)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule that checks if an attempted value is smaller than a specified threshold.
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
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="smallerThan">The integer value the attempted value should be smaller than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is smaller than the specified threshold.
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
        public static ValidationRule SmallerThanRule(string propertyName, string attemptedValue, int smallerThan)
        {
            return new ValidationRule(() => ValidationFailures.SmallerThan(propertyName, attemptedValue, smallerThan),
                (v) =>
                {
                    if (!int.TryParse(attemptedValue, out var parsedValue) || parsedValue >= smallerThan)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule that checks if an attempted value is smaller than a specified threshold.
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
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="smallerThan">The long value the property value should be smaller than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is smaller than the specified threshold.
        /// The rule will store the parsed long value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if value is 0 or above.
        /// <code>
        /// var idSmallerThanRule = ValidationRules.SmallerThanRule("Id", id, 0L);
        ///
        /// if(idSmallerThanRule.IsFailed())
        ///     return idSmallerThanRule.Failure.ToResponse();
        ///
        /// var parsedId = idSmallerThanRule.GetStoredValue&lt;long&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule SmallerThanRule(string propertyName, string attemptedValue, long smallerThan)
        {
            return new ValidationRule(() => ValidationFailures.SmallerThan(propertyName, attemptedValue, smallerThan),
                (v) =>
                {
                    if (!long.TryParse(attemptedValue, out var parsedValue) || parsedValue >= smallerThan)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule that checks if an attempted value is smaller than a specified threshold.
        /// <para>
        /// The rule parses the property value as a float and compares it against the given threshold (`smallerThan`).
        /// If the value is smaller than the threshold, it is considered valid, and the parsed float is stored, which can be
        /// retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        /// </para>
        /// <para>
        /// If the property value is not a valid float or the value is not smaller than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="smallerThan">The float value the attempted value should be smaller than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is smaller than the specified threshold.
        /// The rule will store the parsed float value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if value is 0 or above.
        /// <code>
        /// var idSmallerThanRule = ValidationRules.SmallerThanRule("Id", id, 0f);
        ///
        /// if(idSmallerThanRule.IsFailed())
        ///     return idSmallerThanRule.Failure.ToResponse();
        ///
        /// var parsedId = idSmallerThanRule.GetStoredValue&lt;float&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule SmallerThanRule(string propertyName, string attemptedValue, float smallerThan)
        {
            return new ValidationRule(() => ValidationFailures.SmallerThan(propertyName, attemptedValue, smallerThan),
                (v) =>
                {
                    if (!float.TryParse(attemptedValue, out var parsedValue) || parsedValue >= smallerThan)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule that checks if an attempted value is smaller than a specified threshold.
        /// <para>
        /// The rule parses the property value as a double and compares it against the given threshold (`smallerThan`).
        /// If the value is smaller than the threshold, it is considered valid, and the parsed double is stored, which can be
        /// retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        /// </para>
        /// <para>
        /// If the property value is not a valid double or the value is not smaller than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="smallerThan">The double value the attempted value should be smaller than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is smaller than the specified threshold.
        /// The rule will store the parsed double value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if value is 0 or above.
        /// <code>
        /// var idSmallerThanRule = ValidationRules.SmallerThanRule("Id", id, 0d);
        ///
        /// if(idSmallerThanRule.IsFailed())
        ///     return idSmallerThanRule.Failure.ToResponse();
        ///
        /// var parsedId = idSmallerThanRule.GetStoredValue&lt;double&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule SmallerThanRule(string propertyName, string attemptedValue, double smallerThan)
        {
            return new ValidationRule(() => ValidationFailures.SmallerThan(propertyName, attemptedValue, smallerThan),
                (v) =>
                {
                    if (!double.TryParse(attemptedValue, out var parsedValue) || parsedValue >= smallerThan)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule that checks if an attempted value is smaller than a specified threshold.
        /// <para>
        /// The rule parses the property value as a decimal and compares it against the given threshold (`smallerThan`).
        /// If the value is smaller than the threshold, it is considered valid, and the parsed decimal is stored, which can be
        /// retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        /// </para>
        /// <para>
        /// If the property value is not a valid decimal or the value is not smaller than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="smallerThan">The decimal value the attempted value should be smaller than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is smaller than the specified threshold.
        /// The rule will store the parsed decimal value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if value is 0 or above.
        /// <code>
        /// var idSmallerThanRule = ValidationRules.SmallerThanRule("Id", id, 0m);
        ///
        /// if(idSmallerThanRule.IsFailed())
        ///     return idSmallerThanRule.Failure.ToResponse();
        ///
        /// var parsedId = idSmallerThanRule.GetStoredValue&lt;decimal&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule SmallerThanRule(string propertyName, string attemptedValue, decimal smallerThan)
        {
            return new ValidationRule(() => ValidationFailures.SmallerThan(propertyName, attemptedValue, smallerThan),
                (v) =>
                {
                    if (!decimal.TryParse(attemptedValue, out var parsedValue) || parsedValue >= smallerThan)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule that checks if an attempted value is smaller than a specified threshold.
        /// <para>
        /// The rule parses the property value as a short and compares it against the given threshold (`smallerThan`).
        /// If the value is smaller than the threshold, it is considered valid.
        /// </para>
        /// <para>
        /// If the property value is not a valid short or the value is not smaller than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the attempted value to be validated.</param>
        /// <param name="smallerThan">The short value the attempted value should be smaller than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is smaller than the specified threshold.
        /// The rule will store the parsed short value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if value is 0 or above.
        /// <code>
        /// var idSmallerThanRule = ValidationRules.SmallerThanRule("Id", id, (short)0);
        ///
        /// if(idSmallerThanRule.IsFailed())
        ///     return idSmallerThanRule.Failure.ToResponse();
        /// </code>
        /// </remarks>
        public static ValidationRule SmallerThanRule(string propertyName, short propertyValue, short smallerThan)
        {
            return new ValidationRule(() => ValidationFailures.SmallerThan(propertyName, propertyValue, smallerThan),
                () => propertyValue < smallerThan);
        }

        /// <summary>
        /// Creates a validation rule that checks if an attempted value is smaller than a specified threshold.
        /// <para>
        /// The rule parses the property value as an int and compares it against the given threshold (`smallerThan`).
        /// If the value is smaller than the threshold, it is considered valid.
        /// </para>
        /// <para>
        /// If the property value is not a valid int or the value is not smaller than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the attempted value to be validated.</param>
        /// <param name="smallerThan">The int value the attempted value should be smaller than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is smaller than the specified threshold.
        /// The rule will store the parsed int value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if value is 0 or above.
        /// <code>
        /// var idSmallerThanRule = ValidationRules.SmallerThanRule("Id", id, 0);
        ///
        /// if(idSmallerThanRule.IsFailed())
        ///     return idSmallerThanRule.Failure.ToResponse();
        /// </code>
        /// </remarks>
        public static ValidationRule SmallerThanRule(string propertyName, int propertyValue, int smallerThan)
        {
            return new ValidationRule(() => ValidationFailures.SmallerThan(propertyName, propertyValue, smallerThan),
                () => propertyValue < smallerThan);
        }

        /// <summary>
        /// Creates a validation rule that checks if an attempted value is smaller than a specified threshold.
        /// <para>
        /// The rule parses the property value as a long and compares it against the given threshold (`smallerThan`).
        /// If the value is smaller than the threshold, it is considered valid.
        /// </para>
        /// <para>
        /// If the property value is not a valid long or the value is not smaller than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="smallerThan">The long value the attempted value should be smaller than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is smaller than the specified threshold.
        /// The rule will store the parsed long value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if value is 0 or above.
        /// <code>
        /// var idSmallerThanRule = ValidationRules.SmallerThanRule("Id", id, 0L);
        ///
        /// if(idSmallerThanRule.IsFailed())
        ///     return idSmallerThanRule.Failure.ToResponse();
        /// </code>
        /// </remarks>
        public static ValidationRule SmallerThanRule(string propertyName, long attemptedValue, long smallerThan)
        {
            return new ValidationRule(() => ValidationFailures.SmallerThan(propertyName, attemptedValue, smallerThan),
                () => attemptedValue < smallerThan);
        }

        /// <summary>
        /// Creates a validation rule that checks if an attempted value is smaller than a specified threshold.
        /// <para>
        /// The rule parses the property value as a float and compares it against the given threshold (`smallerThan`).
        /// If the value is smaller than the threshold, it is considered valid.
        /// </para>
        /// <para>
        /// If the property value is not a valid float or the value is not smaller than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="smallerThan">The float value the attempted value should be smaller than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is smaller than the specified threshold.
        /// The rule will store the parsed float value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if value is 0 or above.
        /// <code>
        /// var idSmallerThanRule = ValidationRules.SmallerThanRule("Id", id, 0f);
        ///
        /// if(idSmallerThanRule.IsFailed())
        ///     return idSmallerThanRule.Failure.ToResponse();
        /// </code>
        /// </remarks>
        public static ValidationRule SmallerThanRule(string propertyName, float attemptedValue, float smallerThan)
        {
            return new ValidationRule(() => ValidationFailures.SmallerThan(propertyName, attemptedValue, smallerThan),
                () => attemptedValue < smallerThan);
        }

        /// <summary>
        /// Creates a validation rule that checks if an attempted value is smaller than a specified threshold.
        /// <para>
        /// The rule parses the property value as a double and compares it against the given threshold (`smallerThan`).
        /// If the value is smaller than the threshold, it is considered valid.
        /// </para>
        /// <para>
        /// If the property value is not a valid double or the value is not smaller than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="smallerThan">The double value the attempted value should be smaller than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is smaller than the specified threshold.
        /// The rule will store the parsed double value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if value is 0 or above.
        /// <code>
        /// var idSmallerThanRule = ValidationRules.SmallerThanRule("Id", id, 0d);
        ///
        /// if(idSmallerThanRule.IsFailed())
        ///     return idSmallerThanRule.Failure.ToResponse();
        /// </code>
        /// </remarks>
        public static ValidationRule SmallerThanRule(string propertyName, double attemptedValue, double smallerThan)
        {
            return new ValidationRule(() => ValidationFailures.SmallerThan(propertyName, attemptedValue, smallerThan),
                () => attemptedValue < smallerThan);
        }

        /// <summary>
        /// Creates a validation rule that checks if an attempted value is smaller than a specified threshold.
        /// <para>
        /// The rule parses the property value as a decimal and compares it against the given threshold (`smallerThan`).
        /// If the value is smaller than the threshold, it is considered valid.
        /// </para>
        /// <para>
        /// If the property value is not a valid decimal or the value is not smaller than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="smallerThan">The decimal value the property value should be smaller than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is smaller than the specified threshold.
        /// The rule will store the parsed decimal value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if value is 0 or above.
        /// <code>
        /// var idSmallerThanRule = ValidationRules.SmallerThanRule("Id", id, 0m);
        ///
        /// if(idSmallerThanRule.IsFailed())
        ///     return idSmallerThanRule.Failure.ToResponse();
        /// </code>
        /// </remarks>
        public static ValidationRule SmallerThanRule(string propertyName, decimal attemptedValue, decimal smallerThan)
        {
            return new ValidationRule(() => ValidationFailures.SmallerThan(propertyName, attemptedValue, smallerThan),
                () => attemptedValue < smallerThan);
        }
    }
}