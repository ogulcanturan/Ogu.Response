using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace Ogu.Response.Json
{
    public static class JsonResponseTExtensions
    {
        public static JsonResponse<T> ToSuccessJsonResponseT<T>(this HttpStatusCode statusCode, T data, JsonSerializerOptions serializerOptions = null)
        {
            return CreateSuccessResponse(data, statusCode, serializerOptions);
        }

        public static JsonResponse<T> ToSuccessJsonResponseT<T>(this HttpStatusCode statusCode, JsonSerializerOptions serializerOptions = null)
        {
            return CreateSuccessResponse<T>(statusCode, serializerOptions);
        }

        public static JsonResponse<T> Failure<T>(this HttpStatusCode statusCode, IResponseValidationFailure validationFailure, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponseExtensions.CreateFailureResponse<T>(statusCode,
                new List<IResponseError> { JsonResponseExtensions.CreateValidationError(validationFailure) },
                serializerOptions);
        }

        public static JsonResponse<T> Failure<T>(this HttpStatusCode statusCode, List<IResponseValidationFailure> validationFailures, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponseExtensions.CreateFailureResponse<T>(statusCode,
                new List<IResponseError> { JsonResponseExtensions.CreateValidationErrors(validationFailures) },
                serializerOptions);
        }

        public static JsonResponse<T> Failure<T, TEnum>(this HttpStatusCode statusCode, TEnum @enum, JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            return JsonResponseExtensions.CreateFailureResponse<T>(statusCode,
                new List<IResponseError> { @enum.ToJsonResponseError() }, serializerOptions);
        }

        public static JsonResponse<T> Failure<T, TEnum>(this HttpStatusCode statusCode, TEnum[] enums, JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            return JsonResponseExtensions.CreateFailureResponse<T>(statusCode,
                enums?.Select(e => e.ToJsonResponseError()).ToList(), serializerOptions);
        }

        private static JsonResponse<T> CreateSuccessResponse<T>(T data, HttpStatusCode statusCode, JsonSerializerOptions options)
        {
            return new JsonResponse<T>(data, true, statusCode, null, null, null, options);
        }

        private static JsonResponse<T> CreateSuccessResponse<T>(HttpStatusCode statusCode, JsonSerializerOptions options)
        {
            return new JsonResponse<T>(data: default, true, statusCode,null, null, null, options);
        }
    }
}