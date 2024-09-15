using Ogu.Response.Abstractions;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace Ogu.Response.Json
{
    public class JsonResponse : IJsonResponse
    {
        private static readonly Lazy<JsonSerializerOptions> LazyDefaultJsonSerializerOptions =
            new Lazy<JsonSerializerOptions>(() => new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonDictionaryKeyConverter<string>() }
            }, LazyThreadSafetyMode.ExecutionAndPublication);

        public static JsonSerializerOptions DefaultJsonSerializerOptions => LazyDefaultJsonSerializerOptions.Value;

        [JsonConstructor]
        public JsonResponse(object data, int status, bool success, JsonSerializerOptions serializerOptions = null,
            string serializedResponse = null)
            : this(JsonResponseResult.Builder.WithData(data).Build(), status, success, serializerOptions,
                serializedResponse)
        {
        }

        [JsonConstructor]
        public JsonResponse(IResponseResult<object> result, int status, bool success, JsonSerializerOptions serializerOptions = null, string serializedResponse = null)
        {
            Result = result ?? new JsonResponseResult<object>();
            Status = status;
            Success = success; 
            SerializerOptions = serializerOptions ?? DefaultJsonSerializerOptions;
            SerializedResponse = serializedResponse;
        }

        public JsonSerializerOptions SerializerOptions { get; }

        public bool Success { get; }

        public int Status { get; }

        [JsonIgnore]
        public string SerializedResponse { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IResponseResult<object> Result { get; }

        public static JsonResponse Other(object data, int status, bool success,
            JsonSerializerOptions serializerOptions = null) 
            => new JsonResponse(data, status, success, serializerOptions);

        public static JsonResponse Other(IResponseResult<object> result, int status, bool success,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(result, status, success, serializerOptions);

        public static JsonResponse Successful(object data, int status,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(data, status, true, serializerOptions);

        public static JsonResponse Successful(IResponseResult<object> result, int status,
            JsonSerializerOptions serializerOptions = null) 
            => new JsonResponse(result, status, true, serializerOptions);

        public static JsonResponse Failure(int status, IResponseResult<object> result = null,
            JsonSerializerOptions serializerOptions = null) 
            => new JsonResponse(result, status, false, serializerOptions);

        public static JsonResponse Failure(int status, IResponseValidationFailure validationFailure, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(JsonResponseResult.Builder.ValidationFailure(JsonResponseError.Builder, validationFailure, data), status, false, serializerOptions);

        public static JsonResponse Failure(int status, IResponseValidationFailure[] validationFailures, object data = null,
            JsonSerializerOptions serializerOptions = null) 
            => new JsonResponse(JsonResponseResult.Builder.ValidationFailure(JsonResponseError.Builder, validationFailures, data), status, false, serializerOptions);

        public static JsonResponse Failure<TEnum>(int status, TEnum @enum, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum 
            => new JsonResponse(JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, @enum, data), status, false, serializerOptions);

        public static JsonResponse Failure<TEnum>(int status, TEnum[] @enums, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => new JsonResponse(JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, @enums, data), status, false, serializerOptions);

        public static JsonResponse Failure<TEnum>(int status, TEnum? @enum, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => new JsonResponse(@enum.HasValue ? JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, @enum.Value, data) : null, status, false, serializerOptions);

        public static JsonResponse Failure<TEnum>(int status, TEnum?[] @enums, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            var enumArray = @enums.Where(e => e.HasValue).Select(e => e.Value).ToArray();
            return new JsonResponse(enumArray.Length > 0 ? JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, enumArray, data) : null, status, false, serializerOptions);
        }

        public static JsonResponse Failure(int status, IResponseError error, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(JsonResponseResult.Builder.CustomFailure(error, data), status, false, serializerOptions);

        public static JsonResponse Failure(int status, IResponseError[] errors, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(JsonResponseResult.Builder.CustomFailure(errors, data), status, false, serializerOptions);

        public static JsonResponse Failure(int status, Exception exception, bool includeTraces = false, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(JsonResponseResult.Builder.ExceptionFailure(JsonResponseError.Builder, exception, includeTraces, data), status, false, serializerOptions);

        public static JsonResponse Failure(int status, Exception[] exceptions, bool includeTraces = false, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(JsonResponseResult.Builder.ExceptionFailure(JsonResponseError.Builder, exceptions, includeTraces, data), status, false, serializerOptions);

        public static JsonResponse Failure(int status, string error, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, error, data), status, false, serializerOptions);

        public static JsonResponse Failure(int status, string[] errors, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(JsonResponseResult.Builder.CustomFailure<object>(JsonResponseError.Builder, errors, data), status, false, serializerOptions);
    }
}