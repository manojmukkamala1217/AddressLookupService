using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Library.Contracts
{
	public interface IHttpClientProviderService
	{
		Task<string> GetResultAsync(string url, CancellationToken cancellationToken = default);
		Task<string> PostDataAsync<T>(string uri, T payload, CancellationToken cancellationToken = default) where T : class, new();
	}
}
