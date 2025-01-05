using Ogu.Response.Abstractions;
using System;
using System.Text.Json;

namespace Ogu.Response.Json
{
    public static class JsonValidationRules
    {
        /// <summary>
        ///     Creates a validation rule to check if a property value is equal to a specified value.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="equalTo">The integer value the property value should be equal to.</param>
        /// <returns>
        ///     A <see cref="ValidationRule"/> that checks if the property value is equal to the specified value.
        ///     The rule will store the parsed integer value if the validation is successful.
        /// </returns>
        /// <remarks>
        ///     Example usage: Validation failure occurs if the value is not equal to the specified threshold.
        ///     <code>
        ///     var equalToRule = JsonValidationRules.EqualToRule("Id", id, 10);
        /// 
        ///     if (equalToRule.IsFailed())
        ///         return equalToRule.Failure.ToJsonResponse();
        /// 
        ///     var parsedId = equalToRule.GetStoredValue&lt;int&gt;();
        ///     </code>
        /// </remarks>
        public static ValidationRule EqualToRule(string propertyName, string propertyValue, int equalTo)
        {
            return new ValidationRule(JsonValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
                (v) =>
                {
                    if (!int.TryParse(propertyValue, out var parsedValue) || parsedValue != equalTo)
                    {
                        return true;
                    }

                    v.Store(parsedValue);

                    return false;
                });
        }

        /// <summary>
        ///     Creates a validation rule to check if a property value is equal to a specified value.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="equalTo">The long value the property value should be equal to.</param>
        /// <returns>
        ///     A <see cref="ValidationRule"/> that checks if the property value is equal to the specified value.
        ///     The rule will store the parsed long value if the validation is successful.
        /// </returns>
        /// <remarks>
        ///     Example usage: Validation failure occurs if the value is not equal to the specified threshold.
        ///     <code>
        ///     var equalToRule = JsonValidationRules.EqualToRule("Id", id, 10);
        /// 
        ///     if (equalToRule.IsFailed())
        ///         return equalToRule.Failure.ToJsonResponse();
        /// 
        ///     var parsedId = equalToRule.GetStoredValue&lt;long&gt;();
        ///     </code>
        /// </remarks>
        public static ValidationRule EqualToRule(string propertyName, string propertyValue, long equalTo)
        {
            return new ValidationRule(JsonValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
                (v) =>
                {
                    if (!long.TryParse(propertyValue, out var parsedValue) || parsedValue != equalTo)
                    {
                        return true;
                    }

                    v.Store(parsedValue);

                    return false;
                });
        }

        /// <summary>
        ///     Creates a validation rule to check if a property value is greater than a specified threshold.
        ///     <para>
        ///         The rule parses the property value as an integer and compares it against the given threshold (`greaterThan`).
        ///         If the value is greater than the threshold, it is considered valid, and the parsed integer is stored, which can be
        ///         retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        ///     </para>
        ///     <para>
        ///         If the property value is not a valid integer or the value is not greater than the threshold, the validation fails.
        ///     </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="greaterThan">The integer value the property value should be greater than.</param>
        /// <returns>
        ///     A <see cref="ValidationRule"/> that checks if the property value is greater than the specified threshold.
        ///     The rule will store the parsed integer value if the validation is successful.
        /// </returns>
        /// <remarks>
        ///     Example usage: ValidationFailure occurs if the value is 0 or below.
        ///     <code>
        ///     var idGreaterThanRule = JsonValidationRules.GreaterThanRule("Id", id, 0);
        ///
        ///     if(idGreaterThanRule.IsFailed())
        ///         return idGreaterThanRule.Failure.ToJsonResponse();
        ///
        ///     var parsedId = idGreaterThanRule.GetStoredValue&lt;int&gt;()
        ///     </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, string propertyValue, int greaterThan)
        {
            return new ValidationRule(JsonValidationFailures.GreaterThan(propertyName, propertyValue, greaterThan),
                (v) =>
                {
                    if (!int.TryParse(propertyValue, out var parsedValue) || parsedValue <= greaterThan)
                    {
                        return true;
                    }

                    v.Store(parsedValue);

                    return false;
                });
        }

