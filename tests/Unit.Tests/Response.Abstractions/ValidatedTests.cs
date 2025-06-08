using Ogu.Response;
using Ogu.Response.Abstractions;

namespace Unit.Tests.Response.Abstractions;

public class ValidatedTests
{
    [Fact]
    public void Constructor_WhenCalled_WithValidationFailures_InitializesCorrectly()
    {
        var implementation = new Implementation(
            [
                new ValidationFailure("Id", "Id is required."),
                new ValidationFailure("ProductId", "ProductId is required.")
            ]
        );

        // Assert
        Assert.NotNull(implementation.Failures);
        Assert.Equal(2, implementation.Failures.Count);
        Assert.True(implementation.HasFailed);
    }

    [Fact]
    public void Constructor_WhenCalled_WithEmptyValidationFailures_InitializesCorrectly()
    {
        var implementation = new Implementation([]);

        // Assert
        Assert.NotNull(implementation.Failures);
        Assert.Empty(implementation.Failures);
        Assert.False(implementation.HasFailed);
    }

    [Fact]
    public void Constructor_WhenCalled_WithNullValidationFailures_InitializesCorrectly()
    {
        List<IValidationFailure>? failures = null;

        var implementation = new Implementation(failures);

        // Assert
        Assert.NotNull(implementation.Failures);
        Assert.Empty(implementation.Failures);
        Assert.False(implementation.HasFailed);
    }

    private class Implementation(List<IValidationFailure>? failures) : Validated(failures);
}