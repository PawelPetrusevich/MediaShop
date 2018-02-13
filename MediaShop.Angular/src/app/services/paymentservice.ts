import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { ItemDto } from '../Models/Payment/item-dto';
import { PayPalPaymentDto } from '../Models/Payment/pay-pal-payment-dto';
import {Cart} from '../Models/Cart/cart';

@Injectable()
export class Paymentservice {
    constructor(private http: Http){}

    payPalPayment(cart:Cart): Observable<string>{
        var options = new RequestOptions();
        options.body = cart;
        return this.http.post('http://localhost:51289/api/payment/paypalpayment', options)
        .map(resp=>resp.json())
        .catch(err=>Observable.throw(err));
    }
}
