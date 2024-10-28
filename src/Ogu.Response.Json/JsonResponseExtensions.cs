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
        public static IJsonResponse ToJsonResponse<TData>(this IJsonResponse<TData> jsonResponse, JsonSerializerOptions serializerOptions = null)
        {
            return jsonResponse.Success
                ? new JsonResponse(jsonResponse.Data, true, jsonResponse.Status, jsonResponse.Extras, jsonResponse.Errors, jsonResponse.SerializedResponse, serializerOptions ?? jsonResponse.SerializerOptions)
                : new JsonResponse(default, false, jsonResponse.Status, jsonResponse.Extras, jsonResponse.Errors, jsonResponse.SerializedResponse, serializerOptions ?? jsonResponse.SerializerOptions);
        }

        public static IJsonResponse ToSuccessJsonResponse(this HttpStatusCode status, JsonSerializerOptions serializerOptions = null)
        {
            return new JsonResponse(null, success: true, status, null, null, null, serializerOptions);
        }

        public static IJsonResponse ToSuccessJsonResponse(this HttpStatusCode status, object data, JsonSerializerOptions serializerOptions = null)
        {
            return new JsonResponse(data, success: true, status, null, null, null, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode status, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(status, null, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode status, IError error, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(status, new List<IError> { error }, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode status, List<IError> errors, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(status, errors, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode status, IValidationFailure failure, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(status, new List<IError> { new JsonError(failure) }, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode status, List<IValidationFailure> failures, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(status, new List<IError> { new JsonError(failures) }, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse<TEnum>(this HttpStatusCode status, TEnum @enum, JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            return JsonResponse.Failure(status, new List<IError> { @enum.ToJsonError() }, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse<TEnum>(this HttpStatusCode status, TEnum[] enums, JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            return JsonResponse.Failure(status, enums?.Select(e => e.ToJsonError()).ToList(), serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode status, Exception exception, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(status, GetErrors(exception, includeTraces), serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode status, Exception[] exceptions, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(status, exceptions.SelectMany(e => GetErrors(e, includeTraces)).ToList(), serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode status, string error, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(status, new List<IError> { new JsonError(error) }, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode status, string title, string description, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(status, new List<IError> { new JsonError(title, description) }, serializerOptions);
        }

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode status, string[] errors, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(status, errors.Select(e => (IError)new JsonError(e)).ToList(), serializerOptions);
        }

        private static List<IError> GetErrors(Exception exception, bool includeTraces)
        {
            return exception is AggregateException aex
                ? aex.InnerExceptions.Select(e => e.ToJsonError(includeTraces)).ToList()
                : new List<IError> { exception.ToJsonError(includeTraces) };
        }
    }
}