using AddressLookupService.GeoIp.Api.Contracts;
using AddressLookupService.GeoIp.Api.Models;
using Api.Library.Contracts;
using Api.Library.Models;

namespace AddressLookupService.GeoIp.Api.Services
{
	public class GeoIpService : IGeoIpService
    {
        private readonly GeoIpOptions _geoIpOptions;
        private readonly IHttpClientProviderService _httpClientProviderService;

        public GeoIpService(GeoIpOptions geoIpOptions, IHttpClientProviderService dataProviderService)
        {
            _geoIpOptions = geoIpOptions;
            _httpClientProviderService = dataProviderService;
        }

        public async Task<GeoIpResult> GetResultAsync(string address, CancellationToken cancellationToken = default)
        {
            var data = await _httpClientProviderService.GetResultAsync($"{_geoIpOptions.BaseUrl}/json/{address}", cancellationToken);
            var result = new GeoIpResult { Message = data, IsSuccess = !string.IsNullOrWhiteSpace(data) };
            return result;
        }
    }
}
