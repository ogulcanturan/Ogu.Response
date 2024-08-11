using Ogu.Response.Abstractions;
using System.Text.Json;

namespace Ogu.Response.Json
{
    public interface IJsonResponse<out T> : IResponse<T>
    {
        JsonSerializerOptions SerializerOptions { get; }
    }
}