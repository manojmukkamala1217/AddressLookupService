using AddressLookupService.Gateway.Api.Contracts;
using AddressLookupService.Gateway.Api.Domain;
using Api.Library.Models;
using Api.Library.Services;
using Api.Library.Validations;

namespace AddressLookupService.Gateway.Api.Services;

public class PingLookupService : IPingLookupService
{
    private readonly ApiServiceOptions _apiOptions;

    public PingLookupService(ApiServiceOptions apiOptions)
    {
        _apiOptions = apiOptions;
    }

    public async Task<PingResult> GetPingDataAsync(string address)
    {
        PingLookup pingLookup = new($"{_apiOptions.PingApiUrl}/api/ping/{address}");
        PingResult result = await pingLookup.FetchDataAsync();

        return result;
    }
}
