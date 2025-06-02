using System.Collections.Generic;
using System.Net;

namespace Ogu.Response.Abstractions
{
    /// <summary>
    /// Represents a response, encapsulating the result and any associated errors.
    /// </summary>
    /// <typeparam name="TData">The type of data contained in the response.</typeparam>
    public interface IResponse<out TData> 
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// The HTTP status code representing the result of the operation.
        /// </summary>
        HttpStatusCode Status { get; }

        /// <summary>
        /// The data returned by the operation. 
        /// This may be the default value of <typeparamref name="TData"/> if the operation failed.
        /// </summary>
        TData Data { get; }

        /// <summary>
        /// A list of errors that occurred during the operation, if any.
        /// </summary>
        List<IError> Errors { get; }

        /// <summary>
        /// Additional metadata related to the response, stored as key-value pairs.
        /// </summary>
        IDictionary<string, object> Extras { get; }
    }
}