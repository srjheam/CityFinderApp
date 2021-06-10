using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CityFinder.Services
{
    /// <summary>
    /// This service class is used for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.
    /// </summary>
    public static class HttpDataService
    {
        private static readonly HttpClient _client = new HttpClient();

        /// <summary>
        /// Send a GET request to the specified Uri and return a deserialized object from the response body in an asynchronous operation.
        /// </summary>
        /// <typeparam name="T">The object the response body will be deserialized in.</typeparam>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="accessToken">(optional) The token to be passed along the GET request.</param>
        /// <returns>The deserialized object from the response body.</returns>
        /// <exception cref="JsonReaderException">Thrown when the request response body isn't a valid JSON.</exception>
        public static async Task<T> GetAsync<T>(string uri)
        {
            T result = default;

            var json = await _client.GetStringAsync(uri).ConfigureAwait(false);
            result = await Task.Run(() => JsonConvert.DeserializeObject<T>(json)).ConfigureAwait(false);

            return result;
        }
    }
}
