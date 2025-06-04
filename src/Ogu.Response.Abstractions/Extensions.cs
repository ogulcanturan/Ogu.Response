using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace Ogu.Response.Abstractions
{
    public static class Extensions
    {
        private static readonly Lazy<ConcurrentDictionary<Type, ConcurrentDictionary<string, TitleAttribute>>> LazyEnumTypeToEnumNameToTitleAttribute = new Lazy<ConcurrentDictionary<Type, ConcurrentDictionary<string, TitleAttribute>>>(() => new ConcurrentDictionary<Type, ConcurrentDictionary<string, TitleAttribute>>());

        private static readonly Lazy<ConcurrentDictionary<Type, ConcurrentDictionary<string, DescriptionAttribute>>> LazyEnumTypeToEnumNameToDescriptionAttribute = new Lazy<ConcurrentDictionary<Type, ConcurrentDictionary<string, DescriptionAttribute>>>(() => new ConcurrentDictionary<Type, ConcurrentDictionary<string, DescriptionAttribute>>());

        private static readonly Lazy<ConcurrentDictionary<Type, ConcurrentDictionary<string, TracesAttribute>>> LazyEnumTypeToEnumNameToTracesAttribute = new Lazy<ConcurrentDictionary<Type, ConcurrentDictionary<string, TracesAttribute>>>();

        private static readonly Lazy<ConcurrentDictionary<Type, ConcurrentDictionary<string, HelpLinkAttribute>>> LazyEnumTypeToEnumNameToHelpLinkAttribute = new Lazy<ConcurrentDictionary<Type, ConcurrentDictionary<string, HelpLinkAttribute>>>(() => new ConcurrentDictionary<Type, ConcurrentDictionary<string, HelpLinkAttribute>>());

        private static readonly Lazy<ConcurrentDictionary<Type, ConcurrentDictionary<string, object>>> LazyEnumTypeToEnumNameToEnumValue = new Lazy<ConcurrentDictionary<Type, ConcurrentDictionary<string, object>>>(() => new ConcurrentDictionary<Type, ConcurrentDictionary<string, object>>());

        private static readonly Lazy<ConcurrentDictionary<Type, ConcurrentDictionary<string, ErrorAttribute>>> LazyEnumTypeToEnumNameToErrorAttribute = new Lazy<ConcurrentDictionary<Type, ConcurrentDictionary<string, ErrorAttribute>>>(() => new ConcurrentDictionary<Type, ConcurrentDictionary<string, ErrorAttribute>>());

        public static object GetValue<TEnum>(this TEnum @enum, Type enumType, string enumName) where TEnum : struct, Enum
        {
            var enumTypeToEnumNameToEnumValue = LazyEnumTypeToEnumNameToEnumValue.Value;

            if (!enumTypeToEnumNameToEnumValue.TryGetValue(enumType, out var enumNameToEnumValue))
            {
                enumNameToEnumValue = new ConcurrentDictionary<string, object>();
                enumTypeToEnumNameToEnumValue[enumType] = enumNameToEnumValue;
            }

            if (enumNameToEnumValue.TryGetValue(enumName, out var enumValue))
            {
                return enumValue;
            }

            enumValue = Convert.ChangeType(@enum, Enum.GetUnderlyingType(enumType));

            enumNameToEnumValue[enumName] = enumValue;

            return enumValue;
        }

        public static string GetTitleFromEnum(Type enumType, string enumName)
        {
            return GetAttributeFromEnum(LazyEnumTypeToEnumNameToTitleAttribute, enumType, enumName)?.Title;
        }

        public static string GetDescriptionFromEnum(Type enumType, string enumName)
        {
            return GetAttributeFromEnum(LazyEnumTypeToEnumNameToDescriptionAttribute, enumType, enumName)?.Description;
        }

        public static string GetTracesFromEnum(Type enumType, string enumName)
        {
            return GetAttributeFromEnum(LazyEnumTypeToEnumNameToTracesAttribute, enumType, enumName)?.Traces;
        }

        public static string GetHelpLinkFromEnum(Type enumType, string enumName)
        {
            return GetAttributeFromEnum(LazyEnumTypeToEnumNameToHelpLinkAttribute, enumType, enumName)?.HelpLink;
        }

        public static ErrorAttribute GetErrorAttributeFromEnum(Type enumType, string enumName)
        {
            return GetAttributeFromEnum(LazyEnumTypeToEnumNameToErrorAttribute, enumType, enumName);
        }

        private static T GetAttributeFromEnum<T>(Lazy<ConcurrentDictionary<Type, ConcurrentDictionary<string, T>>> lazyEnumTypeToEnumNameToT, Type enumType, string enumName) where T : Attribute
        {
            var enumTypeToEnumNameToT = lazyEnumTypeToEnumNameToT.Value;

            if (!enumTypeToEnumNameToT.TryGetValue(enumType, out var enumNameToT))
            {
                enumNameToT = new ConcurrentDictionary<string, T>();
                enumTypeToEnumNameToT[enumType] = enumNameToT;
            }

            if (enumNameToT.TryGetValue(enumName, out var t))
            {
                return t;
            }

            t = enumType.GetField(enumName)?.GetCustomAttribute<T>();

            enumNameToT[enumName] = t;

            return t;
        }
    }
}