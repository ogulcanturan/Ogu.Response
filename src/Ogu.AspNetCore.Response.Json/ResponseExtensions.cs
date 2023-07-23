using System;
using System.Net;
using System.Text.Json;

namespace Ogu.AspNetCore.Response.Json
{
    public static class ResponseExtensions
    {
        public static IResponse ToOtherResponse(this HttpStatusCode status, object data, bool success,
            IResult result = null,
            JsonSerializerOptions serializerOptions = null) =>
            Response.Other(data, (int)status, success, result, serializerOptions);

        public static IResponse ToSuccessResponse(this HttpStatusCode status, object data, IResult result = null,
            JsonSerializerOptions serializerOptions = null) =>
            Response.Successful(data, (int)status, result, serializerOptions);

        public static IResponse ToFailResponse(this HttpStatusCode status, IResult result = null, object data = null,
            JsonSerializerOptions serializerOptions = null) =>
            Response.Failure((int)status,result, data, serializerOptions);

        public static IResponse ToFailResponse(this HttpStatusCode status, IValidationFailure[] validationFailures,
            object data = null,
            JsonSerializerOptions serializerOptions = null) =>
            Response.Failure((int)status, validationFailures, data, serializerOptions);

        public static IResponse ToFailResponse<TEnum>(this HttpStatusCode status, TEnum @enum, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum =>
            Response.Failure((int)status, @enum, data, serializerOptions);
    }
}