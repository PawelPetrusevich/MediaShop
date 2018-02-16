import { Component } from '@angular/core';
import { Permissions } from '../../../Models/User/permissions';
import { UserService } from '../../../Services/User/userservise';


@Component({
  selector: 'app-set-permission',
  templateUrl: './set-permission.component.html',
  styleUrls: ['./set-permission.component.css']
})
export class SetPermissionComponent {


  status: any;
  constructor(private userService: UserService) {}


  SetPermission(permission: Permissions): void {
    // тут два аргумента надо передать ))
      this.userService.SetPermission(permission, Permissions.Create)
        .subscribe(resp => (this.status = resp.status), err => console.log(err));
    }
}
