using Api.Library.Models;

namespace AddressLookupService.Gateway.Api.Services;

public interface IPingLookupService
{
    Task<PingResult> GetPingDataAsync(string address);
}
