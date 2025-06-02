using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace Sample.Api.Dtos;

public class ResponseDto : ResponseDto<object>
{
}

public class ResponseDto<T>
{
    public bool Success { get; init; }

    public HttpStatusCode Status { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T Data { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ErrorDto[] Errors { get; init; } = [];

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object> Extras { get; init; } = [];
}