using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ogu.AspNetCore.Response
{
    public interface IResponse<T> : IActionResult
    {
        T Data { get; set; }
        bool Success { get; }
        int Status { get; }
        string SerializedResponse { get; }
        IResult Result { get; }

        Task ExecuteResultAsync(HttpContext context);
    }
}