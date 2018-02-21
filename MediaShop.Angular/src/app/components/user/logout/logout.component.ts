import { Component, OnInit } from '@angular/core';
import {AccountService} from '../../../Services/User/AccountService';
import {AppSettings} from '../../../Settings/AppSettings';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  constructor(private accountService: AccountService, private router: Router) { }

  logOut(): void {
    this.accountService.logout().subscribe(resp => {
      localStorage.removeItem(AppSettings.tokenKey);
      this.router.navigate(['login']);
    });
  }

  ngOnInit() {
    this.logOut();
  }

}
