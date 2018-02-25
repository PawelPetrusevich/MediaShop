import { Component, OnInit } from '@angular/core';
import { SignalR } from 'ng2-signalr';
import { SignalRServiceConnector } from '../../signalR/signalr-service';
import { AccountService } from '../../Services/User/AccountService';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  constructor(private signalRServ: SignalRServiceConnector, private accountService: AccountService) { }

  ngOnInit() {
    if (this.accountService.isAuthorized())
      this.signalRServ.Connect();
  }
}
