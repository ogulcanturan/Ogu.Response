using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public class JsonDictionaryKeyConverter<TKey> : JsonConverter<IDictionary<TKey, object>>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(IDictionary<string, object>).IsAssignableFrom(typeToConvert);
        }

        public override void Write(Utf8JsonWriter writer, IDictionary<TKey, object> dictionary, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach (var entry in dictionary)
            {
                var key = ConvertKeyToString(entry.Key, options);
                writer.WritePropertyName(key);
                JsonSerializer.Serialize(writer, entry.Value, options);
            }

            writer.WriteEndObject();
        }

        private static string ConvertKeyToString(TKey key, JsonSerializerOptions options)
        {
            if (key == null)
            {
                return null;
            }

            if (options.PropertyNamingPolicy == null)
            {
                return key.ToString();
            }

            var propertyName = options.PropertyNamingPolicy.ConvertName(key.ToString());

            return propertyName;
        }

        private static TKey ConvertStringToKey(string propertyName, JsonSerializerOptions options)
        {
            if (propertyName == null)
            {
                return default;
            }

            if (options.PropertyNamingPolicy != null)
            {
                propertyName = options.PropertyNamingPolicy.ConvertName(propertyName);
            }

            return (TKey)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(propertyName);
        }

        public override IDictionary<TKey, object> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token");
            }

            var dictionary = new Dictionary<TKey, object>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return dictionary;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected PropertyName token");
                }

                var propertyName = reader.GetString();
                var key = ConvertStringToKey(propertyName, options);
                reader.Read();

                var value = JsonSerializer.Deserialize<object>(ref reader, options);
                dictionary[key] = value;
            }

            throw new JsonException("Unexpected end of JSON while reading dictionary");
        }
    }
}