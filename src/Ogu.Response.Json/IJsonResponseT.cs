using Ogu.Response.Abstractions;
using System.Text.Json;

namespace Ogu.Response.Json
{
    public interface IJsonResponse<T> : IResponse<T>
    {
        JsonSerializerOptions SerializerOptions { get; }
    }
}