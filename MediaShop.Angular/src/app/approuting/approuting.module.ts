import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LoginComponent } from '../components/user/login/login.component';
import { ProductListComponent } from '../components/Content/product-list/product-list.component';
import { NotfoundComponent } from '../components/notfound/notfound.component';
import { AuthGuard } from '../guards/auth.guard';

import { UserListComponent } from '../components/user/user-list/user-list.component';
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent },
      {
        path: 'product-list',
        component: ProductListComponent,
        canActivate: [AuthGuard],
        canLoad: [AuthGuard]
      },
      { path: '**', component: NotfoundComponent },
      { path: 'user-list', component: UserListComponent }
    ])
   


  ],
  declarations: [],
  exports: [RouterModule]
})
export class ApproutingModule {}
