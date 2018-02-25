import { Component, OnInit } from '@angular/core';
import { UserInfoService } from '../../../Services/User/userInfoService';
import { AppSettings } from '../../../Settings/AppSettings';
import { Account } from '../../../Models/User/account';
import { Profile } from '../../../Models/User/profile';
import { Settings } from '../../../Models/User/settings';
import { ProfileDto } from '../../../Models/User/profileDto';
import { DatePipe } from '@angular/common';
import { SettingsDto } from '../../../Models/User/settingsDto';
import { Languages } from '../../../Models/User/languages';

@Component({
  selector: 'app-user-settings',
  templateUrl: './user-settings.component.html',
  styleUrls: ['./user-settings.component.css']
})
export class UserSettingsComponent implements OnInit {
  user: Account;
  profile: Profile;
  settings: Settings;
  InterfaceLanguage: Languages;
  NotificationStatus: boolean;

  constructor(private userInfoService: UserInfoService) {}

  ngOnInit() {
    this.getUser();
  }

  setLanguage(num: number) {
    this.InterfaceLanguage = num;
  }

  getUser() {
    this.userInfoService.getUserInfo().subscribe(result => {
      this.user = result;
      this.profile = result.Profile;
      this.settings = result.Settings;
      this.InterfaceLanguage = this.settings.InterfaceLanguage;
      this.NotificationStatus = this.settings.NotificationStatus;

      console.log(result);
    });
  }

  saveSettings(): void {
    const settingsChanged = new SettingsDto();

    settingsChanged.InterfaceLanguage = this.InterfaceLanguage;
    settingsChanged.NotificationStatus = this.NotificationStatus;
    console.log(settingsChanged);
    this.userInfoService
      .updateSettings(settingsChanged)
      .subscribe(resp => console.log(resp), err => console.log(err));
  }
}
