using Ogu.Response.Abstractions;
using System;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Ogu.Response.Json
{
    public static class ExceptionExtensions
    {
        public static IJsonResponse ToJsonResponse(this Exception exception, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic, JsonSerializerOptions serializerOptions = null)
        {
            return HttpStatusCode.InternalServerError.ToFailureJsonResponse(exception, traceLevel, serializerOptions);
        }

        public static IJsonResponse ToJsonResponse(this Exception[] exceptions, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic, JsonSerializerOptions serializerOptions = null)
        {
            return HttpStatusCode.InternalServerError.ToFailureJsonResponse(exceptions, traceLevel, serializerOptions);
        }

        public static IJsonResponse<TData> ToJsonResponse<TData>(this Exception exception, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic, JsonSerializerOptions serializerOptions = null)
        {
            return HttpStatusCode.InternalServerError.ToFailureJsonResponse<TData>(exception, traceLevel, serializerOptions);
        }

        public static IJsonResponse<TData> ToJsonResponse<TData>(this Exception[] exceptions, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic, JsonSerializerOptions serializerOptions = null)
        {
            return HttpStatusCode.InternalServerError.ToFailureJsonResponse<TData>(exceptions, traceLevel, serializerOptions);
        }

        public static IError ToJsonError(this Exception exception, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic)
        {
            return new JsonError(exception, traceLevel);
        }

        internal static string GetConcatenatedExceptionMessages(this Exception exception, ExceptionTraceLevel traceLevel)
        {
            switch (traceLevel)
            {
                default:
                case ExceptionTraceLevel.None:
                    return null;

                case ExceptionTraceLevel.Basic:

                    var result = new StringBuilder();

                    var ex = exception;

                    while (ex != null)
                    {
                        result.Append(ex.GetType().Name);
                        result.Append(": ");
                        result.Append(ex.Message);
                        result.Append(" - > ");

                        ex = ex.InnerException;
                    }

                    if (result.Length > 4)
                    {
                        result.Length -= 5;
                    }

                    return result.ToString();

                case ExceptionTraceLevel.Full:
                    return exception.ToString();
            }
        }
    }
}