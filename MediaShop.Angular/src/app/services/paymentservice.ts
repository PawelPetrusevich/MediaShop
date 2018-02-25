import { Injectable } from '@angular/core';
import { Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { ItemDto } from '../Models/Payment/item-dto';
import { PayPalPaymentDto } from '../Models/Payment/pay-pal-payment-dto';
import { Cart } from '../Models/Cart/cart';
import { HttpParams, HttpClient } from '@angular/common/http';
import { AppSettings } from '../Settings/AppSettings';

@Injectable()
export class Paymentservice {
  constructor(private http: HttpClient) {}

  payPalPayment(cart: Cart): Observable<string> {
    return this.http
      .post<string>(AppSettings.API_PUBLIC + 'api/payment/paypalpayment', cart);
  }

  executePayment(paymentId: string, token: string): Observable<PayPalPaymentDto> {
    return this.http
      .get<PayPalPaymentDto>(AppSettings
        .API_PUBLIC + 'api/payment/paypalpayment/executepaypalpaymentasync?paymentId=' + paymentId + '&token=' + token);
  }
}
