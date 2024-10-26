using Ogu.Response.Abstractions;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public class JsonResponse<T> : IJsonResponse<T>
    {
        [JsonConstructor]
        public JsonResponse(T data, bool success, HttpStatusCode status, IDictionary<string, object> extras, List<IError> errors, object serializedResponse, JsonSerializerOptions serializerOptions)
        {
            Data = data;
            Success = success;
            Status = status;
            Errors = errors ?? new List<IError>();
            Extras = extras ?? new Dictionary<string, object>();
            SerializedResponse = serializedResponse;
            SerializerOptions = serializerOptions ?? Constants.DefaultJsonSerializerOptions;
        }

        public bool Success { get; }

        public HttpStatusCode Status { get; }

        [JsonIgnore]
        public object SerializedResponse { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T Data { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<IError> Errors { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, object> Extras { get; }

        [JsonIgnore]
        public JsonSerializerOptions SerializerOptions { get; }

        public static JsonResponse<T> Failure(HttpStatusCode statusCode, List<IError> errors, JsonSerializerOptions serializerOptions = null)
        {
            return new JsonResponse<T>(default, false, statusCode, null, errors, null, serializerOptions);
        }

        public static implicit operator JsonResponse(JsonResponse<T> response)
        {
            return new JsonResponse(response.Data, response.Success, response.Status, response.Extras, response.Errors, response.SerializedResponse, response.SerializerOptions);
        }
    }
}