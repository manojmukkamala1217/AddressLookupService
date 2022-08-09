using AddressLookupService.Rdap.Api.Contracts;
using AddressLookupService.Rdap.Api.Models;
using Api.Library.Contracts;
using Api.Library.Models;

namespace AddressLookupService.Rdap.Api.Services
{
	public class RdapService : IRdapService
    {
        private readonly RdapOptions _rdapApiOptions;
        private readonly IHttpClientProviderService _httpClientProviderService;

        public RdapService(RdapOptions rdapApiOptions, IHttpClientProviderService dataProviderService)
        {
            _rdapApiOptions = rdapApiOptions;
            _httpClientProviderService = dataProviderService;
        }

        public async Task<RdapResult> GetResultAsync(string type, string address, CancellationToken cancellationToken = default)
        {
            var url = type.ToLower() switch
            {
                "ip" => $"{_rdapApiOptions.BaseUrl}/ip",
                "domain" => $"{_rdapApiOptions.BaseUrl}/domain",
                _ => throw new Exception("Input Address is Invalid, Please provide a vlaid IP / Domain")
            };


            var data = await _httpClientProviderService.GetResultAsync($"{url}/{address}", cancellationToken);
            var result = new RdapResult { Message = data, IsSuccess = !string.IsNullOrWhiteSpace(data) };
            return result;
        }
    }
}
