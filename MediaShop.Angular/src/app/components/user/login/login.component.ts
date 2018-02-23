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
      console.log(resp);
      this.router.navigate(['product-list']);
    },
    (err: HttpErrorResponse) => {
      this.showError = true ;
      this.errorMessage = err.statusText;
      console.log(err);
    }
  );
  }
}
