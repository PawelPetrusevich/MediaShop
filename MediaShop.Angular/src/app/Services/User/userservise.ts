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
import { HttpClient } from '@angular/common/http';
import { AppSettings } from '../../Settings/AppSettings';
import { Account } from '../../Models/User/account';
import { environment } from '../../../environments/environment';

@Injectable()
export class UserService {
  constructor(private http: HttpClient) {}

  SetPermission(
    id: number,
    login: string,
    email: string,
    permission: Permissions
  ): Observable<PermissionDto> {
    const permissionDto = new PermissionDto();
    permissionDto.Id = id;
    permissionDto.Login = login;
    permissionDto.Email = email;
    permissionDto.Permissions = permission;
    return this.http
      .post<PermissionDto>(environment.API_ENDPOINT + 'api/user/permission/addMask', permissionDto);
  }
  RemovePermission (id: number, permission: Permissions): Observable<Account> {
    const permissionDto = new PermissionDto();
    permissionDto.Id = id;
    permissionDto.Permissions = permission;
    return this.http
      .post<Account>(environment.API_ENDPOINT + permissionDto.Id + 'api/user/permission/delete', permissionDto);
  }
  GetAllUsers(): Observable<PermissionDto[]> {
    return this.http
      .get<PermissionDto[]>(environment.API_ENDPOINT+ 'api/account/GetAllUsers');
  }
  SetFlagIsBanned(id: number): Observable<PermissionDto> {
    return this.http
      .post<PermissionDto>(environment.API_ENDPOINT + '/api/user/banned/set', id);
  }
  RemoveFlagIsBanned(id: number): Observable<Account> {
    return this.http
      .post<Account>(environment.API_ENDPOINT + 'api/user/banned/remove', id);
  }
  deleteUserByIdAsync(id: number): Observable<PermissionDto> {
     return this.http
       .post<PermissionDto>(environment.API_ENDPOINT + 'api/user/deleteByIdAsync', id);
   }
   GetUserById(id: number | string): Observable<PermissionDto> {
    return this.http
      .get<PermissionDto>(environment.API_ENDPOINT + 'api/user/getUserDtoAsync/' + id);
  }
}
