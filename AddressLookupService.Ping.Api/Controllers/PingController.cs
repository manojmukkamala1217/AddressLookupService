using AddressLookupService.Ping.Api.Contracts;
using Api.Library.Contracts;
using Api.Library.Models;
using Microsoft.AspNetCore.Mvc;
namespace AddressLookupService.Ping.Api.Controllers
{
	[Produces("application/json")]
    [Route("api/ping")]
    public class PingController : ControllerBase
	{
        private readonly IPingService _pingService;
        private readonly IValdiateAddress _validateAddress;

        public PingController(IPingService pingService, IValdiateAddress validateAddress)
		{
			_pingService = pingService;
			_validateAddress = validateAddress;
		}

		[ProducesResponseType(typeof(PingResult), 200)]
        [ProducesResponseType(typeof(ErrorResult), 400)]
        [HttpGet("{address}")]
        public async Task<IActionResult> GetResultAsync(string address)
        {
            var isValidAddress = _validateAddress.IsAddressValid(address);
            if (!isValidAddress)
            {
                return BadRequest(new ErrorResult("Validation Failed", "Invalid Address"));
            }

            var result = await _pingService.GetResultAsync(address);
            return Ok(result);
        }
    }
}