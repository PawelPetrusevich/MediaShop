import { Component, OnInit } from '@angular/core';
import { Cart } from '../../Models/Cart/cart';
import { Cartservice } from '../../services/cartservice';
import { Paymentservice } from '../../services/paymentservice';
import { ContentCartDto } from '../../Models/Cart/content-cart-dto';
import { HttpErrorResponse } from '@angular/common/http';
import { PaymentComponent } from '../payment/payment.component';
import { Router } from '@angular/router';
import { CartDataProvider } from './cartDataProvider';
import { SignalR } from 'ng2-signalr';


@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})

export class CartComponent implements OnInit {
  cart: Cart = new Cart();
  isLoaded = false;
  showError = false;
  errorMessage: string;
  urlForPayment: string;
  indexCurrentElement: number;
  isPayment = false;

  constructor(private cartService: Cartservice, private paymentService: Paymentservice, private router: Router
    , private dataProvider: CartDataProvider, private _signalR: SignalR) {
  }

  ngOnInit() {
    this.cartService.get().subscribe(resp => {
      this.cart = resp;
      this.dataProvider.storageCart = resp;
      this.isLoaded = true;
    }, (err: HttpErrorResponse) => {
      this.errorMessage = ' Oops!)) Error....';
      if (err.status === 404) {
        this.errorMessage = err.statusText;
        this.showError = true;
      }
      if (err.status === 400) {
        this.errorMessage = err.statusText;
        this.showError = true;
      }
      if (err.status === 500) {
        this.errorMessage = err.statusText;
        this.showError = true;
      }
      this.showError = true;
    }
    );
  }

  getCart() {
    this.cartService.get().subscribe(resp => {
      this.cart = resp;
      this.isLoaded = true;
    }, (err: HttpErrorResponse) => {
      this.errorMessage = ' Oops!)) Error....';
      if (err.status === 404) {
        this.errorMessage = err.statusText;
        this.showError = true;
      }
      if (err.status === 400) {
        this.errorMessage = err.statusText;
        this.showError = true;
      }
      if (err.status === 500) {
        this.errorMessage = err.statusText;
        this.showError = true;
      }
      this.showError = true;
    }
    );
  }

  delete(element: ContentCartDto) {
    this.showError = false;
    this.indexCurrentElement = this.cart.ContentCartDtoCollection.indexOf(element, 0);
    if (this.indexCurrentElement < 0) {
      this.showError = true;
      this.errorMessage = 'element index not found';
      return;
    }
    this.cartService.deleteById(element.Id).subscribe(resp => {
      if (this.indexCurrentElement > -1) {
        this.cart.ContentCartDtoCollection.splice(this.indexCurrentElement, 1);
      } else {
        this.showError = true;
        this.errorMessage = 'element index not found';
      }
    }, (err: HttpErrorResponse) => {
      this.errorMessage = ' Oops!)) Error....';
      if (err.status === 404) {
        this.errorMessage = err.statusText;
        this.showError = true;
      }
      if (err.status === 400) {
        this.errorMessage = err.statusText;
        this.showError = true;
      }
      if (err.status === 500) {
        this.errorMessage = err.statusText;
        this.showError = true;
      }
      this.showError = true;
    }
  );
  }

  clearCart() {
    this.showError = false;
    this.cartService.clearCart().subscribe(resp => {
      this.cart = resp;
      this.isLoaded = true;
    }, (err: HttpErrorResponse) => {
      this.errorMessage = ' Oops!)) Error....';
      if (err.status === 404) {
        this.errorMessage = err.statusText;
        this.showError = true;
      }
      if (err.status === 400) {
        this.errorMessage = err.statusText;
        this.showError = true;
      }
      if (err.status === 500) {
        this.errorMessage = err.statusText;
        this.showError = true;
      }
      this.showError = true;
    }
  );
  }

  paypalPayment() {
    this.paymentService.payPalPayment(this.cart).subscribe(resp => {
      this.urlForPayment = resp;
      this.paypalExecutePayment();
    }, (err: HttpErrorResponse) => {
      this.errorMessage = ' Oops!)) Error....';
      if (err.status === 404) {
        this.errorMessage = err.statusText;
        this.showError = true;
      }
      if (err.status === 400) {
        this.errorMessage = err.statusText;
        this.showError = true;
      }
      if (err.status === 500) {
        this.errorMessage = err.statusText;
        this.showError = true;
      }
      this.showError = true;
    }
    );
  }

  paypalExecutePayment() {
    this.dataProvider.storageCart = this.cart;
    this.dataProvider.storageUrl = this.urlForPayment;
    this.router.navigate(['payment']);
  }
}
