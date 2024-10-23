using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace Ogu.Response.Json
{
    public static class Constants
    {
        internal static readonly Type DictionaryType = typeof(IDictionary<string, object>);

        private static readonly Lazy<JsonSerializerOptions> LazyDefaultJsonSerializerOptions =
            new Lazy<JsonSerializerOptions>(() => new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new JsonDictionaryKeyConverter<string>() }
            }, LazyThreadSafetyMode.ExecutionAndPublication);

        public static JsonSerializerOptions DefaultJsonSerializerOptions { get; } = LazyDefaultJsonSerializerOptions.Value;
    }
}