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
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { ForgotPasswordDto } from '../../Models/User/forgot-password-dto';
import { environment } from '../../../environments/environment';
import { SignalRServiceConnector } from '../../signalR/signalr-service';
import { Subject } from 'rxjs/Subject';
import { Router } from '@angular/router';

@Injectable()
export class AccountService {
  private ErrMsg = new Subject<string>();

  userLoggedIn = new Subject<boolean>();
  constructor(
    private http: HttpClient,
    private signalRServiceConnector: SignalRServiceConnector,
    private router: Router

  ) {
  }

  register(registerUser: RegisterUserDto): Observable<Account> {
    return this.http.post<Account>(
      environment.API_ENDPOINT + 'api/account/registerAsync',
      registerUser
    );
  }

  login(login: string, password: string) {
    const body =
      'grant_type=password&username=' + login + '&password=' + password;

    const headers = new HttpHeaders({
      'Content-Type': 'application/x-www-form-urlencoded',
      'Access-Control-Allow-Origin': '*'
    });

    const options = {
      headers,
      withCredentials: true
    };

    this.http
      .post<TokenResponse>(environment.API_ENDPOINT + 'token', body, options)
      .subscribe(
        resp => {
          localStorage.setItem(AppSettings.tokenKey, resp.access_token);
          localStorage.setItem(AppSettings.userId, resp.userId);

          this.signalRServiceConnector.Connect(true);
          this.userLoggedIn.next(true);
          this.router.navigate(['product-list']);
        },
        (err: HttpErrorResponse) => {
          this.ErrMsg.next(err.error.error_description);
        }
      );

  }

  logout() {
    this.signalRServiceConnector.Disconnect();

     this.http.post(
      environment.API_ENDPOINT + 'api/account/logout',
      null)
      .subscribe(resp => {
      localStorage.removeItem(AppSettings.tokenKey);
      localStorage.removeItem(AppSettings.userId);
      this.router.navigate(['login']);
      },
      (err: HttpErrorResponse) => {
      this.ErrMsg.next(err.error.Message);
      }
    );
  }

  getLoginEvent() {
    return this.userLoggedIn.asObservable();
  }

  isAuthorized(): boolean {
    return localStorage.getItem(AppSettings.tokenKey) != null;

  }

  forgotPassword(model: ForgotPasswordDto) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    const options = {
      headers
    };
    return this.http.post(
        environment.API_ENDPOINT  + 'api/account/initRecoveryPassword',
      model,
      options
      );
  }

  confirm(email: string, token: string) {
    return this.http.get(
      environment.API_ENDPOINT + 'api/account/confirm/' + email + '/' + token
    );
  }

  recoveryPassword(resetMasswor: PasswordRecovery) {
    return this.http.post(
        environment.API_ENDPOINT + 'api/account/recoveryPassword',
        resetMasswor
      );
  }

  getError() {
    return this.ErrMsg.asObservable();
  }
}
