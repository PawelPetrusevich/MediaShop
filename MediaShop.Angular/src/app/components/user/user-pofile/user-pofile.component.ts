import { Component, OnInit } from '@angular/core';
import { UserInfoService } from '../../../Services/User/userInfoService';
import { AppSettings } from '../../../Settings/AppSettings';
import { Account } from '../../../Models/User/account';

@Component({
  selector: 'app-user-pofile',
  templateUrl: './user-pofile.component.html',
  styleUrls: ['./user-pofile.component.css']
})
export class UserPofileComponent implements OnInit {
  user: Account;

  constructor(private userInfoService: UserInfoService) { }

  ngOnInit() {
    this.getUser();
  }

  getUser () {
    this.userInfoService.getUserInfo().subscribe(result => {
      this.user = result;
    });
  }


}
