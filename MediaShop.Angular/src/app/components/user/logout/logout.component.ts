import { Component, OnInit, Output, EventEmitter} from '@angular/core';
import {AccountService} from '../../../Services/User/AccountService';
import {AppSettings} from '../../../Settings/AppSettings';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {
  showError = false;
  errorMessage: string;

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit() {
    this.accountService.getError().subscribe(resp => {
      if (resp != null) {
        this.showError = true;
        this.errorMessage = resp;
      }
    } );

    this.accountService.logout();
  }
}
