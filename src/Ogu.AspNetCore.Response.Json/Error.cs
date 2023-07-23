using System;
using System.Text.Json.Serialization;

namespace Ogu.AspNetCore.Response.Json
{
    public class Error : IError
    {
        public Error(string title, string description, string code, IValidationFailure[] validationFailures, ErrorType type)
        {
            Title = title;
            Description = description;
            Code = code; 
            ValidationFailures = validationFailures;
            Type = type;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Title { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Description { get; set; }

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

        public static IError Basic(string title, string description, string code)
            => Builder
                .WithTitle(title)
                .WithDescription(description)
                .WithCode(code)
                .Build();

        public static IError Basic<TEnum>(TEnum @enum, string description) where TEnum : struct, Enum
            => Builder
                .WithTitle($"{@enum}")
                .WithDescription(description ?? AspNetCore.Response.Extensions.GetDescription(@enum))
                .WithCode($"{AspNetCore.Response.Extensions.GetValue(@enum)}")
                .Build();
    }
}