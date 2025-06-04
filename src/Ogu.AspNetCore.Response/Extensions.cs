using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Ogu.Response.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ogu.Response
{
    public static class Extensions
    {
        public static Task ExecuteResultAsync(this IActionResult actionResult, HttpContext context)
        {
            return actionResult.ExecuteResultAsync(new ActionContext(context, context.GetRouteData() ?? new RouteData(), new ActionDescriptor()));
        }

        public static IResponse ToFailureResponse(this HttpStatusCode statusCode, ModelStateDictionary modelState)
        {
            return Response.Failure(statusCode, new List<IError> { new Error(modelState.ToValidationFailures()) });
        }

        public static IResponse<TData> ToFailureResponse<TData>(this HttpStatusCode statusCode, ModelStateDictionary modelState)
        {
            return Response<TData>.Failure(statusCode, new List<IError> { new Error(modelState.ToValidationFailures()) });
        }

        public static List<IValidationFailure> ToValidationFailures(this ModelStateDictionary modelState)
        {
            return modelState.Select(x => x.Value.Errors.Select(y =>
                    (IValidationFailure)new ValidationFailure(x.Key, y.ErrorMessage, x.Value.AttemptedValue)))
                .SelectMany(x => x).ToList();
        }

        public static IResponse ToResponse(this ModelStateDictionary modelState)
        {
            return HttpStatusCode.BadRequest.ToFailureResponse(modelState);
        }

        public static IResponse<T> ToResponse<T>(this ModelStateDictionary modelState)
        {
            return HttpStatusCode.BadRequest.ToFailureResponse<T>(modelState);
        }

        public static IActionResult ToAction(this ModelStateDictionary modelState)
        {
            return HttpStatusCode.BadRequest.ToFailureResponse(modelState).ToAction();
        }

        public static IActionResult ToAction<T>(this ModelStateDictionary modelState)
        {
            return HttpStatusCode.BadRequest.ToFailureResponse<T>(modelState).ToAction();
        }

        public static IActionResult ToAction(this IResponse response)
        {
            return InternalToAction((int)response.Status, response);
        }

        public static IActionResult ToAction<T>(this IResponse<T> response)
        {
            return InternalToAction((int)response.Status, response);
        }

        private static IActionResult InternalToAction(int statusCode, object response)
        {
            return ResponseDefaults.NoResponseStatusCodes.Contains(statusCode)
                ? (IActionResult)new StatusCodeResult(statusCode)
                : new ObjectResult(response);
        }
    }
}