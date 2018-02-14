import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {HttpClientModule} from '@angular/common/http';
import {HttpModule} from '@angular/http';
import {FormsModule} from '@angular/forms';

import { AppComponent } from './app.component';
import { RegisterUserComponent } from './components/user/register-user/register-user.component';
import {LoginComponent} from './components/user/login/login.component';
import { AccountService } from './Services/AccountService';
import { HttpClient } from 'selenium-webdriver/http';

@NgModule({
  declarations: [
    AppComponent,
    RegisterUserComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule, NgbModule.forRoot(),HttpClientModule,
  ],
  providers: [AccountService],
  bootstrap: [AppComponent]
})
export class AppModule { }
