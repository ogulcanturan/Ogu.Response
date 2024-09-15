using System;
using System.Linq;

namespace Ogu.Response.Abstractions
{
    public static class ResponseResultBuilderExtensions
    {
        public static IResponseResult<object> ValidationFailure(this IResponseResultBuilder resultBuilder,
            IResponseErrorBuilder errorBuilder, IResponseValidationFailure validationFailure, string instance = null,
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

        public static IResponseResult<T> ValidationFailure<T>(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, IResponseValidationFailure validationFailure, string instance = null,
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
                .WithDetail(detail).Build<T>();

        public static IResponseResult<object> ValidationFailure(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, IResponseValidationFailure[] validationFailures, string instance = null,
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

        public static IResponseResult<T> ValidationFailure<T>(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, IResponseValidationFailure[] validationFailures, string instance = null,
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
                .WithDetail(detail).Build<T>();

        public static IResponseResult<object> CustomFailure<TEnum>(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, TEnum @enum, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            where TEnum : struct, Enum
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errorBuilder.Custom(@enum))
                .Build();

        public static IResponseResult<T> CustomFailure<TEnum, T>(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, TEnum @enum, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            where TEnum : struct, Enum
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errorBuilder.Custom(@enum))
                .Build<T>();

        public static IResponseResult<object> CustomFailure<TEnum>(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, TEnum[] @enums, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            where TEnum : struct, Enum
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errorBuilder.Custom(@enums))
                .Build();

        public static IResponseResult<T> CustomFailure<TEnum, T>(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, TEnum[] @enums, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            where TEnum : struct, Enum
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errorBuilder.Custom(@enums))
                .Build<T>();

        public static IResponseResult<object> CustomFailure(this IResponseResultBuilder builder, IResponseError error, string instance = null,
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

        public static IResponseResult<T> CustomFailure<T>(this IResponseResultBuilder builder, IResponseError error, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            => builder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(error)
                .Build<T>();

        public static IResponseResult<object> CustomFailure(this IResponseResultBuilder builder, IResponseError[] errors, string instance = null,
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

        public static IResponseResult<T> CustomFailure<T>(this IResponseResultBuilder builder, IResponseError[] errors, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            => builder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errors)
                .Build<T>();

        public static IResponseResult<object> CustomFailure(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, string error, string instance = null,
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

        public static IResponseResult<T> CustomFailure<T>(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, string error, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errorBuilder.Custom(error))
                .Build<T>();

        public static IResponseResult<object> CustomFailure(this IResponseResultBuilder resultBuilder,
            IResponseErrorBuilder errorBuilder, string[] errors, string instance = null,
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

        public static IResponseResult<T> CustomFailure<T>(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, string[] errors, string instance = null,
            string type = null, int? status = 400, string title = "Bad Request",
            string detail = "Custom failure occurred.")
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errors.Select(e => errorBuilder.Custom(e)).ToArray())
                .Build<T>();

        public static IResponseResult<object> ExceptionFailure(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, Exception exception, bool includeTraces, string instance = null,
            string type = null, int? status = 500, string title = "Internal Server Error", string detail = "Exception occurred.")
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errorBuilder.Exception(exception, includeTraces))
                .Build();

        public static IResponseResult<T> ExceptionFailure<T>(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, Exception exception, bool includeTraces, string instance = null,
            string type = null, int? status = 500, string title = "Internal Server Error", string detail = "Exception occurred.")
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errorBuilder.Exception(exception, includeTraces))
                .Build<T>();

        public static IResponseResult<object> ExceptionFailure(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, Exception[] exceptions, bool includeTraces, string instance = null,
            string type = null, int? status = 500, string title = "Internal Server Error", string detail = "Exception occurred.")
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errorBuilder.Exception(exceptions, includeTraces))
                .Build();

        public static IResponseResult<T> ExceptionFailure<T>(this IResponseResultBuilder resultBuilder, IResponseErrorBuilder errorBuilder, Exception[] exceptions, bool includeTraces, string instance = null,
            string type = null, int? status = 500, string title = "Internal Server Error", string detail = "Exception occurred.")
            => resultBuilder
                .WithInstance(instance)
                .WithType(type)
                .WithStatus(status)
                .WithTitle(title)
                .WithDetail(detail)
                .WithErrors(errorBuilder.Exception(exceptions, includeTraces))
                .Build<T>();
    }
}