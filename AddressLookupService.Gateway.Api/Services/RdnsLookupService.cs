using AddressLookupService.Gateway.Api.Contracts;
using AddressLookupService.Gateway.Api.Domain;
using Api.Library.Models;
using Api.Library.Services;
using Api.Library.Validations;

namespace AddressLookupService.Gateway.Api.Services;

public class RdnsLookupService : IRdnsLookupService
{
    private readonly ApiServiceOptions _apiOptions;

    public RdnsLookupService(ApiServiceOptions apiOptions)
    {
        _apiOptions = apiOptions;
    }

    public async Task<RdnsResult> GetRdnsDataAsync(string address)
    {
        RdnsLookup reverseDnsWorker = new($"{_apiOptions.ReverseDnsApiUrl}/api/rdns/{address}");
        RdnsResult result = await reverseDnsWorker.FetchDataAsync();

        return result;
    }
}
