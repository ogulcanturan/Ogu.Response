using Ogu.Response.Abstractions;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public class JsonResponse<T> : IJsonResponse<T>
    {
        public JsonResponse(T data, IResponseResult<T> result, int statusCode, bool success, JsonSerializerOptions serializerOptions = null, string serializedResponse = null)
        {
            Result = result ?? new JsonResponseResult<T>();
            Result.Data = data;
            Status = statusCode;
            Success = success;
            SerializerOptions = serializerOptions ?? JsonResponse.DefaultJsonSerializerOptions;
            SerializedResponse = serializedResponse;
        }

        public JsonSerializerOptions SerializerOptions { get; }

        public bool Success { get; }

        public int Status { get; }

        [JsonIgnore]
        public string SerializedResponse { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IResponseResult<T> Result { get; }

        public static implicit operator JsonResponse(JsonResponse<T> response) => new JsonResponse(response.Result.Data, (IResponseResult<object>)response.Result, response.Status, response.Success, response.SerializerOptions, response.SerializedResponse);

        public static JsonResponse<T> Other(T data, int status, bool success, IResponseResult<T> result = null,
            JsonSerializerOptions serializerOptions = null) =>
            new JsonResponse<T>(data, result, status, success, serializerOptions);

        public static JsonResponse<T> Successful(T data, int status, IResponseResult<T> result = null,
            JsonSerializerOptions serializerOptions = null) =>
            new JsonResponse<T>(data, result, status, true, serializerOptions);

        public static JsonResponse<T> Failure(int status, IResponseResult<T> result = null, T data = default,
            JsonSerializerOptions serializerOptions = null) =>
            new JsonResponse<T>(data, result, status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, IResponseValidationFailure[] validationFailures, T data = default,
            JsonSerializerOptions serializerOptions = null) =>
            new JsonResponse<T>(data, JsonResponseResult.Builder.ValidationFailure<T>(JsonResponseError.Builder, validationFailures), status, false, serializerOptions);

        public static JsonResponse<T> Failure<TEnum>(int status, TEnum @enum, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            new JsonResponse<T>(data, JsonResponseResult.Builder.CustomFailure<TEnum, T>(JsonResponseError.Builder, @enum), status, false, serializerOptions);

        public static JsonResponse<T> Failure<TEnum>(int status, TEnum[] @enums, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            new JsonResponse<T>(data, JsonResponseResult.Builder.CustomFailure<TEnum, T>(JsonResponseError.Builder, @enums), status, false, serializerOptions);

        public static JsonResponse<T> Failure<TEnum>(int status, TEnum? @enum, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            new JsonResponse<T>(data, @enum.HasValue ? JsonResponseResult.Builder.CustomFailure<TEnum, T>(JsonResponseError.Builder, @enum.Value) : null, status, false, serializerOptions);

        public static JsonResponse<T> Failure<TEnum>(int status, TEnum?[] @enums, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            var enumArray = @enums.Where(e => e.HasValue).Select(e => e.Value).ToArray();
            return new JsonResponse<T>(data, enumArray.Length > 0 ? JsonResponseResult.Builder.CustomFailure<TEnum, T>(JsonResponseError.Builder, enumArray) : null, status, false, serializerOptions);
        }

        public static JsonResponse<T> Failure(int status, IResponseError error, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(data, JsonResponseResult<T>.Builder.CustomFailure<T>(error), status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, IResponseError[] errors, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(data, JsonResponseResult.Builder.CustomFailure<T>(errors), status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, Exception exception, bool includeTraces = false, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(data, JsonResponseResult.Builder.ExceptionFailure<T>(JsonResponseError.Builder, exception, includeTraces), status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, Exception[] exceptions, bool includeTraces = false, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(data, JsonResponseResult.Builder.ExceptionFailure<T>(JsonResponseError.Builder, exceptions, includeTraces), status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, string error, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(data, JsonResponseResult.Builder.CustomFailure<T>(JsonResponseError.Builder, error), status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, string[] errors, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(data, JsonResponseResult.Builder.CustomFailure<T>(JsonResponseError.Builder, errors), status, false, serializerOptions);
    }
}