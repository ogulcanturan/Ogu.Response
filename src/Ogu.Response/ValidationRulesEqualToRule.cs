using Ogu.Response.Abstractions;
using System;

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
        /// var parsedId = equalToRule.GetStoredValue&lt;short&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule EqualToRule(string propertyName, string propertyValue, short equalTo)
        {
            return new ValidationRule(() => ValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
                (v) =>
                {
                    if (!short.TryParse(propertyValue, out var parsedValue) || parsedValue != equalTo)
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
            return new ValidationRule(() => ValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
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
            return new ValidationRule(() => ValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
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
        /// var equalToRule = ValidationRules.EqualToRule("Id", id, 10f);
        /// 
        /// if (equalToRule.IsFailed())
        ///     return equalToRule.Failure.ToResponse();
        /// 
        /// var parsedId = equalToRule.GetStoredValue&lt;float&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule EqualToRule(string propertyName, string propertyValue, float equalTo)
        {
            return new ValidationRule(() => ValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
                (v) =>
                {
                    if (!float.TryParse(propertyValue, out var parsedValue) || !AreEqual(parsedValue, equalTo))
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
        /// var equalToRule = ValidationRules.EqualToRule("Id", id, 10d);
        /// 
        /// if (equalToRule.IsFailed())
        ///     return equalToRule.Failure.ToResponse();
        /// 
        /// var parsedId = equalToRule.GetStoredValue&lt;double&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule EqualToRule(string propertyName, string propertyValue, double equalTo)
        {
            return new ValidationRule(() => ValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
                (v) =>
                {
                    if (!double.TryParse(propertyValue, out var parsedValue) || !AreEqual(parsedValue, equalTo))
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
        /// var equalToRule = ValidationRules.EqualToRule("Id", id, 10m);
        /// 
        /// if (equalToRule.IsFailed())
        ///     return equalToRule.Failure.ToResponse();
        /// 
        /// var parsedId = equalToRule.GetStoredValue&lt;decimal&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule EqualToRule(string propertyName, string propertyValue, decimal equalTo)
        {
            return new ValidationRule(() => ValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
                (v) =>
                {
                    if (!decimal.TryParse(propertyValue, out var parsedValue) || parsedValue != equalTo)
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
        /// var parsedId = equalToRule.GetStoredValue&lt;short&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule EqualToRule(string propertyName, short propertyValue, short equalTo)
        {
            return new ValidationRule(() => ValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
                () => propertyValue == equalTo);
        }

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
        public static ValidationRule EqualToRule(string propertyName, int propertyValue, int equalTo)
        {
            return new ValidationRule(() => ValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
                () => propertyValue == equalTo);
        }

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
        /// var parsedId = equalToRule.GetStoredValue&lt;long&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule EqualToRule(string propertyName, long propertyValue, long equalTo)
        {
            return new ValidationRule(() => ValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
                () => propertyValue == equalTo);
        }

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
        /// var equalToRule = ValidationRules.EqualToRule("Id", id, 10f);
        /// 
        /// if (equalToRule.IsFailed())
        ///     return equalToRule.Failure.ToResponse();
        /// 
        /// var parsedId = equalToRule.GetStoredValue&lt;float&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule EqualToRule(string propertyName, float propertyValue, float equalTo)
        {
            return new ValidationRule(() => ValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
                () => AreEqual(propertyValue, equalTo));
        }

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
        /// var equalToRule = ValidationRules.EqualToRule("Id", id, 10d);
        /// 
        /// if (equalToRule.IsFailed())
        ///     return equalToRule.Failure.ToResponse();
        /// 
        /// var parsedId = equalToRule.GetStoredValue&lt;double&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule EqualToRule(string propertyName, double propertyValue, double equalTo)
        {
            return new ValidationRule(() => ValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
                () => AreEqual(propertyValue, equalTo));
        }

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
        /// var equalToRule = ValidationRules.EqualToRule("Id", id, 10m);
        /// 
        /// if (equalToRule.IsFailed())
        ///     return equalToRule.Failure.ToResponse();
        /// 
        /// var parsedId = equalToRule.GetStoredValue&lt;decimal&gt;();
        /// </code>
        /// </remarks>
        public static ValidationRule EqualToRule(string propertyName, decimal propertyValue, decimal equalTo)
        {
            return new ValidationRule(() => ValidationFailures.EqualTo(propertyName, propertyValue, equalTo),
                () => propertyValue == equalTo);
        }

        private const float ToleranceForFloat = 0.0001f; // 1e-5f

        private static bool AreEqual(float value1, float value2)
        {
            return Math.Abs(value1 - value2) < ToleranceForFloat;
        }

        private const double ToleranceForDouble = 0.0000000001d; // 1e-10d

        private static bool AreEqual(double value1, double value2)
        {
            return Math.Abs(value1 - value2) < ToleranceForDouble;
        }
    }
}