import { Component, OnInit } from "@angular/core";
import { Permissions } from "../../../Models/User/permissions";
import { UserService } from "../../../Services/User/userservise";
import { PermissionDto } from "../../../Models/User/permissionDto";
import { NgIf } from "@angular/common";

@Component({
  selector: "app-user-list",
  templateUrl: "./user-list.component.html",
  styleUrls: ["./user-list.component.css"]
})
export class UserListComponent implements OnInit {
  users: PermissionDto[];
  currentUser: PermissionDto;
  showError = false;

  see: boolean;
  seeNum: number;
  create: boolean;
  createNum: number;
  del: boolean;
  delNum: number;
  newPermission: Permissions;

  status: any;
  constructor(private userService: UserService) {}
  ngOnInit() {
    this.getAll();
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
          (this.status = resp.status),
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
          (this.status = resp.status),
          (permissionDto.IsBanned = resp.IsBanned)
        ),
        err => console.log(err)
      );
  }
  SetPermission(user: PermissionDto): void {
    if (this.see) {
      this.seeNum = 1;
    } else {
      this.seeNum = 0;
    }
    if (this.create) {
      this.createNum = 2;
    } else {
      this.createNum = 0;
    }
    if (this.del) {
      this.delNum = 4;
    } else {
      this.delNum = 0;
    }
    this.newPermission = this.seeNum | this.createNum | this.delNum;

    this.userService
      .SetPermission(user.Id, user.Login, user.Email, this.newPermission)
      .subscribe(resp => (console.log(this.seeNum), this.status = resp.status), err => console.log(err));
  }
  GetPermission(user: PermissionDto) {
    console.log(user.Permissions);
    /*console.log(Permissions.See);
    console.log(user.Permission & Permissions.See);
    console.log((user.Permission & Permissions.See) === 0);*/

    this.see = (user.Permissions & Permissions.See) === 0;
    this.create = (user.Permissions & Permissions.Create) === 0;
    this.del = (user.Permissions & Permissions.Delete) === 0;
  }
}
