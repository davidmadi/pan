import { Component } from '@angular/core';
import { PaymentIntent, PaymentIntentResponse, StripeContext } from 'src/library/models/Stripe';
import { User } from 'src/library/models/User';

declare var Stripe: any;

@Component({
  selector: 'app-user-make-payment',
  templateUrl: './user-make-payment.component.html',
  styleUrls: ['./user-make-payment.component.scss']
})
export class UserMakePaymentComponent {
  protected user:User = {} as User;


  ngOnInit(): void {
    this.initStripe();
  }

  initStripe():void {
    var ctx = new StripeContext();
    ctx.CreatePaymentIntent(100, "cad").then((intentResponse:PaymentIntentResponse)=>{
      const options = {
        clientSecret: intentResponse.paymentIntent.clientSecret,
        // Fully customizable with appearance API.
        appearance: {/*...*/},
      };
      
      var stripe = Stripe(intentResponse.publish_key);
      // Set up Stripe.js and Elements to use in checkout form, passing the client secret obtained in a previous step
      const elements = stripe.elements(options);
      
      // Create and mount the Payment Element
      const paymentElement = elements.create('payment');
      paymentElement.mount('#payment-element');
    })
  }

}
