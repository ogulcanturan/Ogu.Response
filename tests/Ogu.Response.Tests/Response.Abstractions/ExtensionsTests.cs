using Ogu.Response.Abstractions;
using System.ComponentModel;

namespace Unit.Tests.Response.Abstractions;

public class ExtensionsTests
{
    [Fact]
    public void GetValue_WithCorrectEnumType_ShouldReturnEnumValue()
    {
        var enumType = typeof(Sample);
        const Sample sample = Sample.Second;

        // Act
        var value = Extensions.GetValue(sample, enumType, sample.ToString());

        // Assert
        Assert.NotEqual(66, value);
        Assert.Equal((short)66, value);
    }

    [Fact]
    public void GetValue_WithInvalidEnumType_ShouldThrowAnException()
    {
        var enumType = typeof(ExtensionsTests);
        const Sample sample = Sample.Second;

        // Act
        var exception = Record.Exception(() => Extensions.GetValue(sample, enumType, sample.ToString()));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public void GetTitleFromEnum_WhenFound_ShouldReturnEnumTitle()
    {
        const Sample sample = Sample.First;

        // Act
        var title = Extensions.GetTitleFromEnum(typeof(Sample), sample.ToString());

        // Assert
        Assert.NotNull(title);
        Assert.Equal("First Title", title);
    }

    [Fact]
    public void GetTitleFromEnum_WhenNotFound_ShouldReturnEnumTitle()
    {
        const Sample sample = Sample.Second;

        // Act
        var title = Extensions.GetTitleFromEnum(typeof(Sample), sample.ToString());

        // Assert
        Assert.Null(title);
    }

    [Fact]
    public void GetDescriptionFromEnum_WhenFound_ShouldReturnEnumDescription()
    {
        const Sample sample = Sample.First;

        // Act
        var description = Extensions.GetDescriptionFromEnum(typeof(Sample), sample.ToString());

        // Assert
        Assert.NotNull(description);
        Assert.Equal("First Description", description);
    }

    [Fact]
    public void GetDescriptionFromEnum_WhenNotFound_ShouldReturnEnumDescription()
    {
        const Sample sample = Sample.Second;

        // Act
        var description = Extensions.GetDescriptionFromEnum(typeof(Sample), sample.ToString());

        // Assert
        Assert.Null(description);
    }

    [Fact]
    public void GetTracesFromEnum_WhenFound_ShouldReturnEnumTraces()
    {
        const Sample sample = Sample.First;

        // Act
        var traces = Extensions.GetTracesFromEnum(typeof(Sample), sample.ToString());

        Assert.NotNull(traces);
        Assert.Equal("First Traces", traces);
    }

    [Fact]
    public void GetTracesFromEnum_WhenNotFound_ShouldReturnEnumTraces()
    {
        const Sample sample = Sample.Second;

        // Act
        var traces = Extensions.GetTracesFromEnum(typeof(Sample), sample.ToString());

        // Assert
        Assert.Null(traces);
    }

    [Fact]
    public void GetHelpLinkFromEnum_WhenFound_ShouldReturnEnumHelpLink()
    {
        const Sample sample = Sample.First;

        // Act
        var helpLink = Extensions.GetHelpLinkFromEnum(typeof(Sample), sample.ToString());

        // Assert
        Assert.NotNull(helpLink);
        Assert.Equal("First HelpLink", helpLink);
    }

    [Fact]
    public void GetHelpLinkFromEnum_WhenNotFound_ShouldReturnEnumHelpLink()
    {
        const Sample sample = Sample.Second;

        // Act
        var helpLink = Extensions.GetHelpLinkFromEnum(typeof(Sample), sample.ToString());

        // Assert
        Assert.Null(helpLink);
    }

    [Fact]
    public void GetErrorAttributeFromEnum_WhenFound_ShouldReturnErrorAttribute()
    {
        const Sample sample = Sample.Third;

        // Act
        var errorAttribute = Extensions.GetErrorAttributeFromEnum(typeof(Sample), sample.ToString());

        // Assert
        Assert.NotNull(errorAttribute);
        Assert.Equal("Third Title", errorAttribute.Title);
        Assert.Equal("Third Description", errorAttribute.Description);
        Assert.Equal("Third Traces", errorAttribute.Traces);
        Assert.Equal("Third HelpLink", errorAttribute.HelpLink);
    }

    [Fact]
    public void GetErrorAttributeFromEnum_WhenNotFound_ShouldReturnNull()
    {
        const Sample sample = Sample.Second;

        // Act
        var errorAttribute = Extensions.GetErrorAttributeFromEnum(typeof(Sample), sample.ToString());

        // Assert
        Assert.Null(errorAttribute);
    }

    public enum Sample : short
    {
        [Title("First Title")]
        [Description("First Description")]
        [Traces("First Traces")]
        [HelpLink("First HelpLink")]
        First = 55,

        Second = 66,

        [Error("Third Title", "Third Description", "Third Traces", "Third HelpLink")]
        Third = 77
    }
}