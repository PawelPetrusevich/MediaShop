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
import { environment } from '../../../environments/environment';

@Injectable()
export class UserInfoService {
  constructor(private http: HttpClient) {}

  getUserInfo(): Observable<Account> {
    return this.http
      .get<Account>(environment.API_ENDPOINT + 'api/user/getUserInfo');
  }

  updateSettings(settings: SettingsDto): Observable<Settings> {
    return this.http
      .post<SettingsDto>(environment.API_ENDPOINT + 'api/user/modifySettingsAsync', settings);
  }

  updateProfile(profile: ProfileDto): Observable<Profile> {
    return this.http
      .post<ProfileDto>(environment.API_ENDPOINT + 'api/user/modifyProfile', profile);
  }
}
