import { Component } from '@angular/core';
import { Permissions } from '../../../Models/User/permissions';
import { UserService } from '../../../Services/User/userservise';
import { PermissionDto } from '../../../Models/User/permissionDto';

@Component({
  selector: 'app-set-permission',
  templateUrl: './set-permission.component.html',
  styleUrls: ['./set-permission.component.css']
})
export class SetPermissionComponent {

  users: PermissionDto[];
  currentUser: PermissionDto;
  isLoaded = false;
  showError = false;

  status: any;
  constructor(private userService: UserService) {}

  getAll() {
    this.userService.GetAllUsers().subscribe(result => {
      this.users = result;
      this.isLoaded = true;
    });
  }
  SetPermission(id: number, login: string, email: string, permission: Permissions): void {
    // тут два аргумента надо передать ))
      this.userService.SetPermission(id, login, email, permission)
        .subscribe(resp => (this.status = resp.status), err => console.log(err));
    }
}
