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
        public static IJsonResponse<T> ToSuccessJsonResponse<T>(this HttpStatusCode status, T data, JsonSerializerOptions serializerOptions = null)
        {
            return new JsonResponse<T>(data, success: true, status, null, null, null, serializerOptions);
        }

        public static IJsonResponse<T> ToSuccessJsonResponse<T>(this HttpStatusCode status, JsonSerializerOptions serializerOptions = null)
        {
            return new JsonResponse<T>(data: default, true, status, null, null, null, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, null, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, IResponseError error, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, new List<IResponseError> { error }, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, List<IResponseError> errors, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, errors, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, IResponseValidationFailure validationFailure, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, new List<IResponseError> { new JsonResponseError(validationFailure) }, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, List<IResponseValidationFailure> validationFailures, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, new List<IResponseError> { new JsonResponseError(validationFailures) }, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T, TEnum>(this HttpStatusCode status, TEnum @enum, JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            return JsonResponse<T>.Failure(status, new List<IResponseError> { @enum.ToJsonResponseError() }, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T, TEnum>(this HttpStatusCode status, TEnum[] enums, JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            return JsonResponse<T>.Failure(status, enums?.Select(e => e.ToJsonResponseError()).ToList(), serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, Exception exception, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, new List<IResponseError> { exception.ToJsonResponseError(includeTraces) }, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, Exception[] exceptions, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, exceptions?.Where(e => e != null).Select(e => e.ToJsonResponseError(includeTraces)).ToList(), serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, string error, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, new List<IResponseError> { new JsonResponseError(error) }, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, string[] errors, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, errors?.Where(e => e != null).Select(e => (IResponseError)new JsonResponseError(e)).ToList(), serializerOptions);
        }
    }
}