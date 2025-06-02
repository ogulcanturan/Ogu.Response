using Ogu.Response.Abstractions;

namespace Ogu.Response
{
    public static partial class ValidationRules
    {
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
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="greaterThan">The integer value the property value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is greater than the specified threshold.
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
        /// var parsedId = idGreaterThanRule.GetStoredValue&lt;short&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, string propertyValue, short greaterThan)
        {
            return new ValidationRule(ValidationFailures.GreaterThan(propertyName, propertyValue, greaterThan),
                (v) =>
                {
                    if (!short.TryParse(propertyValue, out var parsedValue) || parsedValue <= greaterThan)
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
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="greaterThan">The integer value the property value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is greater than the specified threshold.
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
        public static ValidationRule GreaterThanRule(string propertyName, string propertyValue, int greaterThan)
        {
            return new ValidationRule(ValidationFailures.GreaterThan(propertyName, propertyValue, greaterThan),
                (v) =>
                {
                    if (!int.TryParse(propertyValue, out var parsedValue) || parsedValue <= greaterThan)
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
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="greaterThan">The long value the property value should be greater than.</param>
        /// <returns>
        ///     A <see cref="ValidationRule"/> that checks if the property value is greater than the specified threshold.
        ///     The rule will store the parsed long value if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is 0 or below.
        /// <code>
        /// var idGreaterThanRule = ValidationRules.GreaterThanRule("Id", id, 0);
        ///
        /// if(idGreaterThanRule.IsFailed())
        ///     return idGreaterThanRule.Failure.ToResponse();
        ///
        /// var parsedId = idGreaterThanRule.GetStoredValue&lt;long&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, string propertyValue, long greaterThan)
        {
            return new ValidationRule(ValidationFailures.GreaterThan(propertyName, propertyValue, greaterThan),
                (v) =>
                {
                    if (!long.TryParse(propertyValue, out var parsedValue) || parsedValue <= greaterThan)
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
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="greaterThan">The integer value the property value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is greater than the specified threshold.
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
        /// var parsedId = idGreaterThanRule.GetStoredValue&lt;float&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, string propertyValue, float greaterThan)
        {
            return new ValidationRule(ValidationFailures.GreaterThan(propertyName, propertyValue, greaterThan),
                (v) =>
                {
                    if (!float.TryParse(propertyValue, out var parsedValue) || parsedValue <= greaterThan)
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
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="greaterThan">The integer value the property value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is greater than the specified threshold.
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
        /// var parsedId = idGreaterThanRule.GetStoredValue&lt;double&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, string propertyValue, double greaterThan)
        {
            return new ValidationRule(ValidationFailures.GreaterThan(propertyName, propertyValue, greaterThan),
                (v) =>
                {
                    if (!double.TryParse(propertyValue, out var parsedValue) || parsedValue <= greaterThan)
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
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="greaterThan">The integer value the property value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is greater than the specified threshold.
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
        /// var parsedId = idGreaterThanRule.GetStoredValue&lt;decimal&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, string propertyValue, decimal greaterThan)
        {
            return new ValidationRule(ValidationFailures.GreaterThan(propertyName, propertyValue, greaterThan),
                (v) =>
                {
                    if (!decimal.TryParse(propertyValue, out var parsedValue) || parsedValue <= greaterThan)
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
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="greaterThan">The integer value the property value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is greater than the specified threshold.
        /// The rule will store the parsed integer value if the validation is successful.
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
        public static ValidationRule GreaterThanRule(string propertyName, short propertyValue, short greaterThan)
        {
            return new ValidationRule(ValidationFailures.GreaterThan(propertyName, propertyValue, greaterThan),
                () => propertyValue > greaterThan);
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
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="greaterThan">The integer value the property value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is greater than the specified threshold.
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
        public static ValidationRule GreaterThanRule(string propertyName, int propertyValue, int greaterThan)
        {
            return new ValidationRule(ValidationFailures.GreaterThan(propertyName, propertyValue, greaterThan),
                () => propertyValue > greaterThan);
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
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="greaterThan">The integer value the property value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is greater than the specified threshold.
        /// The rule will store the parsed integer value if the validation is successful.
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
        public static ValidationRule GreaterThanRule(string propertyName, long propertyValue, long greaterThan)
        {
            return new ValidationRule(ValidationFailures.GreaterThan(propertyName, propertyValue, greaterThan),
                () => propertyValue > greaterThan);
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
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="greaterThan">The integer value the property value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is greater than the specified threshold.
        /// The rule will store the parsed integer value if the validation is successful.
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
        public static ValidationRule GreaterThanRule(string propertyName, float propertyValue, float greaterThan)
        {
            return new ValidationRule(ValidationFailures.GreaterThan(propertyName, propertyValue, greaterThan),
                () => propertyValue > greaterThan);
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
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="greaterThan">The integer value the property value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is greater than the specified threshold.
        /// The rule will store the parsed integer value if the validation is successful.
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
        public static ValidationRule GreaterThanRule(string propertyName, double propertyValue, double greaterThan)
        {
            return new ValidationRule(ValidationFailures.GreaterThan(propertyName, propertyValue, greaterThan),
                () => propertyValue > greaterThan);
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
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="greaterThan">The integer value the property value should be greater than.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is greater than the specified threshold.
        /// The rule will store the parsed integer value if the validation is successful.
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
        public static ValidationRule GreaterThanRule(string propertyName, decimal propertyValue, decimal greaterThan)
        {
            return new ValidationRule(ValidationFailures.GreaterThan(propertyName, propertyValue, greaterThan),
                () => propertyValue > greaterThan);
        }
    }
}