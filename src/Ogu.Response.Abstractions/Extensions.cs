using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Ogu.Response.Abstractions
{
    public static class Extensions
    {
        private static readonly Lazy<Dictionary<Type, Dictionary<string, TitleAttribute>>> LazyEnumTypeToEnumNameToTitleAttribute = new Lazy<Dictionary<Type, Dictionary<string, TitleAttribute>>>(() => new Dictionary<Type, Dictionary<string, TitleAttribute>>());

        private static readonly Lazy<Dictionary<Type, Dictionary<string, DescriptionAttribute>>> LazyEnumTypeToEnumNameToDescriptionAttribute = new Lazy<Dictionary<Type, Dictionary<string, DescriptionAttribute>>>(() => new Dictionary<Type, Dictionary<string, DescriptionAttribute>>());

        private static readonly Lazy<Dictionary<Type, Dictionary<string, HelpLinkAttribute>>> LazyEnumTypeToEnumNameToHelpLinkAttribute = new Lazy<Dictionary<Type, Dictionary<string, HelpLinkAttribute>>>(() => new Dictionary<Type, Dictionary<string, HelpLinkAttribute>>());

        private static readonly Lazy<Dictionary<Type, Dictionary<string, object>>> LazyEnumTypeToEnumNameToEnumValue = new Lazy<Dictionary<Type, Dictionary<string, object>>>(() => new Dictionary<Type, Dictionary<string, object>>());

        private static readonly Lazy<Dictionary<Type, Dictionary<string, ErrorAttribute>>> LazyEnumTypeToEnumNameToErrorAttribute = new Lazy<Dictionary<Type, Dictionary<string, ErrorAttribute>>>(() => new Dictionary<Type, Dictionary<string, ErrorAttribute>>());

        public static object GetValue<TEnum>(this TEnum @enum, Type enumType, string enumName) where TEnum : struct, Enum
        {
            var enumTypeToEnumNameToEnumValue = LazyEnumTypeToEnumNameToEnumValue.Value;

            if (!enumTypeToEnumNameToEnumValue.TryGetValue(enumType, out var enumNameToEnumValue))
            {
                enumNameToEnumValue = new Dictionary<string, object>();
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

        public static string GetHelpLinkFromEnum(Type enumType, string enumName)
        {
            return GetAttributeFromEnum(LazyEnumTypeToEnumNameToHelpLinkAttribute, enumType, enumName)?.HelpLink;
        }

        public static ErrorAttribute GetErrorAttributeFromEnum(Type enumType, string enumName)
        {
            return GetAttributeFromEnum(LazyEnumTypeToEnumNameToErrorAttribute, enumType, enumName);
        }

        private static T GetAttributeFromEnum<T>(Lazy<Dictionary<Type, Dictionary<string, T>>> lazyEnumTypeToEnumNameToT, Type enumType, string enumName) where T : Attribute
        {
            var enumTypeToEnumNameToT = lazyEnumTypeToEnumNameToT.Value;

            if (!enumTypeToEnumNameToT.TryGetValue(enumType, out var enumNameToT))
            {
                enumNameToT = new Dictionary<string, T>();
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