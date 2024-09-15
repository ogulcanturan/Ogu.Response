using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ogu.AspNetCore.Response.Abstractions;
using Ogu.Response.Abstractions;
using Ogu.Response.Json;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ogu.AspNetCore.Response.Json
{
    public static class Extensions
    {
        private const string ResponseContentType = "application/json";

        public static IJsonResponse<T> ToFailJsonResponseT<T>(this HttpStatusCode status, IResponseValidationFailure[] validationFailures,
            T data = default,
            JsonSerializerOptions serializerOptions = null)
            => JsonResponse<T>.Failure((int)status, validationFailures, data, serializerOptions);

        public static IJsonResponse ToFailJsonResponse(this HttpStatusCode status, ModelStateDictionary modelState, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => JsonFailure((int)status, modelState, data, serializerOptions);

        public static JsonResponse JsonFailure(int status, ModelStateDictionary modelState, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse(data, JsonResponseResult<object>.Builder.ValidationFailure<object>(JsonResponseError.Builder, modelState.ToJsonValidationFailures()), status, false, serializerOptions);

        public static JsonResponse<T> JsonFailure<T>(int status, ModelStateDictionary modelState, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new JsonResponse<T>(data, JsonResponseResult<T>.Builder.ValidationFailure<T>(JsonResponseError.Builder, modelState.ToJsonValidationFailures()), status, false, serializerOptions);

        public static IResponseValidationFailure[] ToJsonValidationFailures(this ModelStateDictionary modelState)
        {
            return modelState.Select(x => x.Value?.Errors.Select(y => new
                    JsonValidationFailure(x.Key, y.ErrorMessage, x.Value.AttemptedValue)))
                .SelectMany(x => x ?? Enumerable.Empty<IResponseValidationFailure>()).ToArray();
        }

        public static Task ExecuteJsonResponseAsync(this ActionContext actionContext, object obj, string serializedResponse, int status, JsonSerializerOptions serializerOptions)
        {
            return ExecuteJsonResponseAsync(actionContext.HttpContext, obj, serializedResponse, status, serializerOptions);
        }

        public static Task ExecuteJsonResponseAsync(this HttpContext context, object obj, string serializedResponse, int status, JsonSerializerOptions serializerOptions)
        {
            if (Constants.NoResponseStatusCodes.Contains(status))
            {
                return Task.CompletedTask;
            }

            var response = context.Response;

            response.ContentType = ResponseContentType;
            response.StatusCode = status;

            var json = serializedResponse ?? JsonSerializer.Serialize(obj, serializerOptions);

            try
            {
                return response.WriteAsync(json, context.RequestAborted);
            }
            catch (OperationCanceledException)
            {
                return Task.FromCanceled(context.RequestAborted);
            }
        }

        public static IActionResponse ToJsonAction(this IResponse response) => new JsonActionResponse(response);
        public static IActionResponse ToAction(this IJsonResponse response) => new JsonActionResponse(response);

        //public static void AddErrors(this IJsonResponse response, params IResponseError[] errors)
        //{
        //    if (response.Result == null)
        //    {
        //        response.Result = JsonResponseResult.Builder.WithErrors(errors).Build();
        //        return;
        //    }

        //    Result = new JsonResponseResultBuilder(Result).WithErrors(errors).Build();
        //}

        public static IActionResponse<T> ToJsonAction<T>(this IResponse<T> response) => new JsonActionResponse<T>(response);
        public static IActionResponse<T> ToAction<T>(this IJsonResponse<T> response) => new JsonActionResponse<T>(response);
    }
}