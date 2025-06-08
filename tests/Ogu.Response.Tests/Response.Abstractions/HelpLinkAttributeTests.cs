using Ogu.Response.Abstractions;

namespace Unit.Tests.Response.Abstractions;

public class HelpLinkAttributeTests
{
    [Fact]
    public void Constructor_WhenCalled_InitializesCorrectly()
    {
        const string helpLink = "https://google.com";

        var attribute = new HelpLinkAttribute(helpLink);

        Assert.NotNull(attribute);
        Assert.Equal(helpLink, attribute.HelpLink);
    }
}