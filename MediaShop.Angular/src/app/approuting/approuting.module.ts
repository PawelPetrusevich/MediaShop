import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LoginComponent } from '../components/user/login/login.component';
import { RegisterUserComponent } from '../components/user/register-user/register-user.component';
import { ProductListComponent } from '../components/Content/product-list/product-list.component';
import { NotfoundComponent } from '../components/notfound/notfound.component';
import { AuthGuard } from '../guards/auth.guard';
import { LogoutComponent } from '../components/user/logout/logout.component';
import { ProductUploadComponent } from '../components/Content/product-upload/product-upload.component';
import { ProductInfoComponent } from '../components/Content/product-info/product-info.component';
import { CartComponent } from '../components/cart/cart.component';
import { PasswordRecoveryComponent } from '../components/user/password-recovery/password-recovery.component';
import { ForgotPasswordComponent } from '../Components/user/forgot-password/forgot-password.component';
import { ConfirmComponent } from '../Components/user/confirm/confirm.component';

import { UserListComponent } from '../components/user/user-list/user-list.component';
import { PaymentInfoComponent } from '../components/payment-info/payment-info.component';
import { PaymentComponent } from '../components/payment/payment.component';
import { UserSettingsComponent } from '../components/user/user-settings/user-settings.component';
import { UserPofileComponent } from '../components/user/user-pofile/user-pofile.component';
import { SetPermissionComponent } from '../components/user/set-permission/set-permission.component';
import { ProductPurshasedComponent } from '../Components/Content/product-purshased/product-purshased.component';
import { ProductListUploadedComponent } from '../Components/Content/product-list-uploaded/product-list-uploaded.component';
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot([
      { path: '', component: ProductListComponent },
      { path: 'login', component: LoginComponent },
     { path: 'logOut',
       component: LogoutComponent,
       canActivate: [AuthGuard],
       canLoad: [AuthGuard]
       },
      { path: 'userSettings',
      component: UserSettingsComponent,
      canActivate: [AuthGuard],
      canLoad: [AuthGuard]
      },
      { path: 'userProfile',
      component: UserPofileComponent,
      canActivate: [AuthGuard],
      canLoad: [AuthGuard]
      },
     { path: 'register', component: RegisterUserComponent },
      { path: 'recovery-password', component: PasswordRecoveryComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      {
        path: 'product-list',
        component: ProductListComponent
      },
      { path: 'cart',
        component: CartComponent
       },
      { path: 'payment',
        component: PaymentComponent,
        canActivate: [AuthGuard],
        canLoad: [AuthGuard]
       },
      {
        path: 'product-upload',
        component: ProductUploadComponent,
        canActivate: [AuthGuard],
        canLoad: [AuthGuard]
      },
      {
        path: 'product-info/:id',
        component: ProductInfoComponent
      },
      {
        path: 'product-purshased',
        component: ProductPurshasedComponent,
        canActivate: [AuthGuard],
        canLoad: [AuthGuard]
      },
      {
        path: 'confirm',
        component: ConfirmComponent
      },
      { path: 'user-list/:id',
        component: SetPermissionComponent,
        canActivate: [AuthGuard],
        canLoad: [AuthGuard]
     },
      { path: 'user-list',
        component: UserListComponent,
        canActivate: [AuthGuard],
        canLoad: [AuthGuard] },
      {
        path: 'payment-info',
        component: PaymentInfoComponent,
        canActivate: [AuthGuard],
        canLoad: [AuthGuard]
       },
       {
        path: 'product-list-uploaded',
        component: ProductListUploadedComponent,
        /*canActivate: [AuthGuard],
        canLoad: [AuthGuard]*/
       },
      { path: '**', component: NotfoundComponent }
    ])
  ],
  declarations: [],
  exports: [RouterModule]
})
export class ApproutingModule { }
