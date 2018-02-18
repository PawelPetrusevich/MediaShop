import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { PermissionDto } from '../../Models/User/permissionDto';
import { Permissions } from '../../Models/User/permissions';

@Injectable()
export class UserService {
  static url = 'http://localhost:51289/api';
  constructor(private http: Http) {}

  SetPermission(id: number, login: string, email: string, permission: Permissions) {
    const permissionDto = new PermissionDto();
    permissionDto.Id = id;
    permissionDto.Login = login;
    permissionDto.Email = email;
    permissionDto.Permission = permission;
    return this.http.post(UserService.url + permissionDto.Id + '/user/permission/add', permissionDto).map(resp => resp.json())
    .catch(err => Observable.throw(err));
  }
  RemovePermission(id: number, permission: Permissions) {
    const permissionDto = new PermissionDto();
    permissionDto.Id = id;
    permissionDto.Permission = permission;
    return this.http.post(UserService.url + permissionDto.Id + '/user/permission/delete', permissionDto).map(resp => resp.json())
    .catch(err => Observable.throw(err));
  }
 Logout(id: number) {
 return this.http.post(UserService.url + '/account/logout', id).map(resp => resp.json())
 .catch(err => Observable.throw(err));
 }
 GetAllUsers() {
  return this.http.get(UserService.url + '/account/GetAllUsers').map(resp => resp.json())
  .catch(err => Observable.throw(err));
 }
 SetFlagIsBanned(id: number) {
  const headers = new Headers({ 'Content-Type': 'application/json' });
  const options = new RequestOptions({ headers: headers });

  return this.http.post(UserService.url + '/user/banned/set', id, options).map(resp => resp.json())
  .catch(err => Observable.throw(err));
  }
  RemoveFlagIsBanned(id: number) {
  const headers = new Headers({ 'Content-Type': 'application/json' });
  const options = new RequestOptions({ headers: headers });

  return this.http.post(UserService.url + '/user/banned/remove', id, options).map(resp => resp.json())
    .catch(err => Observable.throw(err));
  }
}
