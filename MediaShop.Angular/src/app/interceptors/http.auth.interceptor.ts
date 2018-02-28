import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AppSettings } from '../Settings/AppSettings';
import { AccountService } from '../Services/User/AccountService';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private accountService: AccountService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      if (this.accountService.isAuthorized()) {
        req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${localStorage.getItem(AppSettings.tokenKey)}`
        }
      });
    }

    return next.handle(req);
  }
}
