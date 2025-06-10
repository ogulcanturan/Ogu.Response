using Ogu.Response;
using Ogu.Response.Abstractions;
using System.Net;

namespace Unit.Tests.Response;

public class ResponseExtensionsTests
{
    [Fact]
    public void ToResponse_ShouldConvertIResponseTDataToResponse()
    {
        const string data = "Test Data";

        // Arrange
        var typedResponse = HttpStatusCode.OK.ToSuccessResponseOf<string>(data);

        // Act
        IResponse response = ResponseExtensions.ToResponse(typedResponse);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.Status);
        Assert.True(response.Success);
        Assert.Equal(data, response.Data);
        Assert.Empty(response.Errors);
        Assert.Empty(response.Extras);
    }

    [Fact]
    public void ToSuccessResponse_ShouldReturnSuccessfulResponse()
    {
        // Act
        IResponse response = HttpStatusCode.OK.ToSuccessResponse();

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.Status);
        Assert.True(response.Success);
        Assert.Null(response.Data);
        Assert.Empty(response.Errors);
        Assert.Empty(response.Extras);
    }

    [Fact]
    public void ToSuccessResponse_WithObjectData_ShouldReturnSuccessfulResponse()
    {
        object data = "Test Data";

        // Act
        IResponse response = HttpStatusCode.OK.ToSuccessResponse(data);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.Status);
        Assert.True(response.Success);
        Assert.Equal(data, response.Data);
        Assert.Empty(response.Errors);
        Assert.Empty(response.Extras);
    }

    [Fact]
    public void ToFailureResponse_ShouldReturnFailureResponse()
    {
        // Act
        IResponse response = ResponseExtensions.ToFailureResponse(HttpStatusCode.NotFound);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.Status);
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.Empty(response.Errors);
        Assert.Empty(response.Extras);
    }

    [Fact]
    public void ToFailureResponse_WithError_ShouldReturnFailureResponse()
    {
        var error = new Error("Test Error");

        // Act
        IResponse response = ResponseExtensions.ToFailureResponse(HttpStatusCode.NotFound, error);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.Status);
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.Errors);
        Assert.Equal(error, response.Errors.First());
        Assert.Empty(response.Extras);
    }

    [Fact]
    public void ToFailureResponse_WithErrorList_ShouldReturnFailureResponse()
    {
        List<IError> errors =
        [
            new Error("Err1"),
            new Error("Err2")
        ];

        // Act
        IResponse response = ResponseExtensions.ToFailureResponse(HttpStatusCode.NotFound, errors);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.Status);
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.Errors);
        Assert.Equal(errors, response.Errors);
        Assert.Empty(response.Extras);
    }

    [Fact]
    public void ToFailureResponse_WithValidationFailure_ShouldReturnFailureResponse()
    {
        var validationFailure = new ValidationFailure("Id", "Id is required.");

        // Act
        IResponse response = ResponseExtensions.ToFailureResponse(HttpStatusCode.BadRequest, validationFailure);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.Status);
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.Errors);
        Assert.Equal(validationFailure, response.Errors.First().ValidationFailures.First());
        Assert.Empty(response.Extras);
    }

    [Fact]
    public void ToFailureResponse_WithValidationFailureList_ShouldReturnFailureResponse()
    {
        var validationFailures = new List<IValidationFailure>
        {
            new ValidationFailure("Id", "Id is required."),
            new ValidationFailure("ProductId", "ProductId is required."),
        };

        // Act
        IResponse response = ResponseExtensions.ToFailureResponse(HttpStatusCode.BadRequest, validationFailures);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.Status);
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.Errors);
        Assert.Equal(validationFailures, response.Errors.First().ValidationFailures);
        Assert.Empty(response.Extras);
    }

    [Fact]
    public void ToFailureResponse_WithEnum_ShouldReturnFailureResponse()
    {
        // Act
        IResponse response = ResponseExtensions.ToFailureResponse<Sample>(HttpStatusCode.BadRequest, Sample.First);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.Status);
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.Errors);
        Assert.Empty(response.Extras);

        var error = response.Errors.First();

        Assert.Equal("Error", error.Title);
        Assert.Equal("First", error.Description);
        Assert.Null(error.Traces);
        Assert.Null(error.HelpLink);
        Assert.Equal($"{(int)Sample.First}", error.Code);
        Assert.Empty(error.ValidationFailures);
    }

    [Fact]
    public void ToFailureResponse_WithEnums_ShouldReturnFailureResponse()
    {
        Sample[] failures =
        [
            Sample.Second,
            Sample.Third
        ];

        // Act
        IResponse response = ResponseExtensions.ToFailureResponse<Sample>(HttpStatusCode.BadRequest, failures);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.Status);
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.Errors);
        Assert.Empty(response.Extras);
        Assert.Equal(2, response.Errors.Count);
    }

    [Fact]
    public void ToFailureResponse_WithExceptionLevelNone_ShouldReturnFailureResponse()
    {
        var exception = new Exception("Test exception occurred.");

        // Act
        IResponse response = ResponseExtensions.ToFailureResponse(HttpStatusCode.InternalServerError, exception, ExceptionTraceLevel.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.Status);
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.Errors);
        Assert.Empty(response.Extras);

        var error = response.Errors.First();

        Assert.Equal("Exception", error.Title);
        Assert.Equal(ResponseDefaults.ErrorDescriptions.SomethingWentWrong, error.Description);
        Assert.Null(error.Traces);
        Assert.Null(error.HelpLink);
        Assert.Null(error.Code);
        Assert.Empty(error.ValidationFailures);
    }

    [Fact]
    public void ToFailureResponse_WithExceptionLevelBasic_ShouldReturnFailureResponse()
    {
        var exception = new Exception("Test exception occurred.");

        // Act
        IResponse response = ResponseExtensions.ToFailureResponse(HttpStatusCode.InternalServerError, exception, ExceptionTraceLevel.Basic);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.Status);
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.Errors);
        Assert.Empty(response.Extras);

        var error = response.Errors.First();

        Assert.Equal("Exception", error.Title);
        Assert.Equal(exception.Message, error.Description);
        Assert.Null(error.Traces);
        Assert.Null(error.HelpLink);
        Assert.Null(error.Code);
        Assert.Empty(error.ValidationFailures);
    }

    [Fact]
    public void ToFailureResponse_WithExceptionLevelSummary_ShouldReturnFailureResponse()
    {
        var exception = new Exception("Test exception occurred.");

        // Act
        IResponse response = ResponseExtensions.ToFailureResponse(HttpStatusCode.InternalServerError, exception, ExceptionTraceLevel.Summary);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.Status);
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.Errors);
        Assert.Empty(response.Extras);

        var error = response.Errors.First();

        Assert.Equal("Exception", error.Title);
        Assert.Equal(exception.Message, error.Description);
        Assert.NotNull(error.Traces);
        Assert.Null(error.HelpLink);
        Assert.NotNull(error.Code);
        Assert.Empty(error.ValidationFailures);
    }

    [Fact]
    public void ToFailureResponse_WithExceptionLevelFull_ShouldReturnFailureResponse()
    {
        var exception = new Exception("Test exception occurred.");

        // Act
        IResponse response = ResponseExtensions.ToFailureResponse(HttpStatusCode.InternalServerError, exception, ExceptionTraceLevel.Full);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.Status);
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.Errors);
        Assert.Empty(response.Extras);

        var error = response.Errors.First();

        Assert.Equal("Exception", error.Title);
        Assert.Equal(exception.Message, error.Description);
        Assert.NotNull(error.Traces);
        Assert.Null(error.HelpLink);
        Assert.NotNull(error.Code);
        Assert.Empty(error.ValidationFailures);
    }

    [Fact]
    public void ToFailureResponse_WithExceptions_ShouldReturnFailureResponse()
    {
        Exception[] exceptions =
        [
            new("Test 1) exception occurred."),
            new("Test 2) exception occurred.")
        ];

        // Act
        IResponse response = ResponseExtensions.ToFailureResponse(HttpStatusCode.InternalServerError, exceptions);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.Status);
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.Errors);
        Assert.Empty(response.Extras);
        Assert.Equal(2, response.Errors.Count);
    }

    [Fact]
    public void ToFailureResponse_WithStringError_ShouldReturnFailureResponse()
    {
        const string description = "Something went wrong.";

        // Act
        IResponse response = ResponseExtensions.ToFailureResponse(HttpStatusCode.InternalServerError, description);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.Status);
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.Errors);
        Assert.Empty(response.Extras);

        var error = response.Errors.First();

        Assert.Equal("Error", error.Title);
        Assert.Equal(description, error.Description);
        Assert.Null(error.Traces);
        Assert.Null(error.HelpLink);
        Assert.Null(error.Code);
        Assert.Empty(error.ValidationFailures);
    }

    [Fact]
    public void ToFailureResponse_WithStringTitleAndDescription_ShouldReturnFailureResponse()
    {
        const string title = "Custom Error!";
        const string description = "Something went wrong.";

        // Act
        IResponse response = ResponseExtensions.ToFailureResponse(HttpStatusCode.InternalServerError, title, description);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.Status);
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.Errors);
        Assert.Empty(response.Extras);

        var error = response.Errors.First();

        Assert.Equal(title, error.Title);
        Assert.Equal(description, error.Description);
        Assert.Null(error.Traces);
        Assert.Null(error.HelpLink);
        Assert.Null(error.Code);
        Assert.Empty(error.ValidationFailures);
    }

    [Fact]
    public void ToFailureResponse_WithStringDescriptions_ShouldReturnFailureResponse()
    {
        HashSet<string> errors = ["Something went wrong.", "Unknown error occurred."];

        // Act
        IResponse response = ResponseExtensions.ToFailureResponse(HttpStatusCode.InternalServerError, errors);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.Status);
        Assert.False(response.Success);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.Errors);
        Assert.Empty(response.Extras);
        Assert.Equal(2, response.Errors.Count);
        Assert.All(response.Errors.Select(e => e.Description), e => Assert.Contains(e, errors));
    }

    public enum Sample
    {
        First,
        Second,
        Third
    }
}