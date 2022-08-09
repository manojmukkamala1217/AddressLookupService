using AddressLookupService.Ping.Api.Contracts;
using AddressLookupService.Ping.Api.Models;
using Api.Library.Contracts;
using Api.Library.Models;

namespace AddressLookupService.Ping.Api.Services
{
	public class PingService : IPingService
    {
        private readonly PingOptions _pingOptions;
        private readonly IHttpClientProviderService _httpClientProviderService;

        public PingService(PingOptions PingOptions, IHttpClientProviderService dataProviderService)
        {
            _pingOptions = PingOptions;
            _httpClientProviderService = dataProviderService;
        }

        public async Task<PingResult> GetResultAsync(string address, CancellationToken cancellationToken = default)
        {
            var data = await _httpClientProviderService.GetResultAsync($"{_pingOptions.BaseUrl}/?host={address}", cancellationToken);
            var result = new PingResult { Message = data, IsSuccess = !string.IsNullOrWhiteSpace(data) };
            return result;
        }
    }
}
