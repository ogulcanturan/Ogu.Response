using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Ogu.AspNetCore.Response.Json
{
    public class Response : IResponse
    {
        private readonly JsonSerializerOptions _serializerOptions;
        private const string ResponseContentType = "application/json";

        [JsonConstructor]
        public Response(object data, IResult result, int status, bool success, JsonSerializerOptions serializerOptions = null, string serializedResponse = null)
        {
            Data = data;
            Result = result;
            Status = status;
            Success = success;
            _serializerOptions = serializerOptions ?? DefaultJsonSerializerOptions;
            SerializedResponse = serializedResponse;
        }

        public Response(string serializedResponse, int status) : this(null, null, status, false, null, serializedResponse) { }

        public Response(object data, int status, bool success, JsonSerializerOptions serializerOptions = null) : this(data, null, status, success, serializerOptions) { }

        public Response(int status, bool success, JsonSerializerOptions serializerOptions = null) : this(null, null, status, success, serializerOptions) { }

        public Response(IResult result, int status, JsonSerializerOptions serializerOptions = null) : this(null, result, status, false, serializerOptions) { }

        public Response(IResult result, int status, bool success, JsonSerializerOptions serializerOptions = null) : this(null, result, status, success, serializerOptions) { }

        public Response(object data, IResult result, int status, bool success) : this(data, result, status, success, null) { }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Data { get; set; }

        public bool Success { get; set; }

        public int Status { get; set; }

        [JsonIgnore]
        public string SerializedResponse { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IResult Result { get; set; }

        public Task ExecuteResultAsync(ActionContext context)
            => ExecuteResponseAsync(context, this, SerializedResponse, Status, _serializerOptions);

        public static Task ExecuteResponseAsync(ActionContext context, object obj, string serializedResponse, int status, JsonSerializerOptions serializerOptions)
        {
            var response = context.HttpContext.Response;
            response.ContentType = ResponseContentType;
            response.StatusCode = status;

            var json = serializedResponse ?? JsonSerializer.Serialize(obj, serializerOptions);

            return response.WriteAsync(json, context.HttpContext.RequestAborted);
        }

        public static JsonSerializerOptions DefaultJsonSerializerOptions { get; set; } = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonDictionaryKeyConverter<string>() }
        };

        public static Response Other(object data, int status, bool success, IResult result = null,
            JsonSerializerOptions serializerOptions = null) 
            => new Response(data, result, status, success, serializerOptions);

        public static Response Successful(object data, int status, IResult result = null,
            JsonSerializerOptions serializerOptions = null) 
            => new Response(data, result, status, true, serializerOptions);

        public static Response Failure(int status, IResult result = null, object data = null,
            JsonSerializerOptions serializerOptions = null) 
            => new Response(data, result, status, false, serializerOptions);

        public static Response Failure(int status, IValidationFailure[] validationFailures, object data = null,
            JsonSerializerOptions serializerOptions = null) 
            => new Response(data, Json.Result.ValidationFailure(validationFailures), status, false, serializerOptions);

        public static Response Failure(int status, ModelStateDictionary modelState, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new Response(data, Json.Result.ValidationFailure(ValidationFailure.ToValidationFailures(modelState)), status, false, serializerOptions);

        public static Response Failure<TEnum>(int status, TEnum @enum, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum 
            => new Response(data, Json.Result.CustomFailure(@enum), status, false, serializerOptions);

        public static Response Failure<TEnum>(int status, TEnum[] @enums, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => new Response(data, Json.Result.CustomFailure(@enums), status, false, serializerOptions);

        public static Response Failure(int status, Exception exception, bool includeTraces = false, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new Response(data, Json.Result.ExceptionFailure(exception, includeTraces), status, false, serializerOptions);

        public static Response Failure(int status, string error, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new Response(data, Json.Result.CustomFailure(error), status, false, serializerOptions);
    }
}