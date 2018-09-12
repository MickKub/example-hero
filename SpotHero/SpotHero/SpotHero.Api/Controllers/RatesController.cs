using Microsoft.AspNetCore.Mvc;
using SpotHero.Common;
using SpotHero.DataModel;
using SpotHero.Operations.Abstraction;
using System;
using System.Threading.Tasks;

namespace SpotHero.Api.Controllers
{
	[Route("api/rate")]
    [ApiController]
    public class RatesController : ControllerBase
    {
	    private readonly IRateOperations _rateOperations;

	    public RatesController(IRateOperations rateOperations)
	    {
		    _rateOperations = rateOperations;
	    }

		/// <summary>
		/// Computes a price for a specified datetime range
		/// </summary>
		/// <param name="from"> input date/times as ISO-8601 </param>
		/// <param name="to"> input date/times as ISO-8601 </param>
		/// <response code="200">
		/// Response representing a rate, including a price and date ranges
		/// </response>
		/// <response code="400">
		/// Bad Request 
		/// - There are not rates for current date ranges
		/// - Parameters are not valid date times
		/// - One or two parameters are missing
		/// - The from parameter is greater the to
		/// - The from datetime is greater than the to datetime
		/// </response>
		/// <response code="500">
		///	- Internal server error
		/// </response>
		/// <returns></returns>
		[HttpGet("from/{from:iso8601date}/to/{to:iso8601date}")]
        [ProducesResponseType(statusCode: 200, type: typeof(RateModel))]
		[ProducesResponseType(statusCode: 400, type: typeof(ErrorDetails))]
		[ProducesResponseType(statusCode: 500, type: typeof(ErrorDetails))]
		public async Task<IActionResult> Get(DateTimeOffset from , DateTimeOffset to)
		{
			return Ok(await _rateOperations.GetAsync(from, to));
		}
	}
}