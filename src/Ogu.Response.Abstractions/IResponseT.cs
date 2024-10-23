using System.Collections.Generic;
using System.Net;

namespace Ogu.Response.Abstractions
{
    public interface IResponse<out TData, TSerialized> 
    {
        bool Success { get; }

        HttpStatusCode StatusCode { get; }

        TData Data { get; }

        List<IResponseError> Errors { get; }

        IDictionary<string, object> Extensions { get; }

        TSerialized SerializedResponse { get; set; }
    }
}