using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Ogu.AspNetCore.Response
{
    public static class ResponseExtensions
    {
        public static object GetValue<TEnum>(this TEnum enumValue) where TEnum : struct, Enum
            => Convert.ChangeType(enumValue, Enum.GetUnderlyingType(typeof(TEnum)));

        public static string GetDescription<TEnum>(this TEnum @enum) where TEnum : struct, Enum
            => @enum.GetType()
                .GetField(@enum.ToString())?
                .GetCustomAttribute<DescriptionAttribute>()?.Description;

        public static string GetHelpLink<TEnum>(this TEnum @enum) where TEnum : struct, Enum
             => @enum.GetType()
                .GetField(@enum.ToString())?
                .GetCustomAttribute<HelpLinkAttribute>()?.HelpLink;

        public static bool IsErrorExists(this IResult result)
            => result?.Extensions?.ContainsKey("Errors") ?? false;

        public static IEnumerable<IError> GetErrors(this IResult result)
        {
            if (result?.Extensions?.TryGetValue("Errors", out var errors) ?? false)
            {
                return (IError[])errors;
            }

            return Enumerable.Empty<IError>();
        }
    }
}