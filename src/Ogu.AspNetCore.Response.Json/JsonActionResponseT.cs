using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ogu.AspNetCore.Response.Abstractions;
using Ogu.Response.Abstractions;
using Ogu.Response.Json;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ogu.AspNetCore.Response.Json
{
    public class JsonActionResponse<TData> : IActionResponse<TData>
    {
        private readonly JsonSerializerOptions _serializerOptions;

        [JsonConstructor]
        public JsonActionResponse(TData data, bool success, HttpStatusCode status, IDictionary<string, object> extras,  List<IError> errors)
        {
            Data = data;
            Success = success;
            Status = status;
            Errors = errors ?? new List<IError>();
            Extras = extras ?? new Dictionary<string, object>();
        }

        public JsonActionResponse(IResponse<TData> response)
        {
            Data = response.Data;
            Success = response.Success;
            Status = response.Status;
            Errors = response.Errors ?? new List<IError>();
            Extras = response.Extras ?? new Dictionary<string, object>();
            _serializerOptions = response is JsonResponse<TData> jsonResponse ? jsonResponse.SerializerOptions : JsonResponse.DefaultSerializerOptions;
            SerializedResponse = response.SerializedResponse;
        }

        public JsonActionResponse(IJsonResponse<TData> response)
        {
            Data = response.Data;
            Status = response.Status;
            Success = response.Success;
            Errors = response.Errors ?? new List<IError>();
            Extras = response.Extras ?? new Dictionary<string, object>();
            _serializerOptions = response.SerializerOptions;
            SerializedResponse = response.SerializedResponse;
        }

        public bool Success { get; }

        public HttpStatusCode Status { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TData Data { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<IError> Errors { get; internal set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, object> Extras { get; internal set; }

        [JsonIgnore]
        public object SerializedResponse { get; set; }

        public Task ExecuteResultAsync(HttpContext context)
        {
            return context.ExecuteJsonResponseAsync(this, SerializedResponse, Status, _serializerOptions);
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return context.ExecuteJsonResponseAsync(this, SerializedResponse, Status, _serializerOptions);
        }
    }
}