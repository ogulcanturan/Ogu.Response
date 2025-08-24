using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;

namespace Ogu.Response
{
    public static partial class ValidationRules
    {
        /// <summary>
        /// Creates a validation rule to check if an attempted value is not empty.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The attempted value to be validated.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is empty.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is null or white space.
        /// <code>
        /// var notEmptyRule = ValidationRules.NotEmptyRule("Data", data);
        ///
        /// if(notEmptyRule.IsFailed())
        ///     return notEmptyRule.Failure.ToResponse();
        ///
        /// ...
        /// </code>
        /// </remarks>
        public static ValidationRule NotEmptyRule(string propertyName, string attemptedValue)
        {
            return new ValidationRule(() => ValidationFailures.NotEmpty(propertyName, attemptedValue), () => !string.IsNullOrWhiteSpace(attemptedValue));
        }

        /// <summary>
        /// Creates a validation rule to check if an attempted value is not empty.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The attempted value to be validated.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is empty.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is empty.
        /// <code>
        /// var notEmptyRule = ValidationRules.NotEmptyRule("Data", data);
        ///
        /// if(notEmptyRule.IsFailed())
        ///     return notEmptyRule.Failure.ToResponse();
        ///
        /// ...
        /// </code>
        /// </remarks>
        public static ValidationRule NotEmptyRule(string propertyName, Guid attemptedValue)
        {
            return new ValidationRule(() => ValidationFailures.NotEmpty(propertyName, attemptedValue), () => attemptedValue != Guid.Empty);
        }

        /// <summary>
        /// Creates a validation rule to check if an attempted value is not empty.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="attemptedValue">The attempted value to be validated.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is empty.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is empty.
        /// <code>
        /// var notEmptyRule = ValidationRules.NotEmptyRule("Data", data);
        ///
        /// if(notEmptyRule.IsFailed())
        ///     return notEmptyRule.Failure.ToResponse();
        ///
        /// ...
        /// </code>
        /// </remarks>
        public static ValidationRule NotEmptyRule(string propertyName, Guid? attemptedValue)
        {
            return new ValidationRule(() => ValidationFailures.NotEmpty(propertyName, attemptedValue), () => attemptedValue != null && attemptedValue != Guid.Empty);
        }

        /// <summary>
        /// Creates a validation rule to check if a collection is not empty.
        /// A <c>null</c> collection is also considered empty and will cause the rule to fail.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="collection">The collection to be validated.</param>
        /// <returns>
        /// A <see cref="ValidationRule"/> that checks if the attempted value is empty.
        /// </returns>
        /// <remarks>
        /// Example usage: ValidationFailure occurs if the value is empty.
        /// <code>
        /// var notEmptyRule = ValidationRules.NotEmptyRule("Data", data);
        ///
        /// if(notEmptyRule.IsFailed())
        ///     return notEmptyRule.Failure.ToResponse();
        ///
        /// ...
        /// </code>
        /// </remarks>
        public static ValidationRule NotEmptyRule<T>(string propertyName, IReadOnlyCollection<T> collection)
        {
            return new ValidationRule(() => ValidationFailures.NotEmpty(propertyName, collection), () => collection?.Count > 0);
        }
    }
}