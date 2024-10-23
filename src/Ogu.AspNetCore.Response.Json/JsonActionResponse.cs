﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ogu.AspNetCore.Response.Abstractions;
using Ogu.Response.Abstractions;
using Ogu.Response.Json;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Constants = Ogu.Response.Json.Constants;

namespace Ogu.AspNetCore.Response.Json
{
    public class JsonActionResponse : IActionResponse
    {
        private readonly JsonSerializerOptions _serializerOptions;

        [JsonConstructor]
        public JsonActionResponse(object data, bool success, HttpStatusCode statusCode, IDictionary<string, object> extensions, List<IResponseError> errors)
        {
            Data = data;
            Success = success;
            StatusCode = statusCode;
            Errors = errors ?? new List<IResponseError>();
            Extensions = extensions ?? new Dictionary<string, object>();
        }

        public JsonActionResponse(IResponse<string> response)
        {
            Data = response.Data;
            StatusCode = response.StatusCode;
            Success = response.Success;
            Errors = response.Errors;
            Extensions = response.Extensions;
            _serializerOptions = response is JsonResponse jsonResponse ? jsonResponse.SerializerOptions : Constants.DefaultJsonSerializerOptions;
            SerializedResponse = response.SerializedResponse;
        }

        public JsonActionResponse(IJsonResponse response)
        {
            Data = response.Data;
            StatusCode = response.StatusCode;
            Success = response.Success;
            Errors = response.Errors ?? new List<IResponseError>();
            Extensions = response.Extensions ?? new Dictionary<string, object>();
            _serializerOptions = response.SerializerOptions;
            SerializedResponse = response.SerializedResponse;
        }

        public bool Success { get; }

        public HttpStatusCode StatusCode { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public object Data { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<IResponseError> Errors { get; internal set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, object> Extensions { get; internal set; }

        [JsonIgnore]
        public string SerializedResponse { get; set; }

        public Task ExecuteResultAsync(HttpContext context)
        {
            return context.ExecuteJsonResponseAsync(this, SerializedResponse, StatusCode, _serializerOptions);
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return context.ExecuteJsonResponseAsync(this, SerializedResponse, StatusCode, _serializerOptions);
        }

        public static implicit operator JsonResponse(JsonActionResponse response)
        {
            return new JsonResponse(response.Data,  response.Success, response.StatusCode, response.Extensions, response.Errors,
                response.SerializedResponse, response._serializerOptions);
        }
    }
}