import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import { AppComponent } from './app.component';
import { CartComponent } from './components/cart/cart.component';
import { ExecutePaymentComponent } from './components/execute-payment/execute-payment.component';
import { ContentCartComponent } from './components/content-cart/content-cart.component';
import { Cartservice } from './services/cartservice';
import { Paymentservice } from './services/paymentservice';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';


@NgModule({
  declarations: [
    AppComponent,
    CartComponent,
    ExecutePaymentComponent,
    ContentCartComponent
  ],
  imports: [
    BrowserModule, NgbModule.forRoot(), HttpClientModule, FormsModule, HttpModule
  ],
  providers: [Cartservice, Paymentservice],
  bootstrap: [AppComponent]
})
export class AppModule { }
