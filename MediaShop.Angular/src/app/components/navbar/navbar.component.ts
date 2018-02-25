import { Component, OnInit } from '@angular/core';
import { Permissions } from '../../Models/User/permissions';
import { UserInfoService } from '../../Services/User/userInfoService';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  IsAdmin: boolean;

  constructor(private userInfoService: UserInfoService) {}

  ngOnInit() {
    console.log(this.Admin());
  }

  Admin() {
    this.userInfoService.getUserInfo().subscribe(result => {
      this.IsAdmin = (result.Permissions & Permissions.Delete) !== 0;
    });
  }
}
