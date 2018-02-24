import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { PermissionDto } from '../../Models/User/permissionDto';
import { Permissions } from '../../Models/User/permissions';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import {SetPermissionComponent} from '../../components/user/set-permission/set-permission.component';

@Injectable()
export class UserService {
  static url = 'http://localhost:51289/api';
  constructor(private http: Http) {}

  SetPermission(
    id: number,
    login: string,
    email: string,
    permission: Permissions
  ) {
    const permissionDto = new PermissionDto();
    permissionDto.Id = id;
    permissionDto.Login = login;
    permissionDto.Email = email;
    permissionDto.Permissions = permission;
    return this.http
      .post(UserService.url + '/user/permission/addMask', permissionDto)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }
  RemovePermission(id: number, permission: Permissions) {
    const permissionDto = new PermissionDto();
    permissionDto.Id = id;
    permissionDto.Permissions = permission;
    return this.http
      .post(
        UserService.url + permissionDto.Id + '/user/permission/delete',
        permissionDto
      )
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }
  GetAllUsers() {
    const options = new RequestOptions();
    options.headers = new Headers();
    options.headers.append(
      'Authorization',
      'Bearer ' + localStorage.getItem('token')
    );
    return this.http
      .get(UserService.url + '/account/GetAllUsers', options )
      .map(resp => {
        console.log(resp.json());
        return resp.json();
      })
      .catch(err => Observable.throw(err));
  }
  SetFlagIsBanned(id: number) {
    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });

    return this.http
      .post(UserService.url + '/user/banned/set', id, options)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }
  RemoveFlagIsBanned(id: number) {
    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });

    return this.http
      .post(UserService.url + '/user/banned/remove', id, options)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }
  deleteUserByIdAsync(id: number) {
     const options = new RequestOptions();
     options.headers = new Headers();
     options.headers.append(
       'Authorization',
       'Bearer ' + localStorage.getItem('token')
     );

     options.headers.append('Content-Type', 'application/json');
     return this.http
       .post(UserService.url + '/user/deleteByIdAsync', id, options)
       .map(resp => resp.json())
       .catch(err => Observable.throw(err));
   }

  GetUser(id: number | string) {

    return (
      this.GetAllUsers()
        // (+) before `id` turns the string into a number
        .map(users => users.find(user => user.Id === +id))
    );
  }
}
