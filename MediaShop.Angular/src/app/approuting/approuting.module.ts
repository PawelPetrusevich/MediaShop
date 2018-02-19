import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LoginComponent } from '../components/user/login/login.component';
import { ProductListComponent } from '../components/Content/product-list/product-list.component';
import { NotfoundComponent } from '../components/notfound/notfound.component';
import { AuthGuard } from '../guards/auth.guard';
import { ProductUploadComponent } from '../components/Content/product-upload/product-upload.component';
import { ProductFilterComponent } from '../components/Content/product-filter/product-filter.component';
import { ProductInfoComponent } from '../components/Content/product-info/product-info.component';
import { PasswordRecoveryComponent } from '../components/user/password-recovery/password-recovery.component';
import { ForgotPasswordComponent } from '../Components/user/forgot-password/forgot-password.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent },
      { path: 'recovery-password', component: PasswordRecoveryComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      {
        path: 'product-list',
        component: ProductListComponent,
        canActivate: [AuthGuard],
        canLoad: [AuthGuard]
      },
      {
        path: 'product-upload',
        component: ProductUploadComponent
      },
      {
        path: 'product-filter',
        component: ProductFilterComponent
      },
      {
        path: 'product-info/:id',
        component: ProductInfoComponent
      },

      { path: '**', component: NotfoundComponent }
    ])
  ],
  declarations: [],
  exports: [RouterModule]
})
export class ApproutingModule { }
