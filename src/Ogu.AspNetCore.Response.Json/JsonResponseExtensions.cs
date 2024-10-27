using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ogu.AspNetCore.Response.Json;
using Ogu.Response.Abstractions;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace Ogu.Response.Json
{
    public static class JsonResponseExtensions
    {
        public static IJsonResponse ToFailureJsonResponse(this HttpStatusCode statusCode, ModelStateDictionary modelState, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse.Failure(statusCode, new List<IError> { new JsonError(modelState.ToJsonValidationFailures()) }, serializerOptions);
        }

        public static IJsonResponse<TData> ToFailureJsonResponse<TData>(this HttpStatusCode statusCode, ModelStateDictionary modelState, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<TData>.Failure(statusCode, new List<IError> { new JsonError(modelState.ToJsonValidationFailures()) }, serializerOptions);
        }
    }
}