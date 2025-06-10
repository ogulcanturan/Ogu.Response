using Ogu.Response.Abstractions;
using System.Net;

namespace Unit.Tests.Response;

public class ResponseTests
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
        var response = new Ogu.Response.Response(data, success, status, extras, errors);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(data, response.Data);
        Assert.Equal(success, response.Success);
        Assert.Equal(status, response.Status);
        Assert.Empty(response.Extras);
        Assert.Empty(response.Errors);
    }

    [Fact]
    public void Failure_WhenCalled_ReturnResponse()
    {
        const HttpStatusCode status = HttpStatusCode.BadRequest;

        // Act
        var response = Ogu.Response.Response.Failure(status, []);

        // Assert
        Assert.NotNull(response);
        Assert.Null(response.Data);
        Assert.False(response.Success);
        Assert.Equal(status, response.Status);
        Assert.Empty(response.Extras);
        Assert.Empty(response.Errors);
    }
}