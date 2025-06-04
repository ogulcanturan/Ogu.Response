using Ogu.Response.Abstractions;
using System.Collections.Generic;
using System.Net;

namespace Ogu.Response
{
    public class Response : Response<object>, IResponse
    {
        public Response(object data, bool success, HttpStatusCode status, IDictionary<string, object> extras, List<IError> errors) : base(data, success, status, extras, errors)
        {
        }
         
        public new static Response Failure(HttpStatusCode statusCode, List<IError> errors)
        {
            return new Response(null, false, statusCode, null, errors);
        }
    }
}