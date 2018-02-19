import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { ItemDto } from '../Models/Payment/item-dto';
import { PayPalPaymentDto } from '../Models/Payment/pay-pal-payment-dto';
import { Cart } from '../Models/Cart/cart';

@Injectable()
export class Paymentservice {
  static url = 'http://demo.belpyro.net/api/payment';
  constructor(private http: Http) {}

  payPalPayment(cart: Cart): Observable<string> {
    return this.http
      .post(Paymentservice.url + '/paypalpayment', cart)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }
}
