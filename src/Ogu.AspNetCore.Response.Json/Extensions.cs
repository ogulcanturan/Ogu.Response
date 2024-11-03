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
#if NETCOREAPP3_1_OR_GREATER
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
#endif

namespace Ogu.AspNetCore.Response.Json
{
    public static class Extensions
    {
        private const string ResponseContentType = "application/json";

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

            var json = serializedResponse == null
                ? SerializeToJson(obj, serializerOptions
#if NETCOREAPP3_1_OR_GREATER 
                                       ?? context.RequestServices.GetService<IOptions<JsonOptions>>().Value.JsonSerializerOptions
#endif
                                       )
                : serializedResponse as string ?? serializedResponse.ToString();

            try
            {
                return response.WriteAsync(json, context.RequestAborted);
            }
            catch (OperationCanceledException)
            {
                return Task.FromCanceled(context.RequestAborted);
            }
        }

        public static Task ExecuteJsonResponseAsync<TData>(this ActionContext actionContext, JsonActionResponse<TData> obj, object serializedResponse, HttpStatusCode statusCode, JsonSerializerOptions serializerOptions)
        {
            return ExecuteJsonResponseAsync(actionContext.HttpContext, obj, serializedResponse, statusCode, serializerOptions);
        }

        public static Task ExecuteJsonResponseAsync<TData>(this HttpContext context, JsonActionResponse<TData> obj, object serializedResponse, HttpStatusCode statusCode, JsonSerializerOptions serializerOptions)
        {
            var statusCodeAsInt = (int)statusCode;

            if (Constants.NoResponseStatusCodes.Contains(statusCodeAsInt))
            {
                return Task.CompletedTask;
            }

            var response = context.Response;

            response.ContentType = ResponseContentType;
            response.StatusCode = statusCodeAsInt;

            var json = serializedResponse == null
                ? SerializeToJson(obj, serializerOptions
#if NETCOREAPP3_1_OR_GREATER
                                       ?? context.RequestServices.GetService<IOptions<JsonOptions>>().Value.JsonSerializerOptions
#endif
                                )
                : serializedResponse as string ?? serializedResponse.ToString();

            try
            {
                return response.WriteAsync(json, context.RequestAborted);
            }
            catch (OperationCanceledException)
            {
                return Task.FromCanceled(context.RequestAborted);
            }
        }

        public static List<IValidationFailure> ToJsonValidationFailures(this ModelStateDictionary modelState)
        {
            return modelState.Select(x => x.Value.Errors.Select(y => (IValidationFailure)new JsonValidationFailure(x.Key, y.ErrorMessage, x.Value.AttemptedValue))).SelectMany(x => x).ToList();
        }

        public static IActionResponse ToJsonAction(this ModelStateDictionary modelState)
        {
            return HttpStatusCode.BadRequest.ToFailureJsonResponse(modelState).ToAction();
        }

        public static IActionResponse ToJsonAction(this IResponse response)
        {
            return new JsonActionResponse(response);
        }

        public static IActionResponse ToAction(this IJsonResponse response)
        {
            return new JsonActionResponse(response);
        }

        public static IActionResponse<T> ToJsonAction<T>(this ModelStateDictionary modelState)
        {
            return HttpStatusCode.BadRequest.ToFailureJsonResponse<T>(modelState).ToAction();
        }

        public static IActionResponse<T> ToJsonAction<T>(this IResponse<T> response)
        {
            return new JsonActionResponse<T>(response);
        }

        public static IActionResponse<T> ToAction<T>(this IJsonResponse<T> response)
        {
            return new JsonActionResponse<T>(response);
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
    }
}