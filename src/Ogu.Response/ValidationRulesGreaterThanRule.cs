using Ogu.Response.Abstractions;

namespace Ogu.Response
{
    public static partial class ValidationRules
    {
        /// <summary>
        /// Creates a validation rule to check if a property value is greater than a specified threshold.
        /// <para>
        ///     The rule parses the property value as a short and compares it against the given threshold (`greaterThan`).
        ///     If the value is greater than the threshold, it is considered valid, and the parsed short is stored, which can be
        ///     retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        /// </para>
        /// <para>
        ///     If the property value is not a valid short or the value is not greater than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="greaterThan">The short value the attempted value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is greater than the specified threshold.
        /// The rule will store the parsed short value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is 0 or below.
        /// <code>
        /// var idGreaterThanRule = ValidationRules.GreaterThanRule("Id", id, (short)0);
        ///
        /// if(idGreaterThanRule.IsFailed())
        ///     return idGreaterThanRule.Failure.ToResponse();
        ///
        /// var parsedId = idGreaterThanRule.GetStoredValue&lt;short&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, string attemptedValue, short greaterThan)
        {
            return new ValidationRule(() => ValidationFailures.GreaterThan(propertyName, attemptedValue, greaterThan),
                (v) =>
                {
                    if (!short.TryParse(attemptedValue, out var parsedValue) || parsedValue <= greaterThan)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is greater than a specified threshold.
        /// <para>
        ///     The rule parses the property value as an integer and compares it against the given threshold (`greaterThan`).
        ///     If the value is greater than the threshold, it is considered valid, and the parsed integer is stored, which can be
        ///     retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        /// </para>
        /// <para>
        ///     If the property value is not a valid integer or the value is not greater than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="greaterThan">The integer value the attempted value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is greater than the specified threshold.
        /// The rule will store the parsed integer value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is 0 or below.
        /// <code>
        /// var idGreaterThanRule = ValidationRules.GreaterThanRule("Id", id, 0);
        ///
        /// if(idGreaterThanRule.IsFailed())
        ///     return idGreaterThanRule.Failure.ToResponse();
        ///
        /// var parsedId = idGreaterThanRule.GetStoredValue&lt;int&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, string attemptedValue, int greaterThan)
        {
            return new ValidationRule(() => ValidationFailures.GreaterThan(propertyName, attemptedValue, greaterThan),
                (v) =>
                {
                    if (!int.TryParse(attemptedValue, out var parsedValue) || parsedValue <= greaterThan)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is greater than a specified threshold.
        /// <para>
        ///     The rule parses the property value as a long and compares it against the given threshold (`greaterThan`).
        ///     If the value is greater than the threshold, it is considered valid, and the parsed long is stored, which can be
        ///     retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        /// </para>
        /// <para>
        ///     If the property value is not a valid long or the value is not greater than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="greaterThan">The long value the attempted value should be greater than.</param>
        /// <returns>
        ///     A <see cref="ValidationRule"/> that checks if the attempted value is greater than the specified threshold.
        ///     The rule will store the parsed long value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is 0 or below.
        /// <code>
        /// var idGreaterThanRule = ValidationRules.GreaterThanRule("Id", id, 0L);
        ///
        /// if(idGreaterThanRule.IsFailed())
        ///     return idGreaterThanRule.Failure.ToResponse();
        ///
        /// var parsedId = idGreaterThanRule.GetStoredValue&lt;long&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, string attemptedValue, long greaterThan)
        {
            return new ValidationRule(() => ValidationFailures.GreaterThan(propertyName, attemptedValue, greaterThan),
                (v) =>
                {
                    if (!long.TryParse(attemptedValue, out var parsedValue) || parsedValue <= greaterThan)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is greater than a specified threshold.
        /// <para>
        ///     The rule parses the property value as a float and compares it against the given threshold (`greaterThan`).
        ///     If the value is greater than the threshold, it is considered valid, and the parsed float is stored, which can be
        ///     retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        /// </para>
        /// <para>
        ///     If the property value is not a valid float or the value is not greater than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="greaterThan">The float value the attempted value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is greater than the specified threshold.
        /// The rule will store the parsed float value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is 0 or below.
        /// <code>
        /// var idGreaterThanRule = ValidationRules.GreaterThanRule("Id", id, 0f);
        ///
        /// if(idGreaterThanRule.IsFailed())
        ///     return idGreaterThanRule.Failure.ToResponse();
        ///
        /// var parsedId = idGreaterThanRule.GetStoredValue&lt;float&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, string attemptedValue, float greaterThan)
        {
            return new ValidationRule(() => ValidationFailures.GreaterThan(propertyName, attemptedValue, greaterThan),
                (v) =>
                {
                    if (!float.TryParse(attemptedValue, out var parsedValue) || parsedValue <= greaterThan)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is greater than a specified threshold.
        /// <para>
        ///     The rule parses the property value as a double and compares it against the given threshold (`greaterThan`).
        ///     If the value is greater than the threshold, it is considered valid, and the parsed double is stored, which can be
        ///     retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        /// </para>
        /// <para>
        ///     If the property value is not a valid double or the value is not greater than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="greaterThan">The double value the attempted value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is greater than the specified threshold.
        /// The rule will store the parsed double value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is 0 or below.
        /// <code>
        /// var idGreaterThanRule = ValidationRules.GreaterThanRule("Id", id, 0d);
        ///
        /// if(idGreaterThanRule.IsFailed())
        ///     return idGreaterThanRule.Failure.ToResponse();
        ///
        /// var parsedId = idGreaterThanRule.GetStoredValue&lt;double&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, string attemptedValue, double greaterThan)
        {
            return new ValidationRule(() => ValidationFailures.GreaterThan(propertyName, attemptedValue, greaterThan),
                (v) =>
                {
                    if (!double.TryParse(attemptedValue, out var parsedValue) || parsedValue <= greaterThan)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is greater than a specified threshold.
        /// <para>
        ///     The rule parses the property value as a decimal and compares it against the given threshold (`greaterThan`).
        ///     If the value is greater than the threshold, it is considered valid, and the parsed decimal is stored, which can be
        ///     retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        /// </para>
        /// <para>
        ///     If the property value is not a valid decimal or the value is not greater than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="greaterThan">The decimal value the attempted value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is greater than the specified threshold.
        /// The rule will store the parsed decimal value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is 0 or below.
        /// <code>
        /// var idGreaterThanRule = ValidationRules.GreaterThanRule("Id", id, 0m);
        ///
        /// if(idGreaterThanRule.IsFailed())
        ///     return idGreaterThanRule.Failure.ToResponse();
        ///
        /// var parsedId = idGreaterThanRule.GetStoredValue&lt;decimal&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, string attemptedValue, decimal greaterThan)
        {
            return new ValidationRule(() => ValidationFailures.GreaterThan(propertyName, attemptedValue, greaterThan),
                (v) =>
                {
                    if (!decimal.TryParse(attemptedValue, out var parsedValue) || parsedValue <= greaterThan)
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is greater than a specified threshold.
        /// <para>
        ///     The rule parses the property value as a short and compares it against the given threshold (`greaterThan`).
        ///     If the value is greater than the threshold, it is considered valid.
        /// </para>
        /// <para>
        ///     If the property value is not a valid short or the value is not greater than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="greaterThan">The short value the attempted value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is greater than the specified threshold.
        /// The rule will store the parsed short value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is 0 or below.
        /// <code>
        /// var idGreaterThanRule = ValidationRules.GreaterThanRule("Id", id, (short)0);
        ///
        /// if(idGreaterThanRule.IsFailed())
        ///     return idGreaterThanRule.Failure.ToResponse();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, short attemptedValue, short greaterThan)
        {
            return new ValidationRule(() => ValidationFailures.GreaterThan(propertyName, attemptedValue, greaterThan),
                () => attemptedValue > greaterThan);
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is greater than a specified threshold.
        /// <para>
        ///     The rule parses the property value as an integer and compares it against the given threshold (`greaterThan`).
        ///     If the value is greater than the threshold, it is considered valid.
        /// </para>
        /// <para>
        ///     If the property value is not a valid integer or the value is not greater than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="greaterThan">The integer value the attempted value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is greater than the specified threshold.
        /// The rule will store the parsed integer value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is 0 or below.
        /// <code>
        /// var idGreaterThanRule = ValidationRules.GreaterThanRule("Id", id, 0);
        ///
        /// if(idGreaterThanRule.IsFailed())
        ///     return idGreaterThanRule.Failure.ToResponse();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, int attemptedValue, int greaterThan)
        {
            return new ValidationRule(() => ValidationFailures.GreaterThan(propertyName, attemptedValue, greaterThan),
                () => attemptedValue > greaterThan);
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is greater than a specified threshold.
        /// <para>
        ///     The rule parses the property value as an long and compares it against the given threshold (`greaterThan`).
        ///     If the value is greater than the threshold, it is considered valid.
        /// </para>
        /// <para>
        ///     If the property value is not a valid long or the value is not greater than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="greaterThan">The long value the attempted value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is greater than the specified threshold.
        /// The rule will store the parsed long value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is 0 or below.
        /// <code>
        /// var idGreaterThanRule = ValidationRules.GreaterThanRule("Id", id, 0L);
        ///
        /// if(idGreaterThanRule.IsFailed())
        ///     return idGreaterThanRule.Failure.ToResponse();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, long attemptedValue, long greaterThan)
        {
            return new ValidationRule(() => ValidationFailures.GreaterThan(propertyName, attemptedValue, greaterThan),
                () => attemptedValue > greaterThan);
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is greater than a specified threshold.
        /// <para>
        ///     The rule parses the property value as a float and compares it against the given threshold (`greaterThan`).
        ///     If the value is greater than the threshold, it is considered valid.
        /// </para>
        /// <para>
        ///     If the property value is not a valid float or the value is not greater than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="greaterThan">The float value the attempted value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is greater than the specified threshold.
        /// The rule will store the parsed float value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is 0 or below.
        /// <code>
        /// var idGreaterThanRule = ValidationRules.GreaterThanRule("Id", id, 0f);
        ///
        /// if(idGreaterThanRule.IsFailed())
        ///     return idGreaterThanRule.Failure.ToResponse();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, float attemptedValue, float greaterThan)
        {
            return new ValidationRule(() => ValidationFailures.GreaterThan(propertyName, attemptedValue, greaterThan),
                () => attemptedValue > greaterThan);
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is greater than a specified threshold.
        /// <para>
        ///     The rule parses the property value as a double and compares it against the given threshold (`greaterThan`).
        ///     If the value is greater than the threshold, it is considered valid.
        /// </para>
        /// <para>
        ///     If the property value is not a valid double or the value is not greater than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="greaterThan">The double value the attempted value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is greater than the specified threshold.
        /// The rule will store the parsed double value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is 0 or below.
        /// <code>
        /// var idGreaterThanRule = ValidationRules.GreaterThanRule("Id", id, 0d);
        ///
        /// if(idGreaterThanRule.IsFailed())
        ///     return idGreaterThanRule.Failure.ToResponse();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, double attemptedValue, double greaterThan)
        {
            return new ValidationRule(() => ValidationFailures.GreaterThan(propertyName, attemptedValue, greaterThan),
                () => attemptedValue > greaterThan);
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is greater than a specified threshold.
        /// <para>
        ///     The rule parses the property value as a decimal and compares it against the given threshold (`greaterThan`).
        ///     If the value is greater than the threshold, it is considered valid.
        /// </para>
        /// <para>
        ///     If the property value is not a valid decimal or the value is not greater than the threshold, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="greaterThan">The decimal value the attempted value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is greater than the specified threshold.
        /// The rule will store the parsed decimal value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is 0 or below.
        /// <code>
        /// var idGreaterThanRule = ValidationRules.GreaterThanRule("Id", id, 0m);
        ///
        /// if(idGreaterThanRule.IsFailed())
        ///     return idGreaterThanRule.Failure.ToResponse();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, decimal attemptedValue, decimal greaterThan)
        {
            return new ValidationRule(() => ValidationFailures.GreaterThan(propertyName, attemptedValue, greaterThan),
                () => attemptedValue > greaterThan);
        }
    }
}