using Api.Library.Models;

namespace AddressLookupService.GeoIp.Api.Contracts
{
	public interface IGeoIpService
    {
        Task<GeoIpResult> GetResultAsync(string address, CancellationToken cancellationToken = default);
    }
}
