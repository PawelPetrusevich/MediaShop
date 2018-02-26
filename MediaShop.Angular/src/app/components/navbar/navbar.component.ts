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
  IsAdmin: boolean;
  Login: string;

  constructor(private accountService: AccountService, private userInfoService: UserInfoService, private signalRServ: SignalRServiceConnector) {}

  ngOnInit(): void {
    console.log('oninit');
    this.userInfoService.getUserInfo().subscribe(result => {
      console.log(result);
      this.Login = result.Login;
      this.IsAdmin = (result.Permissions & Permissions.Delete) !== 0;
    });
    if (this.accountService.isAuthorized())
      this.signalRServ.Connect();
  }
}
