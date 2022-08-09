using Api.Library.Constants;
using Api.Library.Contracts;
using Api.Library.Extensions;

namespace Api.Library.Services
{

	public class HttpClientProviderService : IHttpClientProviderService
    {
        private readonly HttpClient _httpClient;

        public HttpClientProviderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetResultAsync(string url, CancellationToken cancellationToken = default)
        {
            using var response = await _httpClient.GetAsync(url, cancellationToken);
            
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                if (responseContent.Contains("RateLimited", StringComparison.OrdinalIgnoreCase))
                {
                    return ErrorCodes.RATE_LIMITED;
                }

                return responseContent;
            }

            if (response.StatusCode == 0)
            {
                return ErrorCodes.SERVER_OFFLINE;
            }

            return $"{ErrorCodes.ERROR_PREFIX}{responseContent}";
        }

        public async Task<string> PostDataAsync<T>(string uri, T payload, CancellationToken cancellationToken = default) where T : class, new()
        {
            using var response = await _httpClient.PostAsJsonAsync(uri, payload, cancellationToken);
            return await response.Content.ReadAsStringAsync(cancellationToken);            
        }
    }
}
