import { Component } from '@angular/core';
import { Permissions } from '../../../Models/User/permissions';
import { UserService } from '../../../Services/User/userservise';
import { PermissionDto } from '../../../Models/User/permissionDto';
import { UserListComponent } from '../../../components/user/user-list/user-list.component';

import 'rxjs/add/operator/switchMap';
import { OnInit, HostBinding } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule, Http } from '@angular/http';

@Component({
  selector: 'app-set-permission',
  templateUrl: './set-permission.component.html',
  styleUrls: ['./set-permission.component.css']
})
export class SetPermissionComponent implements OnInit {
  user: Observable<PermissionDto>;

  isLoaded = false;
  showError = false;
  see: boolean;
  seeNum: number;
  create: boolean;
  createNum: number;
  del: boolean;
  delNum: number;
  oldPermission: Permissions;
  newPermission: Permissions;
  status: any;
  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.user = this.route.paramMap.switchMap((params: ParamMap) =>
      this.userService.GetUserById(params.get('id'))
    );
    this.user.subscribe(res => {
      this.oldPermission = res.Permissions;

      this.see = (this.oldPermission & Permissions.See) === Permissions.See;
      this.create =
        (this.oldPermission & Permissions.Create) === Permissions.Create;
      this.del =
        (this.oldPermission & Permissions.Delete) === Permissions.Delete;
    });
  }
  gotoUsers(user: PermissionDto, mode: boolean) {
  if (mode) {this.SetPermission(user); }
    this.router.navigate(['/user-list']);
  }
  SetPermission(user: PermissionDto): void {
    if (this.see) {
      this.seeNum = Permissions.See;
    } else {
      this.seeNum = 0;
    }
    if (this.create) {
      this.createNum = Permissions.Create;
    } else {
      this.createNum = 0;
    }
    if (this.del) {
      this.delNum = Permissions.Delete;
    } else {
      this.delNum = 0;
    }
    this.newPermission = this.seeNum | this.createNum | this.delNum;
    console.log(this.newPermission);
    this.userService
      .SetPermission(user.Id, user.Login, user.Email, this.newPermission)
      .subscribe(resp => (console.log(resp)), err => console.log(err));
  }
}
