using Ogu.Response.Abstractions;
using System.Text.Json;

namespace Ogu.Response.Json
{
    public interface IJsonResponse<out TData> : IResponse<TData>
    {
        JsonSerializerOptions SerializerOptions { get; }
    }
}