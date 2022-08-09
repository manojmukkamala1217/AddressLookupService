using Api.Library.Models;

namespace AddressLookupService.Gateway.Api.Services;

public interface IGeoIpLookupService
{
    Task<GeoIpResult> GetGeoIpDataAsync(string address);
}
