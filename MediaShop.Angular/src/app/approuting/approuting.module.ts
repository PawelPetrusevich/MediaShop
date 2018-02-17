import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LoginComponent } from '../components/user/login/login.component';
import { RegisterUserComponent } from '../components/user/register-user/register-user.component';
import { ProductListComponent } from '../components/Content/product-list/product-list.component';
import { NotfoundComponent } from '../components/notfound/notfound.component';
import { AuthGuard } from '../guards/auth.guard';
import { LogoutComponent } from '../components/user/logout/logout.component';

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
