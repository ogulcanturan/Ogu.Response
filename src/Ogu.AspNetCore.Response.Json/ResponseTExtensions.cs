using System;
using System.Net;
using System.Text.Json;

namespace Ogu.AspNetCore.Response.Json
{
    public static class ResponseTExtensions
    {
        public static IResponse<T> ToOtherResponseT<T>(this HttpStatusCode status, T data, bool success,
            IResult result = null,
            JsonSerializerOptions serializerOptions = null) =>
            Response<T>.Other(data, (int)status, success, result, serializerOptions);

        public static IResponse<T> ToSuccessResponseT<T>(this HttpStatusCode status, T data, IResult result = null,
            JsonSerializerOptions serializerOptions = null) =>
            Response<T>.Successful(data, (int)status, result, serializerOptions);

        public static IResponse<T> ToFailResponseT<T>(this HttpStatusCode status, IResult result = null, T data = default,
            JsonSerializerOptions serializerOptions = null) =>
            Response<T>.Failure((int)status, result, data, serializerOptions);

        public static IResponse<T> ToFailResponseT<T>(this HttpStatusCode status, IValidationFailure[] validationFailures,
            T data = default,
            JsonSerializerOptions serializerOptions = null) =>
            Response<T>.Failure((int)status, validationFailures, data, serializerOptions);

        public static IResponse<T> ToFailResponseT<T, TEnum>(this HttpStatusCode status, TEnum @enum, T data = default,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            Response<T>.Failure((int)status, @enum, data, serializerOptions);
    }
}