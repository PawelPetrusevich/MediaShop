import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import { AppComponent } from './app.component';
import { NotificationComponent } from './Messaging/notification/notification.component';
import { CartComponent } from './cart/cart.component';
import { ExecutePaymentComponent } from './execute-payment/execute-payment.component';
import { CartComponent } from './components/cart/cart.component';
import { ExecutePaymentComponent } from './components/execute-payment/execute-payment.component';
import { ContentCartComponent } from './components/content-cart/content-cart.component';
import { Cartservice } from './services/cartservice';
import { Paymentservice } from './services/paymentservice';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { PaymentComponent } from './components/payment/payment.component';
import { SetPermissionComponent } from './components/user/set-permission/set-permission.component';
import { RemovePermissionComponent } from './components/user/remove-permission/remove-permission.component';
import { LogoutComponent } from './components/user/logout/logout.component';

import { UserService } from './Services/User/userservise';


@NgModule({
  declarations: [
    AppComponent,
    NotificationComponent
    CartComponent,
    ExecutePaymentComponent,
    ContentCartComponent,
    PaymentComponent,
    SetPermissionComponent,
    RemovePermissionComponent,
    LogoutComponent
  ],
  imports: [
    BrowserModule, NgbModule.forRoot(), HttpClientModule, FormsModule, HttpModule
  ],
  providers: [Cartservice, Paymentservice, UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
