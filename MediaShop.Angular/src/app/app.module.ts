import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { HttpClient } from 'selenium-webdriver/http';

import { AppComponent } from './app.component';
import { CartComponent } from './components/cart/cart.component';
import { ExecutePaymentComponent } from './components/execute-payment/execute-payment.component';
import { ContentCartComponent } from './components/content-cart/content-cart.component';
import { Cartservice } from './services/cartservice';
import { Paymentservice } from './services/paymentservice';

import { PaymentComponent } from './components/payment/payment.component';
import { RegisterUserComponent } from './components/user/register-user/register-user.component';
import { LoginComponent } from './components/user/login/login.component';
import { AccountService } from './Services/User/AccountService';

import { SetPermissionComponent } from './components/user/set-permission/set-permission.component';
import { RemovePermissionComponent } from './components/user/remove-permission/remove-permission.component';
import { LogoutComponent } from './components/user/logout/logout.component';

import { UserService } from './Services/User/userservise';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ApproutingModule } from './approuting/approuting.module';
import { ProductService } from './Services/product-service.service';
import { ProductComponent } from './Components/Content/product/product.component';
import { ProductListComponent } from './components/Content/product-list/product-list.component';
@NgModule({
  declarations: [
    AppComponent,
    RegisterUserComponent,
    LoginComponent,
    CartComponent,
    ExecutePaymentComponent,
    ContentCartComponent,
    PaymentComponent,
    SetPermissionComponent,
    RemovePermissionComponent,
    LogoutComponent,
    NavbarComponent,
    ProductComponent,
    ProductListComponent
  ],
  imports: [
    BrowserModule,
    NgbModule.forRoot(),
    HttpClientModule,
    FormsModule,
    HttpModule,
    ApproutingModule
  ],
  providers: [AccountService, Cartservice, Paymentservice, UserService, ProductService],
  bootstrap: [AppComponent]
})
export class AppModule {}
