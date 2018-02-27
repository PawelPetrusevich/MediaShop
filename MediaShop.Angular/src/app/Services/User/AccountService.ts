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
import { UserInfoService } from './userInfoService';
import { Permissions } from '../../Models/User/permissions';

@Injectable()
export class AccountService {
  private ErrMsg = new Subject<string>();
  private IsAuthorized = new Subject<boolean>();
  private IsAdmin = new Subject<boolean>();
  private IsCreator = new Subject<boolean>();
  private Login = new Subject<string>();

  constructor(
    private http: HttpClient,
    private signalRServiceConnector: SignalRServiceConnector,
    private router: Router,
    private userInfoService: UserInfoService

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

          this.userInfoService.getUserInfo().subscribe(result => {
            this.IsAuthorized.next(true);
            this.IsAdmin.next((result.Permissions & Permissions.Delete) !== 0);
            this.IsCreator.next((result.Permissions & Permissions.Create) !== 0);
            this.Login.next(result.Login);
          });

          this.signalRServiceConnector.Connect(true);
          this.router.navigate(['product-list']);
        },
        (err: HttpErrorResponse) => {
          this.ErrMsg.next(err.error.error_description);
        }
      );
  }

  logout() {
    this.signalRServiceConnector.Disconnect();
    localStorage.removeItem(AppSettings.tokenKey);
    localStorage.removeItem(AppSettings.userId);
    this.router.navigate(['login']);
    this.IsAuthorized.next(false);
    this.IsAdmin.next(false);
    this.IsCreator.next(false);
    this.Login.next(' ');
    return this.http.post(
      environment.API_ENDPOINT + 'api/account/logout',
      null
    );
  }

  isAuthorized(): boolean {
    if (localStorage.getItem(AppSettings.tokenKey) === null) {
      return false;
    }

    return true;
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

  getIsAuthorized() {
    return this.IsAuthorized.asObservable();
  }

  getIsAdmin() {
    return this.IsAdmin.asObservable();
  }

  getIsCreator() {
    return this.IsCreator.asObservable();
  }

  getLogin() {
    return this.Login.asObservable();
  }
}
