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
    public class JsonActionResponse<T> : IActionResponse<T>
    {
        private readonly JsonSerializerOptions _serializerOptions;

        [JsonConstructor]
        public JsonActionResponse(JsonResponse<T> response)
        {
            Result = response.Result;
            Status = response.Status;
            Success = response.Success;
            _serializerOptions = response.SerializerOptions;
            SerializedResponse = response.SerializedResponse;
        }

        public JsonActionResponse(IResponse<T> response)
        {
            Result = response.Result;
            Status = response.Status;
            Success = response.Success;
            _serializerOptions = response is JsonResponse<T> jsonResponse ? jsonResponse.SerializerOptions : JsonResponse.DefaultJsonSerializerOptions;
            SerializedResponse = response.SerializedResponse;
        }

        public JsonActionResponse(IJsonResponse<T> response)
        {
            Result = response.Result;
            Status = response.Status;
            Success = response.Success;
            _serializerOptions = response.SerializerOptions;
            SerializedResponse = response.SerializedResponse;
        }

        public bool Success { get; }

        public int Status { get; }

        [JsonIgnore]
        public string SerializedResponse { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IResponseResult<T> Result { get; }

        public Task ExecuteResultAsync(ActionContext context)
            => JsonActionResponse.ExecuteResponseAsync(context, this, SerializedResponse, Status, _serializerOptions);

        public Task ExecuteResultAsync(HttpContext context)
            => JsonActionResponse.ExecuteResponseAsync(context, this, SerializedResponse, Status, _serializerOptions);

        public static implicit operator JsonResponse<T>(JsonActionResponse<T> response)
        {
            return new JsonResponse<T>(response.Result.Data, response.Result, response.Status, response.Success,
                response._serializerOptions, response.SerializedResponse);
        }
    }
}