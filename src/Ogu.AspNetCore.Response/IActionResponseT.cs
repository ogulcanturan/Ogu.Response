using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ogu.Response.Abstractions;
using System.Threading.Tasks;

namespace Ogu.AspNetCore.Response.Abstractions
{
    public interface IActionResponse<T> : IResponse<T>, IActionResult
    {
        Task ExecuteResultAsync(HttpContext context);
    }
}