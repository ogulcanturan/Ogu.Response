using Ogu.Response.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Ogu.Response
{
    public static class ResponseTExtensions
    {
        /// <summary>
        /// Converts an <see cref="IResponse"/> to a strongly typed <see cref="IResponse{TData}"/> response.
        /// </summary>
        /// <typeparam name="TData">The target type of the response's data.</typeparam>
        /// <param name="response">The <see cref="IResponse"/> to convert.</param>
        /// <returns>
        /// A new <see cref="IResponse{TData}"/> instance with data cast to the specified type <typeparamref name="TData"/>,
        /// including the original response's status, extras, errors, and serialization settings.
        /// </returns>
        /// <remarks>
        ///  If the response is successful and data in <paramref name="response"/> cannot be cast to the specified type <typeparamref name="TData"/>,
        ///  an <see cref="InvalidCastException"/> or <see cref="FormatException"/> may be thrown.
        /// </remarks>
        /// <exception cref="InvalidCastException">
        /// Thrown if the data in <paramref name="response"/> is not compatible with <typeparamref name="TData" />.
        /// </exception>
        /// <exception cref="FormatException">
        /// Thrown if the data in <paramref name="response"/> is not compatible with <typeparamref name="TData" />.
        /// </exception>
        public static IResponse<TData> ToResponseOf<TData>(this IResponse response)
        {
            TData data;

            switch (response.Data)
            {
                case null:
                    data = default(TData);
                    break;
                case TData tData:
                    data = tData;
                    break;
                default:
                    data = (TData)Convert.ChangeType(response.Data, typeof(TData));
                    break;
            }

            return new Response<TData>(data, response.Success, response.Status, response.Extras, response.Errors);
        }

        public static IResponse<TData> ToSuccessResponseOf<TData>(this HttpStatusCode status, TData data)
        {
            return new Response<TData>(data, success: true, status, null, null);
        }

        public static IResponse<TData> ToSuccessResponse<TData>(this HttpStatusCode status)
        {
            return new Response<TData>(data: default, true, status, null, null);
        }

        public static IResponse<TData> ToFailureResponse<TData>(this HttpStatusCode status)
        {
            return Response<TData>.Failure(status, null);
        }

        public static IResponse<TData> ToFailureResponse<TData>(this HttpStatusCode status, IError error)
        {
            return Response<TData>.Failure(status, new List<IError> { error });
        }

        public static IResponse<TData> ToFailureResponse<TData>(this HttpStatusCode status, List<IError> errors)
        {
            return Response<TData>.Failure(status, errors);
        }

        public static IResponse<TData> ToFailureResponse<TData>(this HttpStatusCode status, IValidationFailure failure)
        {
            return Response<TData>.Failure(status, new List<IError> { new Error(failure) });
        }

        public static IResponse<TData> ToFailureResponse<TData>(this HttpStatusCode status, List<IValidationFailure> failures)
        {
            return Response<TData>.Failure(status, new List<IError> { new Error(failures) });
        }

        public static IResponse<TData> ToFailureResponse<TData, TEnum>(this HttpStatusCode status, TEnum @enum) where TEnum : struct, Enum
        {
            return Response<TData>.Failure(status, new List<IError> { @enum.ToError() });
        }

        public static IResponse<TData> ToFailureResponse<TData, TEnum>(this HttpStatusCode status, IEnumerable<TEnum> enums) where TEnum : struct, Enum
        {
            return Response<TData>.Failure(status, enums.Select(e => e.ToError()).ToList());
        }

        public static IResponse<TData> ToFailureResponse<TData>(this HttpStatusCode status, Exception exception, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic)
        {
            return Response<TData>.Failure(status, new List<IError> { new Error(exception, traceLevel) });
        }

        public static IResponse<TData> ToFailureResponse<TData>(this HttpStatusCode status, IEnumerable<Exception> exceptions, ExceptionTraceLevel traceLevel = ExceptionTraceLevel.Basic)
        {
            return Response<TData>.Failure(status, exceptions.Select(e => (IError)new Error(e, traceLevel)).ToList());
        }

        public static IResponse<TData> ToFailureResponse<TData>(this HttpStatusCode status, string error)
        {
            return Response<TData>.Failure(status, new List<IError> { new Error(error) });
        }

        public static IResponse<TData> ToFailureResponse<TData>(this HttpStatusCode status, string title, string description)
        {
            return Response<TData>.Failure(status, new List<IError> { new Error(title, description) });
        }

        public static IResponse<TData> ToFailureResponse<TData>(this HttpStatusCode status, IEnumerable<string> errors)
        {
            return Response<TData>.Failure(status, errors.Select(e => (IError)new Error(e)).ToList());
        }
    }
}