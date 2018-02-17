import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LoginComponent } from '../components/user/login/login.component';
import { ProductListComponent } from '../components/Content/product-list/product-list.component';
import { UserListComponent } from '../components/user/user-list/user-list.component';
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot([{ path: '', component: LoginComponent },
  { path: 'product-list', component: ProductListComponent},
  { path: 'user-list', component: UserListComponent}
])
  ],
  declarations: [],
  exports: [RouterModule]
})
export class ApproutingModule {}
