using Ogu.Response.Abstractions;
using System.Collections.Generic;
using System.Net;

namespace Ogu.Response
{
    public static class ValidationFailureExtensions
    {
        public static IResponse ToResponse(this IValidationFailure failure)
        {
            return HttpStatusCode.BadRequest.ToFailureResponse(failure);
        }

        public static IResponse ToResponse(this List<IValidationFailure> failures)
        {
            return HttpStatusCode.BadRequest.ToFailureResponse(failures);
        }

        public static IResponse<TData> ToResponse<TData>(this IValidationFailure failure)
        {
            return HttpStatusCode.BadRequest.ToFailureResponse<TData>(failure);
        }

        public static IResponse<TData> ToResponse<TData>(this List<IValidationFailure> failures)
        {
            return HttpStatusCode.BadRequest.ToFailureResponse<TData>(failures);
        }
    }
}