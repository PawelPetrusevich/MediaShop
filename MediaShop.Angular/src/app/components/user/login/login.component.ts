import { Component, OnInit } from '@angular/core';
import { TokenResponse } from '../../../Models/User/token-response';
import { AccountService } from '../../../Services/User/AccountService';
import { HttpErrorResponse } from '@angular/common/http';
import { AppSettings } from '../../../Settings/AppSettings';
import { Router } from '@angular/router';
import { SignalRServiceConnector } from '../../../signalR/signalr-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})


export class LoginComponent implements OnInit {

  showError = false;
  errorMessage: string;

  constructor(private accountService: AccountService, private router: Router, private signalRServiceConnector: SignalRServiceConnector) { }

  ngOnInit() {
    this.accountService.getError().subscribe(resp => {
      if (resp != null) {
        this.showError = true;
        this.errorMessage = resp;
      }
    } );
  }

  login(name: string, password: string): void {
     this.accountService.login(name, password);
  }
}
