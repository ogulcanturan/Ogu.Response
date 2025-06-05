using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
#if NETCOREAPP3_1_OR_GREATER
using System.Text.Json;
#endif

namespace Ogu.Response
{
    /// <summary>
    /// Provides a set of validation rules, allowing for validation of various data types and conditions.
    /// </summary>
    public static partial class ValidationRules
    {
#if NETCOREAPP3_1_OR_GREATER
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
        ///     var validJsonRule = ValidationRules.ValidJsonRule("Data", data);
        ///
        ///     if(validJsonRule.IsFailed())
        ///         return validJsonRule.Failure.ToResponse();
        ///
        ///     using var parsedJsonDocument = validJsonRule.GetStoredValue&lt;JsonDocument&gt;()
        ///
        ///     </code>
        /// </remarks>
        public static ValidationRule ValidJsonRule(string propertyName, string propertyValue)
        {
            return new ValidationRule(() => ValidationFailures.InvalidJsonFormat(propertyName, propertyValue), (v) =>
            {
                try
                {
                    var jsonDocument = JsonDocument.Parse(propertyValue);

                    v.Store(jsonDocument);

                    return true;
                }
                catch (JsonException)
                {
                    return false;
                }
            });
        }
#endif

        /// <summary>
        /// Creates a validation rule to check if a property value is a valid enum value of the specified enum type.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum to check against. It must be a value type and an enum.</typeparam>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The value to be validated. It can be of any type (string, int, enum, or null).</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is a valid value of the specified enum.
        /// If valid, the parsed enum value is stored and can be retrieved via <see cref="ValidationRule.GetStoredValue{T}"/>.
        /// </returns>
        /// <remarks>
        /// Example usage: Validation failure occurs if the property value is not a valid enum value.
        /// <code>
        /// var validEnumRule = ValidationRules.ValidEnumRule&lt;MyEnum&gt;("MyEnum", "Active");
        /// 
        /// if (validEnumRule.IsFailed())
        ///     return validEnumRule.Failure.ToResponse();
        /// 
        /// var parsedMyEnum = validEnumRule.GetStoredValue&lt;MyEnum&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule ValidEnumRule<TEnum>(string propertyName, object propertyValue) where TEnum : struct, Enum
        {
            var type = typeof(TEnum);

            return ValidEnumRule(propertyName, propertyValue, type);
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is a valid enum value of the specified enum type.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <param name="enumType">The type of the enum to check against.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is a valid value of the specified enum.
        /// If valid, the parsed enum value is stored and can be retrieved via <see cref="ValidationRule.GetStoredValue{T}"/>.
        /// </returns>
        /// <remarks>
        /// Example usage: Validation failure occurs if the property value is not a valid enum value.
        /// <code>
        /// var validEnumRule = ValidationRules.ValidEnumRule("MyEnum", "Active", typeof(MyEnum));
        /// 
        /// if (validEnumRule.IsFailed())
        ///     return validEnumRule.Failure.ToResponse();
        /// 
        /// var parsedMyEnum = validEnumRule.GetStoredValue&lt;MyEnum&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule ValidEnumRule(string propertyName, object propertyValue, Type enumType)
        {
            return new ValidationRule(() => ValidationFailures.InvalidEnumFormat(propertyName, propertyValue, enumType),
                (v) =>
                {
                    var underlyingType = Nullable.GetUnderlyingType(enumType);

                    var isNullableEnum = underlyingType != null;

                    if (propertyValue == null)
                    {
                        if (!isNullableEnum)
                        {
                            return false;
                        }

                        v.Store(null);

                        return true;
                    }

                    var underlyingEnumType = isNullableEnum ? underlyingType : enumType;

                    switch (propertyValue)
                    {
                        case string stringValue:
#if NETSTANDARD2_0 || NET462
                            try
                            {
                                var enumValue = Enum.Parse(underlyingEnumType, stringValue, true);

                                if (!Enum.IsDefined(underlyingEnumType, enumValue))
                                {
                                    return false;
                                }

                                v.Store(enumValue);

                                return true;
                            }
                            catch (ArgumentException)
                            {
                                return false;
                            }
#else
                            if (!Enum.TryParse(underlyingEnumType, stringValue, true, out var enumValue) ||
                                !Enum.IsDefined(underlyingEnumType, enumValue))
                            {
                                return false;
                            }

                            v.Store(enumValue);

                            return true;
#endif
                        default:

                            if (!Enum.IsDefined(underlyingEnumType, propertyValue))
                            {
                                return false;
                            }

                            v.Store(propertyValue);

                            return true;
                    }
                });
        }


        /// <summary>
        /// Creates a validation rule to check if a property value is a valid boolean string ("true" or "false").
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is a valid boolean string.
        /// If valid, the parsed boolean value is stored and can be retrieved via <see cref="ValidationRule.GetStoredValue{T}"/>.
        /// </returns>
        /// <remarks>
        /// Example usage: Validation failure occurs if the property value is not a valid boolean string ("true" or "false").
        /// <code>
        /// var validBooleanRule = ValidationRules.ValidBooleanRule("IsActive", isActive);
        ///
        /// if (validBooleanRule.IsFailed())
        ///     return validBooleanRule.Failure.ToResponse();
        ///
        /// var parsedStatus = validBooleanRule.GetStoredValue&lt;bool&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule ValidBooleanRule(string propertyName, string propertyValue)
        {
            return new ValidationRule(() => ValidationFailures.InvalidBooleanFormat(propertyName, propertyValue),
                (v) =>
                {
                    if (!bool.TryParse(propertyValue, out var parsedValue))
                    {
                        return false;
                    }

                    v.Store(parsedValue);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is a valid number (long or floating-point).
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is a valid number.
        /// The parsed value is stored if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: Validation failure occurs if the property value is not a valid number.
        /// <code>
        /// var validNumberRule = ValidationRules.ValidNumberRule("Price", "123.45");
        /// 
        /// if (validNumberRule.IsFailed())
        ///     return validNumberRule.Failure.ToResponse();
        /// 
        /// var parsedPrice = validNumberRule.GetStoredValue&lt;decimal&gt;(); // returns -> 123.45m
        /// var parsedPrice = validNumberRule.GetStoredValue&lt;long&gt;();    // returns -> 123
        /// </code>
        /// </remarks>
        public static ValidationRule ValidNumberRule(string propertyName, string propertyValue)
        {
            return new ValidationRule(() => ValidationFailures.InvalidNumberFormat(propertyName, propertyValue),
                (v) =>
                {
                    if (long.TryParse(propertyValue, out var parsedLong))
                    {
                        v.Store(parsedLong);
                        return true;
                    }

                    if (!decimal.TryParse(propertyValue, out var parsedDecimal))
                    {
                        return false;
                    }

                    v.Store(parsedDecimal);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is a valid HashSet.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="propertyValue">The string representation of the property value to be validated.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the property value is a valid HashSet.
        /// The parsed value is stored if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: Validation failure occurs if the property value is not a valid HashSet.
        /// <code>
        /// var validHashSetRule = ValidationRules.ValidHashSetRule("Prices", "1,2,3");
        /// 
        /// if (validHashSetRule.IsFailed())
        ///     return validHashSetRule.Failure.ToResponse();
        /// 
        /// var parsedHashSet = validHashSetRule.GetStoredValue&lt;HashSet&lt;int&gt;&gt;(); // returns -> [1, 2, 3]
        /// </code>
        /// </remarks>
        public static ValidationRule ValidHashSetRule<TType>(string propertyName, string propertyValue, params char[] separators)
        {
            return ValidHashSetRule(propertyName, propertyValue, EqualityComparer<TType>.Default, separators);
        }

        public static ValidationRule ValidHashSetRule<TType>(string propertyName, string propertyValue, IEqualityComparer<TType> comparer, params char[] separators)
        {
            return new ValidationRule(() => ValidationFailures.InvalidHashSet<TType>(propertyName, propertyValue),
                (v) =>
                {
                    if (string.IsNullOrWhiteSpace(propertyValue))
                    {
                        return false;
                    }

                    var type = typeof(TType);

                    var result = new HashSet<TType>(comparer);

                    var elements = propertyValue
                        .Split(separators?.Length > 0 ? separators : CommaSeparator, StringSplitOptions.RemoveEmptyEntries)
                        .Select(value => value.Trim())
                        .Where(value => value != string.Empty);

                    foreach (var element in elements)
                    {
                        try
                        {
                            var parsedItem = (TType)Convert.ChangeType(element, type);
                            result.Add(parsedItem);
                        }
                        catch
                        {
                            return false;
                        }
                    }

                    v.Store(result);

                    return true;
                });
        }

        private static readonly char[] CommaSeparator = { ',' };
    }
}