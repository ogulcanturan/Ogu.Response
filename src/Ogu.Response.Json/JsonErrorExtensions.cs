using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ogu.Response.Json
{
    public static class JsonErrorExtensions
    {
        private static readonly Lazy<Dictionary<Type, Dictionary<string, JsonError>>> LazyEnumTypeToEnumNameToJsonResponseError = new Lazy<Dictionary<Type,Dictionary<string,JsonError>>>(() => new Dictionary<Type, Dictionary<string, JsonError>>());

        public static IError ToJsonError<TEnum>(this TEnum @enum) where TEnum : struct, Enum
        {
            var enumType = typeof(TEnum);
            var enumName = @enum.ToString();

            var enumTypeToEnumNameToJsonResponseError = LazyEnumTypeToEnumNameToJsonResponseError.Value;

            if (!enumTypeToEnumNameToJsonResponseError.TryGetValue(enumType, out var enumNameToJsonResponseError))
            {
                enumNameToJsonResponseError = new Dictionary<string, JsonError>();
                enumTypeToEnumNameToJsonResponseError[enumType] = enumNameToJsonResponseError;
            }

            if (enumNameToJsonResponseError.TryGetValue(enumName, out var jsonResponseError))
            {
                return jsonResponseError;
            }
            
            jsonResponseError = new JsonError(Extensions.GetTitleFromEnum(enumType, enumName) ?? ErrorTitles.BadRequest, Extensions.GetDescriptionFromEnum(enumType, enumName) ?? enumName, enumName, @enum.GetValue(enumType, enumName).ToString(), Extensions.GetHelpLinkFromEnum(enumType, enumName), null, ErrorType.Custom);

            enumNameToJsonResponseError[enumName] = jsonResponseError;

            return jsonResponseError;
        }

        public static Exception ToException(this IError error)
        {
            var exception = new Exception(error.Description)
            {
                HelpLink = error.HelpLink,
            };

            if (!string.IsNullOrWhiteSpace(error.Title))
            {
                exception.Data[nameof(error.Title)] = error.Title;
            }

            if (!string.IsNullOrWhiteSpace(error.Traces))
            {
                exception.Data[nameof(error.Traces)] = error.Traces;
            }

            if (!string.IsNullOrWhiteSpace(error.Code))
            {
                exception.Data[nameof(error.Code)] = error.Code;
            }

            exception.Data[nameof(error.Type)] = error.Type;

            if (error.ValidationFailures?.Count > 0)
            {
                exception.Data[nameof(error.ValidationFailures)] = error.ValidationFailures;
            }

            return exception;
        }

        public static Exception ToException(this List<IError> errors)
        {
            return new AggregateException(errors.Select(error => error.ToException()));
        }
    }
}