        /// <summary>
        ///     Creates a validation rule to check if a property value is greater than a specified threshold.
        ///     <para>
        ///         The rule parses the property value as a long and compares it against the given threshold (`greaterThan`).
        ///         If the value is greater than the threshold, it is considered valid, and the parsed long is stored, which can be
        ///         retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        ///     </para>
        ///     <para>
        ///         If the property value is not a valid long or the value is not greater than the threshold, the validation fails.
        ///     </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="greaterThan">The long value the property value should be greater than.</param>
        /// <returns>
        ///     A <see cref="ValidationRule"/> that checks if the property value is greater than the specified threshold.
        ///     The rule will store the parsed long value if the validation is successful.
        /// </returns>
        /// <remarks>
        ///     Example usage: ValidationFailure occurs if the value is 0 or below.
        ///     <code>
        ///     var idGreaterThanRule = JsonValidationRules.GreaterThanRule("Id", id, 0);
        ///
        ///     if(idGreaterThanRule.IsFailed())
        ///         return idGreaterThanRule.Failure.ToJsonResponse();
        ///
        ///     var parsedId = idGreaterThanRule.GetStoredValue&lt;long&gt;()
        ///     </code>
        /// </remarks>
        public static ValidationRule GreaterThanRule(string propertyName, string propertyValue, long greaterThan)
        {
            return new ValidationRule(JsonValidationFailures.GreaterThan(propertyName, propertyValue, greaterThan),
                (v) =>
                {
                    if (!long.TryParse(propertyValue, out var parsedValue) || parsedValue <= greaterThan)
                    {
                        return true;
                    }

                    v.Store(parsedValue);

                    return false;
                });
        }

        /// <summary>
        ///     Creates a validation rule that checks if a property value is smaller than a specified threshold.
        ///     <para>
        ///         The rule parses the property value as an integer and compares it against the given threshold (`smallerThan`).
        ///         If the value is smaller than the threshold, it is considered valid, and the parsed integer is stored, which can be
        ///         retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        ///     </para>
        ///     <para>
        ///         If the property value is not a valid integer or the value is not smaller than the threshold, the validation fails.
        ///     </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="smallerThan">The integer value the property value should be smaller than.</param>
        /// <returns>
        ///     A <see cref="ValidationRule"/> that checks if the property value is smaller than the specified threshold.
        ///     The rule will store the parsed integer value if the validation is successful.
        /// </returns>
        /// <remarks>
        ///     Example usage: ValidationFailure occurs if value is 0 or above.
        ///     <code>
        ///     var idSmallerThanRule = JsonValidationRules.SmallerThanRule("Id", id, 0);
        ///
        ///     if(idSmallerThanRule.IsFailed())
        ///         return idSmallerThanRule.Failure.ToJsonResponse();
        ///
        ///     var parsedId = idSmallerThanRule.GetStoredValue&lt;int&gt;()
        ///     </code>
        /// </remarks>
        public static ValidationRule SmallerThanRule(string propertyName, string propertyValue, int smallerThan)
        {
            return new ValidationRule(JsonValidationFailures.SmallerThan(propertyName, propertyValue, smallerThan),
                (v) =>
                {
                    if (!int.TryParse(propertyValue, out var parsedValue) || parsedValue >= smallerThan)
                    {
                        return true;
                    }

                    v.Store(parsedValue);

                    return false;
                });
        }

