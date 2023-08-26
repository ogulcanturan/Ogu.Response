using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Ogu.AspNetCore.Response.Json
{
    public class Error : IError
    {
        public Error(string title, string description, string details, string code, IValidationFailure[] validationFailures, ErrorType type)
        {
            Title = title;
            Description = description;
            Details = details;
            Code = code; 
            ValidationFailures = validationFailures;
            Type = type;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Title { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Description { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Details { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Code { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IValidationFailure[] ValidationFailures { get; set; }

        public ErrorType Type { get; set; }

        public static IErrorBuilder Builder => new ErrorBuilder();

        public static IError Validation(string title, string description, params IValidationFailure[] validationFailures)
            => Builder
                .WithTitle(title)
                .WithDescription(description)
                .WithValidationFailures(validationFailures)
                .Build();

        public static IError Custom(string title, string description = null, string details = null, string code = null)
            => Builder
                .WithTitle(title)
                .WithDescription(description)
                .WithDetails(details)
                .WithCode(code)
                .WithErrorType(ErrorType.Custom)
                .Build();

        public static IError Custom<TEnum>(TEnum @enum, string description, string details) where TEnum : struct, Enum
            => Builder
                .WithTitle($"{@enum}")
                .WithDescription(description ?? Extensions.GetDescription(@enum))
                .WithDetails(details)
                .WithCode($"{Extensions.GetValue(@enum)}")
                .WithErrorType(ErrorType.Custom)
                .Build();

        public static IError[] Custom<TEnum>(params TEnum[] @enums) where TEnum : struct, Enum
            => @enums.Select(e => 
                    Builder
                        .WithTitle($"{e}")
                        .WithDescription(Extensions.GetDescription(e))
                        .WithCode($"{Extensions.GetValue(e)}")
                        .WithErrorType(ErrorType.Custom)
                        .Build()).ToArray();

        public static IError[] Custom<TEnum>(IList<TEnum> @enums) where TEnum : struct, Enum
            => @enums.Select(e =>
                Builder
                    .WithTitle($"{e}")
                    .WithDescription(Extensions.GetDescription(e))
                    .WithCode($"{Extensions.GetValue(e)}")
                    .WithErrorType(ErrorType.Custom)
                    .Build()).ToArray();

        public static IError Exception(Exception exception, bool includeTraces = false)
        {
            var builder = Builder
                .WithTitle(exception.GetType().Name)
                .WithDescription(exception.Message)
                .WithCode($"{exception.HResult}")
                .WithErrorType(ErrorType.Exception);

            if (includeTraces)
                builder = builder.WithDetails(exception.ToString());

            return builder.Build();
        }

        public static IError[] Exception(Exception[] exceptions, bool includeTraces = false)
        {
            return exceptions.Select(e => Exception(e, includeTraces)).ToArray();
        }

        public static IError[] Exception(IList<Exception> exceptions, bool includeTraces = false)
        {
            return exceptions.Select(e => Exception(e, includeTraces)).ToArray();
        }
    }
}