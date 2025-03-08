using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ogu.AspNetCore.Response.Abstractions;
using Ogu.Response.Abstractions;
using Ogu.Response.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ogu.AspNetCore.Response.Json
{
    /// <summary>
    /// Represents a JSON action response that conforms to the <see cref="IActionResponse{TData}"/> interface.
    /// This class encapsulates the data returned from an action, along with metadata about the operation's success, status, and any errors.
    /// </summary>
    /// <typeparam name="TData">The type of data contained in the response.</typeparam>
    public class JsonActionResponse<TData> : IActionResponse<TData>
    {
        private readonly JsonSerializerOptions _serializerOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonActionResponse{TData}"/> class.
        /// </summary>
        /// <param name="data">The data to be returned in the response.</param>
        /// <param name="success">Indicates whether the operation was successful.</param>
        /// <param name="status">The HTTP status code representing the result of the operation.</param>
        /// <param name="extras">Additional metadata related to the response, stored as key-value pairs.</param>
        /// <param name="errors">A list of errors that occurred during the operation, if any.</param>
        [JsonConstructor]
        public JsonActionResponse(TData data, bool success, HttpStatusCode status, Dictionary<string, object> extras, List<JsonError> errors) : this(data, success, status, extras, new List<IError>(errors ?? Enumerable.Empty<JsonError>()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonActionResponse{TData}"/> class.
        /// </summary>
        /// <param name="data">The data to be returned in the response.</param>
        /// <param name="success">Indicates whether the operation was successful.</param>
        /// <param name="status">The HTTP status code representing the result of the operation.</param>
        /// <param name="extras">Additional metadata related to the response, stored as key-value pairs.</param>
        /// <param name="errors">A list of errors that occurred during the operation, if any.</param>
        public JsonActionResponse(TData data, bool success, HttpStatusCode status, IDictionary<string, object> extras, List<IError> errors)
        {
            Data = data;
            Success = success;
            Status = status;
            Errors = errors ?? new List<IError>();
            Extras = extras ?? new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonActionResponse{TData}"/> class from an <see cref="IResponse{TData}"/>.
        /// </summary>
        /// <param name="response">The response object from which to initialize the <see cref="JsonActionResponse{TData}"/>.</param>
        public JsonActionResponse(IResponse<TData> response)
        {
            Data = response.Data;
            Success = response.Success;
            Status = response.Status;
            Errors = response.Errors ?? new List<IError>();
            Extras = response.Extras ?? new Dictionary<string, object>();
            _serializerOptions = response is JsonResponse<TData> jsonResponse ? jsonResponse.SerializerOptions : null;
            SerializedResponse = response.SerializedResponse;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonActionResponse{TData}"/> class from an <see cref="IJsonResponse{TData}"/>.
        /// </summary>
        /// <param name="response">The JSON response object from which to initialize the <see cref="JsonActionResponse{TData}"/>.</param>
        public JsonActionResponse(IJsonResponse<TData> response)
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
        public TData Data { get; }

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
    }
}