        /// <summary>
        ///     Creates a validation rule that checks if a property value is smaller than a specified threshold.
        ///     <para>
        ///         The rule parses the property value as a long and compares it against the given threshold (`smallerThan`).
        ///         If the value is smaller than the threshold, it is considered valid, and the parsed long is stored, which can be
        ///         retrieved later via the response's <see cref="ValidationRule.GetStoredValue{T}"/> method.
        ///     </para>
        ///     <para>
        ///         If the property value is not a valid long or the value is not smaller than the threshold, the validation fails.
        ///     </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="smallerThan">The long value the property value should be smaller than.</param>
        /// <returns>
        ///     A <see cref="ValidationRule"/> that checks if the property value is smaller than the specified threshold.
        ///     The rule will store the parsed long value if the validation is successful.
        /// </returns>
        /// <remarks>
        ///     Example usage: ValidationFailure occurs if value is 0 or above.
        ///     <code>
        ///     var idSmallerThanRule = JsonValidationRules.SmallerThanRule("Id", id, 0);
        ///
        ///     if(idSmallerThanRule.IsFailed())
        ///         return idSmallerThanRule.Failure.ToJsonResponse();
        ///
        ///     var parsedId = idSmallerThanRule.GetStoredValue&lt;int&gt;()
        ///     </code>
        /// </remarks>
        public static ValidationRule SmallerThanRule(string propertyName, string propertyValue, long smallerThan)
        {
            return new ValidationRule(JsonValidationFailures.SmallerThan(propertyName, propertyValue, smallerThan),
                (v) =>
                {
                    if (!long.TryParse(propertyValue, out var parsedValue) || parsedValue >= smallerThan)
                    {
                        return true;
                    }

                    v.Store(parsedValue);

                    return false;
                });
        }

        /// <summary>
        ///     Creates a validation rule to check if a property value is empty.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The property value to be validated.</param>
        /// <returns></returns>
        /// <remarks>
        ///     Example usage: ValidationFailure occurs if the value is null or white space.
        ///     <code>
        ///     var notEmptyRule = JsonValidationRules.NotEmptyRule("Data", data);
        ///
        ///     if(notEmptyRule.IsFailed())
        ///         return notEmptyRule.Failure.ToJsonResponse();
        ///
        ///     ...
        ///     </code>
        /// </remarks>
        public static ValidationRule NotEmptyRule(string propertyName, string propertyValue)
        {
            return new ValidationRule(JsonValidationFailures.NotEmpty(propertyName), () => string.IsNullOrWhiteSpace(propertyValue));
        }

        /// <summary>
        ///     Creates a validation rule to check if a property value is empty.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The property value to be validated.</param>
        /// <returns></returns>
        public static ValidationRule NotEmptyRule(string propertyName, Guid propertyValue)
        {
            return new ValidationRule(JsonValidationFailures.NotEmpty(propertyName), () => propertyValue == Guid.Empty);
        }

        /// <summary>
        ///     Creates a validation rule to check if a property value is a valid json string.
        ///     <para>
        ///         The rule attempts to parse the property value using `JsonDocument.Parse`. If valid, the parsed json is stored 
        ///         and can be retrieved via <see cref="ValidationRule.GetStoredValue{T}"/>.
        ///     </para>
        ///     <para>
        ///         If the property value is invalid json, the validation fails.
        ///     </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <returns>
        ///     A <see cref="ValidationRule"/> that checks if the property value is valid json, storing the parsed 
        ///     <see cref="JsonDocument"/> on success.
        /// </returns>
        /// <remarks>
        ///     Example usage: ValidationFailure occurs if the value is not valid json.
        ///     <code>
        ///     var validJsonRule = JsonValidationRules.ValidJsonRule("Data", data);
        ///
        ///     if(validJsonRule.IsFailed())
        ///         return validJsonRule.Failure.ToJsonResponse();
        ///
        ///     var parsedJsonDocument = validJsonRule.GetStoredValue&lt;JsonDocument&gt;()
        ///     </code>
        /// </remarks>
        public static ValidationRule ValidJsonRule(string propertyName, string propertyValue)
        {
            return new ValidationRule(JsonValidationFailures.InvalidJsonFormat(propertyName, propertyValue), (v) =>
            {
                try
                {
                    var jsonDocument = JsonDocument.Parse(propertyValue);

                    v.Store(jsonDocument);

                    return false;
                }
                catch (JsonException)
                {
                    return true;
                }
            });
        }

