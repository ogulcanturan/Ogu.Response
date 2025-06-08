using Ogu.Response.Abstractions;

namespace Unit.Tests.Response.Abstractions;

public class TitleAttributeTests
{
    [Fact]
    public void Constructor_WhenCalled_InitializesCorrectly()
    {
        const string title = "Hello, World!";

        var attribute = new TitleAttribute(title);

        Assert.NotNull(attribute);
        Assert.Equal(title, attribute.Title);
    }
}