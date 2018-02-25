import { Component, OnInit } from '@angular/core';
import {TokenResponse} from '../../../Models/User/token-response';
import {AccountService} from '../../../Services/User/AccountService';
import { HttpErrorResponse } from '@angular/common/http';
import {AppSettings} from '../../../Settings/AppSettings';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})


export class LoginComponent implements OnInit {

  data: TokenResponse;
  showError = false;
  errorMessage: string;

  constructor(private accountService: AccountService,  private router: Router) { }

  ngOnInit() {
  }

  login(name: string, password: string): void {
    this.accountService.login(name, password)
    .subscribe(resp => {
      this.data = resp;
      localStorage.setItem(AppSettings.tokenKey, this.data.access_token);
      localStorage.setItem(AppSettings.userId, this.data.userId);
      this.router.navigate(['product-list']);
    },
    (err: HttpErrorResponse) => {
      console.log(err);
      this.showError = true ;
      if (err.status === 400){
      this.errorMessage = 'Incorrect login or  password';
      }
      this.showError = true ;
      if (err.status === 401){
      this.errorMessage = 'User is not aothorized';
      }
      if (err.status === 500){
        this.errorMessage = err.status + ' ' + err.statusText;
      }
    }
  );
  }
}
