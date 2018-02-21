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
import { HttpClient } from '@angular/common/http';

@Injectable()
export class UserInfoService {
  constructor(private http: Http) { }

  getUserInfo(): Observable<Account> {
    const options = new RequestOptions();
    options.headers = new Headers();
    options.headers.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('token')
    );

    return this.http
      .get(AppSettings.API_ENDPOINT + 'api/user/getUserInfo', options)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }

  deleteUserAsync(): Observable<Account> {
    const options = new RequestOptions();
    return this.http
       .post(AppSettings.API_ENDPOINT + 'api/user/deleteAsync', options)
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
    const options = new RequestOptions();
    options.headers = new Headers();
    options.headers.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('token')
    );

    return this.http
       .post(AppSettings.API_ENDPOINT + 'api/user/modifyProfile', profile, options)
       .map(resp => resp.json())
       .catch(err => Observable.throw(err));
   }
}
