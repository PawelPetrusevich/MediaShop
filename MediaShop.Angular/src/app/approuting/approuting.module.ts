import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LoginComponent } from '../components/user/login/login.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot([{ path: '', component: LoginComponent }])
  ],
  declarations: [],
  exports: [RouterModule]
})
export class ApproutingModule {}
