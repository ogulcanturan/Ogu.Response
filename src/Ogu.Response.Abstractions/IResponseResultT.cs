using System.Collections.Generic;

namespace Ogu.Response.Abstractions
{
    public interface IResponseResult<T>
    {
        /// <summary>
        /// A URI reference that identifies the result type.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// A short, human-readable title for the result.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// The HTTP status code associated with the result.
        /// </summary>
        int? Status { get; }

        /// <summary>
        /// The code associated with the result.
        /// </summary>
        string Code { get; }

        /// <summary>
        /// A detailed description or explanation of the result
        /// </summary>
        string Detail { get; }

        /// <summary>
        /// The URI of the specific instance where the result occurred.
        /// </summary>
        string Instance { get; }

        bool HasError { get; }
        
        T Data { get; set; }

        /// <summary>
        /// </summary>
        IDictionary<string, object> Extensions { get; }

        void AddErrorsToExtensions(IEnumerable<IResponseError> errors);
    }
}