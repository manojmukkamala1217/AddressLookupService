using Api.Library.Models;

namespace AddressLookupService.Rdap.Api.Contracts
{
	public interface IRdapService
    {
        Task<RdapResult> GetResultAsync(string addressType, string address, CancellationToken cancellationToken = default);
    }
}
