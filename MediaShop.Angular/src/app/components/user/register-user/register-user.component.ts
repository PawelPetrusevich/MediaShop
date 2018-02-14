import { Component, OnInit } from '@angular/core';
import {RegisterUserDto} from '../../../Models/User/register-userDto';
import {AccountService} from '../../../Services/AccountService';
import { Account } from '../../../Models/User/account';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})


export class RegisterUserComponent implements OnInit {

  status : number;
  userInfo : Account =new Account();
  
  constructor(private accountService : AccountService) { }

  register(username : string, password : string, confirmPassword : string, email : string): void {
    const user = new RegisterUserDto();
    user.Email = username;
    user.Password = password;
    user.ConfirmPassword = confirmPassword;
    user.Email = email;
    
    this.accountService
      .register(user)
      .subscribe(resp =>  this.userInfo = resp, err => console.log(err));
  }
  
  ngOnInit() {
  }
}
