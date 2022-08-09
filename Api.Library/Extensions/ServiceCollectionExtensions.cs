using Api.Library.Contracts;
using Api.Library.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Library.Extensions
{
	public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataProviderService(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<IHttpClientProviderService, HttpClientProviderService>();
            return services;
        }
    }
}
