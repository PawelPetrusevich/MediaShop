import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../Services/User/AccountService';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  email: string;
  constructor(private accountService: AccountService) { }

  ngOnInit() {
  }

  forgotPassword() {
    this.accountService.forgotPassword(this.email).subscribe(resp => {
      console.log(resp);
    });
  }
}
