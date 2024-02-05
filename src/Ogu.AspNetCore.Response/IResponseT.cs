using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        bool HasErrors { get; }

        IEnumerable<IError> GetErrorsOrDefault();

        Task ExecuteResultAsync(HttpContext context);
    }
}