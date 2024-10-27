using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;

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
    }
}