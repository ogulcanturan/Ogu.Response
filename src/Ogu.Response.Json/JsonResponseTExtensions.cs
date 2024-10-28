using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace Ogu.Response.Json
{
    public static class JsonResponseTExtensions
    {
        /// <summary>
        ///     Converts an <see cref="IJsonResponse"/> to a strongly typed <see cref="IJsonResponse{TData}"/> response.
        /// </summary>
        /// <typeparam name="TData">The target type of the response's data.</typeparam>
        /// <param name="jsonResponse">The <see cref="IJsonResponse"/> to convert.</param>
        /// <param name="serializerOptions">Optional. Custom Json serialization options for the response.</param>
        /// <returns>
        ///     A new <see cref="IJsonResponse{TData}"/> instance with data cast to the specified type <typeparamref name="TData"/>,
        ///     including the original response's status, extras, errors, and serialization settings.
        /// </returns>
        /// <remarks>
        ///     If the response is successful and data in <paramref name="jsonResponse"/> cannot be cast to the specified type <typeparamref name="TData"/>,
        ///     an <see cref="InvalidCastException"/> or <see cref="InvalidOperationException"/> may be thrown.
        /// </remarks>
        /// <exception cref="InvalidCastException">
        ///     Thrown if the data in <paramref name="jsonResponse"/> is not compatible with <typeparamref name="TData"/>.
        /// </exception>
        public static IJsonResponse<TData> ToJsonResponseOf<TData>(this IJsonResponse jsonResponse, JsonSerializerOptions serializerOptions = null)
        {
            return jsonResponse.Success
                ? new JsonResponse<TData>((TData)jsonResponse.Data, true, jsonResponse.Status, jsonResponse.Extras, jsonResponse.Errors, jsonResponse.SerializedResponse, serializerOptions ?? jsonResponse.SerializerOptions)
                : new JsonResponse<TData>(default, false, jsonResponse.Status, jsonResponse.Extras, jsonResponse.Errors, jsonResponse.SerializedResponse, serializerOptions ?? jsonResponse.SerializerOptions);
        }

        public static IJsonResponse<TData> ToSuccessJsonResponseOf<TData>(this HttpStatusCode status, TData data, JsonSerializerOptions serializerOptions = null)
        {
            return new JsonResponse<TData>(data, success: true, status, null, null, null, serializerOptions);
        }

        public static IJsonResponse<TData> ToSuccessJsonResponse<TData>(this HttpStatusCode status, JsonSerializerOptions serializerOptions = null)
        {
            return new JsonResponse<TData>(data: default, true, status, null, null, null, serializerOptions);
        }

        public static IJsonResponse<TData> ToFailureJsonResponse<TData>(this HttpStatusCode status, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<TData>.Failure(status, null, serializerOptions);
        }

        public static IJsonResponse<TData> ToFailureJsonResponse<TData>(this HttpStatusCode status, IError error, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<TData>.Failure(status, new List<IError> { error }, serializerOptions);
        }

        public static IJsonResponse<TData> ToFailureJsonResponse<TData>(this HttpStatusCode status, List<IError> errors, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<TData>.Failure(status, errors, serializerOptions);
        }

        public static IJsonResponse<TData> ToFailureJsonResponse<TData>(this HttpStatusCode status, IValidationFailure failure, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<TData>.Failure(status, new List<IError> { new JsonError(failure) }, serializerOptions);
        }

        public static IJsonResponse<TData> ToFailureJsonResponse<TData>(this HttpStatusCode status, List<IValidationFailure> failures, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<TData>.Failure(status, new List<IError> { new JsonError(failures) }, serializerOptions);
        }

        public static IJsonResponse<TData> ToFailureJsonResponse<TData, TEnum>(this HttpStatusCode status, TEnum @enum, JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            return JsonResponse<TData>.Failure(status, new List<IError> { @enum.ToJsonError() }, serializerOptions);
        }

        public static IJsonResponse<TData> ToFailureJsonResponse<TData, TEnum>(this HttpStatusCode status, TEnum[] enums, JsonSerializerOptions serializerOptions = null) where TEnum : struct, Enum
        {
            return JsonResponse<TData>.Failure(status, enums?.Select(e => e.ToJsonError()).ToList(), serializerOptions);
        }

        public static IJsonResponse<TData> ToFailureJsonResponse<TData>(this HttpStatusCode status, Exception exception, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<TData>.Failure(status, new List<IError> { exception.ToJsonError(includeTraces) }, serializerOptions);
        }

        public static IJsonResponse<TData> ToFailureJsonResponse<TData>(this HttpStatusCode status, Exception[] exceptions, bool includeTraces = false, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<TData>.Failure(status, exceptions.Select(e => e.ToJsonError(includeTraces)).ToList(), serializerOptions);
        }

        public static IJsonResponse<TData> ToFailureJsonResponse<TData>(this HttpStatusCode status, string error, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<TData>.Failure(status, new List<IError> { new JsonError(error) }, serializerOptions);
        }

        public static IJsonResponse<TData> ToFailureJsonResponse<TData>(this HttpStatusCode status, string title, string description, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<TData>.Failure(status, new List<IError> { new JsonError(title, description) }, serializerOptions);
        }

        public static IJsonResponse<TData> ToFailureJsonResponse<TData>(this HttpStatusCode status, string[] errors, JsonSerializerOptions serializerOptions = null)
        {
            return JsonResponse<TData>.Failure(status, errors.Select(e => (IError)new JsonError(e)).ToList(), serializerOptions);
        }
    }
}