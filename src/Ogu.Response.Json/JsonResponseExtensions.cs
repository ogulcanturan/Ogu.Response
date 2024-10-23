using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace Ogu.Response.Json
{
    public static class JsonResponseExtensions
    {
        public static IJsonResponse ToSuccessJsonResponse(this HttpStatusCode statusCode, JsonSerializerOptions serializerOptions = null)
        {
            return new JsonResponse(null, success: true, statusCode, null, null, null, serializerOptions);
        }

        public static IJsonResponse ToSuccessJsonResponse(this HttpStatusCode statusCode, object data, JsonSerializerOptions serializerOptions = null)
        {
            return new JsonResponse(data, success: true, statusCode, null, null, null, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, IResponseError error, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(statusCode, new List<IResponseError> { error }, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, List<IResponseError> errors, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(statusCode, errors, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, IResponseValidationFailure validationFailure, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(statusCode, new List<IResponseError> { new JsonResponseError(validationFailure) }, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, List<IResponseValidationFailure> validationFailures, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(statusCode, new List<IResponseError> { new JsonResponseError(validationFailures) }, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse<TEnum>(this HttpStatusCode statusCode, TEnum @enum, JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            return JsonResponse.Failure(statusCode, new List<IResponseError> { @enum.ToJsonResponseError() }, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse<TEnum>(this HttpStatusCode statusCode, TEnum[] enums, JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            return JsonResponse.Failure(statusCode, enums?.Select(e => e.ToJsonResponseError()).ToList(), serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, Exception exception, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(statusCode, new List<IResponseError> { exception.ToJsonResponseError(includeTraces) }, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, Exception[] exceptions, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(statusCode, exceptions?.Where(e => e != null).Select(e => e.ToJsonResponseError(includeTraces)).ToList(), serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, string error, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(statusCode, new List<IResponseError> { new JsonResponseError(error) }, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, string[] errors, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(statusCode, errors?.Where(e => e != null).Select(e => (IResponseError)new JsonResponseError(e)).ToList(), serializerOptions);
        }
    }
}