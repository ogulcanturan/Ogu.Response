using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Net;

namespace Ogu.Response
{
    public static class ExceptionExtensions
    {
        public static IResponse ToResponse(this Exception exception, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic)
        {
            return HttpStatusCode.InternalServerError.ToFailureResponse(exception, traceLevel);
        }

        public static IResponse ToResponse(this IEnumerable<Exception> exceptions, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic)
        {
            return HttpStatusCode.InternalServerError.ToFailureResponse(exceptions, traceLevel);
        }

        public static IResponse<TData> ToResponseOf<TData>(this Exception exception, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic)
        {
            return HttpStatusCode.InternalServerError.ToFailureResponseOf<TData>(exception, traceLevel);
        }

        public static IResponse<TData> ToResponseOf<TData>(this IEnumerable<Exception> exceptions, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic)
        {
            return HttpStatusCode.InternalServerError.ToFailureResponseOf<TData>(exceptions, traceLevel);
        }

        public static IError ToError(this Exception exception, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic)
        {
            return new Error(exception, traceLevel);
        }
    }
}