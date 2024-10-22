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
        public JsonResponse(T data, bool success, HttpStatusCode statusCode, IDictionary<string, object> extensions, IList<IResponseError> errors, string serializedResponse, JsonSerializerOptions serializerOptions)
        {
            Data = data;
            Success = success;
            StatusCode = statusCode;
            Extensions = extensions ?? new Dictionary<string, object>();
            Errors = errors ?? new List<IResponseError>();
            SerializedResponse = serializedResponse;
            SerializerOptions = serializerOptions;
        }

        public T Data { get; }

        public bool Success { get; }

        public HttpStatusCode StatusCode { get; }

        public string SerializedResponse { get; set; }

        public IDictionary<string, object> Extensions { get; }

        public IList<IResponseError> Errors { get; }

        public JsonSerializerOptions SerializerOptions { get; }

        public static implicit operator JsonResponse(JsonResponse<T> response) => new JsonResponse(response.Data,
            response.Success, response.StatusCode, response.Extensions, response.Errors,
            response.SerializedResponse, response.SerializerOptions);
    }
}