using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Api.Library.Extensions
{
	public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T data, CancellationToken cancelToken = default)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PostAsync(url, content, cancelToken);
        }

        public static async Task<T> ReadFromJsonAsync<T>(this HttpResponseMessage response)
        {
            var stringContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(stringContent, null, response.StatusCode);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(stringContent);
            }
        }
    }
}