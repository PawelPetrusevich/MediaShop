import { Component, OnInit } from '@angular/core';
import { PayPalPaymentDto } from '../../Models/Payment/pay-pal-payment-dto';
import { ItemDto } from '../../Models/Payment/item-dto';
import { Paymentservice } from '../../services/paymentservice';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-payment-info',
  templateUrl: './payment-info.component.html',
  styleUrls: ['./payment-info.component.css']
})
export class PaymentInfoComponent implements OnInit {
paymentId: string;
token: string;
payment: PayPalPaymentDto = new PayPalPaymentDto();
isLoaded = false;
showError = false;
errorMessage: string;

  constructor(private paymentService: Paymentservice, private route: ActivatedRoute, private notificationService: NotificationsService) { }

  ngOnInit() {
    this.route.queryParams
    .subscribe(params => {
      this.paymentId = params['paymentId'];
      this.token = params['token'];
    });
    if (this.paymentId && this.token) {
      this.paymentService.executePayment(this.paymentId, this.token)
      .subscribe( (data) => {
        this.payment = data;
        this.isLoaded = true; },
        (err: HttpErrorResponse) => {
          this.ShowError(err);
      }
    );
  }
}

  ShowError(error: HttpErrorResponse) {
    this.notificationService.error (
      'OperationError',
      error.error.Message,
      {
        timeOut: 5000,
        showProgressBar: true,
        clickToClose: true
      }
    );
  }
}
