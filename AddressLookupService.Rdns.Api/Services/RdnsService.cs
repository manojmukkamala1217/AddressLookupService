using AddressLookupService.Rdns.Api.Contracts;
using AddressLookupService.Rdns.Api.Models;
using Api.Library.Contracts;
using Api.Library.Models;

namespace AddressLookupService.Rdns.Api.Services
{
	public class RdnsService : IRdnsService
    {
        private readonly RdnsOptions _RdnsOptions;
        private readonly IHttpClientProviderService _httpClientProviderService;

        public RdnsService(RdnsOptions RdnsOptions, IHttpClientProviderService dataProviderService)
        {
            _RdnsOptions = RdnsOptions;
            _httpClientProviderService = dataProviderService;
        }

        public async Task<RdnsResult> GetResultAsync(string address, CancellationToken cancellationToken = default)
        {
            var data = await _httpClientProviderService.GetResultAsync($"{_RdnsOptions.BaseUrl}/?q={address}", cancellationToken);
            var result = new RdnsResult { Message = data, IsSuccess = !string.IsNullOrWhiteSpace(data) };
            return result;
        }
    }
}
