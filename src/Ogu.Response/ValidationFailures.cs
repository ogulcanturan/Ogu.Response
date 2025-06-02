using System;

namespace Ogu.Response
{
    public static class ValidationFailures
    {
        public static ValidationFailure EqualTo(string propertyName, object attemptedValue, object desiredValue)
        {
            return new ValidationFailure(propertyName, $"{propertyName} must be equal to {desiredValue}.", attemptedValue);
        }

        public static ValidationFailure GreaterThan(string propertyName, object attemptedValue, object desiredValue)
        {
            return new ValidationFailure(propertyName, $"{propertyName} must be valid number and value must be greater than {desiredValue}.", attemptedValue);
        }

        public static ValidationFailure SmallerThan(string propertyName, object attemptedValue, object desiredValue)
        {
            return new ValidationFailure(propertyName, $"{propertyName} must be valid number and value must be smaller than {desiredValue}.", attemptedValue);
        }

        public static ValidationFailure NotEmpty(string propertyName)
        {
            return new ValidationFailure(propertyName, $"{propertyName} must not be empty.");
        }

        public static ValidationFailure AlreadyExists(string propertyName, object attemptedValue)
        {
            return new ValidationFailure(propertyName, $"The value '{attemptedValue ?? "null"}' already exists.", attemptedValue);
        }

        public static ValidationFailure InvalidJsonFormat(string propertyName, object attemptedValue)
        {
            return new ValidationFailure(propertyName, $"The '{propertyName}' must be valid json.", attemptedValue);
        }

        public static ValidationFailure InvalidFormat(string propertyName, object attemptedValue)
        {
            return new ValidationFailure(
                propertyName,
                $"The value '{attemptedValue ?? "null"}' for '{propertyName}' is not in a valid format.",
                attemptedValue
            );
        }

        public static ValidationFailure InvalidBooleanFormat(string propertyName, object attemptedValue)
        {
            return new ValidationFailure(
                propertyName,
                $"The value '{attemptedValue ?? "null"}' for '{propertyName}' is not a valid boolean.",
                attemptedValue
            );
        }

        public static ValidationFailure InvalidNumberFormat(string propertyName, object attemptedValue)
        {
            return new ValidationFailure(
                propertyName,
                $"The value '{attemptedValue ?? "null"}' for '{propertyName}' is not a valid number.",
                attemptedValue
            );
        }

        public static ValidationFailure InvalidEnumFormat(string propertyName, object attemptedValue, Type enumType)
        {
            return new ValidationFailure(
                propertyName,
                $"The value '{attemptedValue ?? "null"}' for '{propertyName}' is not a valid value for the '{enumType.Name}' enum.",
                attemptedValue
            );
        }
    }
}