import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';


import { AppComponent } from './app.component';
import { ProductComponent } from './Content/Component/product/product.component';
import { ProductService } from './Content/Component/shared/product-service.service';
import { CartComponent } from './cart/cart.component';
import { ExecutePaymentComponent } from './execute-payment/execute-payment.component';


@NgModule({
  declarations: [
    AppComponent,
    ProductComponent
    CartComponent,
    ExecutePaymentComponent
  ],
  imports: [
    BrowserModule,
    NgbModule.forRoot(),
    HttpClientModule,
    HttpModule,
    FormsModule
  ],
  providers: [ProductService],
  bootstrap: [AppComponent]
})
export class AppModule { }
