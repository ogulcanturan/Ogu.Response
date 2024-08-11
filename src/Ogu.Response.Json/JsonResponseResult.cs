using Ogu.Response.Abstractions;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public class JsonResponseResult : IResponseResult
    {
        public JsonResponseResult(string title, string detail, int? status, string type, string instance, string code, bool hasError, IDictionary<string, object> extensions)
        {
            Title = title;
            Detail = detail;
            Status = status;
            Type = type;
            Instance = instance;
            Code = code;
            HasError = hasError;
            Extensions = extensions;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Type { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Title { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Status { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Code { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Detail { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Instance { get; }

        public bool HasError { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, object> Extensions { get; }

        public static JsonResponseResultBuilder Builder => new JsonResponseResultBuilder();
    }
}