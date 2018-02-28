import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { ItemDto } from '../Models/Payment/item-dto';
import { PayPalPaymentDto } from '../Models/Payment/pay-pal-payment-dto';
import { Cart } from '../Models/Cart/cart';
import { HttpParams, HttpClient } from '@angular/common/http';
import { AppSettings } from '../Settings/AppSettings';
import { environment } from '../../environments/environment';

@Injectable()
export class Paymentservice {
  constructor(private http: HttpClient) {}

  payPalPayment(cart: Cart): Observable<string> {
    return this.http
      .post<string>(environment.API_ENDPOINT + 'api/payment/paypalpayment', cart);
  }

  executePayment(paymentId: string, token: string): Observable<PayPalPaymentDto> {
    return this.http
      .get<PayPalPaymentDto>(
        environment.API_ENDPOINT + 'api/payment/paypalpayment/executepaypalpaymentasync?paymentId=' + paymentId + '&token=' + token);
  }
}
