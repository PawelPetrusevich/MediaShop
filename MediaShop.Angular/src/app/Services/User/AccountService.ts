import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { RegisterUserDto } from '../../Models/User/register-userDto';
import { Account } from '../../Models/User/account';
import { TokenResponse } from '../../Models/User/token-response';
import { AppSettings } from '../../Settings/AppSettings'
import { PasswordRecovery } from '../../Models/User/password-recovery';

@Injectable()
export class AccountService {
  constructor(private http: Http) { }

  register(registerUser: RegisterUserDto): Observable<Account> {
    return this.http
      .post(AppSettings.API_ENDPOINT + 'api/account/register', registerUser)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }

  login(login: string, password: string): Observable<TokenResponse> {
    const body =
      'grant_type=password&username=' + login + '&password=' + password;

    const options = new RequestOptions();
    options.headers = new Headers();
    options.headers.append('Content-Type', 'application/x-www-form-urlencoded');
    options.headers.append('Access-Control-Allow-Origin', '*');

    return this.http
      .post(AppSettings.API_ENDPOINT + 'token', body, options)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }

  logout(id: number) {
    return this.http
      .post(AppSettings.API_ENDPOINT + 'api/account/logout', id)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }

  forgotPassword(email: string) {
    return this.http
      .post(
        AppSettings.API_ENDPOINT + 'api/account/initRecoveryPassword',
        email
      )
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }

  confirm(email: string, token: string) {
    return this.http
      .get(AppSettings.API_ENDPOINT + 'api/account/confirm/' + email + '/' + token)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }

  recoveryPassword(resetMasswor: PasswordRecovery) {
    return this.http
      .post(
        AppSettings.API_ENDPOINT + 'api/account/recoveryPassword',
        resetMasswor
      )
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }
}
