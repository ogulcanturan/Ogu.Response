using Ogu.Response.Abstractions;
using System;
using System.Net;
using System.Text.Json;

namespace Ogu.Response.Json
{
    public static class JsonResponseTExtensions
    {
        public static IJsonResponse<T> ToOtherJsonResponse<T>(this HttpStatusCode status, T data, bool success,
            IResponseResult<T> result = null,
            JsonSerializerOptions serializerOptions = null) 
            => JsonResponse<T>.Other(data, (int)status, success, result, serializerOptions);

        public static IJsonResponse<T> ToSuccessJsonResponse<T>(this HttpStatusCode status, T data = default, IResponseResult<T> result = null,
            JsonSerializerOptions serializerOptions = null) 
            => JsonResponse<T>.Successful(data, (int)status, result, serializerOptions);

        public static IJsonResponse<T> ToFailJsonResponse<T>(this HttpStatusCode status, IResponseResult<T> result = null, T data = default,
            JsonSerializerOptions serializerOptions = null) 
            => JsonResponse<T>.Failure((int)status, result, data, serializerOptions);

        public static IJsonResponse<T> ToFailJsonResponse<T>(this HttpStatusCode status, IResponseValidationFailure[] validationFailures,
            T data = default,
            JsonSerializerOptions serializerOptions = null) 
            => JsonResponse<T>.Failure((int)status, validationFailures, data, serializerOptions);

        public static IJsonResponse<T> ToFailJsonResponse<T, TEnum>(this HttpStatusCode status, TEnum @enum, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum 
            => JsonResponse<T>.Failure((int)status, @enum, data, serializerOptions);

        public static IJsonResponse<T> ToFailJsonResponse<T, TEnum>(this HttpStatusCode status, TEnum[] @enums, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => JsonResponse<T>.Failure((int)status, @enums, data, serializerOptions);

        public static IResponse<T> ToFailJsonResponse<T, TEnum>(this HttpStatusCode status, TEnum? @enum, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => JsonResponse<T>.Failure((int)status, @enum, data, serializerOptions);

        public static IResponse<T> ToFailJsonResponse<T, TEnum>(this HttpStatusCode status, TEnum?[] @enums, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => JsonResponse<T>.Failure((int)status, @enums, data, serializerOptions);

        public static IResponse<T> ToFailJsonResponse<T>(this HttpStatusCode status, IResponseError error, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => JsonResponse<T>.Failure((int)status, error, data, serializerOptions);

        public static IResponse<T> ToFailJsonResponse<T>(this HttpStatusCode status, IResponseError[] errors, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => JsonResponse<T>.Failure((int)status, errors, data, serializerOptions);

        public static IResponse<T> ToFailJsonResponse<T>(this HttpStatusCode status, Exception exception, bool includeTraces = false,
            T data = default, JsonSerializerOptions serializerOptions = null)
            => JsonResponse<T>.Failure((int)status, exception, includeTraces, data, serializerOptions);

        public static IResponse<T> ToFailJsonResponse<T>(this HttpStatusCode status, Exception[] exceptions, bool includeTraces = false,
            T data = default, JsonSerializerOptions serializerOptions = null)
            => JsonResponse<T>.Failure((int)status, exceptions, includeTraces, data, serializerOptions);

        public static IResponse<T> ToFailJsonResponse<T>(this HttpStatusCode status, string error, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => JsonResponse<T>.Failure((int)status, error, data, serializerOptions);
    }
}