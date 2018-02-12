import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import { AppComponent } from './app.component';
import { CartComponent } from './cart/cart.component';
import { ExecutePaymentComponent } from './execute-payment/execute-payment.component';


@NgModule({
  declarations: [
    AppComponent,
    CartComponent,
    ExecutePaymentComponent
  ],
  imports: [
    BrowserModule, NgbModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
