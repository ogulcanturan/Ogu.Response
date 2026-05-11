using Ogu.Response.Abstractions;
using System.Collections.Generic;
using System.Net;

namespace Ogu.Response
{
    public class Response<TData> : IResponse<TData>
    {
        private List<IError> _errors;
        private IDictionary<string, object> _extras;

        public Response(TData data, bool success, HttpStatusCode status, IDictionary<string, object> extras, List<IError> errors)
        {
            Data = data;
            Success = success;
            Status = status;
            _errors = errors;
            _extras = extras;
        }

        public bool Success { get; }

        public HttpStatusCode Status { get; }

        public TData Data { get; }

        public List<IError> Errors
        {
            get { return _errors ?? (_errors = new List<IError>()); }
        }

        public IDictionary<string, object> Extras
        {
            get { return _extras ?? (_extras = new Dictionary<string, object>()); }
        }

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