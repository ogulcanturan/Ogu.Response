using Ogu.Response;
using Ogu.Response.Abstractions;

namespace Unit.Tests.Response;

public class ValidationFailureTests
{
    [Fact]
    public void Constructor_WhenCalled_WithOptionalParams_InitializesCorrectly()
    {
        const string propertyName = "TestField";
        const string message = "Test error message";

        // Act
        var validationFailure = new ValidationFailure(propertyName, message);

        // Assert
        Assert.NotNull(validationFailure);
        Assert.Equal(propertyName, validationFailure.PropertyName);
        Assert.Equal(message, validationFailure.Message);
        Assert.Null(validationFailure.AttemptedValue);
        Assert.Equal(Severity.Error, validationFailure.Severity);
        Assert.Null(validationFailure.Code);
    }

    [Fact]
    public void Constructor_WhenCalled_WithAllParams_InitializesCorrectly()
    {
        const string propertyName = "TestField";
        const string message = "Test error message";
        const string attemptedValue = "**TestValue**";
        const Severity severity = Severity.Warning;
        const string code = "TEST_CODE";

        // Act
        var validationFailure = new ValidationFailure(propertyName, message, attemptedValue, severity, code);

        // Assert
        Assert.NotNull(validationFailure);
        Assert.Equal(propertyName, validationFailure.PropertyName);
        Assert.Equal(message, validationFailure.Message);
        Assert.Equal(attemptedValue, validationFailure.AttemptedValue);
        Assert.Equal(severity, validationFailure.Severity);
        Assert.Equal(code, validationFailure.Code);
    }
}