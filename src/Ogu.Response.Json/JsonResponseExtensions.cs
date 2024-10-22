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
        public static JsonResponse ToSuccessJsonResponse(this HttpStatusCode statusCode,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(null, success: true, statusCode, null, null, null, serializerOptions);

        public static JsonResponse ToSuccessJsonResponse(this HttpStatusCode statusCode, object data,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(data, success: true, statusCode, null, null, null, serializerOptions);

        public static JsonResponse<T> ToSuccessJsonResponse<T>(this HttpStatusCode statusCode, T data,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(data, success: true, statusCode, null, null, null, serializerOptions);

        public static JsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, IResponseValidationFailure validationFailure, JsonSerializerOptions serializerOptions = null)
         => CreateFailureResponse(statusCode, new List<IResponseError> { CreateValidationError(validationFailure) }, serializerOptions);

        public static JsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, List<IResponseValidationFailure> validationFailures, JsonSerializerOptions serializerOptions = null)
            => CreateFailureResponse(statusCode, new List<IResponseError> { CreateValidationErrors(validationFailures) }, serializerOptions);

        public static JsonResponse ToFailureJsonResponse<TEnum>(this HttpStatusCode statusCode, TEnum @enum, JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => CreateFailureResponse(statusCode, new List<IResponseError> { @enum.ToJsonResponseError() }, serializerOptions);

        public static JsonResponse ToFailureJsonResponse<TEnum>(this HttpStatusCode statusCode, TEnum[] enums, JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => CreateFailureResponse(statusCode, enums?.Select(e => e.ToJsonResponseError()).ToList(), serializerOptions);

        public static JsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, Exception exception, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
            => CreateFailureResponse(statusCode, new List<IResponseError> { exception.ToJsonResponseError(includeTraces) }, serializerOptions);

        public static JsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, Exception[] exceptions, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
            => CreateFailureResponse(statusCode, exceptions?.Where(e => e != null).Select(e => e.ToJsonResponseError(includeTraces)).ToList(), serializerOptions);

        public static JsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, string error, JsonSerializerOptions serializerOptions = null)
            => CreateFailureResponse(statusCode, new List<IResponseError> { CreateCustomError(error) }, serializerOptions);

        public static JsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, string[] errors, JsonSerializerOptions serializerOptions = null)
            => CreateFailureResponse(statusCode, errors?.Select(CreateCustomError).ToList(), serializerOptions);

        public static IResponseError ToJsonResponseError<TEnum>(this TEnum @enum) where TEnum : struct, Enum
        {
            return new JsonResponseError(@enum.ToString(), @enum.GetDescription(), ErrorDetails.CustomFailureOccurred,
                @enum.GetValue().ToString(), @enum.GetHelpLink(), null, ErrorType.Custom);
        }

        public static IResponseError ToJsonResponseError(this Exception exception, bool includeTraces)
        {
            return new JsonResponseError(exception.GetType().Name, exception.Message,
                includeTraces ? exception.ToString() : ErrorDetails.ExceptionOccurred,
                exception.HResult.ToString(), exception.HelpLink, null, ErrorType.Exception);
        }

        internal static IResponseError CreateValidationError(IResponseValidationFailure validationFailure)
        {
            return new JsonResponseError(ErrorTitles.BadRequest, null, ErrorDetails.OneOrMoreValidationErrorsOccurred, null, null,
                new List<IResponseValidationFailure> { validationFailure }, ErrorType.Validation);
        }

        internal static JsonResponse<T> CreateFailureResponse<T>(HttpStatusCode statusCode, List<IResponseError> errors, JsonSerializerOptions options)
        {
            return new JsonResponse<T>(data: default, false, statusCode, null, errors, null, options);
        }

        internal static IResponseError CreateValidationErrors(List<IResponseValidationFailure> validationFailures)
        {
            return new JsonResponseError(ErrorTitles.BadRequest, null, ErrorDetails.OneOrMoreValidationErrorsOccurred, null, null,
                validationFailures, ErrorType.Validation);
        }

        private static IResponseError CreateCustomError(string error)
        {
            return new JsonResponseError(error, null, ErrorDetails.CustomFailureOccurred, null, null, null, ErrorType.Custom);
        }

        internal static JsonResponse CreateFailureResponse(HttpStatusCode statusCode, List<IResponseError> errors, JsonSerializerOptions options)
        {
            return new JsonResponse(null, false, statusCode, null, errors, null, options);
        }
    }
}