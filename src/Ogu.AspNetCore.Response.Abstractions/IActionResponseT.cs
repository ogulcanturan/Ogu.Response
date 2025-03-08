using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ogu.Response.Abstractions;
using System.Threading.Tasks;

namespace Ogu.AspNetCore.Response.Abstractions
{
    /// <summary>
    /// Represents a response from an action result, encapsulating the result and allowing for asynchronous execution.
    /// </summary>
    /// <typeparam name="TData">The type of data contained in the response.</typeparam>
    public interface IActionResponse<out TData> : IResponse<TData>, IActionResult
    {
        /// <summary>
        /// Executes the result asynchronously, writing the response to the specified <see cref="HttpContext"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> for the current request, which provides access to the request and response information.</param>
        /// <returns>A task that represents the completion of the write operation.</returns>
        Task ExecuteResultAsync(HttpContext context);
    }
}