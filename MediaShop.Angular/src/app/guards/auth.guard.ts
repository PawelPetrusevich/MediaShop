import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanLoad,
  RouterStateSnapshot,
  Route,
  Router
} from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AccountService } from '../Services/User/AccountService';

@Injectable()
export class AuthGuard implements CanActivate, CanLoad {
  constructor(private accountService: AccountService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.accountService.isAuthorized()) {
      return true;
    }
    this.router.navigate(['login']);
    return false;
  }

  canLoad(route: Route): boolean {
    if (this.accountService.isAuthorized()) {
      return true;
    }
    this.router.navigate(['login']);
    return false;
  }
}
