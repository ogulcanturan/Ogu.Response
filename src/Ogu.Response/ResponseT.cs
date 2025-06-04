using Ogu.Response.Abstractions;
using System.Collections.Generic;
using System.Net;

namespace Ogu.Response
{
    public class Response<TData> : IResponse<TData>
    {
        public Response(TData data, bool success, HttpStatusCode status, IDictionary<string, object> extras, List<IError> errors)
        {
            Data = data;
            Success = success;
            Status = status;
            Errors = errors ?? new List<IError>();
            Extras = extras ?? new Dictionary<string, object>();
        }

        public bool Success { get; }

        public HttpStatusCode Status { get; }

        public TData Data { get; }

        public List<IError> Errors { get; }

        public IDictionary<string, object> Extras { get; }

        public static Response<TData> Failure(HttpStatusCode statusCode, List<IError> errors)
        {
            return new Response<TData>(default, false, statusCode, null, errors);
        }

        public static implicit operator Response(Response<TData> response)
        {
            return new Response(response.Data, response.Success, response.Status, response.Extras, response.Errors);
        }
    }
}