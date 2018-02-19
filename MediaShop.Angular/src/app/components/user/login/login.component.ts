import { Component, OnInit } from '@angular/core';
import {TokenResponse} from '../../../Models/User/token-response';
import {AccountService} from '../../../Services/User/AccountService';
import { HttpErrorResponse } from '@angular/common/http';
import {AppSettings} from '../../../Settings/AppSettings';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})


export class LoginComponent implements OnInit {

  data: TokenResponse;

  constructor(private accountService: AccountService) { }

  ngOnInit() {
  }

  login(name: string, password: string): void {
    this.accountService.login(name, password)
    .subscribe(resp => {
      this.data = resp;
      localStorage.setItem(AppSettings.tokenKey, this.data.access_token);
      localStorage.setItem(AppSettings.authorizationFlag, 'true');
      console.log(resp);
    }, err => console.log(err));
  }
}
