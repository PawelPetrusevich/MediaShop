import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../Services/User/AccountService';
import { ForgotPasswordDto } from '../../../Models/User/forgot-password-dto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  model: ForgotPasswordDto;

  constructor(private accountService: AccountService, private router: Router) {
    this.model = new ForgotPasswordDto();
  }

  ngOnInit() {
  }

  forgotPassword() {
    this.accountService.forgotPassword(this.model).subscribe(resp => {
      console.log(resp);
      this.router.navigate(['login']);
    });
  }
}
