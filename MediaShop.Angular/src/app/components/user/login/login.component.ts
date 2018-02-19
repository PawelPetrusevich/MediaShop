import { Component, OnInit } from '@angular/core';
import {TokenResponse} from '../../../Models/User/token-response';
import {AccountService} from '../../../Services/User/AccountService';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})


export class LoginComponent implements OnInit {

  data: TokenResponse;

  constructor(private accountService: AccountService) { }

  login(name: string, password: string): void {
    this.accountService.login(name, password).subscribe(resp => {
      this.data = resp;
      localStorage.setItem('token', this.data.access_token);
    });
  }

  ngOnInit() {
  }

}
