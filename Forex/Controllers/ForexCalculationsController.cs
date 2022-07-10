using Forex.Services.Contracts;
using Forex.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forex.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ForexCalculationsController : ControllerBase
    {
        private readonly ICalculationServiceProvider calculationProvider;
        public ForexCalculationsController(ICalculationServiceProvider calculationProvider)
        {
            this.calculationProvider = calculationProvider;
        }

        [HttpGet]
        [Route("{symbol}/24hAvgPrice")]
        public IActionResult DailyAvgPrice([FromRoute]string symbol)
        {
            try
            {
                if (string.IsNullOrEmpty(symbol))
                {
                    throw new ArgumentException("Please provide symbol value");
                }
                var result = this.calculationProvider.CalculateLastDayAvgPrice(symbol);
                return StatusCode(StatusCodes.Status200OK, new ResponseResult(result.ToString()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("{symbol}/SimpleMovingAverage")]
        public IActionResult SimpleMovingAverage([FromRoute] string symbol, int n, string p, string s)
        {
            try
            {
                if (string.IsNullOrEmpty(symbol))
                {
                    throw new ArgumentException("Please provide symbol value");
                }
                var result = this.calculationProvider.CalculateSimpleMovingAverage(symbol, n, p, DateTime.Parse(s));
                return StatusCode(StatusCodes.Status200OK, new ResponseResult(string.Join(", ", result)));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}