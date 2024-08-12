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
        public JsonResponse(object data, IResponseResult result, int status, bool success, JsonSerializerOptions serializerOptions = null, string serializedResponse = null)
        {
            Data = data;
            Result = result;
            Status = status;
            Success = success; 
            SerializerOptions = serializerOptions ?? DefaultJsonSerializerOptions;
            SerializedResponse = serializedResponse;
        }

        public JsonSerializerOptions SerializerOptions { get; }

        public JsonResponse(int status, bool success, JsonSerializerOptions serializerOptions = null) : this(null, null, status, success, serializerOptions) { }
        public JsonResponse(int status, bool success) : this(status, success, null) { }
        public JsonResponse(object data, int status, bool success, JsonSerializerOptions serializerOptions = null) : this(data, null, status, success, serializerOptions) { }
        public JsonResponse(IResponseResult result, int status, bool success, JsonSerializerOptions serializerOptions = null) : this(null, result, status, success, serializerOptions) { }
        public JsonResponse(object data, IResponseResult result, int status, bool success) : this(data, result, status, success, null) { }
        public JsonResponse(IResponseResult result, int status, JsonSerializerOptions serializerOptions = null) : this(null, result, status, false, serializerOptions) { }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Data { get; }

        public bool Success { get; }

        public int Status { get; }

        [JsonIgnore]
        public string SerializedResponse { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IResponseResult Result { get; }

        public static JsonResponse Other(object data, int status, bool success, IResponseResult result = null,
            JsonSerializerOptions serializerOptions = null) 
            => new JsonResponse(data, result, status, success, serializerOptions);

        public static JsonResponse Successful(object data, int status, IResponseResult result = null,
            JsonSerializerOptions serializerOptions = null) 
            => new JsonResponse(data, result, status, true, serializerOptions);

        public static JsonResponse Failure(int status, IResponseResult result = null, object data = null,
            JsonSerializerOptions serializerOptions = null) 
            => new JsonResponse(data, result, status, false, serializerOptions);

        public static JsonResponse Failure(int status, IResponseValidationFailure validationFailure, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(data, JsonResponseResult.Builder.ValidationFailure(JsonResponseError.Builder, validationFailure), status, false, serializerOptions);

        public static JsonResponse Failure(int status, IResponseValidationFailure[] validationFailures, object data = null,
            JsonSerializerOptions serializerOptions = null) 
            => new JsonResponse(data, JsonResponseResult.Builder.ValidationFailure(JsonResponseError.Builder, validationFailures), status, false, serializerOptions);

        public static JsonResponse Failure<TEnum>(int status, TEnum @enum, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum 
            => new JsonResponse(data, JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, @enum), status, false, serializerOptions);

        public static JsonResponse Failure<TEnum>(int status, TEnum[] @enums, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => new JsonResponse(data, JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, @enums), status, false, serializerOptions);

        public static JsonResponse Failure<TEnum>(int status, TEnum? @enum, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => new JsonResponse(data, @enum.HasValue ? JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, @enum.Value) : null, status, false, serializerOptions);

        public static JsonResponse Failure<TEnum>(int status, TEnum?[] @enums, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            var enumArray = @enums.Where(e => e.HasValue).Select(e => e.Value).ToArray();
            return new JsonResponse(data, enumArray.Length > 0 ? JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, enumArray) : null, status, false, serializerOptions);
        }

        public static JsonResponse Failure(int status, IResponseError error, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(data, JsonResponseResult.Builder.CustomFailure(error), status, false, serializerOptions);

        public static JsonResponse Failure(int status, IResponseError[] errors, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(data, JsonResponseResult.Builder.CustomFailure(errors), status, false, serializerOptions);

        public static JsonResponse Failure(int status, Exception exception, bool includeTraces = false, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(data, JsonResponseResult.Builder.ExceptionFailure(JsonResponseError.Builder, exception, includeTraces), status, false, serializerOptions);

        public static JsonResponse Failure(int status, Exception[] exceptions, bool includeTraces = false, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(data, JsonResponseResult.Builder.ExceptionFailure(JsonResponseError.Builder, exceptions, includeTraces), status, false, serializerOptions);

        public static JsonResponse Failure(int status, string error, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(data, JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, error), status, false, serializerOptions);

        public static JsonResponse Failure(int status, string[] errors, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(data, JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, errors), status, false, serializerOptions);
    }
}