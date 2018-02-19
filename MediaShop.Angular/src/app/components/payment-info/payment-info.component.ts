import { Component, OnInit } from '@angular/core';
import { PayPalPaymentDto } from '../../Models/Payment/pay-pal-payment-dto';
import { ItemDto } from '../../Models/Payment/item-dto';
import { Paymentservice } from '../../services/paymentservice';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-payment-info',
  templateUrl: './payment-info.component.html',
  styleUrls: ['./payment-info.component.css']
})
export class PaymentInfoComponent implements OnInit {
paymentId: string;
token: string;
payment: PayPalPaymentDto = new PayPalPaymentDto();

  constructor(private paymentService: Paymentservice, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.queryParams
    .subscribe(params => {
      this.paymentId = params['paymentId'];
      this.token = params['token'];
    });
    this.getPaimentInfo();
  }

  getPaimentInfo() {
    this.paymentService.executePayment(this.paymentId, this.token)
    .subscribe((data) => {
        this.payment = data;
      });
  }
}