        /// <summary>
        ///     Creates a validation rule to check if a property value is a valid enum value of the specified enum type.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="enumType">The type of the enum to check against.</param>
        /// <returns>
        ///     A <see cref="ValidationRule"/> that checks if the property value is a valid value of the specified enum.
        ///     If valid, the parsed enum value is stored and can be retrieved via <see cref="ValidationRule.GetStoredValue{T}"/>.
        /// </returns>
        /// <remarks>
        ///     Example usage: Validation failure occurs if the property value is not a valid enum value.
        ///     <code>
        ///     var validEnumRule = JsonValidationRules.ValidEnumRule("MyEnum", "Active", typeof(MyEnum));
        /// 
        ///     if (validEnumRule.IsFailed())
        ///         return validEnumRule.Failure.ToJsonResponse();
        /// 
        ///     var parsedMyEnum = validEnumRule.GetStoredValue&lt;MyEnum&gt;();
        ///     </code>
        /// </remarks>
        public static ValidationRule ValidEnumRule(string propertyName, string propertyValue, Type enumType)
        {
            return new ValidationRule(JsonValidationFailures.InvalidEnumFormat(propertyName, propertyValue, enumType),
                (v) =>
                {
#if NETSTANDARD2_0 || NET462
                    try
                    {
                        var enumValue = Enum.Parse(enumType, propertyValue, true);

                        if (!Enum.IsDefined(enumType, enumValue))
                        {
                            return true;
                        }

                        v.Store(enumValue);

                        return false;
                    }
                    catch (ArgumentException)
                    {
                        return true;
                    }
#else
                    if (Enum.TryParse(enumType, propertyValue, true, out var enumValue) && Enum.IsDefined(enumType, enumValue))
                    {
                        v.Store(enumValue);

                        return false;
                    }

                    return true;
#endif
                });
        }


        /// <summary>
        ///     Creates a validation rule to check if a property value is a valid boolean string ("true" or "false").
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <returns>
        ///     A <see cref="ValidationRule"/> that checks if the property value is a valid boolean string.
        ///     If valid, the parsed boolean value is stored and can be retrieved via <see cref="ValidationRule.GetStoredValue{T}"/>.
        /// </returns>
        /// <remarks>
        ///     Example usage: Validation failure occurs if the property value is not a valid boolean string ("true" or "false").
        ///     <code>
        ///     var validBooleanRule = JsonValidationRules.ValidBooleanRule("IsActive", isActive);
        /// 
        ///     if (validBooleanRule.IsFailed())
        ///         return validBooleanRule.Failure.ToJsonResponse();
        /// 
        ///     var parsedStatus = validBooleanRule.GetStoredValue&lt;bool&gt;();
        ///     </code>
        /// </remarks>
        public static ValidationRule ValidBooleanRule(string propertyName, string propertyValue)
        {
            return new ValidationRule(JsonValidationFailures.InvalidBooleanFormat(propertyName, propertyValue),
                (v) =>
                {
                    if (!bool.TryParse(propertyValue, out var parsedValue))
                    {
                        return true;
                    }

                    v.Store(parsedValue);

                    return false;
                });
        }

        /// <summary>
        ///     Creates a validation rule to check if a property value is a valid number (integer or floating-point).
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <returns>
        ///     A <see cref="ValidationRule"/> that checks if the property value is a valid number.
        ///     The parsed value is stored if the validation is successful.
        /// </returns>
        /// <remarks>
        ///     Example usage: Validation failure occurs if the property value is not a valid number.
        ///     <code>
        ///     var validNumberRule = JsonValidationRules.ValidNumberRule("Price", "123.45");
        /// 
        ///     if (validNumberRule.IsFailed())
        ///         return validNumberRule.Failure.ToJsonResponse();
        /// 
        ///     var parsedPrice = validNumberRule.GetStoredValue&lt;decimal&gt;();
        ///     </code>
        /// </remarks>
        public static ValidationRule ValidNumberRule(string propertyName, string propertyValue)
        {
            return new ValidationRule(JsonValidationFailures.InvalidNumberFormat(propertyName, propertyValue),
                (v) =>
                {
                    if (long.TryParse(propertyValue, out var parsedLong))
                    {
                        v.Store(parsedLong);
                        return false;
                    }

                    if (!decimal.TryParse(propertyValue, out var parsedDecimal))
                    {
                        return true;
                    }

                    v.Store(parsedDecimal);

                    return false;
                });
        }
    }
}