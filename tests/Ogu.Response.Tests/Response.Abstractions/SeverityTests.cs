using Ogu.Response.Abstractions;

namespace Unit.Tests.Response.Abstractions;

public class SeverityTests
{
    [Fact]
    public void Severity_ShouldHaveExpectedValues()
    {
        Assert.Equal(0, (int)Severity.Error);
        Assert.Equal(1, (int)Severity.Warning);
        Assert.Equal(2, (int)Severity.Info);
    }
}