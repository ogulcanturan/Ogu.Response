using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ogu.AspNetCore.Response.Abstractions;
using Ogu.Response.Abstractions;
using Ogu.Response.Json;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ogu.AspNetCore.Response.Json
{
    /// <summary>
    /// Represents a JSON action response that conforms to the <see cref="IActionResponse"/> interface.
    /// This class encapsulates the data returned from an action, along with metadata about the operation's success, status, and any errors.
    /// </summary>
    public class JsonActionResponse : IActionResponse
    {
        private readonly JsonSerializerOptions _serializerOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonActionResponse"/> class.
        /// </summary>
        /// <param name="data">The data to be returned in the response.</param>
        /// <param name="success">Indicates whether the operation was successful.</param>
        /// <param name="status">The HTTP status code representing the result of the operation.</param>
        /// <param name="extras">Additional metadata related to the response, stored as key-value pairs.</param>
        /// <param name="errors">A list of errors that occurred during the operation, if any.</param>
        public JsonActionResponse(object data, bool success, HttpStatusCode status, IDictionary<string, object> extras, List<IError> errors)
        {
            Data = data;
            Success = success;
            Status = status;
            Errors = errors ?? new List<IError>();
            Extras = extras ?? new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonActionResponse"/> class from an <see cref="IResponse"/>.
        /// </summary>
        /// <param name="response">The response object from which to initialize the <see cref="JsonActionResponse"/>.</param>
        public JsonActionResponse(IResponse response)
        {
            Data = response.Data;
            Status = response.Status;
            Success = response.Success;
            Errors = response.Errors ?? new List<IError>();
            Extras = response.Extras ?? new Dictionary<string, object>();
            SerializedResponse = response.SerializedResponse;
            _serializerOptions = response is IResponse<object> responseObject && responseObject is IJsonResponse<object> jsonResponse ? jsonResponse.SerializerOptions : null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonActionResponse"/> class from an <see cref="IJsonResponse"/>.
        /// </summary>
        /// <param name="response">The JSON response object from which to initialize the <see cref="JsonActionResponse"/>.</param>
        public JsonActionResponse(IJsonResponse response)
        {
            Data = response.Data;
            Status = response.Status;
            Success = response.Success;
            Errors = response.Errors ?? new List<IError>();
            Extras = response.Extras ?? new Dictionary<string, object>();
            _serializerOptions = response.SerializerOptions;
            SerializedResponse = response.SerializedResponse;
        }

        public bool Success { get; }

        public HttpStatusCode Status { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public object Data { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<IError> Errors { get; internal set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, object> Extras { get; internal set; }

        [JsonIgnore]
        public object SerializedResponse { get; set; }

        public Task ExecuteResultAsync(HttpContext context)
        {
            return context.ExecuteJsonResponseAsync(this, SerializedResponse, Status, _serializerOptions);
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return context.ExecuteJsonResponseAsync(this, SerializedResponse, Status, _serializerOptions);
        }

        public static implicit operator JsonResponse(JsonActionResponse response)
        {
            return new JsonResponse(response.Data,  response.Success, response.Status, response.Extras, response.Errors,
                response.SerializedResponse, response._serializerOptions);
        }
    }
}