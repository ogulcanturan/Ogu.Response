using Ogu.Response;
using Ogu.Response.Abstractions;

namespace Unit.Tests.Response;

public class ValidationFailuresTests
{
    [Fact]
    public void EqualTo_WhenCalled_ReturnsValidationFailure()
    {
        // Arrange
        const string propertyName = "Id";
        const string attemptedValue = "5";
        const string desiredValue = "5";

        // Act
        var validationFailure = ValidationFailures.EqualTo(propertyName, attemptedValue, desiredValue);

        // Assert
        Assert.NotNull(validationFailure);
        Assert.Equal(propertyName, validationFailure.PropertyName);
        Assert.Equal(attemptedValue, validationFailure.AttemptedValue);
        Assert.Contains(propertyName, validationFailure.Message);
        Assert.Contains(desiredValue, validationFailure.Message);
    }

    [Fact]
    public void GreaterThan_WhenCalled_ReturnsValidationFailure()
    {
        // Arrange
        const string propertyName = "Id";
        const string attemptedValue = "5";
        const string desiredValue = "5";

        // Act
        var validationFailure = ValidationFailures.GreaterThan(propertyName, attemptedValue, desiredValue);

        // Assert
        Assert.NotNull(validationFailure);
        Assert.Equal(propertyName, validationFailure.PropertyName);
        Assert.Equal(attemptedValue, validationFailure.AttemptedValue);
        Assert.Contains(propertyName, validationFailure.Message);
        Assert.Contains(desiredValue, validationFailure.Message);
    }

    [Fact]
    public void SmallerThan_WhenCalled_ReturnsValidationFailure()
    {
        // Arrange
        const string propertyName = "Id";
        const string attemptedValue = "5";
        const string desiredValue = "5";

        // Act
        var validationFailure = ValidationFailures.SmallerThan(propertyName, attemptedValue, desiredValue);

        // Assert
        Assert.NotNull(validationFailure);
        Assert.Equal(propertyName, validationFailure.PropertyName);
        Assert.Equal(attemptedValue, validationFailure.AttemptedValue);
        Assert.Contains(propertyName, validationFailure.Message);
        Assert.Contains(desiredValue, validationFailure.Message);
    }

    [Fact]
    public void NotEmpty_WhenCalled_ReturnsValidationFailure()
    {
        // Arrange
        const string propertyName = "Id";
        const string attemptedValue = "5";

        // Act
        var validationFailure = ValidationFailures.NotEmpty(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(validationFailure);
        Assert.Equal(propertyName, validationFailure.PropertyName);
        Assert.Equal(attemptedValue, validationFailure.AttemptedValue);
        Assert.Contains(attemptedValue, validationFailure.Message);
        Assert.Contains(propertyName, validationFailure.Message);
    }

    [Fact]
    public void AlreadyExists_WhenCalled_ReturnsValidationFailure()
    {
        // Arrange
        const string propertyName = "Id";
        const string attemptedValue = "5";

        // Act
        var validationFailure = ValidationFailures.AlreadyExists(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(validationFailure);
        Assert.Equal(propertyName, validationFailure.PropertyName);
        Assert.Equal(attemptedValue, validationFailure.AttemptedValue);
        Assert.Contains(attemptedValue, validationFailure.Message);
        Assert.Contains(propertyName, validationFailure.Message);
    }

    [Fact]
    public void InvalidJsonFormat_WhenCalled_ReturnsValidationFailure()
    {
        // Arrange
        const string propertyName = "Id";
        const string attemptedValue = "5";

        // Act
        var validationFailure = ValidationFailures.InvalidJsonFormat(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(validationFailure);
        Assert.Equal(propertyName, validationFailure.PropertyName);
        Assert.Equal(attemptedValue, validationFailure.AttemptedValue);
        Assert.Contains(attemptedValue, validationFailure.Message);
        Assert.Contains(propertyName, validationFailure.Message);
    }

    [Fact]
    public void InvalidFormat_WhenCalled_ReturnsValidationFailure()
    {
        // Arrange
        const string propertyName = "Id";
        const string attemptedValue = "5";

        // Act
        var validationFailure = ValidationFailures.InvalidFormat(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(validationFailure);
        Assert.Equal(propertyName, validationFailure.PropertyName);
        Assert.Equal(attemptedValue, validationFailure.AttemptedValue);
        Assert.Contains(attemptedValue, validationFailure.Message);
        Assert.Contains(propertyName, validationFailure.Message);
    }

    [Fact]
    public void InvalidBooleanFormat_WhenCalled_ReturnsValidationFailure()
    {
        // Arrange
        const string propertyName = "Id";
        const string attemptedValue = "5";

        // Act
        var validationFailure = ValidationFailures.InvalidBooleanFormat(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(validationFailure);
        Assert.Equal(propertyName, validationFailure.PropertyName);
        Assert.Equal(attemptedValue, validationFailure.AttemptedValue);
        Assert.Contains(attemptedValue, validationFailure.Message);
        Assert.Contains(propertyName, validationFailure.Message);
    }

    [Fact]
    public void InvalidNumberFormat_WhenCalled_ReturnsValidationFailure()
    {
        // Arrange
        const string propertyName = "Id";
        const string attemptedValue = "5";

        // Act
        var validationFailure = ValidationFailures.InvalidNumberFormat(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(validationFailure);
        Assert.Equal(propertyName, validationFailure.PropertyName);
        Assert.Equal(attemptedValue, validationFailure.AttemptedValue);
        Assert.Contains(attemptedValue, validationFailure.Message);
        Assert.Contains(propertyName, validationFailure.Message);
    }

    [Fact]
    public void InvalidEnumFormat_WhenCalled_ReturnsValidationFailure()
    {
        // Arrange
        const string propertyName = "Id";
        const string attemptedValue = "5";

        // Act
        var validationFailure = ValidationFailures.InvalidEnumFormat(propertyName, attemptedValue, typeof(ExceptionTraceLevel));

        // Assert
        Assert.NotNull(validationFailure);
        Assert.Equal(propertyName, validationFailure.PropertyName);
        Assert.Equal(attemptedValue, validationFailure.AttemptedValue);
        Assert.Contains(attemptedValue, validationFailure.Message);
        Assert.Contains(propertyName, validationFailure.Message);
        Assert.Contains(nameof(ExceptionTraceLevel), validationFailure.Message);
    }

    [Fact]
    public void InvalidHashSet_WhenCalled_ReturnsValidationFailure()
    {
        // Arrange
        const string propertyName = "Id";
        const string attemptedValue = "5";

        // Act
        var validationFailure = ValidationFailures.InvalidHashSet<int>(propertyName, attemptedValue);

        // Assert
        Assert.NotNull(validationFailure);
        Assert.Equal(propertyName, validationFailure.PropertyName);
        Assert.Equal(attemptedValue, validationFailure.AttemptedValue);
        Assert.Contains(attemptedValue, validationFailure.Message);
        Assert.Contains(propertyName, validationFailure.Message);
        Assert.Contains(nameof(Int32), validationFailure.Message);
    }
}