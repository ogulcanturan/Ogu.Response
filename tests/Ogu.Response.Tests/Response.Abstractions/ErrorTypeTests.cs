using Ogu.Response.Abstractions;

namespace Unit.Tests.Response.Abstractions;

public class ErrorTypeTests
{
    [Fact]
    public void ErrorType_ShouldHaveExpectedValues()
    {
        Assert.Equal(0, (int)ErrorType.Custom);
        Assert.Equal(1, (int)ErrorType.Validation);
        Assert.Equal(2, (int)ErrorType.Exception);
    }
}