using AddressLookupService.Gateway.Api.Contracts;
using Api.Library.Contracts;
using Api.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AddressLookupService.Gateway.Api.Controllers
{
	[Route("api/lookup")]
    public class AddressLookupServiceController : ControllerBase
	{
		private readonly IAddressResolveService _addressLookupService;
		private readonly ILogger<AddressLookupServiceController> _logger;
        private readonly IValdiateAddress _validateAddress;

		public AddressLookupServiceController(IAddressResolveService addressAnalyzeService, ILogger<AddressLookupServiceController> logger, IValdiateAddress validateAddress)
		{
			_addressLookupService = addressAnalyzeService;
			_logger = logger;
			_validateAddress = validateAddress;
		}

		/// <summary>
		/// lookup for the 4 default services for the provided address
		/// </summary>
		/// <param name="address">Address to query.</param>
		/// <returns>The aggregated services result.</returns>
		[ProducesResponseType(typeof(ServiceResult), 200)]
        [ProducesResponseType(typeof(ErrorResult), 400)]
        [ProducesResponseType(typeof(ErrorResult), 404)]
        [HttpGet("{address}")]
        [SwaggerOperation(Tags = new[] { "Address Lookup Service" }, Summary = "Takes IP/domain as input and lookup for the default services - Ping and Rdap.")]
        public async Task<IActionResult> GetResultsForDefaultServicesAsync(string address)
        {
            _logger.LogInformation($"Lookup default Services for the Address: {address}");

            if (!_validateAddress.IsAddressValid(address))
            {
                return BadRequest(new ErrorResult("Validation Failed", "Invalid Address"));
            }

            var result = await GetResultAsync("ping,rdap,geoip,rdns", address);
            if (result is null)
            {
                return NotFound(new ErrorResult("Empty response", "No data found for the provided services and address"));
            }

            return Ok(result);
        }

        /// <summary>
        /// lookup the specified services for the provided address
        /// </summary>
        /// <param name="servicelist">The comma separated list of services to query.</param>
        /// <param name="address">Address to query.</param>
        /// <returns>The aggregated services result.</returns>
        [ProducesResponseType(typeof(ServiceResult), 200)]
        [ProducesResponseType(typeof(ErrorResult), 400)]
        [ProducesResponseType(typeof(ErrorResult), 404)]
        [HttpGet("{servicelist}/{address}")]
        [SwaggerOperation(Tags = new[] { "Address Lookup Service" }, Summary = "Takes IP/domain as input and gathers information about the provided address from the supplied list of services.")]
        public async Task<IActionResult> GetResultAsync(string servicelist, string address)
        {           
            if (!_validateAddress.IsAddressValid(address))
            {
                return BadRequest(new ErrorResult("Validation Failed", "Invalid Address"));                
            }

            if (string.IsNullOrWhiteSpace(servicelist))
            {
                return BadRequest(new ErrorResult("Validation Failed", "Service list is empty"));
            }

            _logger.LogInformation($"Lookup Services: {servicelist} for the Address: {address}");

            var services = servicelist.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(str => str.Trim()).Distinct();
            
			var result = await _addressLookupService.GetResultsAsync(address, services, Request.HttpContext.RequestAborted);

            if (result is null)
            {
                return NotFound(new ErrorResult("Empty response", "No data found for the provided services and address"));                
            }

            return Ok(result);
        }

    }
}