using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace Ogu.AspNetCore.Response.Json
{
    public static class ResponseJsonExtensions
    {
        public static IResponse ToOtherResponse(this HttpStatusCode status, object data, bool success, IResult result = null,
            JsonSerializerOptions serializerOptions = null) 
            => Response.Other(data, (int)status, success, result, serializerOptions);

        public static IResponse ToSuccessResponse(this HttpStatusCode status, object data = null, IResult result = null,
            JsonSerializerOptions serializerOptions = null) 
            => Response.Successful(data, (int)status, result, serializerOptions);

        public static IResponse ToFailResponse(this HttpStatusCode status, IResult result = null, object data = null,
            JsonSerializerOptions serializerOptions = null) 
            => Response.Failure((int)status, result, data, serializerOptions);

        public static IResponse ToFailResponse(this HttpStatusCode status, IValidationFailure validationFailure, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => Response.Failure((int)status, validationFailure, data, serializerOptions);

        public static IResponse ToFailResponse(this HttpStatusCode status, IValidationFailure[] validationFailures, object data = null,
            JsonSerializerOptions serializerOptions = null) 
            => Response.Failure((int)status, validationFailures, data, serializerOptions);

        public static IResponse ToFailResponse(this HttpStatusCode status, ModelStateDictionary modelState, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => Response.Failure((int)status, modelState, data, serializerOptions);

        public static IResponse ToFailResponse<TEnum>(this HttpStatusCode status, TEnum @enum, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum 
            => Response.Failure((int)status, @enum, data, serializerOptions);

        public static IResponse ToFailResponse<TEnum>(this HttpStatusCode status, TEnum[] @enums, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => Response.Failure((int)status, @enums, data, serializerOptions);

        public static IResponse ToFailResponse<TEnum>(this HttpStatusCode status, IList<TEnum> @enums, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => Response.Failure((int)status, @enums, data, serializerOptions);

        public static IResponse ToFailResponse<TEnum>(this HttpStatusCode status, TEnum? @enum, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => Response.Failure((int)status, @enum, data, serializerOptions);

        public static IResponse ToFailResponse<TEnum>(this HttpStatusCode status, TEnum?[] @enums, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => Response.Failure((int)status, @enums, data, serializerOptions);

        public static IResponse ToFailResponse<TEnum>(this HttpStatusCode status, IList<TEnum?> @enums, object data = null,
            JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
            => Response.Failure((int)status, @enums, data, serializerOptions);

        public static IResponse ToFailResponse(this HttpStatusCode status, IError error, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => Response.Failure((int)status, error, data, serializerOptions);

        public static IResponse ToFailResponse(this HttpStatusCode status, IError[] errors, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => Response.Failure((int)status, errors, data, serializerOptions);

        public static IResponse ToFailResponse(this HttpStatusCode status, IList<IError> errors, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => Response.Failure((int)status, errors, data, serializerOptions);

        public static IResponse ToFailResponse(this HttpStatusCode status, Exception exception, bool includeTraces = false, 
            object data = null, JsonSerializerOptions serializerOptions = null)
            => Response.Failure((int)status, exception, includeTraces, data, serializerOptions);

        public static IResponse ToFailResponse(this HttpStatusCode status, Exception[] exceptions, bool includeTraces = false,
            object data = null, JsonSerializerOptions serializerOptions = null)
            => Response.Failure((int)status, exceptions, includeTraces, data, serializerOptions);

        public static IResponse ToFailResponse(this HttpStatusCode status, IList<Exception> exceptions, bool includeTraces = false,
            object data = null, JsonSerializerOptions serializerOptions = null)
            => Response.Failure((int)status, exceptions, includeTraces, data, serializerOptions);

        public static IResponse ToFailResponse(this HttpStatusCode status, string error, object data = null,
            JsonSerializerOptions serializerOptions = null)
            => Response.Failure((int)status, error, data, serializerOptions);
    }
}