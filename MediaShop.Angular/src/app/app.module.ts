import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Pipe } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { HttpClient } from 'selenium-webdriver/http';

import { AppComponent } from './app.component';
import { NotificationComponent } from './components/Messaging/notification/notification.component';
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
import { ProductListComponent } from './components/Content/product-list/product-list.component';

import { UserService } from './Services/User/userservise';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ApproutingModule } from './approuting/approuting.module';
import { ProductComponent } from './components/Content/product/product.component';
import { NotfoundComponent } from './components/notfound/notfound.component';
import { AuthGuard } from './guards/auth.guard';
import { AuthInterceptor } from './interceptors/http.auth.interceptor';
import { PasswordRecoveryComponent } from './components/user/password-recovery/password-recovery.component';
import { ForgotPasswordComponent } from './Components/user/forgot-password/forgot-password.component';
import { ProductService } from './Services/product-service.service';
import { ProductUploadComponent } from './components/Content/product-upload/product-upload.component';
import { ProductInfoComponent } from './components/Content/product-info/product-info.component';
import { ProductDownloadComponent } from './Components/Content/product-download/product-download.component';
import { ConfirmComponent } from './Components/user/confirm/confirm.component';
import {NgbModule, NgbPaginationConfig} from '@ng-bootstrap/ng-bootstrap';
import { NgxPaginationModule } from 'ngx-pagination';
import { PaymentInfoComponent } from './components/payment-info/payment-info.component';

import { UserListComponent } from './components/user/user-list/user-list.component';
import { CartDataProvider } from './components/cart/cartDataProvider';



@NgModule({
  declarations: [
    AppComponent,
    ProductListComponent,
    ProductComponent,
    NotificationComponent,
    RegisterUserComponent,
    LoginComponent,
    NotificationComponent,
    CartComponent,
    ExecutePaymentComponent,
    ContentCartComponent,
    PaymentComponent,
    ProductListComponent,
    SetPermissionComponent,
    RemovePermissionComponent,
    LogoutComponent,
    NavbarComponent,

    PasswordRecoveryComponent,
    ForgotPasswordComponent,
    NotfoundComponent,
    PasswordRecoveryComponent,
    ForgotPasswordComponent,
    ProductUploadComponent,
    ProductInfoComponent,
    ProductDownloadComponent,
    UserListComponent,
    ConfirmComponent,
    PaymentInfoComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    HttpModule,
    ApproutingModule,
    NgxPaginationModule
  ],
  providers: [
    AccountService,
    Cartservice,
    Paymentservice,
    UserService,
    ProductService,
    AuthGuard,
    NgbPaginationConfig,
    CartDataProvider,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
