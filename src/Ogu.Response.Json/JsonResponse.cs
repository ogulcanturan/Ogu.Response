using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace Ogu.Response.Json
{
    public class JsonResponse : IJsonResponse
    {
        private static readonly Lazy<JsonSerializerOptions> LazyDefaultJsonSerializerOptions =
            new Lazy<JsonSerializerOptions>(() => new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonDictionaryKeyConverter<string>() }
            }, LazyThreadSafetyMode.ExecutionAndPublication);

        public static JsonSerializerOptions DefaultJsonSerializerOptions => LazyDefaultJsonSerializerOptions.Value;

        [JsonConstructor]
        public JsonResponse(object data, bool success, HttpStatusCode statusCode, IDictionary<string, object> extensions, IList<IResponseError> errors, string serializedResponse, JsonSerializerOptions serializerOptions)
        {
            Data = data;
            Success = success;
            StatusCode = statusCode;
            Extensions = extensions ?? new Dictionary<string, object>();
            Errors = errors ?? new List<IResponseError>();
            SerializedResponse = serializedResponse;
            SerializerOptions = serializerOptions;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Data { get; }

        public bool Success { get; }

        public HttpStatusCode StatusCode { get; }

        public string SerializedResponse { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, object> Extensions { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<IResponseError> Errors { get; }

        public JsonSerializerOptions SerializerOptions { get; }
    }
}