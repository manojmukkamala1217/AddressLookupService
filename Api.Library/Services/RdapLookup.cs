using Api.Library.Models;

namespace Api.Library.Services
{
	/// <summary>
	/// This class is responsible for talking to the Rdap API that is running in the background for the aggregator API to use.
	/// </summary>
	public class RdapLookup : LookupServiceBase<RdapResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Api.Library.Services.RdapWorker "/> class.
        /// </summary>
        /// <param name="url">URL of Rdap API.</param>
        public RdapLookup(string url) : base(url)
        {
        }
    }
}
