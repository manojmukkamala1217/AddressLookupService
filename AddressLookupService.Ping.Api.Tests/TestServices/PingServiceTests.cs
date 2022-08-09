using AddressLookupService.Ping.Api.Models;
using AddressLookupService.Ping.Api.Services;
using Api.Library.Contracts;

namespace AddressLookupService.Ping.Api.Tests.Services
{
	public class PingServiceTests
    {
        private readonly PingService _pingService;
        private readonly Mock<IHttpClientProviderService> _httpClientProviderService;

        public PingServiceTests()
        {
            _httpClientProviderService = new Mock<IHttpClientProviderService>();
            _pingService = new PingService(new PingOptions { BaseUrl = "" }, _httpClientProviderService.Object);
        }

        [Fact]
        public async Task GetResultAsync_valid_address()
        {
            _httpClientProviderService.Setup(e => e.GetResultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult("sample result"));
            var result = await _pingService.GetResultAsync("8.8.8.8");
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetResultAsync_invalid_address()
        {
            _httpClientProviderService.Setup(e => e.GetResultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<string>(""));
            var result = await _pingService.GetResultAsync("8888");
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }

    }
}
