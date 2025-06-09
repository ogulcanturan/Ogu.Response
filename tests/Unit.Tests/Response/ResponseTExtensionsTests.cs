using Ogu.Response;
using Ogu.Response.Abstractions;
using System;
using System.Net;

namespace Unit.Tests.Response;

public class ResponseTExtensionsTests
{
    [Fact]
    public void ToResponseOf_WithGenericData_ShouldConvertIResponseToIResponseTData()
    {
        const string data = "Test Data";

        // Arrange
        var response = HttpStatusCode.OK.ToSuccessResponse(data);

        // Act
        IResponse<string> typedResponse = ResponseTExtensions.ToResponseOf<string>(response);

        // Assert
        Assert.NotNull(typedResponse);
        Assert.Equal(HttpStatusCode.OK, typedResponse.Status);
        Assert.True(typedResponse.Success);
        Assert.Equal(data, typedResponse.Data);
        Assert.Empty(typedResponse.Errors);
        Assert.Empty(typedResponse.Extras);
    }

    [Fact]
    public void ToResponseOf_WithObjectData_ShouldConvertIResponseToIResponseTData()
    {
        object data = "Test Data";

        // Arrange
        var response = HttpStatusCode.OK.ToSuccessResponse(data);

        // Act
        IResponse<string> typedResponse = ResponseTExtensions.ToResponseOf<string>(response);

        // Assert
        Assert.NotNull(typedResponse);
        Assert.Equal(HttpStatusCode.OK, typedResponse.Status);
        Assert.True(typedResponse.Success);
        Assert.Equal(data, typedResponse.Data);
        Assert.Empty(typedResponse.Errors);
        Assert.Empty(typedResponse.Extras);
    }

    [Fact]
    public void ToResponseOf_WithNullObjectData_ShouldConvertIResponseToIResponseTData()
    {
        object? data = null;

        // Arrange
        var response = HttpStatusCode.OK.ToSuccessResponse(data);

        // Act
        IResponse<string> typedResponse = ResponseTExtensions.ToResponseOf<string>(response);

        // Assert
        Assert.NotNull(typedResponse);
        Assert.Equal(HttpStatusCode.OK, typedResponse.Status);
        Assert.True(typedResponse.Success);
        Assert.Equal(data, typedResponse.Data);
        Assert.Empty(typedResponse.Errors);
        Assert.Empty(typedResponse.Extras);
    }

    [Fact]
    public void ToResponseOf_WithWrongType_ShouldThrowFormatException()
    {
        object data = "Five";

        // Arrange
        var response = HttpStatusCode.OK.ToSuccessResponse(data);

        // Act
        var exception = Record.Exception(() => ResponseTExtensions.ToResponseOf<int>(response));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<FormatException>(exception);
    }

    [Fact]
    public void ToResponseOf_WithWrongType_ShouldThrowInvalidCastException()
    {
        object data = 10.25m;

        // Arrange
        var response = HttpStatusCode.OK.ToSuccessResponse(data);

        // Act
        var exception = Record.Exception(() => ResponseTExtensions.ToResponseOf<ExceptionTraceLevel>(response));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<InvalidCastException>(exception);
    }

    [Fact]
    public void ToSuccessResponseOf_WithGenericData_ShouldReturnSuccessResponse()
    {
        const string data = "Test Data";

        // Act
        IResponse<string> response = ResponseTExtensions.ToSuccessResponseOf(HttpStatusCode.OK, data);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.Status);
        Assert.True(response.Success);
        Assert.Equal(data, response.Data);
        Assert.Empty(response.Errors);
        Assert.Empty(response.Extras);
    }

    [Fact]
    public void ToSuccessResponseOf_ShouldReturnSuccessResponse()
    {
        // Act
        IResponse<string> response = ResponseTExtensions.ToSuccessResponse<string>(HttpStatusCode.OK);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.Status);
        Assert.True(response.Success);
        Assert.Null(response.Data);
        Assert.Empty(response.Errors);
        Assert.Empty(response.Extras);
    }

    [Fact]
    public void ToFailureResponse_ShouldReturnFailureResponse()
    {
        // Act
        IResponse<string> response = ResponseTExtensions.ToFailureResponse<string>(HttpStatusCode.NotFound);

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
        IResponse<string> response = ResponseTExtensions.ToFailureResponse<string>(HttpStatusCode.NotFound, error);

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
        IResponse<string> response = ResponseTExtensions.ToFailureResponse<string>(HttpStatusCode.NotFound, errors);

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
        IResponse<string> response = ResponseTExtensions.ToFailureResponse<string>(HttpStatusCode.BadRequest, validationFailure);

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
        IResponse<string> response = ResponseTExtensions.ToFailureResponse<string>(HttpStatusCode.BadRequest, validationFailures);

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
        IResponse<string> response = ResponseTExtensions.ToFailureResponse<string, Sample>(HttpStatusCode.BadRequest, Sample.First);

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
        IResponse<string> response = ResponseTExtensions.ToFailureResponse<string, Sample>(HttpStatusCode.BadRequest, failures);

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
        IResponse<string> response = ResponseTExtensions.ToFailureResponse<string>(HttpStatusCode.InternalServerError, exception, ExceptionTraceLevel.None);

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
        IResponse<string> response = ResponseTExtensions.ToFailureResponse<string>(HttpStatusCode.InternalServerError, exception, ExceptionTraceLevel.Basic);

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
        IResponse<string> response = ResponseTExtensions.ToFailureResponse<string>(HttpStatusCode.InternalServerError, exception, ExceptionTraceLevel.Summary);

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
        IResponse<string> response = ResponseTExtensions.ToFailureResponse<string>(HttpStatusCode.InternalServerError, exception, ExceptionTraceLevel.Full);

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
        IResponse<string> response = ResponseTExtensions.ToFailureResponse<string>(HttpStatusCode.InternalServerError, exceptions);

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
        IResponse<string> response = ResponseTExtensions.ToFailureResponse<string>(HttpStatusCode.InternalServerError, description);

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
        IResponse<string> response = ResponseTExtensions.ToFailureResponse<string>(HttpStatusCode.InternalServerError, title, description);

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
        IResponse<string> response = ResponseTExtensions.ToFailureResponse<string>(HttpStatusCode.InternalServerError, errors);

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