import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import { AppComponent } from './app.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { ConfirmComponent } from './confirm/confirm.component';
import { SetFlagIsBannedComponent } from './set-flag-is-banned/set-flag-is-banned.component';
import { SetPermissionComponent } from './set-permission/set-permission.component';
import { RemovePermissionComponent } from './remove-permission/remove-permission.component';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';


@NgModule({
  declarations: [
    AppComponent,
    RegisterUserComponent,
    ConfirmComponent,
    SetFlagIsBannedComponent,
    SetPermissionComponent,
    RemovePermissionComponent,
    LoginComponent,
    LogoutComponent
  ],
  imports: [
    BrowserModule, NgbModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
