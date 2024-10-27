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
        [JsonConstructor]
        public JsonResponse(object data, bool success, HttpStatusCode status, IDictionary<string, object> extras, List<IError> errors, object serializedResponse, JsonSerializerOptions serializerOptions)
        {
            Data = data;
            Success = success;
            Status = status;
            Errors = errors ?? new List<IError>();
            Extras = extras ?? new Dictionary<string, object>();
            SerializedResponse = serializedResponse;
            SerializerOptions = serializerOptions ?? DefaultSerializerOptions;
        }

        public bool Success { get; }

        public HttpStatusCode Status { get; }

        [JsonIgnore]
        public object SerializedResponse { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public object Data { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<IError> Errors { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, object> Extras { get; }

        [JsonIgnore]
        public JsonSerializerOptions SerializerOptions { get; }

        public static JsonResponse Failure(HttpStatusCode statusCode, List<IError> errors, JsonSerializerOptions serializerOptions = null)
        {
            return new JsonResponse(null, false, statusCode, null, errors, null, serializerOptions);
        }

        private static readonly Lazy<JsonSerializerOptions> LazyDefaultSerializerOptions =
            new Lazy<JsonSerializerOptions>(() => new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonDictionaryKeyConverter<string>() }
            }, LazyThreadSafetyMode.ExecutionAndPublication);

        public static JsonSerializerOptions DefaultSerializerOptions { get; } = LazyDefaultSerializerOptions.Value;
    }
}