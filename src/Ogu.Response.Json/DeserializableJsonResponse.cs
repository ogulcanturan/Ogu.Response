using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ogu.Response.Json
{
    public class DeserializableJsonResponse
    {
        private static JsonSerializerOptions DefaultJsonSerializerOptions { get; } = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        [JsonExtensionData]
        public Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>(comparer: StringComparer.OrdinalIgnoreCase);

        public IJsonResponse ToJsonResponse()
        {
            var response = GetResponse();

            return new JsonResponse(response.Data, response.Success, response.Status, response.Extras, response.Errors, response.SerializedResponse, null);
        }

        public IJsonResponse<T> ToJsonResponse<T>(JsonSerializerOptions serializerOptions = null)
        {
            var response = GetResponse();

            var data = response.Data.ValueKind == JsonValueKind.Undefined ? default : response.Data.Deserialize<T>(serializerOptions ?? DefaultJsonSerializerOptions);

            return new JsonResponse<T>(data, response.Success, response.Status, response.Extras, response.Errors, response.SerializedResponse, null);
        }

        private (JsonElement Data, bool Success, HttpStatusCode Status, Dictionary<string, object> Extras, object SerializedResponse, List<IError> Errors) GetResponse()
        {
            JsonElement data = default;
            object serializedResponse = default;
            bool success = default;
            HttpStatusCode status = default;
            var extras = new Dictionary<string, object>();
            var errors = new List<IError>();

            if (AdditionalData.TryGetValue("data", out var dataObj) && dataObj is JsonElement dataJsonElement)
            {
                data = dataJsonElement;
            }

            if (AdditionalData.TryGetValue("success", out var successObj) && successObj is JsonElement successJsonElement)
            {
                success = successJsonElement.ValueKind == JsonValueKind.True;
            }

            if (AdditionalData.TryGetValue("status", out var statusObj) && statusObj is JsonElement statusJsonElement)
            {
                status = (HttpStatusCode)statusJsonElement.GetInt32();
            }

            if (AdditionalData.TryGetValue("extras", out var extrasObj) && extrasObj is JsonElement extrasJsonElement)
            {
                extras = extrasJsonElement.Deserialize<Dictionary<string, object>>();
            }

            if (AdditionalData.TryGetValue("serializedresponse", out var serializedResponseObj) && serializedResponseObj is JsonElement serializedResponseJsonElement)
            {
                serializedResponse = serializedResponseJsonElement.GetString();
            }

            if (AdditionalData.TryGetValue("errors", out var errorsObj) && errorsObj is JsonElement errorsJsonElement)
            {
                string title = null, description = null, traces = null, code = null, helpLink = null;
                ErrorType type = default;
                var validationFailures = new List<IValidationFailure>();

                foreach (var errorJsonElement in errorsJsonElement.EnumerateArray().SelectMany(errorObjects => errorObjects.EnumerateObject()))
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
                                object attemptedValue = default;
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

                                validationFailures.Add(new JsonValidationFailure(propertyName, message, attemptedValue, severity, failureCode));
                            }

                            break;

                        case "type":

                            type = (ErrorType)errorJsonElement.Value.GetInt32();

                            break;
                    }
                }

                errors.Add(new JsonError(title, description, traces, code, helpLink, validationFailures, type));
            }

            return (data, success, status, extras, serializedResponse, errors);
        }
    }
}