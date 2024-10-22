using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace Ogu.Response.Abstractions
{
    public static class ResponseExtensions
    {
        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<object, string>> DescriptionCache =
            new ConcurrentDictionary<Type, ConcurrentDictionary<object, string>>();

        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<object, string>> HelpLinkCache =
            new ConcurrentDictionary<Type, ConcurrentDictionary<object, string>>();

        public static object GetValue<TEnum>(this TEnum enumValue) where TEnum : struct, Enum
            => Convert.ChangeType(enumValue, Enum.GetUnderlyingType(typeof(TEnum)));

        public static string GetDescription<TEnum>(this TEnum @enum) where TEnum : struct, Enum
        {
            var type = typeof(TEnum);
            var value = @enum.GetValue();

            return DescriptionCache
                .GetOrAdd(type, _ => new ConcurrentDictionary<object, string>())
                .GetOrAdd(value, v =>
                    type.GetField(@enum.ToString())?
                        .GetCustomAttribute<DescriptionAttribute>()?.Description);
        }

        public static string GetHelpLink<TEnum>(this TEnum @enum) where TEnum : struct, Enum
        {
            var type = typeof(TEnum);
            var value = @enum.GetValue();

            return HelpLinkCache
                .GetOrAdd(type, _ => new ConcurrentDictionary<object, string>())
                .GetOrAdd(value, v =>
                    type.GetField(@enum.ToString())?
                        .GetCustomAttribute<HelpLinkAttribute>()?.HelpLink);
        }
    }
}