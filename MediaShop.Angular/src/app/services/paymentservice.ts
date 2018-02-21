import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { ItemDto } from '../Models/Payment/item-dto';
import { PayPalPaymentDto } from '../Models/Payment/pay-pal-payment-dto';
import { Cart } from '../Models/Cart/cart';
import { HttpParams } from '@angular/common/http';
import { AppSettings } from '../Settings/AppSettings';

@Injectable()
export class Paymentservice {
  constructor(private http: Http) {}

  payPalPayment(cart: Cart): Observable<string> {
    return this.http
      .post(AppSettings.API_ENDPOINT + 'api/payment/paypalpayment', cart)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }

  executePayment(paymentId: string, token: string): Observable<PayPalPaymentDto> {
    const params = new HttpParams()
    .set('paymentId', paymentId.toString())
    .set('token', token.toString());
    return this.http
      .get(AppSettings.API_ENDPOINT + 'api/payment/paypalpayment/executepaypalpaymentasync?paymentId=' + paymentId + '&token=' + token)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }
}
