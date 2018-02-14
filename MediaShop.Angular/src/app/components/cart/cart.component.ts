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
  cart: Cart;
  isLoaded = false;
  showError = false;
  errorMessage: string;
  url: string;

  constructor(private cartService: Cartservice, private paymentService: Paymentservice) {
  }

  ngOnInit() {
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
    this.cartService.delete(element).subscribe(resp => {
      const index = this.cart.ContentCartCollection.indexOf(resp, 0);
      if (index > -1) {
        this.cart.ContentCartCollection.splice(index, 1);
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

  paypalPayment(cart: Cart) {
    this.paymentService.payPalPayment(this.cart).subscribe(resp => {
      this.url = resp;
    }, (err: HttpErrorResponse) => {
      if (err.status === 404) {
        this.showError = true;
      }
      this.errorMessage = err.statusText;
    }
    );
  }

}
