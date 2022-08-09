using AddressLookupService.Gateway.Api.Contracts;
using AddressLookupService.Gateway.Api.Domain;
using Api.Library.Contracts;
using Api.Library.Models;

namespace AddressLookupService.Gateway.Api.Services
{
	public class AddressResolveService : IAddressResolveService
	{
        private readonly ILogger<AddressResolveService> _logger;
        private readonly IPingLookupService _pingLookupService;
        private readonly IRdapLookupService _rdapLookupService;
        private readonly IRdnsLookupService _rdnsLookupService;
        private readonly IGeoIpLookupService _geoipLookupService;

        public AddressResolveService(ILogger<AddressResolveService> logger, 
            IPingLookupService pingLookupService,
            IRdapLookupService rdapLookupService,
            IRdnsLookupService rdnsLookupService,
            IGeoIpLookupService geoipLookupService)
		{
			_logger = logger;
			_pingLookupService = pingLookupService;
            _rdapLookupService = rdapLookupService;
            _rdnsLookupService = rdnsLookupService;
            _geoipLookupService = geoipLookupService;
        }

		/// <summary>
		/// Runs the queries in parallel and builds the lookup result.
		/// </summary>
		/// <param name="address">Address to lookup.</param>
		/// <param name="services">Services to query.</param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<ServiceResult> GetResultsAsync(string address, IEnumerable<string> services, CancellationToken cancellationToken = default)
        {
            Dictionary<string, dynamic> parallelTasks = RunServiceTasksAsync(address, services);
            _logger.LogInformation("before running api tasks");

            ServiceResult result = new();

            foreach (var task in parallelTasks)
            {
                dynamic value = await task.Value;

                switch (task.Key)
                {					
					case "rdap":
						result.Rdap = value;
						break;
					case "rdns":
						result.Rdns = value;
						break;
					case "ping":
						result.Ping = value;
						break;
					case "geoip":
						result.GeoIp = value;
						break;
				}
            }

            return result;
        }

        private Dictionary<string, dynamic> RunServiceTasksAsync(string address, IEnumerable<string> services)
        {
            Dictionary<string, dynamic> parallelTasks = new();

            foreach (string service in services)
            {
                dynamic result = service switch
                {
                    "rdap" => _rdapLookupService.GetRdapDataAsync(address),
                    "rdns" => _rdnsLookupService.GetRdnsDataAsync(address),
                    "ping" => _pingLookupService.GetPingDataAsync(address),
                    "geoip" => _geoipLookupService.GetGeoIpDataAsync(address),
                    _ => throw new Exception("Service not supported")
                };
                //
                parallelTasks.Add(service, result);
            }           
            //
            return parallelTasks;
        }
    }
}
