namespace AddressLookupService.Gateway.Api.Domain;

public class ApiServiceOptions
{
	public string RdapApiUrl { get; set; }
	public string GeoIpApiUrl { get; set; }
	public string ReverseDnsApiUrl { get; set; }
	public string PingApiUrl { get; set; }
}