using Ogu.Response.Abstractions;

namespace Unit.Tests.Response.Abstractions;

public class HashSetRuleOptionsTests
{
    [Fact]
    public void Constructor_WhenCalled_InitializesCorrectly()
    {
        var hashSetRuleOptions = new HashSetRuleOptions(true, true, true);

        // Assert
        Assert.True(hashSetRuleOptions.AllowEmpty);
        Assert.True(hashSetRuleOptions.RequireAllUnique);
        Assert.True(hashSetRuleOptions.ContinueOnFailure);
    }

    [Fact]
    public void Default_InitializesCorrectly()
    {
        var defaultOptions = HashSetRuleOptions.Default;

        // Assert
        Assert.NotNull(defaultOptions);
        Assert.False(defaultOptions.AllowEmpty);
        Assert.False(defaultOptions.RequireAllUnique);
        Assert.False(defaultOptions.ContinueOnFailure);
        Assert.Equal(defaultOptions, HashSetRuleOptions.Default);
    }
}