import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LoginComponent } from '../components/user/login/login.component';
import { RegisterUserComponent } from '../components/user/register-user/register-user.component';
import { ProductListComponent } from '../components/Content/product-list/product-list.component';
import { NotfoundComponent } from '../components/notfound/notfound.component';
import { AuthGuard } from '../guards/auth.guard';
import { LogoutComponent } from '../components/user/logout/logout.component';
import { PasswordRecoveryComponent } from '../components/user/password-recovery/password-recovery.component';
import { ForgotPasswordComponent } from '../Components/user/forgot-password/forgot-password.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent },
     { path: 'logOut',
       component: LogoutComponent,
       canActivate: [AuthGuard],
       canLoad: [AuthGuard]
       },
     { path: 'register', component: RegisterUserComponent },
      { path: 'recovery-password', component: PasswordRecoveryComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      {
        path: 'product-list',
        component: ProductListComponent,
        canActivate: [AuthGuard],
        canLoad: [AuthGuard]
      },
      { path: '**', component: NotfoundComponent }
    ])
  ],
  declarations: [],
  exports: [RouterModule]
})
export class ApproutingModule {}
