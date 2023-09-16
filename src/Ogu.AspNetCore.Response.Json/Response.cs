using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ogu.AspNetCore.Response.Json
{
    public class Response : IResponse
    {
        private readonly JsonSerializerOptions _serializerOptions;
        private const string ResponseContentType = "application/json";
        private static readonly HashSet<int> NoResponseStatusCodes = new HashSet<int>() { 204, 205, 304 };

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

        public Task ExecuteResultAsync(HttpContext context)
            => ExecuteResponseAsync(context, this, SerializedResponse, Status, _serializerOptions);

        public static Task ExecuteResponseAsync(ActionContext actionContext, object obj, string serializedResponse, int status, JsonSerializerOptions serializerOptions)
        {
            return ExecuteResponseAsync(actionContext.HttpContext, obj, serializedResponse, status, serializerOptions);
        }

        public static Task ExecuteResponseAsync(HttpContext context, object obj, string serializedResponse, int status, JsonSerializerOptions serializerOptions)
        {
            var response = context.Response;
            response.ContentType = ResponseContentType;
            response.StatusCode = status;

            if (NoResponseStatusCodes.Contains(response.StatusCode))
                return Task.CompletedTask;

            var json = serializedResponse ?? JsonSerializer.Serialize(obj, serializerOptions);

            try
            {
                return response.WriteAsync(json, context.RequestAborted);
            }
            catch (OperationCanceledException)
            {
                return Task.FromCanceled(context.RequestAborted);
            }
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

        public static Response Failure<TEnum>(int status, IList<TEnum> @enums, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => new Response(data, Json.Result.CustomFailure(@enums), status, false, serializerOptions);

        public static Response Failure<TEnum>(int status, TEnum? @enum, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => new Response(data, @enum.HasValue ? Json.Result.CustomFailure(@enum.Value) : null, status, false, serializerOptions);

        public static Response Failure<TEnum>(int status, TEnum?[] @enums, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            var enumArray = @enums.Where(e => e.HasValue).Select(e => e.Value).ToArray();
            return new Response(data, enumArray.Length > 0 ? Json.Result.CustomFailure(enumArray) : null, status, false, serializerOptions);
        }

        public static Response Failure<TEnum>(int status, IList<TEnum?> @enums, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            var enumArray = @enums.Where(e => e.HasValue).Select(e => e.Value).ToArray();
            return new Response(data, enumArray.Length > 0 ? Json.Result.CustomFailure(enumArray) : null, status, false, serializerOptions);
        } 

        public static Response Failure(int status, IError error, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new Response(data, Json.Result.CustomFailure(error), status, false, serializerOptions);

        public static Response Failure(int status, IError[] errors, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new Response(data, Json.Result.CustomFailure(errors), status, false, serializerOptions);

        public static Response Failure(int status, IList<IError> errors, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new Response(data, Json.Result.CustomFailure(errors), status, false, serializerOptions);

        public static Response Failure(int status, Exception exception, bool includeTraces = false, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new Response(data, Json.Result.ExceptionFailure(exception, includeTraces), status, false, serializerOptions);

        public static Response Failure(int status, Exception[] exceptions, bool includeTraces = false, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new Response(data, Json.Result.ExceptionFailure(exceptions, includeTraces), status, false, serializerOptions);

        public static Response Failure(int status, IList<Exception> exceptions, bool includeTraces = false, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new Response(data, Json.Result.ExceptionFailure(exceptions, includeTraces), status, false, serializerOptions);

        public static Response Failure(int status, string error, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new Response(data, Json.Result.CustomFailure(error), status, false, serializerOptions);

        public static Response Failure(int status, string[] errors, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new Response(data, Json.Result.CustomFailure(errors), status, false, serializerOptions);

        public static Response Failure(int status, IList<string> errors, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new Response(data, Json.Result.CustomFailure(errors), status, false, serializerOptions);
    }
}