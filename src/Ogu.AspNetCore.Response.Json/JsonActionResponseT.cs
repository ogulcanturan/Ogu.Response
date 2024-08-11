using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ogu.AspNetCore.Response.Abstractions;
using Ogu.Response.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Ogu.Response.Abstractions;
using IResponseResult = Ogu.Response.Abstractions.IResponseResult;

namespace Ogu.AspNetCore.Response.Json
{
    public class JsonActionResponse<T> : IActionResponse<T>
    {
        private readonly JsonSerializerOptions _serializerOptions;

        [JsonConstructor]
        public JsonActionResponse(JsonResponse<T> response)
        {
            Data = response.Data;
            Result = response.Result;
            Status = response.Status;
            Success = response.Success;
            _serializerOptions = response.SerializerOptions;
            SerializedResponse = response.SerializedResponse;
        }

        public JsonActionResponse(IResponse<T> response)
        {
            Data = response.Data;
            Result = response.Result;
            Status = response.Status;
            Success = response.Success;
            _serializerOptions = response is JsonResponse<T> jsonResponse ? jsonResponse.SerializerOptions : JsonResponse.DefaultJsonSerializerOptions;
            SerializedResponse = response.SerializedResponse;
        }

        public JsonActionResponse(IJsonResponse<T> response)
        {
            Data = response.Data;
            Result = response.Result;
            Status = response.Status;
            Success = response.Success;
            _serializerOptions = response.SerializerOptions;
            SerializedResponse = response.SerializedResponse;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T Data { get; }

        public bool Success { get; }

        public int Status { get; }

        [JsonIgnore]
        public string SerializedResponse { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IResponseResult Result { get; }

        public Task ExecuteResultAsync(ActionContext context)
            => JsonActionResponse.ExecuteResponseAsync(context, this, SerializedResponse, Status, _serializerOptions);

        public Task ExecuteResultAsync(HttpContext context)
            => JsonActionResponse.ExecuteResponseAsync(context, this, SerializedResponse, Status, _serializerOptions);

        public static implicit operator JsonResponse<T>(JsonActionResponse<T> response)
        {
            return new JsonResponse<T>(response.Data, response.Result, response.Status, response.Success,
                response._serializerOptions, response.SerializedResponse);
        }
    }
}