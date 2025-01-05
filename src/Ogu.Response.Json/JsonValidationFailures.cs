using Ogu.Response.Abstractions;
using System;

namespace Ogu.Response.Json
{
    public static class JsonValidationFailures
    {
        public static IValidationFailure EqualTo(string propertyName, object attemptedValue, object desiredValue)
        {
            return new JsonValidationFailure(propertyName, $"{propertyName} must be equal to {desiredValue}.", attemptedValue);
        }

        public static IValidationFailure GreaterThan(string propertyName, object attemptedValue, object desiredValue)
        {
            return new JsonValidationFailure(propertyName, $"{propertyName} must be valid number and value must be greater than {desiredValue}.", attemptedValue);
        }

        public static IValidationFailure SmallerThan(string propertyName, object attemptedValue, object desiredValue)
        {
            return new JsonValidationFailure(propertyName, $"{propertyName} must be valid number and value must be smaller than {desiredValue}.", attemptedValue);
        }

        public static IValidationFailure NotEmpty(string propertyName)
        {
            return new JsonValidationFailure(propertyName, $"{propertyName} must not be empty.");
        }

        public static IValidationFailure AlreadyExists(string propertyName, object attemptedValue)
        {
            return new JsonValidationFailure(propertyName, $"The value '{attemptedValue}' already exists.", attemptedValue);
        }

        public static IValidationFailure InvalidJsonFormat(string propertyName, object attemptedValue)
        {
            return new JsonValidationFailure(propertyName, $"The '{propertyName}' must be valid json.", attemptedValue);
        }

        public static IValidationFailure InvalidFormat(string propertyName, object attemptedValue)
        {
            return new JsonValidationFailure(
                propertyName,
                $"The value '{attemptedValue}' for '{propertyName}' is not in a valid format.",
                attemptedValue
            );
        }

        public static IValidationFailure InvalidBooleanFormat(string propertyName, object attemptedValue)
        {
            return new JsonValidationFailure(
                propertyName,
                $"The value '{attemptedValue}' for '{propertyName}' is not a valid boolean.",
                attemptedValue
            );
        }

        public static IValidationFailure InvalidNumberFormat(string propertyName, object attemptedValue)
        {
            return new JsonValidationFailure(
                propertyName,
                $"The value '{attemptedValue}' for '{propertyName}' is not a valid number.",
                attemptedValue
            );
        }

        public static IValidationFailure InvalidEnumFormat(string propertyName, object attemptedValue, Type enumType)
        {
            return new JsonValidationFailure(
                propertyName,
                $"The value '{attemptedValue}' for '{propertyName}' is not a valid value for the '{enumType.Name}' enum."
            );
        }
    }
}