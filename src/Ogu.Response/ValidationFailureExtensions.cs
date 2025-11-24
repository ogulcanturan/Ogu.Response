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

        public static IResponse<TData> ToResponseOf<TData>(this IValidationFailure failure)
        {
            return HttpStatusCode.BadRequest.ToFailureResponseOf<TData>(failure);
        }

        public static IResponse<TData> ToResponseOf<TData>(this List<IValidationFailure> failures)
        {
            return HttpStatusCode.BadRequest.ToFailureResponseOf<TData>(failures);
        }
    }
}