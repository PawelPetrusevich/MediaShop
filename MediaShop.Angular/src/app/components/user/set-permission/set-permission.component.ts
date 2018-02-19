import { Component } from '@angular/core';
import { Permissions } from '../../../Models/User/permissions';
import { UserService } from '../../../Services/User/userservise';
import { PermissionDto } from '../../../Models/User/permissionDto';
import { UserListComponent } from '../../../components/user/user-list/user-list.component';
@Component({
  selector: 'app-set-permission',
  templateUrl: './set-permission.component.html',
  styleUrls: ['./set-permission.component.css']
})
export class SetPermissionComponent {

  isLoaded = false;
  showError = false;

  status: any;
  constructor(private userService: UserService) {}

  SetPermission(id: number, login: string, email: string, permission: Permissions): void {
    // тут два аргумента надо передать ))
      this.userService.SetPermission(id, login, email, permission)
        .subscribe(resp => (this.status = resp.status), err => console.log(err));
    }
}
