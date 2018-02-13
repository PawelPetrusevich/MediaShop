import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';


import { AppComponent } from './app.component';
import { ProductComponent } from './components/Content/product/product.component';
import { ProductService } from './Services/product-service.service';
import { CartComponent } from './components/cart/cart.component';
import { ExecutePaymentComponent } from './components/execute-payment/execute-payment.component';


@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
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
