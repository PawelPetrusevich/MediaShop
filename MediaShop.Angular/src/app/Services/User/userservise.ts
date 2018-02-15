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

  SetPermission(id: number, permission: Permissions) {
    const permissionDto = new PermissionDto();
    permissionDto.Id = id;
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
 return this.http.post(UserService.url + 'account/account/logout', id).map(resp => resp.json())
 .catch(err => Observable.throw(err));
 }
}
