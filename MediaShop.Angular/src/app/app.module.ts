import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Input } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';


import { AppComponent } from './app.component';
import { ProductComponent } from './components/Content/product/product.component';
import { ProductService } from './Services/product-service.service';
import { CartComponent } from './components/cart/cart.component';
import { ExecutePaymentComponent } from './components/execute-payment/execute-payment.component';
import { ContentCartComponent } from './components/content-cart/content-cart.component';
import { Cartservice } from './services/cartservice';
import { Paymentservice } from './services/paymentservice';
import { PaymentComponent } from './components/payment/payment.component';
import { SetPermissionComponent } from './components/user/set-permission/set-permission.component';
import { RemovePermissionComponent } from './components/user/remove-permission/remove-permission.component';
import { LogoutComponent } from './components/user/logout/logout.component';

import { UserService } from './Services/User/userservise';
import { ProductListComponent } from './components/Content/product-list/product-list.component';
import { ProductInfoComponent } from './components/Content/product-info/product-info.component';
import { ProductUploadComponent } from './components/Content/product-upload/product-upload.component';
import { ProductFilterComponent } from './components/Content/product-filter/product-filter.component';


@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    CartComponent,
    ExecutePaymentComponent,
    ProductListComponent,
    ProductInfoComponent,
    ContentCartComponent,
    PaymentComponent,
    SetPermissionComponent,
    RemovePermissionComponent,
    LogoutComponent,
    ProductUploadComponent,
    ProductFilterComponent
  ],
  imports: [
    BrowserModule, NgbModule.forRoot(), HttpClientModule, FormsModule, HttpModule
  ],
  providers: [Cartservice, Paymentservice, UserService, ProductService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
