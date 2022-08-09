using Api.Library.Models;

namespace AddressLookupService.Gateway.Api.Services;

public interface IRdapLookupService
{
    Task<RdapResult> GetRdapDataAsync(string address);
}
