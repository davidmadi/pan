using System;
using Library.Envelope;
using Library.Back.Calculator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackApi.Controllers.v1;

[ApiController]
[Route("v1/api/tax")]

public class BackCalculatorController : ControllerBase
{
    [HttpGet("calculator/{year}/{income}/{raise}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<IncomeTaxResult>> Get(int year, decimal income, decimal raise)
    {
        try {
            bool withCache = Request.Headers["Pragma"] != "no-cache" &&
                Request.Headers["Cache-Control"] != "no-cache";

            var taxService = Library.Back.Calculator.Factory.GetTaxServiceBy(year);
            var brackets = taxService.FetchBrackets(year, withCache);
            var incomeTaxResult = IncomeBackCalculator.Calculate(year ,income, raise, brackets);

            return new Response<IncomeTaxResult>(){
                Result = incomeTaxResult,
                Success = true
            };
        }
        catch (Exception e) {
            Library.Logging.LogManager.EnqueueException(e, null);
            return NotFound(new Response<IncomeTaxResult>(){
                Success = false,
                Message = e.Message
            });
        }
    }    
    
}
