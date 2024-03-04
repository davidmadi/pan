using Library.Envelope;
using Library.Back.Calculator;
using Microsoft.AspNetCore.Mvc;
using Library.Entity.Access;
using Library.Entity.S3Bucket;
using System.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Library.Payment.Stripe;

namespace BackApi.Controllers.v1;

[ApiController]
[Route("v1/")]
public class PaymentController : ControllerBase
{
    [HttpPost("stripe/intent/create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<PaymentIntentResponse>> StripePaymentIntent(decimal amount, string currency)
    {
        try
        {
            var db = new PostgresContext();
            var result = new Response<PaymentIntentResponse>()
            {
                Success = false,
                Result = new PaymentIntentResponse {
                  publish_key = Variable.GetInstance().StripePK,
                  PaymentIntent = Library.Payment.Stripe.Proxy.CreatePaymentIntent(amount, currency)
                }
            };
            
            return result;
        }
        catch (Exception e)
        {
            Library.Logging.LogManager.EnqueueException(e, null);
            return NotFound(new Response<IncomeTaxResult>()
            {
                Success = false,
                Message = e.Message
            });
        }
    }

}
