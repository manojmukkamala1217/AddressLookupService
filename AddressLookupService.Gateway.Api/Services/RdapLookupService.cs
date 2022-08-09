using AddressLookupService.Gateway.Api.Domain;
using Api.Library.Contracts;
using Api.Library.Models;
using Api.Library.Services;
using Api.Library.Validations;

namespace AddressLookupService.Gateway.Api.Services;

public class RdapLookupService : IRdapLookupService
{
    private readonly ApiServiceOptions _apiOptions;
    private readonly IValdiateAddress _validateAddress;

    public RdapLookupService(ApiServiceOptions apiOptions, IValdiateAddress valdiateAddress)
	{
		_apiOptions = apiOptions;
        _validateAddress = valdiateAddress;
	}

	public async Task<RdapResult> GetRdapDataAsync(string address)
    {
        string type = string.Empty;

        switch (_validateAddress.GetAddressType(address))
        {
            case AddressType.Domain:
                type = "domain";
                break;
            case AddressType.IPAddress:
                type = "ip";
                break;
        }

        RdapLookup rdapWorker = new($"{_apiOptions.RdapApiUrl}/api/rdap/{type}/{address}");
        RdapResult result = await rdapWorker.FetchDataAsync();

        return result;
    }
}