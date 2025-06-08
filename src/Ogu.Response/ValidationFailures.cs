using System;

namespace Ogu.Response
{
    public static class ValidationFailures
    {
        public static ValidationFailure EqualTo(string propertyName, object attemptedValue, object desiredValue)
        {
            return new ValidationFailure(propertyName, $"The field '{propertyName}' must be equal to {desiredValue}.", attemptedValue);
        }

        public static ValidationFailure GreaterThan(string propertyName, object attemptedValue, object desiredValue)
        {
            return new ValidationFailure(propertyName, $"The field '{propertyName}' must be valid number and value must be greater than '{desiredValue ?? "null"}'.", attemptedValue);
        }

        public static ValidationFailure SmallerThan(string propertyName, object attemptedValue, object desiredValue)
        {
            return new ValidationFailure(propertyName, $"The field '{propertyName}' must be valid number and value must be smaller than '{desiredValue ?? "null"}'.", attemptedValue);
        }

        public static ValidationFailure NotEmpty(string propertyName, object attemptedValue)
        {
            return new ValidationFailure(propertyName, $"The value '{attemptedValue ?? "null"}' for field '{propertyName}' must not be empty.", attemptedValue);
        }

        public static ValidationFailure AlreadyExists(string propertyName, object attemptedValue)
        {
            return new ValidationFailure(propertyName, $"The value '{attemptedValue ?? "null"}' for field '{propertyName}' already exists.", attemptedValue);
        }

        public static ValidationFailure InvalidJsonFormat(string propertyName, object attemptedValue)
        {
            return new ValidationFailure(propertyName, $"The value '{attemptedValue ?? "null"}' for field '{propertyName}' must be valid json.", attemptedValue);
        }

        public static ValidationFailure InvalidFormat(string propertyName, object attemptedValue)
        {
            return new ValidationFailure(
                propertyName,
                $"The value '{attemptedValue ?? "null"}' for field '{propertyName}' is not in a valid format.",
                attemptedValue
            );
        }

        public static ValidationFailure InvalidBooleanFormat(string propertyName, object attemptedValue)
        {
            return new ValidationFailure(
                propertyName,
                $"The value '{attemptedValue ?? "null"}' for field '{propertyName}' is not a valid boolean.",
                attemptedValue
            );
        }

        public static ValidationFailure InvalidNumberFormat(string propertyName, object attemptedValue)
        {
            return new ValidationFailure(
                propertyName,
                $"The value '{attemptedValue ?? "null"}' for field '{propertyName}' is not a valid number.",
                attemptedValue
            );
        }

        public static ValidationFailure InvalidEnumFormat(string propertyName, object attemptedValue, Type enumType)
        {
            return new ValidationFailure(
                propertyName,
                $"The value '{attemptedValue ?? "null"}' for field '{propertyName}' is not a valid value for the '{enumType.Name}' enum.",
                attemptedValue
            );
        }

        public static ValidationFailure InvalidHashSet<TType>(string propertyName, object attemptedValue)
        {
            return new ValidationFailure(
                propertyName,
                $"The value '{attemptedValue ?? "null"}' for field '{propertyName}' is not a valid HashSet of {typeof(TType).Name}.",
                attemptedValue
            );
        }
    }
}