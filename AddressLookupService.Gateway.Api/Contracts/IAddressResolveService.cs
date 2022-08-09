using Api.Library.Models;

namespace AddressLookupService.Gateway.Api.Contracts
{
	public interface IAddressResolveService
	{
		Task<ServiceResult> GetResultsAsync(string address, IEnumerable<string> services, CancellationToken cancellationToken = default);
	}
}