using System.Text.Json.Serialization;
using Ogu.Response.Abstractions;

namespace Sample.Api.Dtos;

public class ValidationFailureDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string PropertyName { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Message { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object AttemptedValue { get; init; }

    public Severity Severity { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Code { get; init; }
}