using Ogu.Response.Abstractions;
using System.Text.Json;

namespace Ogu.Response.Json
{
    public interface IJsonResponse : IResponse
    {
        JsonSerializerOptions SerializerOptions { get; }
    }
}