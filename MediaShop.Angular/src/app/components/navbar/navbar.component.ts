import { Component, OnInit, DoCheck } from '@angular/core';
import { Permissions } from '../../Models/User/permissions';
import { UserInfoService } from '../../Services/User/userInfoService';
import { AccountService } from '../../Services/User/AccountService';
import { SignalR } from 'ng2-signalr';
import { SignalRServiceConnector } from '../../signalR/signalr-service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  IsAutorize: boolean;
  Login: string;
  IsAdmin: boolean;
  IsCreator: boolean;
  constructor(
    private accountService: AccountService,
    private userInfoService: UserInfoService,
    private signalRServ: SignalRServiceConnector
  ) {}

  ngOnInit(): void {
    console.log('oninit');
    this.accountService.getLoginEvent().subscribe(e => {
    this.IsAutorize = e;
    if (e) {
    this.userInfoService.getUserInfo().subscribe(result => {
      console.log(result);
      this.IsAdmin = (result.Permissions & Permissions.Delete) !== 0;
      this.IsCreator = (result.Permissions & Permissions.Create) !== 0;
      this.Login = result.Login;
    });
  }
  });
    if (this.accountService.isAuthorized())
    {
      this.signalRServ.Connect();
    }
  }
}

