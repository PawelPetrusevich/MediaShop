import { Component, OnInit, Input } from '@angular/core';
import { Paymentservice } from '../../services/paymentservice';
import { Cart } from '../../Models/Cart/cart';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  @Input() cart: Cart = new Cart;
  url: string;
  showError = false;
  errorMessage: string;

  constructor(private paymentService: Paymentservice) { }

  ngOnInit() {
  }

  paypalPayment() {
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
