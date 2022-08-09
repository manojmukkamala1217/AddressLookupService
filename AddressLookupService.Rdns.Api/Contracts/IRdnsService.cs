using Api.Library.Models;

namespace AddressLookupService.Rdns.Api.Contracts
{
	public interface IRdnsService
    {
        Task<RdnsResult> GetResultAsync(string address, CancellationToken cancellationToken = default);
    }
}
