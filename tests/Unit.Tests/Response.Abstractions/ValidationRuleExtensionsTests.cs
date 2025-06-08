using Ogu.Response;
using Ogu.Response.Abstractions;

namespace Unit.Tests.Response.Abstractions;

public class ValidationRuleExtensionsTests
{
    [Fact]
    public void ValidateFirstOrDefault_WhenFailed_ReturnsFirstFailure()
    {
        ValidationRule[] rules =
        [
            ValidationRules.NotEmptyRule("Id", Guid.Empty),
            ValidationRules.NotEmptyRule("ProductId", Guid.Empty)
        ];

        // Act
        var failure = rules.ValidateFirstOrDefault();

        // Assert
        Assert.NotNull(failure);
        Assert.Equal(rules[0].Failure, failure);
    }

    [Fact]
    public async Task ValidateFirstOrDefaultAsync_WhenFailed_ReturnsFirstFailure()
    {
        ValidationRule[] rules =
        [
            ValidationRules.NotEmptyRule("Id", Guid.Empty),
            ValidationRules.NotEmptyRule("ProductId", Guid.Empty)
        ];

        // Actp
        var failure = await rules.ValidateFirstOrDefaultAsync();

        // Assert
        Assert.NotNull(failure);
        Assert.Equal(rules[0].Failure, failure);
    }

    [Fact]
    public void ValidateFirstOrDefault_WhenSucceed_ReturnsNull()
    {
        ValidationRule[] rules =
        [
            ValidationRules.NotEmptyRule("Id", Guid.NewGuid()),
            ValidationRules.NotEmptyRule("ProductId", Guid.NewGuid())
        ];

        // Act
        var failure = rules.ValidateFirstOrDefault();

        // Assert
        Assert.Null(failure);
    }

    [Fact]
    public async Task ValidateFirstOrDefaultAsync_WhenSucceed_ReturnsNull()
    {
        ValidationRule[] rules =
        [
            ValidationRules.NotEmptyRule("Id", Guid.NewGuid()),
            ValidationRules.NotEmptyRule("ProductId", Guid.NewGuid())
        ];

        // Act
        var failure = await rules.ValidateFirstOrDefaultAsync();

        // Assert
        Assert.Null(failure);
    }

    [Fact]
    public void ValidateAll_WhenFailed_ReturnsAllFailures()
    {
        ValidationRule[] rules =
        [
            ValidationRules.NotEmptyRule("Id", Guid.Empty),
            ValidationRules.NotEmptyRule("ProductId", Guid.Empty)
        ];

        // Act
        var failures = rules.ValidateAll();

        // Assert
        Assert.NotNull(failures);
        Assert.Equal(2, failures.Count);
    }

    [Fact]
    public async Task ValidateAllAsync_WhenFailed_ReturnsAllFailures()
    {

        ValidationRule[] rules =
        [
            ValidationRules.NotEmptyRule("Id", Guid.Empty),
            ValidationRules.NotEmptyRule("ProductId", Guid.Empty)
        ];

        // Act
        var failures = await rules.ValidateAllAsync();

        // Assert
        Assert.NotNull(failures);
        Assert.Equal(2, failures.Count);
    }

    [Fact]
    public void ValidateAll_WhenSucceed_ReturnsEmptyList()
    {
        ValidationRule[] rules =
        [
            ValidationRules.NotEmptyRule("Id", Guid.NewGuid()),
            ValidationRules.NotEmptyRule("ProductId", Guid.NewGuid())
        ];

        // Act
        var failures = rules.ValidateAll();

        // Assert
        Assert.Empty(failures);
    }

    [Fact]
    public async Task ValidateAllAsync_WhenSucceed_ReturnsEmptyList()
    {
        ValidationRule[] rules =
        [
            ValidationRules.NotEmptyRule("Id", Guid.NewGuid()),
            ValidationRules.NotEmptyRule("ProductId", Guid.NewGuid())
        ];

        // Act
        var failures = await rules.ValidateAllAsync();

        // Assert
        Assert.Empty(failures);
    }
}