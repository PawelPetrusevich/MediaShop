import { Component, OnInit } from '@angular/core';
import { PayPalPaymentDto } from '../../Models/Payment/pay-pal-payment-dto';
import { ItemDto } from '../../Models/Payment/item-dto';

@Component({
  selector: 'app-payment-info',
  templateUrl: './payment-info.component.html',
  styleUrls: ['./payment-info.component.css']
})
export class PaymentInfoComponent implements OnInit {
payment: PayPalPaymentDto;

  constructor() { }

  ngOnInit() {
  }

  getPaimentInfo(paymentInfo: PayPalPaymentDto) {
    this.payment = paymentInfo;
    return this.payment;
  }
}
