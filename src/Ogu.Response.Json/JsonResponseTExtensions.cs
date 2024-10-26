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
        public static IJsonResponse AsNonGeneric<T>(this IJsonResponse<T> jsonResponse)
        {
            return (IJsonResponse)jsonResponse;
        }

        public static IJsonResponse<T> ToSuccessJsonResponseOf<T>(this HttpStatusCode status, T data, JsonSerializerOptions serializerOptions = null)
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

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, IError error, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, new List<IError> { error }, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, List<IError> errors, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, errors, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, IValidationFailure validationFailure, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, new List<IError> { new JsonError(validationFailure) }, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, List<IValidationFailure> validationFailures, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, new List<IError> { new JsonError(validationFailures) }, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T, TEnum>(this HttpStatusCode status, TEnum @enum, JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            return JsonResponse<T>.Failure(status, new List<IError> { @enum.ToJsonError() }, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T, TEnum>(this HttpStatusCode status, TEnum[] enums, JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            return JsonResponse<T>.Failure(status, enums?.Select(e => e.ToJsonError()).ToList(), serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, Exception exception, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, new List<IError> { exception.ToJsonError(includeTraces) }, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, Exception[] exceptions, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, exceptions?.Where(e => e != null).Select(e => e.ToJsonError(includeTraces)).ToList(), serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, string error, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, new List<IError> { new JsonError(error) }, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, string title, string description, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, new List<IError> { new JsonError(title, description) }, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode status, string[] errors, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(status, errors?.Where(e => e != null).Select(e => (IError)new JsonError(e)).ToList(), serializerOptions);
        }
    }
}