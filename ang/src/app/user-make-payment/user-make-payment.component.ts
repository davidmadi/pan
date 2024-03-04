import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs';
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
  protected stripeObject:any;
  protected stripeElements:any;

  constructor(protected route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.initUser();
    this.initStripe();
  }

  initUser():void {
    this.route.params.pipe(map((p) => {
      return p['id']
    })).subscribe((idVal:string)=>{
      this.user.id = Number(idVal);
    });
  }

  initStripe():void {
    var ctx = new StripeContext();
    ctx.CreatePaymentIntent(100, "cad").then((intentResponse:PaymentIntentResponse)=>{
      const options = {
        clientSecret: intentResponse.paymentIntent.clientSecret,
        // Fully customizable with appearance API.
        appearance: {
          theme: 'stripe',
        
          variables: {
            colorPrimary: '#0570de',
            colorBackground: '#ffffff',
            colorText: '#30313d',
            colorDanger: '#df1b41',
            fontFamily: 'Ideal Sans, system-ui, sans-serif',
            // See all possible variables below
          }
        },
      };
      
      this.stripeObject = Stripe(intentResponse.publish_key);
      // Set up Stripe.js and Elements to use in checkout form, passing the client secret obtained in a previous step
      this.stripeElements = this.stripeObject.elements(options);
      // Create and mount the Payment Element
      const paymentElement = this.stripeElements.create('payment');
      paymentElement.mount('#payment-element');
    })
  }

  submit():void{
    this.stripeObject.confirmPayment({
      //`Elements` instance that was used to create the Payment Element
      elements: this.stripeElements,
      confirmParams: {
        return_url: 'https://example.com/order/123/complete',
      },
    }).then((res:any)=>{
      console.log(res);
    }).catch((err:any)=>{
      console.log(err);
    })
    ;
  }

}
