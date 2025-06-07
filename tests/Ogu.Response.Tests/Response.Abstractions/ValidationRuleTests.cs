using Ogu.Response;
using Ogu.Response.Abstractions;

namespace Unit.Tests.Response.Abstractions;

public class ValidationRuleTests
{
    [Fact]
    public async Task Constructor_WithValidData_ShouldSucceedValidation()
    {
        const string id = "";
        const string propertyName = nameof(id);
        const string message = $"{propertyName} is required";

        var validationFailure = new ValidationFailure(nameof(id), message, id);

        // Act
        var validationRule = new ValidationRule(validationFailure, () => !string.IsNullOrWhiteSpace(id));

        // Assert
        Assert.Equal(validationFailure, validationRule.Failure);
        Assert.True(validationRule.IsFailed());
        Assert.True(await validationRule.IsFailedAsync());
    }

    [Fact]
    public async Task Constructor_WithInvalidData_ShouldFailValidation()
    {
        const string id = "1";
        const string propertyName = nameof(id);
        const string message = $"{propertyName} is required";

        var validationFailure = new ValidationFailure(nameof(id), message, id);

        // Act
        var validationRule = new ValidationRule(validationFailure, () => !string.IsNullOrWhiteSpace(id));

        // Assert
        Assert.Equal(validationFailure, validationRule.Failure);
        Assert.False(validationRule.IsFailed());
        Assert.False(await validationRule.IsFailedAsync());
    }
}