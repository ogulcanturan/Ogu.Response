using Ogu.Response.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api
{
    public static class HttpContentJsonResponseExtensions
    {
        public static async Task<IJsonResponse> ToJsonResponseAsync(this HttpContent content, JsonSerializerOptions serializerOptions = null, CancellationToken cancellationToken = default)
        {
            var deserializableJsonResponse = await content.ReadFromJsonAsync<DeserializableJsonResponse>(serializerOptions, cancellationToken);

            return deserializableJsonResponse.ToJsonResponse();
        }

        public static async Task<IJsonResponse<T>> ToJsonResponseAsync<T>(this HttpContent content, JsonSerializerOptions serializerOptions = null, CancellationToken cancellationToken = default)
        {
            var deserializableJsonResponse = await content.ReadFromJsonAsync<DeserializableJsonResponse>(serializerOptions, cancellationToken);

            return deserializableJsonResponse.ToJsonResponse<T>(serializerOptions);
        }
    }
}