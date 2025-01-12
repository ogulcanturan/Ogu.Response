using Ogu.Response.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public class JsonResponse : IJsonResponse
    {
        [JsonConstructor]
        public JsonResponse(object data, bool success, HttpStatusCode status, Dictionary<string, object> extras, List<JsonError> errors, object serializedResponse, JsonSerializerOptions serializerOptions) : this(data, success, status, extras, new List<IError>(errors ?? Enumerable.Empty<JsonError>()), serializedResponse, serializerOptions) { }
        
        public JsonResponse(object data, bool success, HttpStatusCode status, IDictionary<string, object> extras, List<IError> errors, object serializedResponse, JsonSerializerOptions serializerOptions)
        {
            Data = data;
            Success = success;
            Status = status;
            Errors = errors ?? new List<IError>();
            Extras = extras ?? new Dictionary<string, object>();
            SerializedResponse = serializedResponse;
            SerializerOptions = serializerOptions;
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
    }
}