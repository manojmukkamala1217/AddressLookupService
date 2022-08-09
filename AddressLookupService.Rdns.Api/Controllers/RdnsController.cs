using AddressLookupService.Rdns.Api.Contracts;
using Api.Library.Contracts;
using Api.Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddressLookupService.Rdns.Api.Controllers
{
	[Produces("application/json")]
    [Route("api/rdns")]
    public class RdnsController : ControllerBase
	{
        private readonly IRdnsService _RdnsService;
        private readonly IValdiateAddress _validateAddress;

        public RdnsController(IRdnsService RdnsService, IValdiateAddress validateAddress)
		{
			_RdnsService = RdnsService;
			_validateAddress = validateAddress;
		}

		[ProducesResponseType(typeof(RdnsResult), 200)]
        [ProducesResponseType(typeof(ErrorResult), 400)]
        [HttpGet("{address}")]
        public async Task<IActionResult> GetResultAsync(string address)
        {
            var isValidAddress = _validateAddress.IsAddressValid(address);
            if (!isValidAddress)
            {
                return BadRequest(new ErrorResult("Validation Failed", "Invalid Address"));
            }

            var result = await _RdnsService.GetResultAsync(address);
            return Ok(result);
        }
    }
}