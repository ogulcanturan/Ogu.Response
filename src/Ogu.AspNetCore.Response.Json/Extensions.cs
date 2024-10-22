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

namespace Ogu.AspNetCore.Response.Json
{
    public static class Extensions
    {
        private const string ResponseContentType = "application/json";

        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, ModelStateDictionary modelState,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(data: null, false, statusCode, null,
                new List<IResponseError>()
                {
                    new JsonResponseError(ErrorTitles.BadRequest, description: null, ErrorDetails.OneOrMoreValidationErrorsOccurred, code: null, helpLink: null,
                        modelState.ToJsonValidationFailures(), ErrorType.Validation)
                }, serializedResponse: null, serializerOptions);

        public static IList<IResponseValidationFailure> ToJsonValidationFailures(this ModelStateDictionary modelState)
        {
            return modelState.Select(x => x.Value.Errors.Select(y => (IResponseValidationFailure)new
                    JsonValidationFailure(x.Key, y.ErrorMessage, x.Value.AttemptedValue)))
                .SelectMany(x => x).ToList();
        }

        public static Task ExecuteJsonResponseAsync(this ActionContext actionContext, JsonActionResponse obj, string serializedResponse, HttpStatusCode statusCode, JsonSerializerOptions serializerOptions)
        {
            return ExecuteJsonResponseAsync(actionContext.HttpContext, obj, serializedResponse, statusCode, serializerOptions);
        }

        public static Task ExecuteJsonResponseAsync(this HttpContext context, JsonActionResponse obj, string serializedResponse, HttpStatusCode statusCode, JsonSerializerOptions serializerOptions)
        {
            var statusCodeAsInt = (int)statusCode;

            if (Constants.NoResponseStatusCodes.Contains(statusCodeAsInt))
            {
                return Task.CompletedTask;
            }

            var response = context.Response;

            response.ContentType = ResponseContentType;
            response.StatusCode = statusCodeAsInt;

            var json = serializedResponse ?? SerializeToJson(obj, serializerOptions);

            try
            {
                return response.WriteAsync(json, context.RequestAborted);
            }
            catch (OperationCanceledException)
            {
                return Task.FromCanceled(context.RequestAborted);
            }
        }

        public static Task ExecuteJsonResponseAsync<T>(this ActionContext actionContext, JsonActionResponse<T> obj, string serializedResponse, HttpStatusCode statusCode, JsonSerializerOptions serializerOptions)
        {
            return ExecuteJsonResponseAsync(actionContext.HttpContext, obj, serializedResponse, statusCode, serializerOptions);
        }

        public static Task ExecuteJsonResponseAsync<T>(this HttpContext context, JsonActionResponse<T> obj, string serializedResponse, HttpStatusCode statusCode, JsonSerializerOptions serializerOptions)
        {
            var statusCodeAsInt = (int)statusCode;

            if (Constants.NoResponseStatusCodes.Contains(statusCodeAsInt))
            {
                return Task.CompletedTask;
            }

            var response = context.Response;

            response.ContentType = ResponseContentType;
            response.StatusCode = statusCodeAsInt;

            var json = serializedResponse ?? SerializeToJson(obj, serializerOptions);

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
            if (response.Extensions.Count == 0)
            {
                response.Extensions = null;
            }

            if (response.Errors.Count == 0)
            {
                response.Errors = null;
            }

            return JsonSerializer.Serialize(response, serializerOptions);
        }

        private static string SerializeToJson<T>(JsonActionResponse<T> response, JsonSerializerOptions serializerOptions)
        {
            if (response.Extensions.Count == 0)
            {
                response.Extensions = null;
            }

            if (response.Errors.Count == 0)
            {
                response.Errors = null;
            }

            return JsonSerializer.Serialize(response, serializerOptions);
        }

        public static IActionResponse ToJsonAction(this IResponse<string> response) => new JsonActionResponse(response);

        public static IActionResponse ToAction(this IJsonResponse response) => new JsonActionResponse(response);

        public static IActionResponse<T> ToJsonAction<T>(this IResponse<T, string> response) => new JsonActionResponse<T>(response);

        public static IActionResponse<T> ToAction<T>(this IJsonResponse<T> response) => new JsonActionResponse<T>(response);
    }
}