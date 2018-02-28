import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../Services/User/AccountService';
import { ForgotPasswordDto } from '../../../Models/User/forgot-password-dto';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  model: ForgotPasswordDto;

  showMessage: boolean;
  isError: boolean = false;
  message: string;
  constructor(private accountService: AccountService, private router: Router) {
    this.model = new ForgotPasswordDto();
  }

  ngOnInit() {
  }

  forgotPassword(f: NgForm) {
    if (f.invalid)
      return;
    this.accountService.forgotPassword(this.model).subscribe(resp => {
      this.message = "Password reset instructions will be emailed " + resp + " to shortly.";
      this.showMessage = true;
    },
      (err: HttpErrorResponse) => {
        this.isError = true;
        this.showMessage = true;
        this.message = err.error.Message;
      });
  }
}
