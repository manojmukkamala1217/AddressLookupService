using Api.Library.Models;

namespace Api.Library.Services
{
	/// <summary>
	/// This class is responsible for talking to the Geo API that is running in the background for the aggregator API to use.
	/// </summary>
	public class GeoIpLookup : LookupServiceBase<GeoIpResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Api.Library.Services.GeoIpWorker "/> class.
        /// </summary>
        /// <param name="url">URL of GeoIP API.</param>
        public GeoIpLookup(string url) : base(url)
        {
        }
    }
}
