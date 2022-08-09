using Api.Library.Models;

namespace AddressLookupService.Gateway.Api.Services;

public interface IRdnsLookupService
{
    Task<RdnsResult> GetRdnsDataAsync(string address);
}
