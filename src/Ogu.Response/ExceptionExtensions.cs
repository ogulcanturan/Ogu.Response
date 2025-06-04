using Ogu.Response.Abstractions;
using System;
using System.Net;

namespace Ogu.Response
{
    public static class ExceptionExtensions
    {
        public static IResponse ToResponse(this Exception exception, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic)
        {
            return HttpStatusCode.InternalServerError.ToFailureResponse(exception, traceLevel);
        }

        public static IResponse ToResponse(this Exception[] exceptions, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic)
        {
            return HttpStatusCode.InternalServerError.ToFailureResponse(exceptions, traceLevel);
        }

        public static IResponse<TData> ToResponse<TData>(this Exception exception, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic)
        {
            return HttpStatusCode.InternalServerError.ToFailureResponse<TData>(exception, traceLevel);
        }

        public static IResponse<TData> ToResponse<TData>(this Exception[] exceptions, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic)
        {
            return HttpStatusCode.InternalServerError.ToFailureResponse<TData>(exceptions, traceLevel);
        }

        public static IError ToError(this Exception exception, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic)
        {
            return new Error(exception, traceLevel);
        }
    }
}