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
}