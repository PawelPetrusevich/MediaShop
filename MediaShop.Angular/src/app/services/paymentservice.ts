import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { ItemDto } from '../Models/Payment/item-dto';
import { PayPalPaymentDto } from '../Models/Payment/pay-pal-payment-dto';
import { Cart } from '../Models/Cart/cart';
import { HttpParams } from '@angular/common/http';

@Injectable()
export class Paymentservice {
  static url = 'http://localhost:51289/api/payment';
  constructor(private http: Http) {}

  /*payPalPayment(cart: Cart): Observable<string> {
    return this.http
      .post(Paymentservice.url + '/paypalpayment', cart)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }*/
  payPalPayment(cart: Cart): Observable<string> {
    return this.http
      .post(Paymentservice.url + '/paypalpayment', cart)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }

  executePayment(paymentId: string, token: string): Observable<PayPalPaymentDto> {
    const params = new HttpParams()
    .set('paymentId', paymentId.toString())
    .set('token', token.toString());
    return this.http
      .get(Paymentservice.url + '/executepaypalpaymentasync', {params})
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }
}
