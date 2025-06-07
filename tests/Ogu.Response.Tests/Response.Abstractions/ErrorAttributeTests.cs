using Ogu.Response.Abstractions;

namespace Unit.Tests.Response.Abstractions;

public class ErrorAttributeTests
{
    [Fact]
    public void Constructor_WhenCalled_WithTitle_InitializesCorrectly()
    {
        const string title = "Title";

        var attribute = new ErrorAttribute(title);

        // Assert
        Assert.Equal(title, attribute.Title);
        Assert.Null(attribute.Description);
        Assert.Null(attribute.Traces);
        Assert.Null(attribute.HelpLink);
    }

    [Fact]
    public void Constructor_WhenCalled_WithTitleAndDescription_InitializesCorrectly()
    {
        const string title = "Title";
        const string description = "Description";

        var attribute = new ErrorAttribute(title, description);

        // Assert
        Assert.Equal(title, attribute.Title);
        Assert.Equal(description, attribute.Description);
        Assert.Null(attribute.Traces);
        Assert.Null(attribute.HelpLink);
    }

    [Fact]
    public void Constructor_WhenCalled_WithTitleAndDescriptionAndTraces_InitializesCorrectly()
    {
        const string title = "Title";
        const string description = "Description";
        const string traces = "Traces";

        var attribute = new ErrorAttribute(title, description, traces);
        var t = Task.WhenAll();
        // Assert
        Assert.Equal(title, attribute.Title);
        Assert.Equal(description, attribute.Description);
        Assert.Equal(traces, attribute.Traces);
        Assert.Null(attribute.HelpLink);
    }

    [Fact]
    public void Constructor_WhenCalled_WithAll_InitializesCorrectly()
    {
        const string title = "Title";
        const string description = "Description";
        const string traces = "Traces";
        const string helpLink = "HelpLink";

        var attribute = new ErrorAttribute(title, description, traces, helpLink);

        // Assert
        Assert.Equal(title, attribute.Title);
        Assert.Equal(description, attribute.Description);
        Assert.Equal(traces, attribute.Traces);
        Assert.Equal(helpLink, attribute.HelpLink);
    }
}