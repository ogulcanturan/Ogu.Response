using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Task ExecuteResultAsync(HttpContext context) => Response.ExecuteResponseAsync(context, this, SerializedResponse, Status, _serializerOptions);

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
            new Response<T>(data, Json.Result.ValidationFailure(validationFailures), status, false, serializerOptions);

        public static Response<T> Failure(int status, ModelStateDictionary modelState, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new Response<T>(data, Json.Result.ValidationFailure(ValidationFailure.ToValidationFailures(modelState)), status, false, serializerOptions);

        public static Response<T> Failure<TEnum>(int status, TEnum @enum, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            new Response<T>(data, Json.Result.CustomFailure(@enum), status, false, serializerOptions);

        public static Response<T> Failure<TEnum>(int status, TEnum[] @enums, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            new Response<T>(data, Json.Result.CustomFailure(@enums), status, false, serializerOptions);

        public static Response<T> Failure<TEnum>(int status, IList<TEnum> @enums, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            new Response<T>(data, Json.Result.CustomFailure(@enums), status, false, serializerOptions);

        public static Response<T> Failure<TEnum>(int status, TEnum? @enum, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            new Response<T>(data, @enum.HasValue ? Json.Result.CustomFailure(@enum.Value) : null, status, false, serializerOptions);

        public static Response<T> Failure<TEnum>(int status, TEnum?[] @enums, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            var enumArray = @enums.Where(e => e.HasValue).Select(e => e.Value).ToArray();
            return new Response<T>(data, enumArray.Length > 0 ? Json.Result.CustomFailure(enumArray) : null, status, false, serializerOptions);
        }

        public static Response<T> Failure<TEnum>(int status, IList<TEnum?> @enums, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            var enumArray = @enums.Where(e => e.HasValue).Select(e => e.Value).ToArray();
            return new Response<T>(data, enumArray.Length > 0 ? Json.Result.CustomFailure(enumArray) : null, status, false, serializerOptions);
        }

        public static Response<T> Failure(int status, IError error, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new Response<T>(data, Json.Result.CustomFailure(error), status, false, serializerOptions);

        public static Response<T> Failure(int status, IError[] errors, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new Response<T>(data, Json.Result.CustomFailure(errors), status, false, serializerOptions);

        public static Response<T> Failure(int status, IList<IError> errors, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new Response<T>(data, Json.Result.CustomFailure(errors), status, false, serializerOptions);

        public static Response<T> Failure(int status, Exception exception, bool includeTraces = false, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new Response<T>(data, Json.Result.ExceptionFailure(exception, includeTraces), status, false, serializerOptions);

        public static Response<T> Failure(int status, Exception[] exceptions, bool includeTraces = false, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new Response<T>(data, Json.Result.ExceptionFailure(exceptions, includeTraces), status, false, serializerOptions);

        public static Response<T> Failure(int status, IList<Exception> exceptions, bool includeTraces = false, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new Response<T>(data, Json.Result.ExceptionFailure(exceptions, includeTraces), status, false, serializerOptions);

        public static Response<T> Failure(int status, string error, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new Response<T>(data, Json.Result.CustomFailure(error), status, false, serializerOptions);

        public static Response<T> Failure(int status, string[] errors, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new Response<T>(data, Json.Result.CustomFailure(errors), status, false, serializerOptions);

        public static Response<T> Failure(int status, IList<string> errors, T data = default,
            JsonSerializerOptions serializerOptions = null)
            => new Response<T>(data, Json.Result.CustomFailure(errors), status, false, serializerOptions);
    }
}