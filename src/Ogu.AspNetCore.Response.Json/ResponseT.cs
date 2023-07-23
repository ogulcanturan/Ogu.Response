using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ogu.AspNetCore.Response.Json
{
    public class Response<T> : IResponse<T>
    {
        private readonly JsonSerializerOptions _serializerOptions;

        public Response(T data, IResult result, int statusCode, bool success, JsonSerializerOptions serializerOptions = null, string serializedResponse = null)
        {
            Data = data;
            Result = result;
            Status = statusCode;
            Success = success;
            _serializerOptions = serializerOptions ?? Response.DefaultJsonSerializerOptions;
            SerializedResponse = serializedResponse;
        }

        public Response(T data, int status, bool success, JsonSerializerOptions serializerOptions = null) : this(data, null, status, success, serializerOptions) { }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T Data { get; set; }

        public bool Success { get; set; }

        public int Status { get; set; }

        [JsonIgnore]
        public string SerializedResponse { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IResult Result { get; set; }

        public Task ExecuteResultAsync(ActionContext context) => Response.ExecuteResponseAsync(context, this, SerializedResponse, Status, _serializerOptions);

        public static implicit operator Response(Response<T> response) => new Response(response.Data, response.Result, response.Status, response.Success, response._serializerOptions, response.SerializedResponse);

        public static Response<T> Other(T data, int status, bool success, IResult result = null,
            JsonSerializerOptions serializerOptions = null) =>
            new Response<T>(data, result, status, success, serializerOptions);

        public static Response<T> Successful(T data, int status, IResult result = null,
            JsonSerializerOptions serializerOptions = null) =>
            new Response<T>(data, result, status, true, serializerOptions);

        public static Response<T> Failure(int status, IResult result = null, T data = default,
            JsonSerializerOptions serializerOptions = null) =>
            new Response<T>(data, result, status, false, serializerOptions);

        public static Response<T> Failure(int status, IValidationFailure[] validationFailures, T data = default,
            JsonSerializerOptions serializerOptions = null) =>
            new Response<T>(data, Ogu.AspNetCore.Response.Json.Result.ValidationFailure(validationFailures), status, false, serializerOptions);

        public static Response<T> Failure<TEnum>(int status, TEnum @enum, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            new Response<T>(data, Ogu.AspNetCore.Response.Json.Result.BasicFailure(@enum), status, false, serializerOptions);
    }
}