using Ogu.Response;
using Ogu.Response.Abstractions;
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
}