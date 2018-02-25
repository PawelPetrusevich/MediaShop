import { Component, OnInit } from '@angular/core';
import { UserInfoService } from '../../../Services/User/userInfoService';
import { AppSettings } from '../../../Settings/AppSettings';
import { Account } from '../../../Models/User/account';
import { Profile } from '../../../Models/User/profile';
import { Settings } from '../../../Models/User/settings';
import { ProfileDto } from '../../../Models/User/profileDto';
import { HttpErrorResponse } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule, Http } from '@angular/http';

@Component({
  selector: 'app-user-pofile',
  templateUrl: './user-pofile.component.html',
  styleUrls: ['./user-pofile.component.css']
})
export class UserPofileComponent implements OnInit {

  showError = false;
  isModified = false;
  errorMessage: string;
  status: any;
  user: Account;
  profile: Profile;
  settings: Settings;
  firstName: string;
  lastName: string;
  dateBirth: Date;
  phone: string;

  constructor(private userInfoService: UserInfoService) { }

  ngOnInit() {
    this.getUser();
  }

  getUser () {
    this.userInfoService.getUserInfo().subscribe(result => {
      this.user = result;
      this.profile = result.Profile;
      this.settings = result.Settings;
      this.firstName = this.profile.FirstName;
      this.lastName = this.profile.LastName;
      this.dateBirth = this.profile.DateOfBirth;
      this.phone = this.profile.Phone;

      console.log(result);
    }, (err: HttpErrorResponse) => {
      console.log(err);
      this.showError = true ;
      if (err.status === 400){
      this.errorMessage = err.status + ' ' + err.statusText;
      }
      this.showError = true ;
      if (err.status === 401){
      this.errorMessage = 'User is not aothorized';
      }
      if (err.status === 500){
        this.errorMessage = err.status + ' ' + err.statusText;
      }
    });
  }

  saveProfile(): void {
    const profileChanged = new ProfileDto();
    profileChanged.FirstName = this.firstName;
    profileChanged.LastName = this.lastName;
    profileChanged.DateOfBirth = this.dateBirth;
    profileChanged.Phone = this.phone;

    this.userInfoService
      .updateProfile(profileChanged)
      .subscribe(resp =>  {
        console.log(resp);
        this.isModified = true ;
        },
      (err: HttpErrorResponse) => {
        this.isModified = false ;
        console.log(err);
        this.showError = true ;
        if (err.status === 400){
        this.errorMessage = 'One of the field is empty. Try one more time!';
        }
        this.showError = true ;
        if (err.status === 401){
        this.errorMessage = 'User is not aothorized';
        }
        if (err.status === 500){
          this.errorMessage = err.status + ' ' + err.statusText;
        }
      });
  }

}
