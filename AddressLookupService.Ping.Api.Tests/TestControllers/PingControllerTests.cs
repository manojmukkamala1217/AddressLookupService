using AddressLookupService.Ping.Api.Contracts;
using AddressLookupService.Ping.Api.Controllers;
using Api.Library.Contracts;
using Api.Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AddressLookupService.Ping.Api.Tests.Controllers
{
	public class PingControllerTests
    {
        private readonly Mock<IPingService> _mockPingService;
        private readonly PingController _pingController;
        private readonly Mock<IValdiateAddress> _validateAddress;

        public PingControllerTests()
		{
			_validateAddress = new Mock<IValdiateAddress>();
			_mockPingService = new Mock<IPingService>();
			_pingController = new PingController(_mockPingService.Object, _validateAddress.Object);
        }

		[Fact]
        public async Task GetResultAsync_valid_doamin_address()
        {
            _pingController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            var geoIpAnalysisResult = new PingResult { Message = "test", IsSuccess = true };

            _mockPingService.Setup(e => e.GetResultAsync(It.IsAny<string>(), default)).Returns(Task.FromResult(geoIpAnalysisResult));
            _validateAddress.Setup(e => e.IsAddressValid(It.IsAny<string>())).Returns(true);
            var result = await _pingController.GetResultAsync("8.8.8.8") as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, result?.StatusCode);
            Assert.NotNull(result);
            var resultValue = result?.Value as PingResult;
            Assert.True(resultValue?.IsSuccess);
        }

        [Fact]
        public async Task GetResultAsync_invalid_doamin_address()
        {
            _pingController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            var geoIpAnalysisResult = new PingResult { Message = "error", IsSuccess = false };

            _mockPingService.Setup(e => e.GetResultAsync(It.IsAny<string>(), default)).Returns(Task.FromResult(geoIpAnalysisResult));
            _validateAddress.Setup(e => e.IsAddressValid(It.IsAny<string>())).Returns(true);
            var result = await _pingController.GetResultAsync("8888") as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, result?.StatusCode);
            Assert.NotNull(result);
            var resultValue = result?.Value as PingResult;
            Assert.False(resultValue?.IsSuccess);
        }
    }
}
