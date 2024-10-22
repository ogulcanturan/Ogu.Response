using Ogu.Response.Abstractions;
using System.Text.Json;

namespace Ogu.Response.Json
{
    public interface IJsonResponse : IResponse<string>
    {
        JsonSerializerOptions SerializerOptions { get; }
    }
}