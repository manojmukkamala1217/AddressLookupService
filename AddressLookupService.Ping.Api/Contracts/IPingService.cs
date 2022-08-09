using Api.Library.Models;

namespace AddressLookupService.Ping.Api.Contracts
{
	public interface IPingService
    {
        Task<PingResult> GetResultAsync(string address, CancellationToken cancellationToken = default);
    }
}
