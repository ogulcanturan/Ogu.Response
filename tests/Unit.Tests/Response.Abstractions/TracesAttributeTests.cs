using Ogu.Response.Abstractions;

namespace Unit.Tests.Response.Abstractions;

public class TracesAttributeTests
{
    [Fact]
    public void Constructor_WhenCalled_InitializesCorrectly()
    {
        const string traces = "Hello, World!";

        var attribute = new TracesAttribute(traces);

        Assert.NotNull(attribute);
        Assert.Equal(traces, attribute.Traces);
    }
}