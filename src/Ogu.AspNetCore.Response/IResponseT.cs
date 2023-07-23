using Microsoft.AspNetCore.Mvc;

namespace Ogu.AspNetCore.Response
{
    public interface IResponse<T> : IActionResult
    {
        T Data { get; set; }
        bool Success { get; }
        int Status { get; }
        string SerializedResponse { get; }
        IResult Result { get; }
    }
}