using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ogu.AspNetCore.Response.Abstractions;
using Ogu.Response.Abstractions;
using Ogu.Response.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ogu.AspNetCore.Response.Json
{
    public class JsonActionResponse : IActionResponse
    {
        private readonly JsonSerializerOptions _serializerOptions;

        [JsonConstructor]
        public JsonActionResponse(JsonResponse response)
        {
            Result = response.Result;
            Status = response.Status;
            Success = response.Success;
            _serializerOptions = response.SerializerOptions;
            SerializedResponse = response.SerializedResponse;
        }

        public JsonActionResponse(IResponse response)
        {
            Result = response.Result;
            Status = response.Status;
            Success = response.Success;
            _serializerOptions = response is JsonResponse jsonResponse ? jsonResponse.SerializerOptions : JsonResponse.DefaultJsonSerializerOptions;
            SerializedResponse = response.SerializedResponse;
        }

        public JsonActionResponse(IJsonResponse response)
        {
            Result = response.Result;
            Status = response.Status;
            Success = response.Success;
            _serializerOptions = response.SerializerOptions;
            SerializedResponse = response.SerializedResponse;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Data { get; set; }

        public bool Success { get; set; }

        public int Status { get; set; }

        [JsonIgnore]
        public string SerializedResponse { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IResponseResult<object> Result { get; }

        public Task ExecuteResultAsync(ActionContext context)
            => ExecuteResponseAsync(context, this, SerializedResponse, Status, _serializerOptions);

        public Task ExecuteResultAsync(HttpContext context)
            => ExecuteResponseAsync(context, this, SerializedResponse, Status, _serializerOptions);

        public static Task ExecuteResponseAsync(ActionContext actionContext, object obj, string serializedResponse, int status, JsonSerializerOptions serializerOptions)
        {
            return actionContext.ExecuteJsonResponseAsync(obj, serializedResponse, status, serializerOptions);
        }

        public static Task ExecuteResponseAsync(HttpContext context, object obj, string serializedResponse, int status, JsonSerializerOptions serializerOptions)
        {
            return context.ExecuteJsonResponseAsync(obj, serializedResponse, status, serializerOptions);
        }

        public static implicit operator JsonResponse(JsonActionResponse response)
        {
            return new JsonResponse(response.Data, response.Result, response.Status, response.Success,
                response._serializerOptions, response.SerializedResponse);
        }
    }
}