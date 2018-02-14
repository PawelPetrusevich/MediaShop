import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { PermissionDto } from '../../Models/User/permissionDto';
import { Permissions } from '../../Models/User/permissions';

@Injectable()
export class UserService {
  constructor(private http: Http) {}

  SetPermission(id: number, permission: Permissions) {
    const permissionDto = new PermissionDto();
    permissionDto.Id = id;
    permissionDto.Permission = permission;


    return this.http.post('http://localhost:1787/api/user/{id}/permission/add', permissionDto);
  }
}
