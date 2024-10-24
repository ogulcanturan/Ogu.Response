using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;

namespace Ogu.Response.Json
{
    public static class JsonResponseErrorExtensions
    {
        private static readonly Lazy<Dictionary<Type, Dictionary<string, JsonResponseError>>> LazyEnumTypeToEnumNameToJsonResponseError = new Lazy<Dictionary<Type,Dictionary<string,JsonResponseError>>>(() => new Dictionary<Type, Dictionary<string, JsonResponseError>>());

        public static IResponseError ToJsonResponseError<TEnum>(this TEnum @enum) where TEnum : struct, Enum
        {
            var enumType = typeof(TEnum);
            var enumName = @enum.ToString();

            var enumTypeToEnumNameToJsonResponseError = LazyEnumTypeToEnumNameToJsonResponseError.Value;

            if (!enumTypeToEnumNameToJsonResponseError.TryGetValue(enumType, out var enumNameToJsonResponseError))
            {
                enumNameToJsonResponseError = new Dictionary<string, JsonResponseError>();
                enumTypeToEnumNameToJsonResponseError[enumType] = enumNameToJsonResponseError;
            }

            if (enumNameToJsonResponseError.TryGetValue(enumName, out var jsonResponseError))
            {
                return jsonResponseError;
            }
            
            jsonResponseError = new JsonResponseError(ErrorTitles.BadRequest, Extensions.GetDescriptionFromEnum(enumType, enumName) ?? enumName, enumName, @enum.GetValue(enumType, enumName).ToString(), Extensions.GetHelpLinkFromEnum(enumType, enumName), null, ErrorType.Custom);

            enumNameToJsonResponseError[enumName] = jsonResponseError;

            return jsonResponseError;
        }

        public static IResponseError ToJsonResponseError(this Exception exception, bool includeTraces)
        {
            return new JsonResponseError(exception, includeTraces);
        }
    }
}