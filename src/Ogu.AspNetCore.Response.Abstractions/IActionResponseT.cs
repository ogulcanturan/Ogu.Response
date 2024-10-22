using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ogu.Response.Abstractions;
using System.Threading.Tasks;

namespace Ogu.AspNetCore.Response.Abstractions
{
    public interface IActionResponse<out TData> : IResponse<TData, string>, IActionResult
    {
        Task ExecuteResultAsync(HttpContext context);
    }
}