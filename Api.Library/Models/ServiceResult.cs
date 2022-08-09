using Newtonsoft.Json;

namespace Api.Library.Models
{
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class ServiceResult
	{
		public GeoIpResult GeoIp { get; set; }

		public PingResult Ping { get; set; }

		public RdapResult Rdap { get; set; }

		public RdnsResult Rdns { get; set; }
	}
}
