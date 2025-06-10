using Ogu.Response;
using Ogu.Response.Abstractions;
using System.Net;

namespace Unit.Tests.Response;

public class ResponseTTests
{
    [Fact]
    public void Constructor_WhenCalled_InitializesCorrectly()
    {
        const string data = "";
        const bool success = true;
        const HttpStatusCode status = HttpStatusCode.OK;
        Dictionary<string, object>? extras = null;
        List<IError>? errors = null;

        // Act
        var responseT = new Response<string>(data, success, status, extras, errors);

        // Assert
        Assert.NotNull(responseT);
        Assert.Equal(data, responseT.Data);
        Assert.Equal(success, responseT.Success);
        Assert.Equal(status, responseT.Status);
        Assert.Empty(responseT.Extras);
        Assert.Empty(responseT.Errors);
    }

    [Fact]
    public void Failure_WhenCalled_ReturnResponseT()
    {
        const HttpStatusCode status = HttpStatusCode.BadRequest;

        // Act
        var response = Response<string>.Failure(status, []);

        // Assert
        Assert.NotNull(response);
        Assert.Null(response.Data);
        Assert.False(response.Success);
        Assert.Equal(status, response.Status);
        Assert.Empty(response.Extras);
        Assert.Empty(response.Errors);
    }

    [Fact]
    public void ResponseT_WhenImplicitCastToResponse_InitializesCorrectly()
    {
        const string data = "";
        const bool success = true;
        const HttpStatusCode status = HttpStatusCode.OK;
        Dictionary<string, object>? extras = null;
        List<IError>? errors = null;

        var responseT = new Response<string>(data, success, status, extras, errors);

        // Act
        var response = (Ogu.Response.Response)responseT;

        // Assert
        Assert.NotNull(response);
        Assert.Equal(data, response.Data);
        Assert.Equal(success, response.Success);
        Assert.Equal(status, response.Status);
        Assert.Empty(response.Extras);
        Assert.Empty(response.Errors);
    }
}