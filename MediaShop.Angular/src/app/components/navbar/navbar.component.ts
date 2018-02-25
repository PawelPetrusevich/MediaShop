import { Component, OnInit } from '@angular/core';
import { Permissions } from '../../Models/User/permissions';
import { UserInfoService } from '../../Services/User/userInfoService';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  constructor(private userInfoService: UserInfoService) {}

  ngOnInit() {
    this.IsAdmin();
  }
  IsAdmin() {
    this.userInfoService.getUserInfo().subscribe(result => {
      console.log(result);
      return result.Permissions & Permissions.Delete;
    });
  }
}
