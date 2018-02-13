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
  user : Account = new Account();
  
  constructor(private accountService : AccountService) { }

  register(registerUser : RegisterUserDto): void {
    this.accountService
      .register(registerUser)
      .subscribe(resp =>  this.user = resp, err => console.log(err));
  }
  
  ngOnInit() {
  }
}
