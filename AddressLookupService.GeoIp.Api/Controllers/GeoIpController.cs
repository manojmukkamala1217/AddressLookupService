using AddressLookupService.GeoIp.Api.Contracts;
using Api.Library.Contracts;
using Api.Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddressLookupService.GeoIp.Api.Controllers
{
	[Produces("application/json")]
    [Route("api/geoip")]
    public class GeoIpController : ControllerBase
	{
        private readonly IGeoIpService _geoIpService;
        private readonly IValdiateAddress _validateAddress;

        public GeoIpController(IGeoIpService geoIpService, IValdiateAddress validateAddress)
		{
			_geoIpService = geoIpService;
			_validateAddress = validateAddress;
		}

		[HttpGet("{address}")]
        public async Task<IActionResult> GetResultAsync(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return BadRequest(new ErrorResult("Validation Failed", new[] { new Error("Get Address Results", "address cannot be empty") }));
            }

            var isValidAddress = _validateAddress.IsAddressValid(address);
            if (!isValidAddress)
            {
                return BadRequest(new ErrorResult("Validation Failed", "Invalid Address"));
            }

            var result = await _geoIpService.GetResultAsync(address, Request.HttpContext.RequestAborted);
            return Ok(result);
        }
    }
}