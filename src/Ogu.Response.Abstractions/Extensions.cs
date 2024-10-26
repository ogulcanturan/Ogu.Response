using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Ogu.Response.Abstractions
{
    public static class Extensions
    {
        private static readonly Lazy<Dictionary<Type, Dictionary<string, string>>> LazyTitleCache = new Lazy<Dictionary<Type, Dictionary<string, string>>>(() => new Dictionary<Type, Dictionary<string, string>>());

        private static readonly Lazy<Dictionary<Type, Dictionary<string, string>>> LazyDescriptionCache = new Lazy<Dictionary<Type, Dictionary<string, string>>>(() => new Dictionary<Type, Dictionary<string, string>>());

        private static readonly Lazy<Dictionary<Type, Dictionary<string, string>>> LazyHelpLinkCache = new Lazy<Dictionary<Type, Dictionary<string, string>>>(() => new Dictionary<Type, Dictionary<string, string>>());

        private static readonly Lazy<Dictionary<Type, Dictionary<string, object>>> LazyEnumTypeToEnumNameToEnumValue = new Lazy<Dictionary<Type, Dictionary<string, object>>>(() => new Dictionary<Type, Dictionary<string, object>>());

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
            var titleCache = LazyTitleCache.Value;

            if (!titleCache.TryGetValue(enumType, out var enumNameToTitle))
            {
                enumNameToTitle = new Dictionary<string, string>();
                titleCache[enumType] = enumNameToTitle;
            }

            if (enumNameToTitle.TryGetValue(enumName, out var title))
            {
                return title;
            }

            title = enumType.GetField(enumName)?.GetCustomAttribute<TitleAttribute>()?.Title;

            enumNameToTitle[enumName] = title;

            return title;
        }

        public static string GetDescriptionFromEnum(Type enumType, string enumName)
        {
            var descriptionCache = LazyDescriptionCache.Value;

            if(!descriptionCache.TryGetValue(enumType, out var enumNameToDescription))
            {
                enumNameToDescription = new Dictionary<string, string>();
                descriptionCache[enumType] = enumNameToDescription;
            }

            if (enumNameToDescription.TryGetValue(enumName, out var description))
            {
                return description;
            }

            description = enumType.GetField(enumName)?.GetCustomAttribute<DescriptionAttribute>()?.Description;

            enumNameToDescription[enumName] = description;

            return description;
        }

        public static string GetHelpLinkFromEnum(Type enumType, string enumName)
        {
            var helpLinkCache = LazyHelpLinkCache.Value;

            if (!helpLinkCache.TryGetValue(enumType, out var enumNameToHelpLink))
            {
                enumNameToHelpLink = new Dictionary<string, string>();
                helpLinkCache[enumType] = enumNameToHelpLink;
            }

            if (enumNameToHelpLink.TryGetValue(enumName, out var helpLink))
            {
                return helpLink;
            }

            helpLink = enumType.GetField(enumName)?.GetCustomAttribute<HelpLinkAttribute>()?.HelpLink;

            enumNameToHelpLink[enumName] = helpLink;

            return helpLink;
        }
    }
}