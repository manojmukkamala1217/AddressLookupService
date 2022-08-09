using AddressLookupService.Gateway.Api.Contracts;
using AddressLookupService.Gateway.Api.Domain;
using Api.Library.Models;
using Api.Library.Services;
using Api.Library.Validations;

namespace AddressLookupService.Gateway.Api.Services;

public class GeoIpLookupService : IGeoIpLookupService
{
    private readonly ApiServiceOptions _apiOptions;

    public GeoIpLookupService(ApiServiceOptions apiOptions)
    {
        _apiOptions = apiOptions;
    }

    public async Task<GeoIpResult> GetGeoIpDataAsync(string address)
    {
        GeoIpLookup geoIpWorker = new($"{_apiOptions.GeoIpApiUrl}/api/geoip/{address}");
        GeoIpResult result = await geoIpWorker.FetchDataAsync();

        return result;
    }
}