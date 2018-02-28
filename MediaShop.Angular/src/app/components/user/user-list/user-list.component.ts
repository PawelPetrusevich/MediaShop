import { Component, OnInit } from '@angular/core';
import { Permissions } from '../../../Models/User/permissions';
import { UserService } from '../../../Services/User/userservise';
import { UserInfoService } from '../../../Services/User/userInfoService';
import { PermissionDto } from '../../../Models/User/permissionDto';
import { NgIf } from '@angular/common';
import { forEach } from '@angular/router/src/utils/collection';
import { Route } from '@angular/compiler/src/core';
import { Routes, Router } from '@angular/router';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule, Http } from '@angular/http';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: PermissionDto[];
  private selectedId: number;
  currentUser: PermissionDto;
  showError = false;

  status: any;
  constructor(
    private userService: UserService,
    private userInfoService: UserInfoService,
    private route: ActivatedRoute,
    private router: Router
  ) {}
  ngOnInit() {
    return this.userService.GetAllUsers().subscribe(result => {
      this.users = result;
    });
  }
  getAll() {
    this.userService.GetAllUsers().subscribe(result => {
      this.users = result;
    });
  }

  SetFlagIsBanned(permissionDto: PermissionDto) {
    this.userService
      .SetFlagIsBanned(permissionDto.Id)
      .subscribe(
        resp => (
          console.log(resp),
          (permissionDto.IsBanned = resp.IsBanned)
        ),
        err => console.log(err)
      );
  }
  RemoveFlagIsBanned(permissionDto: PermissionDto) {
    this.userService
      .RemoveFlagIsBanned(permissionDto.Id)
      .subscribe(
        resp => (
          console.log(resp),
          (permissionDto.IsBanned = resp.IsBanned)
        ),
        err => console.log(err)
      );
  }
  SetFlagIsDeleted(permissionDto: PermissionDto) {
    this.userService.deleteUserByIdAsync(permissionDto.Id)
    .subscribe(
      resp => (
        console.log(resp),
        (permissionDto.IsDeleted = resp.IsDeleted)
      ),
      err => console.log(err)
    );
  }

  Nothing() {
  }
}
