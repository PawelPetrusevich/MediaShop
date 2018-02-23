import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { AppSettings } from '../../Settings/AppSettings';
import { Account } from '../../Models/User/account';
import { SettingsDto } from '../../Models/User/settingsDto';
import { ProfileDto } from '../../Models/User/profileDto';
import { Profile } from '../../Models/User/profile';
import { Settings } from '../../Models/User/settings';
import { HttpClient } from '@angular/common/http';
import { PermissionDto } from '../../Models/User/permissionDto';

@Injectable()
export class UserInfoService {
  constructor(private http: HttpClient) {}

  getUserInfo(): Observable<Account> {
    return this.http
      .get<Account>(AppSettings.API_PUBLIC + 'api/user/getUserInfo');
  }

  updateSettings(settings: SettingsDto): Observable<Settings> {
    const options = new RequestOptions();
    options.headers = new Headers();
    options.headers.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('token')
    );
    options.headers.append('Content-Type', 'application/json');
    return this.http
      .post<SettingsDto>(AppSettings.API_PUBLIC + 'api/user/modifySettingsAsync', settings);
  }

  updateProfile(profile: ProfileDto): Observable<Profile> {
    return this.http
      .post<ProfileDto>(AppSettings.API_PUBLIC + 'api/user/modifyProfile', profile);
  }
}
