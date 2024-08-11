using Ogu.Response.Abstractions;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public class JsonResponse<T> : IJsonResponse<T>
    {
        public JsonResponse(T data, IResponseResult result, int statusCode, bool success, JsonSerializerOptions serializerOptions = null, string serializedResponse = null)
        {
            Data = data;
            Result = result;
            Status = statusCode;
            Success = success;
            SerializerOptions = serializerOptions ?? JsonResponse.DefaultJsonSerializerOptions;
            SerializedResponse = serializedResponse;
        }

        public JsonResponse(T data, int status, bool success, JsonSerializerOptions serializerOptions = null) : this(data, null, status, success, serializerOptions) { }

        public JsonSerializerOptions SerializerOptions { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T Data { get; }

        public bool Success { get; }

        public int Status { get; }

        [JsonIgnore]
        public string SerializedResponse { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IResponseResult Result { get; }

        public static implicit operator JsonResponse(JsonResponse<T> response) => new JsonResponse(response.Data, response.Result, response.Status, response.Success, response.SerializerOptions, response.SerializedResponse);

        public static JsonResponse<T> Other(T data, int status, bool success, IResponseResult result = null,
            JsonSerializerOptions serializerOptions = null) =>
            new JsonResponse<T>(data, result, status, success, serializerOptions);

        public static JsonResponse<T> Successful(T data, int status, IResponseResult result = null,
            JsonSerializerOptions serializerOptions = null) =>
            new JsonResponse<T>(data, result, status, true, serializerOptions);

        public static JsonResponse<T> Failure(int status, IResponseResult result = null, T data = default,
            JsonSerializerOptions serializerOptions = null) =>
            new JsonResponse<T>(data, result, status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, IResponseValidationFailure[] validationFailures, T data = default,
            JsonSerializerOptions serializerOptions = null) =>
            new JsonResponse<T>(data, JsonResponseResult.Builder.ValidationFailure(JsonResponseError.Builder, validationFailures), status, false, serializerOptions);

        public static JsonResponse<T> Failure<TEnum>(int status, TEnum @enum, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            new JsonResponse<T>(data, JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, @enum), status, false, serializerOptions);

        public static JsonResponse<T> Failure<TEnum>(int status, TEnum[] @enums, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            new JsonResponse<T>(data, JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, @enums), status, false, serializerOptions);

        public static JsonResponse<T> Failure<TEnum>(int status, TEnum? @enum, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            new JsonResponse<T>(data, @enum.HasValue ? JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, @enum.Value) : null, status, false, serializerOptions);

        public static JsonResponse<T> Failure<TEnum>(int status, TEnum?[] @enums, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            var enumArray = @enums.Where(e => e.HasValue).Select(e => e.Value).ToArray();
            return new JsonResponse<T>(data, enumArray.Length > 0 ? JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, enumArray) : null, status, false, serializerOptions);
        }

        public static JsonResponse<T> Failure(int status, IResponseError error, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(data, JsonResponseResult.Builder.CustomFailure(error), status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, IResponseError[] errors, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(data, JsonResponseResult.Builder.CustomFailure(errors), status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, Exception exception, bool includeTraces = false, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(data, JsonResponseResult.Builder.ExceptionFailure(JsonResponseError.Builder, exception, includeTraces), status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, Exception[] exceptions, bool includeTraces = false, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(data, JsonResponseResult.Builder.ExceptionFailure(JsonResponseError.Builder, exceptions, includeTraces), status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, string error, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(data, JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, error), status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, string[] errors, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(data, JsonResponseResult.Builder.CustomFailure(JsonResponseError.Builder, errors), status, false, serializerOptions);
    }
}