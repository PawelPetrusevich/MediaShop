import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LoginComponent } from '../components/user/login/login.component';
import { ProductListComponent } from '../components/Content/product-list/product-list.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot([{ path: '', component: LoginComponent },
  { path: 'product-list', component: ProductListComponent}
])
  ],
  declarations: [],
  exports: [RouterModule]
})
export class ApproutingModule {}
