using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public class JsonResponseConverter : JsonConverter<JsonResponse>
    {
        public override JsonResponse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                var model = Convert<object>(jsonDocument.RootElement, options);

                return new JsonResponse(model.data, model.success, model.status, model.extras, model.errors, model.serializedResponse, null);
            }
        }

        public override void Write(Utf8JsonWriter writer, JsonResponse value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        internal static (T data, bool success, HttpStatusCode status, Dictionary<string, object> extras, List<IError> errors, object serializedResponse) Convert<T>(JsonElement raw, JsonSerializerOptions options)
        {
            T data = default;
            object serializedResponse = null;
            var success = false;
            HttpStatusCode status = default;
            Dictionary<string, object> extras = null;
            List<IError> errors = null;

            foreach (var obj in raw.EnumerateObject())
            {
                switch (obj.Name.ToLower())
                {
                    case "data":
                        data = obj.Value.ValueKind != JsonValueKind.Undefined ||
                                obj.Value.ValueKind != JsonValueKind.Null
                            ? obj.Value.Deserialize<T>(options)
                            : default;
                        break;
                    case "success":
                        success = obj.Value.ValueKind == JsonValueKind.True;
                        break;
                    case "status":
                        status = (HttpStatusCode)obj.Value.GetInt32();
                        break;
                    case "extras":
                        extras = obj.Value.Deserialize<Dictionary<string, object>>(options);
                        break;
                    case "serializedresponse":
                        serializedResponse = obj.Value.GetString();
                        break;
                    case "errors":

                        string title = null, description = null, traces = null, code = null, helpLink = null;
                        ErrorType type = default;
                        var validationFailures = new List<IValidationFailure>();
                        errors = new List<IError>();

                        foreach (var errorJsonElement in obj.Value.EnumerateArray().SelectMany(errorObjects => errorObjects.EnumerateObject()))
                        {
                            switch (errorJsonElement.Name.ToLower())
                            {
                                case "title":

                                    title = errorJsonElement.Value.GetString();

                                    break;

                                case "description":

                                    description = errorJsonElement.Value.GetString();

                                    break;

                                case "traces":

                                    traces = errorJsonElement.Value.GetString();

                                    break;

                                case "code":

                                    code = errorJsonElement.Value.GetString();

                                    break;

                                case "helplink":

                                    helpLink = errorJsonElement.Value.GetString();

                                    break;

                                case "failures":

                                    foreach (var validationFailureJsonElement in errorJsonElement.Value.EnumerateArray()
                                                 .SelectMany(validationFailureObjects =>
                                                     validationFailureObjects.EnumerateObject()))
                                    {
                                        string propertyName = null, message = null, failureCode = null;
                                        object attemptedValue = null;
                                        Severity severity = default;


                                        switch (validationFailureJsonElement.Name.ToLower())
                                        {
                                            case "propertyname":

                                                propertyName = validationFailureJsonElement.Value.GetString();

                                                break;

                                            case "message":

                                                message = validationFailureJsonElement.Value.GetString();

                                                break;

                                            case "attemptedvalue":

                                                attemptedValue = validationFailureJsonElement.Value;

                                                break;

                                            case "severity":

                                                severity = (Severity)errorJsonElement.Value.GetInt32();

                                                break;

                                            case "code":

                                                failureCode = validationFailureJsonElement.Value.GetString();

                                                break;
                                        }

                                        validationFailures.Add(new JsonValidationFailure(propertyName, message,
                                            attemptedValue, severity, failureCode));
                                    }

                                    break;

                                case "type":

                                    type = (ErrorType)errorJsonElement.Value.GetInt32();

                                    break;
                            }
                        }

                        errors.Add(new JsonError(title, description, traces, code, helpLink, validationFailures, type));

                        break;
                }
            }

            return (data, success, status, extras, errors, serializedResponse);
        }
    }
}
