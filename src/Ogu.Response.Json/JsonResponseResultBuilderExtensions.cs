using Ogu.Response.Abstractions;
using System;

namespace Ogu.Response.Json
{
    public static class JsonResponseResultBuilderExtensions
    {
        public static IResponseResult<object> JsonValidationFailure(this IResponseResultBuilder resultBuilder, IResponseValidationFailure validationFailure, string instance = null,
           string type = null, string code = null, int? status = 400, string title = "Bad Request",
           string detail = "One or more validation errors occurred.")
           => resultBuilder.ValidationFailure(JsonResponseError.Builder, validationFailure, instance, type, code, status, title, detail);

        public static IResponseResult<object> JsonValidationFailure(this IResponseResultBuilder resultBuilder, IResponseValidationFailure[] validationFailures, string instance = null,
            string type = null, string code = null, int? status = 400, string title = "Bad Request",
            string detail = "One or more validation errors occurred.")
            => resultBuilder.ValidationFailure(JsonResponseError.Builder, validationFailures, instance, type, code, status, title, detail);

        public static IResponseResult<object> JsonCustomFailure<TEnum>(this IResponseResultBuilder resultBuilder, TEnum @enum, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            where TEnum : struct, System.Enum
            => resultBuilder.CustomFailure(JsonResponseError.Builder, @enum, instance, type, status, title, detail);

        public static IResponseResult<object> JsonCustomFailure<TEnum>(this IResponseResultBuilder resultBuilder, TEnum[] @enums, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            where TEnum : struct, System.Enum
            => resultBuilder.CustomFailure(JsonResponseError.Builder, enums, instance, type, status, title, detail);

        public static IResponseResult<object> JsonCustomFailure(this IResponseResultBuilder resultBuilder, string error, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            => resultBuilder.CustomFailure(JsonResponseError.Builder, error, instance, type, status, title, detail);

        public static IResponseResult<object> JsonCustomFailure(this IResponseResultBuilder resultBuilder, string[] errors, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            => resultBuilder.CustomFailure(JsonResponseError.Builder, errors, instance, type, status, title, detail);

        public static IResponseResult<object> JsonExceptionFailure(this IResponseResultBuilder resultBuilder, Exception exception, bool includeTraces, string instance = null,
            string type = null, int? status = 500, string title = "Internal Server Error", string detail = "Exception occurred.")
            => resultBuilder.ExceptionFailure(JsonResponseError.Builder, exception, includeTraces, instance, type, status, title, detail);

        public static IResponseResult<object> JsonExceptionFailure(this IResponseResultBuilder resultBuilder, Exception[] exceptions, bool includeTraces, string instance = null,
            string type = null, int? status = 500, string title = "Internal Server Error", string detail = "Exception occurred.")
            => resultBuilder.ExceptionFailure(JsonResponseError.Builder, exceptions, includeTraces, instance, type, status, title, detail);
    }
}