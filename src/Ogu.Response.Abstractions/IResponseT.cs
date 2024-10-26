using System.Collections.Generic;
using System.Net;

namespace Ogu.Response.Abstractions
{
    public interface IResponse<out TData> 
    {
        bool Success { get; }

        HttpStatusCode Status { get; }

        TData Data { get; }

        List<IError> Errors { get; }

        IDictionary<string, object> Extras { get; }

        object SerializedResponse { get; set; }
    }
}