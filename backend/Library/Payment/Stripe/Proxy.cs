using Stripe;

namespace Library.Payment.Stripe;

public class Proxy
{

  public static PaymentIntent CreatePaymentIntent(decimal amount, string currency, bool isAutomatic = true)
  {
    StripeConfiguration.ApiKey = Variable.GetInstance().StripeSK;
    var options = new PaymentIntentCreateOptions
    {
      Amount = 2000,
      Currency = "usd",
      AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
      {
        Enabled = isAutomatic,
      },
    };
    var service = new PaymentIntentService();
    return service.Create(options);
  }
}
