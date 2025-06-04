using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Ogu.Response
{
    public static class ResponseExtensions
    {
        public static IResponse ToResponse<TData>(this IResponse<TData> response)
        {
            return response.Success
                ? new Response(response.Data, true, response.Status, response.Extras, response.Errors)
                : new Response(null, false, response.Status, response.Extras, response.Errors);
        }

        public static IResponse ToSuccessResponse(this HttpStatusCode status)
        {
            return new Response(null, success: true, status, null, null);
        }

        public static IResponse ToSuccessResponse(this HttpStatusCode status, object data)
        {
            return new Response(data, success: true, status, null, null);
        }

        public static IResponse ToFailureResponse(this HttpStatusCode status)
        {
            return Response.Failure(status, null);
        }

        public static IResponse ToFailureResponse(this HttpStatusCode status, IError error)
        {
            return Response.Failure(status, new List<IError> { error });
        }

        public static IResponse ToFailureResponse(this HttpStatusCode status, List<IError> errors)
        {
            return Response.Failure(status, errors);
        }

        public static IResponse ToFailureResponse(this HttpStatusCode status, IValidationFailure failure)
        {
            return Response.Failure(status, new List<IError> { new Error(failure) });
        }

        public static IResponse ToFailureResponse(this HttpStatusCode status, List<IValidationFailure> failures)
        {
            return Response.Failure(status, new List<IError> { new Error(failures) });
        }

        public static IResponse ToFailureResponse<TEnum>(this HttpStatusCode status, TEnum @enum) where TEnum : struct, Enum
        {
            return Response.Failure(status, new List<IError> { @enum.ToError() });
        }

        public static IResponse ToFailureResponse<TEnum>(this HttpStatusCode status, IEnumerable<TEnum> enums) where TEnum : struct, Enum
        {
            return Response.Failure(status, enums.Select(e => e.ToError()).ToList());
        }

        public static IResponse ToFailureResponse(this HttpStatusCode status, Exception exception, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic)
        {
            return Response.Failure(status, GetErrors(exception, traceLevel));
        }

        public static IResponse ToFailureResponse(this HttpStatusCode status, IEnumerable<Exception> exceptions, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic)
        {
            return Response.Failure(status, exceptions.SelectMany(e => GetErrors(e, traceLevel)).ToList());
        }

        public static IResponse ToFailureResponse(this HttpStatusCode status, string error)
        {
            return Response.Failure(status, new List<IError> { new Error(error) });
        }

        public static IResponse ToFailureResponse(this HttpStatusCode status, string title, string description)
        {
            return Response.Failure(status, new List<IError> { new Error(title, description) });
        }

        public static IResponse ToFailureResponse(this HttpStatusCode status, string[] errors)
        {
            return Response.Failure(status, errors.Select(e => (IError)new Error(e)).ToList());
        }

        private static List<IError> GetErrors(Exception exception, ExceptionTraceLevel traceLevel)
        {
            return exception is AggregateException aex
                ? aex.InnerExceptions.Select(e => (IError)new Error(e, traceLevel)).ToList()
                : new List<IError> { new Error(exception, traceLevel) };
        }
    }
}