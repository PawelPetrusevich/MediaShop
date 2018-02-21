import { Component, OnInit, Input } from '@angular/core';
import { Paymentservice } from '../../services/paymentservice';
import { Cart } from '../../Models/Cart/cart';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { CartDataProvider } from '../cart/cartDataProvider';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  cart: Cart = new Cart();
  url: string;
  showError = false;
  errorMessage: string;
  tax: number;

  constructor(private paymentService: Paymentservice, private route: ActivatedRoute, private dataProvider: CartDataProvider) {
    this.cart = dataProvider.storageCart;
    this.url = dataProvider.storageUrl;
    this.tax = this.cart.PriceAllItemsCollection * 0.1;
  }

  ngOnInit() {
   /* this.route.queryParams
    .subscribe(params => {
      this.url = params['url'];
    }); */
  }

 /* paypalPayment() {
    this.paymentService.payPalPayment(this.cart).subscribe(resp => {
      this.url = resp;
    }, (err: HttpErrorResponse) => {
      if (err.status === 404) {
        this.showError = true;
      }
      if (err.status === 400) {
        this.showError = true;
      }
      if (err.status === 500) {
        this.showError = true;
      }
      this.showError = true;
      this.errorMessage = ' Упс)) Ошибка....';
    }
    );
  }*/
}
