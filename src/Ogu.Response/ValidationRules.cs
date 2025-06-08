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
        /// Creates a validation rule to check if a property value is a valid json string. Successfully parsed json is stored as a <see cref="JsonDocument"/>.
        /// <para>
        ///     The rule attempts to parse the property value using `JsonDocument.Parse`. If valid, the parsed json is stored 
        ///     and can be retrieved via <see cref="ValidationRule.GetStoredValue{T}"/>.
        /// </para>
        /// <para>
        ///     If the property value is invalid json, the validation fails.
        /// </para>
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is valid json, storing the parsed 
        /// <see cref="JsonDocument"/> on success. Consumers should dispose the stored <see cref="JsonDocument"/> after use.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is not valid json.
        /// <code>
        /// var validJsonRule = ValidationRules.ValidJsonRule("Data", data);
        ///
        /// if(validJsonRule.IsFailed())
        ///     return validJsonRule.Failure.ToResponse();
        ///
        /// using var parsedJsonDocument = validJsonRule.GetStoredValue&lt;JsonDocument&gt;()
        ///
        /// </code>
        /// </remarks>
        public static ValidationRule ValidJsonRule(string propertyName, string attemptedValue)
        {
            return new ValidationRule(() => ValidationFailures.InvalidJsonFormat(propertyName, attemptedValue), (v) =>
            {
                try
                {
                    var jsonDocument = JsonDocument.Parse(attemptedValue);

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
        /// <param name="attemptedValue">The value to be validated. It can be of any type (string, int, enum, or null).</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is a valid value of the specified enum.
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
        public static ValidationRule ValidEnumRule<TEnum>(string propertyName, object attemptedValue) where TEnum : struct, Enum
        {
            var type = typeof(TEnum);

            return ValidEnumRule(propertyName, attemptedValue, type);
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is a valid enum value of the specified enum type.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="enumType">The type of the enum to check against.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is a valid value of the specified enum.
        /// If valid, the parsed enum value is stored and can be retrieved via <see cref="ValidationRule.GetStoredValue{T}"/>.
        /// </returns>
        /// <remarks>
        /// Example usage: Validation failure occurs if the attempted value is not a valid enum value.
        /// <code>
        /// var validEnumRule = ValidationRules.ValidEnumRule("MyEnum", "Active", typeof(MyEnum));
        /// 
        /// if (validEnumRule.IsFailed())
        ///     return validEnumRule.Failure.ToResponse();
        /// 
        /// var parsedMyEnum = validEnumRule.GetStoredValue&lt;MyEnum&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule ValidEnumRule(string propertyName, object attemptedValue, Type enumType)
        {
            return new ValidationRule(() => ValidationFailures.InvalidEnumFormat(propertyName, attemptedValue, enumType),
                (v) =>
                {
                    var underlyingType = Nullable.GetUnderlyingType(enumType);

                    var isNullableEnum = underlyingType != null;

                    if (attemptedValue == null)
                    {
                        if (!isNullableEnum)
                        {
                            return false;
                        }

                        v.Store(null);

                        return true;
                    }

                    var underlyingEnumType = isNullableEnum ? underlyingType : enumType;

                    switch (attemptedValue)
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

                            try
                            {
                                if (!Enum.IsDefined(underlyingEnumType, attemptedValue))
                                {
                                    return false;
                                }

                                v.Store(Enum.ToObject(underlyingEnumType, attemptedValue));

                                return true;
                            }
                            catch (ArgumentException)
                            {
                                return false;
                            }
                    }
                });
        }


        /// <summary>
        /// Creates a validation rule to check if a property value is a valid boolean string ("true" or "false").
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the property value to be validated.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is a valid boolean string.
        /// If valid, the parsed boolean value is stored and can be retrieved via <see cref="ValidationRule.GetStoredValue{T}"/>.
        /// </returns>
        /// <remarks>
        /// Example usage: Validation failure occurs if the attempted value is not a valid boolean string ("true" or "false").
        /// <code>
        /// var validBooleanRule = ValidationRules.ValidBooleanRule("IsActive", isActive);
        ///
        /// if (validBooleanRule.IsFailed())
        ///     return validBooleanRule.Failure.ToResponse();
        ///
        /// var parsedStatus = validBooleanRule.GetStoredValue&lt;bool&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule ValidBooleanRule(string propertyName, string attemptedValue)
        {
            return new ValidationRule(() => ValidationFailures.InvalidBooleanFormat(propertyName, attemptedValue),
                (v) =>
                {
                    if (!bool.TryParse(attemptedValue, out var parsedValue))
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
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is a valid number.
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
        public static ValidationRule ValidNumberRule(string propertyName, string attemptedValue)
        {
            return new ValidationRule(() => ValidationFailures.InvalidNumberFormat(propertyName, attemptedValue),
                (v) =>
                {
                    if (long.TryParse(attemptedValue, out var parsedLong))
                    {
                        v.Store(parsedLong);
                        return true;
                    }

                    if (!decimal.TryParse(attemptedValue, out var parsedDecimal))
                    {
                        return false;
                    }

                    v.Store(parsedDecimal);

                    return true;
                });
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is a valid <see cref="HashSet{T}" />.
        /// </summary>
        /// <typeparam name="TType">The target element type of the HashSet.</typeparam>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="separators">Optional separators to split the attempted value into elements. Defaults to comma (',').</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is a valid HashSet.
        /// The parsed value is stored if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: Validation failure occurs if the attempted value is not a valid HashSet.
        /// <code>
        /// var validHashSetRule = ValidationRules.ValidHashSetRule("Prices", "1,2,3");
        /// 
        /// if (validHashSetRule.IsFailed())
        ///     return validHashSetRule.Failure.ToResponse();
        /// 
        /// var parsedHashSet = validHashSetRule.GetStoredValue&lt;HashSet&lt;int&gt;&gt;(); // returns -> [1, 2, 3]
        /// </code>
        /// </remarks>
        public static ValidationRule ValidHashSetRule<TType>(string propertyName, string attemptedValue, params char[] separators)
        {
            return ValidHashSetRule(propertyName, attemptedValue, EqualityComparer<TType>.Default, null, separators);
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is a valid <see cref="HashSet{T}" />.
        /// </summary>
        /// <typeparam name="TType">The target element type of the HashSet.</typeparam>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="comparer">An equality comparer to be used when creating the HashSet.</param>
        /// <param name="separators">Optional separators to split the property value into elements. Defaults to comma (',').</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is a valid HashSet.
        /// The parsed value is stored if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: Validation failure occurs if the attempted value is not a valid HashSet.
        /// <code>
        /// var validHashSetRule = ValidationRules.ValidHashSetRule("Prices", "a,B,B", StringComparer.OrdinalIgnoreCase);
        /// 
        /// if (validHashSetRule.IsFailed())
        ///     return validHashSetRule.Failure.ToResponse();
        /// 
        /// var parsedHashSet = validHashSetRule.GetStoredValue&lt;HashSet&lt;string&gt;&gt;(); // returns -> [a, B]
        /// </code>
        /// </remarks>
        public static ValidationRule ValidHashSetRule<TType>(string propertyName, string attemptedValue, IEqualityComparer<TType> comparer, params char[] separators)
        {
            return ValidHashSetRule(propertyName, attemptedValue, comparer, null, separators);
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is a valid <see cref="HashSet{T}" />.
        /// </summary>
        /// <typeparam name="TType">The target element type of the HashSet.</typeparam>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="ruleOptions">Optional rule configuration to control behavior like allowing empty sets, enforcing uniqueness.</param>
        /// <param name="separators">Optional separators to split the attempted value into elements. Defaults to comma (',').</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is a valid HashSet.
        /// The parsed value is stored if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: Validation failure occurs if the attempted value is not a valid HashSet.
        /// <code>
        /// var options = new HashSetRuleOptions { AllowEmpty = true };
        /// var validHashSetRule = ValidationRules.ValidHashSetRule("Prices", "a,B,B", options);
        /// 
        /// if (validHashSetRule.IsFailed())
        ///     return validHashSetRule.Failure.ToResponse();
        /// 
        /// var parsedHashSet = validHashSetRule.GetStoredValue&lt;HashSet&lt;string&gt;&gt;(); // returns -> [a, B, B]
        /// </code>
        /// </remarks>
        public static ValidationRule ValidHashSetRule<TType>(string propertyName, string attemptedValue, HashSetRuleOptions ruleOptions, params char[] separators)
        {
            return ValidHashSetRule(propertyName, attemptedValue, EqualityComparer<TType>.Default, ruleOptions, separators);
        }

        /// <summary>
        /// Creates a validation rule to check if a property value is a valid <see cref="HashSet{T}" />.
        /// </summary>
        /// <typeparam name="TType">The target element type of the HashSet.</typeparam>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The string representation of the attempted value to be validated.</param>
        /// <param name="comparer">An equality comparer to compare elements in the HashSet.</param>
        /// <param name="ruleOptions">Optional rule configuration to control behavior like allowing empty sets, enforcing uniqueness.</param>
        /// <param name="separators">Optional separators to split the attempted value into elements. Defaults to comma (',').</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is a valid HashSet.
        /// The parsed value is stored if the validation is successful.
        /// </returns>
        /// <remarks>
        /// Example usage: Validation failure occurs if the attempted value is not a valid HashSet.
        /// <code>
        /// var validHashSetRule = ValidationRules.ValidHashSetRule("Prices", "a,B,B", StringComparer.OrdinalIgnoreCase, HashSetRuleOptions.Default);
        /// 
        /// if (validHashSetRule.IsFailed())
        ///     return validHashSetRule.Failure.ToResponse();
        /// 
        /// var parsedHashSet = validHashSetRule.GetStoredValue&lt;HashSet&lt;string&gt;&gt;(); // returns -> [a, B]
        /// </code>
        /// </remarks>
        public static ValidationRule ValidHashSetRule<TType>(string propertyName, string attemptedValue, IEqualityComparer<TType> comparer, HashSetRuleOptions ruleOptions, params char[] separators)
        {
            return new ValidationRule(() => ValidationFailures.InvalidHashSet<TType>(propertyName, attemptedValue),
                (v) =>
                {
                    ruleOptions = ruleOptions ?? HashSetRuleOptions.Default;
                    comparer = comparer ?? EqualityComparer<TType>.Default;

                    if (string.IsNullOrWhiteSpace(attemptedValue))
                    {
                        if (!ruleOptions.AllowEmpty)
                        {
                            return false;
                        }

                        v.Store(new HashSet<TType>(comparer));

                        return true;
                    }

                    var result = new HashSet<TType>(comparer);

                    var elements = attemptedValue
                        .Split(separators?.Length > 0 ? separators : CommaSeparator, StringSplitOptions.RemoveEmptyEntries)
                        .Select(value => value.Trim())
                        .Where(value => value != string.Empty);

                    var type = typeof(TType);

                    var isFailed = false;

                    foreach (var element in elements)
                    {
                        try
                        {
                            var parsedItem = (TType)Convert.ChangeType(element, type);

                            if (!result.Add(parsedItem) && ruleOptions.RequireAllUnique)
                            {
                                return false;
                            }
                        }
                        catch
                        {
                            if (!ruleOptions.ContinueOnFailure)
                            {
                                return false;
                            }

                            isFailed = true;
                        }
                    }

                    v.Store(result);

                    return !isFailed;
                });
        }

        private static readonly char[] CommaSeparator = { ',' };
    }
}