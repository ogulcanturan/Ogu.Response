using Ogu.Response;
using Ogu.Response.Abstractions;
using Extensions = Ogu.Response.Abstractions.Extensions;

namespace Unit.Tests.Response;

public class ErrorExtensionsTests
{
    [Fact]
    public void ToError_WithEnum_ReturnsExpectedError()
    {
        const Sample sample = Sample.First;

        // Act
        var error = ErrorExtensions.ToError(sample);

        // Assert
        Assert.NotNull(error);
        Assert.Equal(ResponseDefaults.ErrorTitles.Error, error.Title);
        Assert.Equal($"{sample}", error.Description);
        Assert.Null(error.Traces);
        Assert.Null(error.HelpLink);
        Assert.Equal($"{(int)sample}", error.Code);
        Assert.Equal(ErrorType.Custom, error.Type);
        Assert.Empty(error.ValidationFailures);
    }

    [Fact]
    public void ToError_WithEnumAndErrorAttribute_ReturnsExpectedError()
    {
        const Sample sample = Sample.Second;

        // Act
        var error = ErrorExtensions.ToError(sample);

        // Assert
        Assert.NotNull(error);
        Assert.Equal("Second Title", error.Title);
        Assert.Equal("Second Description", error.Description);
        Assert.Equal("Second Traces", error.Traces);
        Assert.Equal("Second HelpLink", error.HelpLink);
        Assert.Equal($"{(int)sample}", error.Code);
        Assert.Equal(ErrorType.Custom, error.Type);
        Assert.Empty(error.ValidationFailures);
    }

    [Fact]
    public void ToError_WithEnumAndAttributes_ReturnsExpectedError()
    {
        const Sample sample = Sample.Third;

        // Act
        var error = ErrorExtensions.ToError(sample);

        // Assert
        Assert.NotNull(error);
        Assert.Equal(string.Empty, error.Title);
        Assert.Equal("Third Title", Extensions.GetTitleFromEnum(typeof(Sample), nameof(Sample.Third)));
        Assert.Null(error.Description);
        Assert.Null(error.Traces);
        Assert.Null(error.HelpLink);
        Assert.Equal($"{(int)sample}", error.Code);
        Assert.Equal(ErrorType.Custom, error.Type);
        Assert.Empty(error.ValidationFailures);

    }

    [Fact]
    public void ToException_WithError_ShouldReturnException()
    {
        var error = new Error(
            "Test Title",
            "Test Description",
            "Test Traces",
            "1",
            "Test HelpLink",
            [],
            ErrorType.Custom);

        // Act
        var exception = ErrorExtensions.ToException(error);

        // Assert
        Assert.NotNull(exception);
        Assert.Equal(1, exception.HResult);
        Assert.True(exception.Data.Contains("Title"));
        Assert.True(exception.Data.Contains("Traces"));
        Assert.True(exception.Data.Contains("Code"));
        Assert.True(exception.Data.Contains("Type"));
        Assert.Equal(error.HelpLink, exception.HelpLink);
        Assert.Equal(error.Title, exception.Data["Title"]);
        Assert.Equal(error.Description, exception.Message);
        Assert.Equal(error.Traces, exception.Data["Traces"]);
        Assert.Equal(error.Code, exception.Data["Code"]);
        Assert.Equal(error.Type, exception.Data["Type"]);
        Assert.Null(exception.Data["ValidationFailures"]);
    }

    [Fact]
    public void ToException_WithEmptyList_ShouldReturnNullException()
    {
        IError[] error = [];

        // Act
        var exception = ErrorExtensions.ToException(error);

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ToException_WithSingleItemList_ShouldReturnException()
    {
        Error[] errors =
        [
            new Error(
                "Test Title",
                "Test Description",
                "Test Traces",
                "1",
                "Test HelpLink",
                [],
                ErrorType.Custom)
        ];

        // Act
        var exception = ErrorExtensions.ToException(errors);

        // Assert
    }

    [Fact]
    public void ToException_WithErrors_ShouldReturnAggregateException()
    {
        Error[] errors =
        [
            new Error(
                "Test Title 1",
                "Test Description 1",
                "Test Traces 1",
                "1",
                "Test HelpLink 1",
                [],
                ErrorType.Custom),
            new Error(
                "Test Title 2",
                "Test Description 2",
                "Test Traces 2",
                "2",
                "Test HelpLink 2",
                [],
                ErrorType.Custom)
        ];

        // Act
        var exception = ErrorExtensions.ToException(errors);

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<AggregateException>(exception);

        var exceptions = ExtractLeafExceptions(exception).ToArray();

        Assert.All(exceptions, e =>
        {
            Assert.True(e.Data.Contains("Title"));
            Assert.True(e.Data.Contains("Traces"));
            Assert.True(e.Data.Contains("Code"));
            Assert.True(e.Data.Contains("Type"));
            Assert.Equal(ErrorType.Custom, e.Data["Type"]);
            Assert.NotEmpty(e.Message);
        });
    }

    public enum Sample
    {
        First,

        [Error("Second Title", "Second Description", "Second Traces", "Second HelpLink")]
        Second,

        [Error("")]
        [Title("Third Title")]
        Third
    }

    public static IEnumerable<Exception> ExtractLeafExceptions(Exception exception)
    {
        while (true)
        {
            if (exception is AggregateException aggregate)
            {
                foreach (var inner in aggregate.InnerExceptions)
                {
                    foreach (var ex in ExtractLeafExceptions(inner))
                    {
                        yield return ex;
                    }
                }
            }
            else if (exception.InnerException is not null)
            {
                exception = exception.InnerException;
                continue;
            }
            else
            {
                yield return exception;
            }

            break;
        }
    }
}