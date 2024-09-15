using Ogu.Response.Abstractions;
using System;
using System.Net;
using System.Text.Json;

namespace Ogu.Response.Json
{
    public static class JsonResponseExtensions
    {
        public static IJsonResponse ToOtherJsonResponse(this HttpStatusCode status, object data, bool success, IResponseResult<object> result = null,
            JsonSerializerOptions serializerOptions = null) 
            => JsonResponse.Other(data, (int)status, success, result, serializerOptions);

        public static IJsonResponse ToOtherJsonResponse(this HttpStatusCode status, bool success, IResponseResult<object> result = null,
            JsonSerializerOptions serializerOptions = null)
            => JsonResponse.Other(null, (int)status, success, result, serializerOptions);

        public static IJsonResponse ToSuccessJsonResponse(this HttpStatusCode status, object data = null, IResponseResult<object> result = null,
            JsonSerializerOptions serializerOptions = null) 
            => JsonResponse.Successful(data, (int)status, result, serializerOptions);

        public static IJsonResponse ToFailJsonResponse(this HttpStatusCode status, IResponseResult<object> result = null, object data = null,
            JsonSerializerOptions serializerOptions = null) 
            => JsonResponse.Failure((int)status, result, data, serializerOptions);

        public static IJsonResponse ToFailJsonResponse(this HttpStatusCode status, IResponseValidationFailure validationFailure, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => JsonResponse.Failure((int)status, validationFailure, data, serializerOptions);

        public static IJsonResponse ToFailJsonResponse(this HttpStatusCode status, IResponseValidationFailure[] validationFailures, object data = null,
            JsonSerializerOptions serializerOptions = null) 
            => JsonResponse.Failure((int)status, validationFailures, data, serializerOptions);

        public static IJsonResponse ToFailJsonResponse<TEnum>(this HttpStatusCode status, TEnum @enum, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum 
            => JsonResponse.Failure((int)status, @enum, data, serializerOptions);

        public static IJsonResponse ToFailJsonResponse<TEnum>(this HttpStatusCode status, TEnum[] @enums, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => JsonResponse.Failure((int)status, @enums, data, serializerOptions);

        public static IJsonResponse ToFailJsonResponse<TEnum>(this HttpStatusCode status, TEnum? @enum, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => JsonResponse.Failure((int)status, @enum, data, serializerOptions);

        public static IJsonResponse ToFailJsonResponse<TEnum>(this HttpStatusCode status, TEnum?[] @enums, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => JsonResponse.Failure((int)status, @enums, data, serializerOptions);

        public static IJsonResponse ToFailJsonResponse(this HttpStatusCode status, IResponseError error, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => JsonResponse.Failure((int)status, error, data, serializerOptions);

        public static IJsonResponse ToFailJsonResponse(this HttpStatusCode status, IResponseError[] errors, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => JsonResponse.Failure((int)status, errors, data, serializerOptions);

        public static IJsonResponse ToFailJsonResponse(this HttpStatusCode status, Exception exception, bool includeTraces = false, 
            object data = null, JsonSerializerOptions serializerOptions = null)
            => JsonResponse.Failure((int)status, exception, includeTraces, data, serializerOptions);

        public static IJsonResponse ToFailJsonResponse(this HttpStatusCode status, Exception[] exceptions, bool includeTraces = false,
            object data = null, JsonSerializerOptions serializerOptions = null)
            => JsonResponse.Failure((int)status, exceptions, includeTraces, data, serializerOptions);

        public static IJsonResponse ToFailJsonResponse(this HttpStatusCode status, string error, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => JsonResponse.Failure((int)status, error, data, serializerOptions);
    }
}