using Ogu.Response;
using Ogu.Response.Abstractions;

namespace Unit.Tests.Response;

public class ValidationFailureExtensionsTests
{
    [Fact]
    public void ToResponse_WithValidationFailure_ReturnsIResponse()
    {
        var validationFailure = new ValidationFailure("TestProperty", "Test error message");

        // Act
        var response = ValidationFailureExtensions.ToResponse(validationFailure);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.Success);
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.Status);
        Assert.Single(response.Errors);

        var failure = response.Errors.First().ValidationFailures.First();

        Assert.Equal(validationFailure, failure);
    }

    [Fact]
    public void ToResponse_WithValidationFailureList_ReturnsIResponse()
    {
        List<IValidationFailure> validationFailures =
        [
            new ValidationFailure("TestProperty1", "Test error message 1"),
            new ValidationFailure("TestProperty2", "Test error message 2")
        ];

        // Act
        var response = ValidationFailureExtensions.ToResponse(validationFailures);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.Success);
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.Status);
        Assert.Single(response.Errors);

        var failures = response.Errors.First().ValidationFailures;

        Assert.Equal(validationFailures, failures);
        Assert.Equal(2, failures.Count);
    }

    [Fact]
    public void ToResponseT_WithValidationFailure_ReturnsIResponse()
    {
        var validationFailure = new ValidationFailure("TestProperty", "Test error message");

        // Act
        var response = ValidationFailureExtensions.ToResponse<int>(validationFailure);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.Success);
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.Status);
        Assert.Single(response.Errors);

        var failure = response.Errors.First().ValidationFailures.First();

        Assert.Equal(validationFailure, failure);
    }

    [Fact]
    public void ToResponseT_WithValidationFailureList_ReturnsIResponse()
    {
        List<IValidationFailure> validationFailures =
        [
            new ValidationFailure("TestProperty1", "Test error message 1"),
            new ValidationFailure("TestProperty2", "Test error message 2")
        ];

        // Act
        var response = ValidationFailureExtensions.ToResponse<int>(validationFailures);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.Success);
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.Status);
        Assert.Single(response.Errors);

        var failures = response.Errors.First().ValidationFailures;

        Assert.Equal(validationFailures, failures);
        Assert.Equal(2, failures.Count);
    }
}