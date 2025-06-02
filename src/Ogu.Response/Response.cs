using Ogu.Response.Abstractions;
using System.Collections.Generic;
using System.Net;

namespace Ogu.Response
{
    public class Response : IResponse
    {
        public Response(object data, bool success, HttpStatusCode status, IDictionary<string, object> extras, List<IError> errors)
        {
            Data = data;
            Success = success;
            Status = status;
            Errors = errors ?? new List<IError>();
            Extras = extras ?? new Dictionary<string, object>();
        }

        public bool Success { get; }

        public HttpStatusCode Status { get; }

        public object Data { get; }

        public List<IError> Errors { get; }

        public IDictionary<string, object> Extras { get; }

        public static Response Failure(HttpStatusCode statusCode, List<IError> errors)
        {
            return new Response(null, false, statusCode, null, errors);
        }
    }
}