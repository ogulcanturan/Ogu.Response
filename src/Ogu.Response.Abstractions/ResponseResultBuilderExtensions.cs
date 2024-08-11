using System;
using System.Linq;

namespace Ogu.Response.Abstractions
{
    public static class ResponseResultBuilderExtensions
    {
        public static IResponseResult ValidationFailure(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, IResponseValidationFailure validationFailure, string instance = null,
            string type = null, string code = null, int? status = 400, string title = "Bad Request",
            string detail = "One or more validation errors occurred.")
            => resultBuilder
                .WithErrors(errorBuilder.Validation(null, null, validationFailure))
                .WithInstance(instance)
                .WithType(type)
                .WithCode(code)
                .WithStatus(status)
                .WithType(type)
                .WithTitle(title)
                .WithDetail(detail).Build();

        public static IResponseResult ValidationFailure(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, IResponseValidationFailure[] validationFailures, string instance = null,
            string type = null, string code = null, int? status = 400, string title = "Bad Request",
            string detail = "One or more validation errors occurred.")
            => resultBuilder
                .WithErrors(errorBuilder.Validation(null, null, validationFailures))
                .WithInstance(instance)
                .WithType(type)
                .WithCode(code)
                .WithStatus(status)
                .WithType(type)
                .WithTitle(title)
                .WithDetail(detail).Build();

        public static IResponseResult CustomFailure<TEnum>(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, TEnum @enum, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            where TEnum : struct, System.Enum
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errorBuilder.Custom(@enum, null, null, null))
                .Build();

        public static IResponseResult CustomFailure<TEnum>(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, TEnum[] @enums, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            where TEnum : struct, System.Enum
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errorBuilder.Custom(@enums))
                .Build();

        public static IResponseResult CustomFailure(this IResponseResultBuilder builder, IResponseError error, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            => builder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(error)
                .Build();

        public static IResponseResult CustomFailure(this IResponseResultBuilder builder, IResponseError[] errors, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            => builder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errors)
                .Build();

        public static IResponseResult CustomFailure(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, string error, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errorBuilder.Custom(error))
                .Build();

        public static IResponseResult CustomFailure(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, string[] errors, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errors.Select(e => errorBuilder.Custom(e)).ToArray())
                .Build();

        public static IResponseResult ExceptionFailure(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, Exception exception, bool includeTraces, string instance = null,
            string type = null, int? status = 500, string title = "Internal Server Error", string detail = "Exception occurred.")
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errorBuilder.Exception(exception, includeTraces))
                .Build();

        public static IResponseResult ExceptionFailure(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, Exception[] exceptions, bool includeTraces, string instance = null,
            string type = null, int? status = 500, string title = "Internal Server Error", string detail = "Exception occurred.")
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errorBuilder.Exception(exceptions, includeTraces))
                .Build();
    }
}