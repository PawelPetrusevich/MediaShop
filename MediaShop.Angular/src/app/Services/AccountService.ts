import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { RegisterUserDto } from '../Models/User/register-userDto';
import { Account } from '../Models/User/account';
import { TokenResponse } from '../Models/User/token-response';

@Injectable()
export class AccountService {
  constructor(private http: Http) {}

  register(registerUser : RegisterUserDto) : Observable<Account>{
    return this.http.post('http://localhost:51289/api/account/register', registerUser)
    .map(resp => resp.json())
    .catch(err => Observable.throw(err));
  }

  login(username: string, password: string) : Observable<TokenResponse> {

    const body =
    'grant_type=password&username=' + username + '&password=' + password;

    const options = new RequestOptions();
    options.headers = new Headers();
    options.headers.append('Content-Type', 'application/x-www-form-urlencoded');
    options.headers.append('Access-Control-Allow-Origin', '*');

    return this.http
    .post('http://localhost:1787/token', body, options)
    .map(resp => resp.json())
    .catch(err => Observable.throw(err));
  }
}