import { Component, OnInit } from '@angular/core';
import {RegisterUserDto} from '../../../Models/User/register-userDto';
import {AccountService} from '../../../Services/User/AccountService';
import { Account } from '../../../Models/User/account';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})


export class RegisterUserComponent implements OnInit {

  userInfo: Account = new Account();
  isRegistered = false;
  showError = false;
  errorMessage: string;

  constructor(private accountService: AccountService, private router: Router) { }

  register(login: string, password: string, confirmPassword: string, email: string): void {
    const user = new RegisterUserDto();
    user.Login = login;
    user.Password = password;
    user.ConfirmPassword = confirmPassword;
    user.Email = email;

    this.accountService
      .register(user)
      .subscribe(resp =>  {
        this.showError = false;
        this.isRegistered = true;
        this.userInfo = resp;
        console.log(resp);
      } ,
      (err: HttpErrorResponse) => {
        console.log(err);
        this.isRegistered = false;
        this.showError = true ;
        this.errorMessage = err.error.Message;
        }
    );
  }

  ngOnInit() {
  }
}
