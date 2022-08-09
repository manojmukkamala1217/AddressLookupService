using AddressLookupService.Rdap.Api.Models;
using Api.Library.Contracts;

namespace AddressLookupService.Rdap.Api.Tests.TestServices
{
	public class RdapServiceTests
	{
		private readonly Api.Services.RdapService _rdapService;
		private readonly Mock<IHttpClientProviderService> _httpClientProviderService;

		public RdapServiceTests()
		{
			_httpClientProviderService = new Mock<IHttpClientProviderService>();
			_rdapService = new Api.Services.RdapService(new RdapOptions { BaseUrl = "" }, _httpClientProviderService.Object);
		}

		[SetUp]
		public void Setup()
		{
			
		}

		[Test]
		public async Task GetResultAsync_valid_address()
		{
			try
			{
				_httpClientProviderService.Setup(e => e.GetResultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult("sample result"));
				var result = await _rdapService.GetResultAsync("ip", "8.8.8.8");

				if (result.IsSuccess)
				{
					Assert.True(true);
				}
				else
				{
					Assert.Fail(result.Message);
				}
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[Test]
		public async Task GetResultAsync_Invalid_address()
		{
			try
			{
				_httpClientProviderService.Setup(e => e.GetResultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<string>(null));
				var result = await _rdapService.GetResultAsync("ip", "8888");

				if (!result.IsSuccess)
				{
					Assert.True(true); 
				}
				else
				{
					Assert.Fail(result.Message);
				}
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
	}
}