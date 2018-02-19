import { Component, OnInit } from '@angular/core';
import { Cart } from '../../Models/Cart/cart';
import { Cartservice } from '../../services/cartservice';
import { Paymentservice } from '../../services/paymentservice';
import { ContentCartDto } from '../../Models/Cart/content-cart-dto';
import { HttpErrorResponse } from '@angular/common/http';

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

  constructor(private cartService: Cartservice, private paymentService: Paymentservice) {
  }

  ngOnInit() {
    this.cartService.get().subscribe(resp => {
      this.cart = resp;
      this.isLoaded = true;
    }, (err: HttpErrorResponse) => {
      if (err.status === 404) {
        this.showError = true;
      }
      this.errorMessage = err.statusText;
    }
    );
  }

  getCart() {
    this.cartService.get().subscribe(resp => {
      this.cart = resp;
      this.isLoaded = true;
    }, (err: HttpErrorResponse) => {
      if (err.status === 404) {
        this.showError = true;
      }
      this.errorMessage = err.statusText;
    }
    );
  }

  delete(element: ContentCartDto) {
    this.showError = false;
    this.indexCurrentElement = this.cart.ContentCartDtoCollection.indexOf(element, 0);
    this.indexCurrentElement = -1;
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
      if (err.status === 404) {
        this.showError = true;
      }
      this.errorMessage = err.statusText;
    }
  );
  }

  clearCart() {
    this.showError = false;
    this.cartService.clearCart(this.cart).subscribe(resp => {
      this.cart = resp;
      this.isLoaded = true;
    }, (err: HttpErrorResponse) => {
      if (err.status === 404) {
        this.showError = true;
      }
      this.errorMessage = err.statusText;
    }
  );
  }

  paypalPayment() {
    this.paymentService.payPalPayment(this.cart).subscribe(resp => {
      this.urlForPayment = resp;
      this.isPayment = true;
    }, (err: HttpErrorResponse) => {
      if (err.status === 404) {
        this.showError = true;
      }
      this.errorMessage = err.statusText;
    }
    );
  }
/*  paypalPayment() {
    if (!this.isPayment) {
      this.isPayment = true;
    } else {
      this.isPayment = false;
    }
  }*/
}
