import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { RegisterUserDto } from '../../Models/User/register-userDto';
import { Account } from '../../Models/User/account';
import { TokenResponse } from '../../Models/User/token-response';
import { AppSettings } from '../../Settings/AppSettings';
import { PasswordRecovery } from '../../Models/User/password-recovery';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';

@Injectable()
export class AccountService {
  constructor(private http: HttpClient) { }

  register(registerUser: RegisterUserDto): Observable<Account> {
    return this.http
      .post<Account>(AppSettings.API_ENDPOINT + 'api/account/registerAsync', registerUser);
  }

  login(login: string, password: string): Observable<TokenResponse> {
    const body =
      'grant_type=password&username=' + login + '&password=' + password;

    /*const options = new RequestOptions();
    options.headers = new Headers();
    options.headers.append('Content-Type', 'application/x-www-form-urlencoded');
    options.headers.append('Access-Control-Allow-Origin', '*');
*/
    const headers = new HttpHeaders({
      'Content-Type': 'application/x-www-form-urlencoded',
      'Access-Control-Allow-Origin': '*'
    });
    const options = {
      headers,
      withCredentials: true
    };

    return this.http
      .post<TokenResponse>(AppSettings.API_ENDPOINT + 'token', body, options);
  }

  logout() {
    /*const options = new RequestOptions();
    options.headers = new Headers();
    options.headers.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('token')
    );*/

    return this.http
      .post(AppSettings.API_ENDPOINT + 'api/account/logout', null);
  }

  isAuthorized(): boolean {
    if (localStorage.getItem(AppSettings.tokenKey) === null ) {
    return false;
    }

    return true;
  }

  forgotPassword(email: string) {
    return this.http
      .post(
        AppSettings.API_ENDPOINT + 'api/account/initRecoveryPassword',
        email
      );
  }

  confirm(email: string, token: string) {
    return this.http
      .get(AppSettings.API_ENDPOINT + 'api/account/confirm/' + email + '/' + token);
  }

  recoveryPassword(resetMasswor: PasswordRecovery) {
    return this.http
      .post(
        AppSettings.API_ENDPOINT + 'api/account/recoveryPassword',
        resetMasswor
      );
  }
}
