using System;
using System.Collections.Generic;
using System.Linq;

namespace Ogu.Response.Abstractions
{
    public static class ErrorBuilderExtensions
    {
        public static IResponseError Validation(this IResponseErrorBuilder builder, string title, string description, params IResponseValidationFailure[] validationFailures)
            => builder
                .WithTitle(title)
                .WithDescription(description)
                .WithValidationFailures(validationFailures)
                .Build();

        public static IResponseError Custom(this IResponseErrorBuilder builder, string title, string description = null, string details = null, string code = null, string helpLink = null)
            => builder
                .WithTitle(title)
                .WithDescription(description)
                .WithDetails(details)
                .WithCode(code)
                .WithHelpLink(helpLink)
                .WithErrorType(ErrorType.Custom)
                .Build();

        public static IResponseError Custom<TEnum>(this IResponseErrorBuilder builder, TEnum @enum, string description, string details, string helpLink) where TEnum : struct, Enum
            => builder
                .WithTitle(@enum.ToString())
                .WithDescription(description ?? @enum.GetDescription())
                .WithDetails(details)
                .WithCode(@enum.GetValue().ToString())
                .WithHelpLink(helpLink ?? @enum.GetHelpLink())
                .WithErrorType(ErrorType.Custom)
                .Build();

        public static IResponseError[] Custom<TEnum>(this IResponseErrorBuilder builder, params TEnum[] @enums) where TEnum : struct, Enum
            => @enums.Select(e =>
                builder
                        .WithTitle(e.ToString())
                        .WithDescription(e.GetDescription())
                        .WithCode(e.GetValue().ToString())
                        .WithHelpLink(e.GetHelpLink())
                        .WithErrorType(ErrorType.Custom)    
                        .Build()).ToArray();

        public static IResponseError Exception(this IResponseErrorBuilder builder, Exception exception, bool includeTraces = false)
        {
            var errorBuilder = builder
                .WithTitle(exception.GetType().Name)
                .WithDescription(exception.Message)
                .WithCode(exception.HResult.ToString())
                .WithHelpLink(exception.HelpLink)
                .WithErrorType(ErrorType.Exception);

            if (includeTraces)
                errorBuilder = errorBuilder.WithDetails(exception.ToString());

            return errorBuilder.Build();
        }

        public static IResponseError[] Exception(this IResponseErrorBuilder builder, IEnumerable<Exception> exceptions, bool includeTraces = false)
        {
            return exceptions.Select(e => builder.Exception(e, includeTraces)).ToArray();
        }
    }
}
