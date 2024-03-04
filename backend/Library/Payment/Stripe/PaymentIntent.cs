using System.Text.Json.Serialization;
using Stripe;
using ThirdParty.Json.LitJson;

namespace Library.Payment.Stripe;

public class PaymentIntentResponse {
  public PaymentIntent? PaymentIntent { get; set; }
  public string? publish_key { get; set; }
}