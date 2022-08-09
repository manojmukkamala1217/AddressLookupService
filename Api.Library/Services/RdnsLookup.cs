using Api.Library.Models;

namespace Api.Library.Services
{
	/// <summary>
	/// This class is responsible for talking to the Rdns API that is running in the background for the aggregator API to use.
	/// </summary>
	public class RdnsLookup : LookupServiceBase<RdnsResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Api.Library.Services.RdnsWorker "/> class.
        /// </summary>
        /// <param name="url">URL of Rdns API.</param>
        public RdnsLookup(string url) : base(url)
        {
        }
    }
}
