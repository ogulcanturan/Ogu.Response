using Ogu.Response.Abstractions;
using System;
using System.Net;
using System.Text.Json;

namespace Ogu.Response.Json
{
    public static class ExceptionExtensions
    {
        public static IJsonResponse ToJsonResponse(this Exception exception, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
        {
            return HttpStatusCode.InternalServerError.ToFailureJsonResponse(exception, includeTraces, serializerOptions);
        }

        public static IJsonResponse ToJsonResponse(this Exception[] exceptions, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
        {
            return HttpStatusCode.InternalServerError.ToFailureJsonResponse(exceptions, includeTraces, serializerOptions);
        }

        public static IJsonResponse<TData> ToJsonResponse<TData>(this Exception exception, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
        {
            return HttpStatusCode.InternalServerError.ToFailureJsonResponse<TData>(exception, includeTraces, serializerOptions);
        }

        public static IJsonResponse<TData> ToJsonResponse<TData>(this Exception[] exceptions, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
        {
            return HttpStatusCode.InternalServerError.ToFailureJsonResponse<TData>(exceptions, includeTraces, serializerOptions);
        }

        public static IError ToJsonError(this Exception exception, bool includeTraces = false)
        {
            return new JsonError(exception, includeTraces);
        }
    }
}