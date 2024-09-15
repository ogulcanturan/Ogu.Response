using Ogu.Response.Abstractions;
using System;

namespace Ogu.Response.Json
{
    public static class JsonResponseResultBuilderExtensions
    {
        public static IResponseResult<object> JsonValidationFailure(this IResponseResultBuilder resultBuilder, IResponseValidationFailure validationFailure, object data = null, string instance = null,
           string type = null, string code = null, int? status = 400, string title = "Bad Request",
           string detail = "One or more validation errors occurred.")
           => resultBuilder.ValidationFailure(JsonResponseError.Builder, validationFailure, data, instance, type, code, status, title, detail);

        public static IResponseResult<object> JsonValidationFailure(this IResponseResultBuilder resultBuilder, IResponseValidationFailure[] validationFailures, object data = null, string instance = null,
            string type = null, string code = null, int? status = 400, string title = "Bad Request",
            string detail = "One or more validation errors occurred.")
            => resultBuilder.ValidationFailure(JsonResponseError.Builder, validationFailures, data, instance, type, code, status, title, detail);

        public static IResponseResult<object> JsonCustomFailure<TEnum>(this IResponseResultBuilder resultBuilder, TEnum @enum, object data = null, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            where TEnum : struct, System.Enum
            => resultBuilder.CustomFailure(JsonResponseError.Builder, @enum, data, instance, type, status, title, detail);

        public static IResponseResult<object> JsonCustomFailure<TEnum>(this IResponseResultBuilder resultBuilder, TEnum[] @enums, object data = null, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            where TEnum : struct, System.Enum
            => resultBuilder.CustomFailure(JsonResponseError.Builder, enums, data, instance, type, status, title, detail);

        public static IResponseResult<object> JsonCustomFailure(this IResponseResultBuilder resultBuilder, string error, object data = null, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            => resultBuilder.CustomFailure(JsonResponseError.Builder, error, data, instance, type, status, title, detail);

        public static IResponseResult<object> JsonCustomFailure(this IResponseResultBuilder resultBuilder, string[] errors, object data = null, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            => resultBuilder.CustomFailure<object>(JsonResponseError.Builder, errors, data, instance, type, status, title, detail);

        public static IResponseResult<object> JsonExceptionFailure(this IResponseResultBuilder resultBuilder, Exception exception, bool includeTraces, object data = null, string instance = null,
            string type = null, int? status = 500, string title = "Internal Server Error", string detail = "Exception occurred.")
            => resultBuilder.ExceptionFailure(JsonResponseError.Builder, exception, includeTraces, data, instance, type, status, title, detail);

        public static IResponseResult<object> JsonExceptionFailure(this IResponseResultBuilder resultBuilder, Exception[] exceptions, bool includeTraces, object data = null, string instance = null,
            string type = null, int? status = 500, string title = "Internal Server Error", string detail = "Exception occurred.")
            => resultBuilder.ExceptionFailure(JsonResponseError.Builder, exceptions, includeTraces, data, instance, type, status, title, detail);
    }
}