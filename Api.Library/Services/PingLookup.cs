using Api.Library.Models;

namespace Api.Library.Services
{
	/// <summary>
	/// This class is responsible for talking to the Ping API that is running in the background for the aggregator API to use.
	/// </summary>
	public class PingLookup : LookupServiceBase<PingResult>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Api.Library.Services.PingWorker "/> class.
        /// </summary>
        /// <param name="url">URL of Ping API.</param>
        public PingLookup(string url) : base(url)
        {
        }
    }
}
