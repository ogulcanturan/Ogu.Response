using System.Collections.Generic;
using System.Net;

namespace Ogu.Response.Abstractions
{
    public interface IResponse<out TData, TSerialized> 
    {
        TData Data { get; }

        bool Success { get; }

        HttpStatusCode StatusCode { get; }

        IDictionary<string, object> Extensions { get; }

        IList<IResponseError> Errors { get; }

        TSerialized SerializedResponse { get; set; }
    }
}