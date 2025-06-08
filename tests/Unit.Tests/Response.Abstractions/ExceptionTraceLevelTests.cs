using Ogu.Response.Abstractions;

namespace Unit.Tests.Response.Abstractions;

public class ExceptionTraceLevelTests
{
    [Fact]
    public void ExceptionTraceLevel_ShouldHaveExpectedValues()
    {
        Assert.Equal(0, (int)ExceptionTraceLevel.None);
        Assert.Equal(1, (int)ExceptionTraceLevel.Basic);
        Assert.Equal(2, (int)ExceptionTraceLevel.Summary);
        Assert.Equal(3, (int)ExceptionTraceLevel.Full);
    }
}