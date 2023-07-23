using System;
using System.ComponentModel;
using System.Reflection;

namespace Ogu.AspNetCore.Response
{
    public class Extensions
    {
        public static object GetValue<TEnum>(TEnum enumValue) where TEnum : struct, Enum
            => Convert.ChangeType(enumValue, Enum.GetUnderlyingType(typeof(TEnum)));

        public static string GetDescription<TEnum>(TEnum @enum)
            => @enum.GetType()
                .GetField(@enum.ToString())?
                .GetCustomAttribute<DescriptionAttribute>()?.Description;
    }
}