using Api.Library.Constants;
using Api.Library.Models;
using Newtonsoft.Json;

namespace Api.Library.Services
{
	public abstract class LookupServiceBase<T> where T : AnalysisResultBase, new()
    {
        protected string SourceUrl { get; }

        protected LookupServiceBase(string url)
        {
            ArgumentNullException.ThrowIfNull(nameof(url));
            //
            SourceUrl = url;
        }

        private readonly HttpClientProviderService _restService = new(new System.Net.Http.HttpClient());

        public async Task<T> FetchDataAsync()
        {
            string apiResult = await _restService.GetResultAsync(SourceUrl);

            if (apiResult == ErrorCodes.SERVER_OFFLINE || apiResult.Contains(ErrorCodes.ERROR_PREFIX) || apiResult.Contains(ErrorCodes.RATE_LIMITED))
            {
				AnalysisResultBase result = new();
				result.FailureReasons.Add(apiResult);
				result.Message = null;
				result.IsSuccess = false;
                //
				return result as T;
			}

            return JsonConvert.DeserializeObject<T>(apiResult);
        }
    }
}
