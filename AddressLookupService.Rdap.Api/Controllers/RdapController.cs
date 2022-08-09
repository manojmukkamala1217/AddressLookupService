using AddressLookupService.Rdap.Api.Contracts;
using Api.Library.Contracts;
using Api.Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddressLookupService.Rdap.Api.Controllers
{
	[Produces("application/json")]
    [Route("api/rdap")]
    public class RdapController : ControllerBase
	{
        private readonly IRdapService _rdapService;
        private readonly IValdiateAddress _validateAddress;

        public RdapController(IRdapService RdapService, IValdiateAddress validateAddress)
		{
			_rdapService = RdapService;
			_validateAddress = validateAddress;
		}

		[HttpGet("{type}/{address}")]
        public async Task<IActionResult> GetResultAsync(string type, string address)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(address))
            {
                return BadRequest(new ErrorResult("Validation Failed", new[] { new Error("Get Address Results", "address and type cannot be empty") }));
            }

            var isValidAddress = _validateAddress.IsAddressValid(address);
            if (!isValidAddress)
            {
                return BadRequest(new ErrorResult("Validation Failed", "Invalid Address"));
            }

            var result = await _rdapService.GetResultAsync(type, address, Request.HttpContext.RequestAborted);
            return Ok(result);
        }
    }
}