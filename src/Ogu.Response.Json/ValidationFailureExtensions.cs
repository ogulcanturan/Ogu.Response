using Ogu.Response.Abstractions;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace Ogu.Response.Json
{
    public static class ValidationFailureExtensions
    {
        public static IJsonResponse ToJsonResponse(this IValidationFailure failure, JsonSerializerOptions serializerOptions = null)
        {
            return HttpStatusCode.BadRequest.ToFailureJsonResponse(failure, serializerOptions);
        }

        public static IJsonResponse ToJsonResponse(this List<IValidationFailure> failures, JsonSerializerOptions serializerOptions = null)
        {
            return HttpStatusCode.BadRequest.ToFailureJsonResponse(failures, serializerOptions);
        }

        public static IJsonResponse<TData> ToJsonResponse<TData>(this IValidationFailure failure, JsonSerializerOptions serializerOptions = null)
        {
            return HttpStatusCode.BadRequest.ToFailureJsonResponse<TData>(failure, serializerOptions);
        }

        public static IJsonResponse<TData> ToJsonResponse<TData>(this List<IValidationFailure> failures, JsonSerializerOptions serializerOptions = null)
        {
            return HttpStatusCode.BadRequest.ToFailureJsonResponse<TData>(failures, serializerOptions);
        }
    }
}