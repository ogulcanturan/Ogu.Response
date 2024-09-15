using Ogu.Response.Abstractions;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public class JsonResponse<T> : IJsonResponse<T>
    {
        public JsonResponse(T data, int statusCode, bool success, JsonSerializerOptions serializerOptions = null,
            string serializedResponse = null)

            : this(JsonResponseResult.Builder.WithData(data).Build<T>(), statusCode, success, serializerOptions,
                serializedResponse)
        {
        }

        [JsonConstructor]
        public JsonResponse(IResponseResult<T> result, int statusCode, bool success, JsonSerializerOptions serializerOptions = null, string serializedResponse = null)
        {
            Result = result ?? new JsonResponseResult<T>();
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

        public static implicit operator JsonResponse(JsonResponse<T> response) => new JsonResponse(response.Result, response.Status, response.Success, response.SerializerOptions, response.SerializedResponse);

        public static JsonResponse<T> Other(T data, int status, bool success,
            JsonSerializerOptions serializerOptions = null) =>
            new JsonResponse<T>(data, status, success, serializerOptions);

        public static JsonResponse<T> Other(IResponseResult<T> result, int status, bool success,
            JsonSerializerOptions serializerOptions = null) =>
            new JsonResponse<T>(result, status, success, serializerOptions);

        public static JsonResponse<T> Successful(T data, int status,
            JsonSerializerOptions serializerOptions = null) =>
            new JsonResponse<T>(data, status, true, serializerOptions);

        public static JsonResponse<T> Successful(IResponseResult<T> result, int status,
            JsonSerializerOptions serializerOptions = null) =>
            new JsonResponse<T>(result, status, true, serializerOptions);

        public static JsonResponse<T> Failure(int status, IResponseResult<T> result = null,
            JsonSerializerOptions serializerOptions = null) =>
            new JsonResponse<T>(result, status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, IResponseValidationFailure[] validationFailures, T data = default,
            JsonSerializerOptions serializerOptions = null) =>
            new JsonResponse<T>(JsonResponseResult.Builder.ValidationFailure<T>(JsonResponseError.Builder, validationFailures, data), status, false, serializerOptions);

        public static JsonResponse<T> Failure<TEnum>(int status, TEnum @enum, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            new JsonResponse<T>(JsonResponseResult.Builder.CustomFailure<TEnum, T>(JsonResponseError.Builder, @enum, data), status, false, serializerOptions);

        public static JsonResponse<T> Failure<TEnum>(int status, TEnum[] @enums, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            new JsonResponse<T>(JsonResponseResult.Builder.CustomFailure<TEnum, T>(JsonResponseError.Builder, @enums, data), status, false, serializerOptions);

        public static JsonResponse<T> Failure<TEnum>(int status, TEnum? @enum, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            new JsonResponse<T>(@enum.HasValue ? JsonResponseResult.Builder.CustomFailure<TEnum, T>(JsonResponseError.Builder, @enum.Value, data) : null, status, false, serializerOptions);

        public static JsonResponse<T> Failure<TEnum>(int status, TEnum?[] @enums, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            var enumArray = @enums.Where(e => e.HasValue).Select(e => e.Value).ToArray();
            return new JsonResponse<T>(enumArray.Length > 0 ? JsonResponseResult.Builder.CustomFailure<TEnum, T>(JsonResponseError.Builder, enumArray, data) : new JsonResponseResult<T> { Data = data, HasError = false }, status, false, serializerOptions);
        }

        public static JsonResponse<T> Failure(int status, IResponseError error, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(JsonResponseResult<T>.Builder.CustomFailure<T>(error, data), status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, IResponseError[] errors, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(JsonResponseResult.Builder.CustomFailure<T>(errors, data), status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, Exception exception, bool includeTraces = false, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(JsonResponseResult.Builder.ExceptionFailure<T>(JsonResponseError.Builder, exception, includeTraces, data), status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, Exception[] exceptions, bool includeTraces = false, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(JsonResponseResult.Builder.ExceptionFailure<T>(JsonResponseError.Builder, exceptions, includeTraces, data), status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, string error, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(JsonResponseResult.Builder.CustomFailure<T>(JsonResponseError.Builder, error, data), status, false, serializerOptions);

        public static JsonResponse<T> Failure(int status, string[] errors, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(JsonResponseResult.Builder.CustomFailure<T>(JsonResponseError.Builder, errors, data), status, false, serializerOptions);
    }
}