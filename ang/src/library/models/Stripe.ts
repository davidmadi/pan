import { HttpClient, HttpHandler, HttpHeaderResponse } from "@angular/common/http";
import Model from "src/library/models/Model";
import { environment } from '../../environments/environment';

export interface PaymentIntentResponse {
  paymentIntent:PaymentIntent,
  publish_key:string
}
export interface PaymentIntent{
  id:string;
  clientSecret:string;
}

export class StripeContext {

  public async CreatePaymentIntent(amount:number, currency:string) : Promise<PaymentIntentResponse> {
    return new Promise<PaymentIntentResponse>((resolve, reject)=>{
      fetch(`${environment.apiUrl}/v1/stripe/intent/create?amount=${amount}&currency=${currency}`, {
        method: "POST", // *GET, POST, PUT, DELETE, etc.
        headers: {
          "Content-Type": "application/json",
        }
      }).then((res) =>{
        res.json().then(v=>{
          resolve((v as Model<PaymentIntentResponse>).result);
        });
      }).catch(err => {
        reject(err);
      });
    });
  }
}