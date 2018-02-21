import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { AppSettings } from '../../Settings/AppSettings';
import { Account } from '../../Models/User/account';
import {SettingsDto} from '../../Models/User/settingsDto';
import {ProfileDto} from '../../Models/User/profileDto';
import { Profile } from '../../Models/User/profile';
import { Settings } from '../../Models/User/settings';

@Injectable()
export class UserInfoService {
  constructor(private http: Http) { }

  getUserInfo(): Observable<Account> {
    return this.http
      .get(AppSettings.API_ENDPOINT + 'api/user/getUserInfoAsync')
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }

  deleteUserAsync(): Observable<Account> {
    return this.http
       .post(AppSettings.API_ENDPOINT + 'api/user/deleteAsync', null)
       .map(resp => resp.json())
       .catch(err => Observable.throw(err));
   }

  updateSettings(settings: SettingsDto): Observable<Settings> {
    return this.http
       .post(AppSettings.API_ENDPOINT + 'api/user/modifySettingsAsync', settings)
       .map(resp => resp.json())
       .catch(err => Observable.throw(err));
   }

   updateProfile(profile: ProfileDto): Observable<Profile> {
    return this.http
       .post(AppSettings.API_ENDPOINT + 'api/user/modifyProfileAsync', profile)
       .map(resp => resp.json())
       .catch(err => Observable.throw(err));
   }
}
