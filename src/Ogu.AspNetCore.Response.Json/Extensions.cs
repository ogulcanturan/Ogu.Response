using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ogu.AspNetCore.Response.Abstractions;
using Ogu.Response.Abstractions;
using Ogu.Response.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Constants = Ogu.AspNetCore.Response.Abstractions.Constants;

namespace Ogu.AspNetCore.Response.Json
{
    public static class Extensions
    {
        private const string ResponseContentType = "application/json";

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, ModelStateDictionary modelState, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(statusCode, new List<IResponseError> { new JsonResponseError(modelState.ToJsonValidationFailures()) }, serializerOptions);
        }

        public static IJsonResponse<T> ToFailureJsonResponse<T>(this HttpStatusCode statusCode, ModelStateDictionary modelState, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<T>.Failure(statusCode, new List<IResponseError> { new JsonResponseError(modelState.ToJsonValidationFailures()) }, serializerOptions);
        }

        public static List<IResponseValidationFailure> ToJsonValidationFailures(this ModelStateDictionary modelState)
        {
            return modelState.Select(x => x.Value.Errors.Select(y => (IResponseValidationFailure)new JsonValidationFailure(x.Key, y.ErrorMessage, x.Value.AttemptedValue))).SelectMany(x => x).ToList();
        }

        public static Task ExecuteJsonResponseAsync(this ActionContext actionContext, JsonActionResponse obj, object serializedResponse, HttpStatusCode statusCode, JsonSerializerOptions serializerOptions)
        {
            return ExecuteJsonResponseAsync(actionContext.HttpContext, obj, serializedResponse, statusCode, serializerOptions);
        }

        public static Task ExecuteJsonResponseAsync(this HttpContext context, JsonActionResponse obj, object serializedResponse, HttpStatusCode statusCode, JsonSerializerOptions serializerOptions)
        {
            var statusCodeAsInt = (int)statusCode;

            if (Constants.NoResponseStatusCodes.Contains(statusCodeAsInt))
            {
                return Task.CompletedTask;
            }

            var response = context.Response;

            response.ContentType = ResponseContentType;
            response.StatusCode = statusCodeAsInt;

            var json = serializedResponse?.ToString() ?? SerializeToJson(obj, serializerOptions);

            try
            {
                return response.WriteAsync(json, context.RequestAborted);
            }
            catch (OperationCanceledException)
            {
                return Task.FromCanceled(context.RequestAborted);
            }
        }

        public static Task ExecuteJsonResponseAsync<T>(this ActionContext actionContext, JsonActionResponse<T> obj, object serializedResponse, HttpStatusCode statusCode, JsonSerializerOptions serializerOptions)
        {
            return ExecuteJsonResponseAsync(actionContext.HttpContext, obj, serializedResponse, statusCode, serializerOptions);
        }

        public static Task ExecuteJsonResponseAsync<T>(this HttpContext context, JsonActionResponse<T> obj, object serializedResponse, HttpStatusCode statusCode, JsonSerializerOptions serializerOptions)
        {
            var statusCodeAsInt = (int)statusCode;

            if (Constants.NoResponseStatusCodes.Contains(statusCodeAsInt))
            {
                return Task.CompletedTask;
            }

            var response = context.Response;

            response.ContentType = ResponseContentType;
            response.StatusCode = statusCodeAsInt;

            var json = serializedResponse?.ToString() ?? SerializeToJson(obj, serializerOptions);

            try
            {
                return response.WriteAsync(json, context.RequestAborted);
            }
            catch (OperationCanceledException)
            {
                return Task.FromCanceled(context.RequestAborted);
            }
        }

        private static string SerializeToJson(JsonActionResponse response, JsonSerializerOptions serializerOptions)
        {
            if (response.Extras.Count == 0)
            {
                response.Extras = null;
            }

            if (response.Errors.Count == 0)
            {
                response.Errors = null;
            }
            else
            {
                foreach (var error in response.Errors.Where(e => e.Type != ErrorType.Validation))
                {
                    error.ValidationFailures = null;
                }
            }

            return JsonSerializer.Serialize(response, serializerOptions);
        }

        private static string SerializeToJson<T>(JsonActionResponse<T> response, JsonSerializerOptions serializerOptions)
        {
            if (response.Extras.Count == 0)
            {
                response.Extras = null;
            }

            if (response.Errors.Count == 0)
            {
                response.Errors = null;
            }
            else
            {
                foreach (var error in response.Errors.Where(e => e.Type != ErrorType.Validation))
                {
                    error.ValidationFailures = null;
                }
            }

            return JsonSerializer.Serialize(response, serializerOptions);
        }

        public static IActionResponse ToJsonAction(this IResponse response) => new JsonActionResponse(response);

        public static IActionResponse ToAction(this IJsonResponse response) => new JsonActionResponse(response);

        public static IActionResponse<T> ToJsonAction<T>(this IResponse<T> response) => new JsonActionResponse<T>(response);

        public static IActionResponse<T> ToAction<T>(this IJsonResponse<T> response) => new JsonActionResponse<T>(response);
    }
}