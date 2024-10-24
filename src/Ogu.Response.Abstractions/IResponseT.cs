using System.Collections.Generic;
using System.Net;

namespace Ogu.Response.Abstractions
{
    public interface IResponse<out TData> 
    {
        bool Success { get; }

        HttpStatusCode Status { get; }

        TData Data { get; }

        List<IResponseError> Errors { get; }

        IDictionary<string, object> Extensions { get; }

        object SerializedResponse { get; set; }
    }
}